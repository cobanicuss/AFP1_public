using System;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Spm.Service.CreateMessage;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Service.SagaData;
using Spm.Service.SagaTransitions;
using Spm.Service.Serialization;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.Service.Sagas
{
    public class MaterialMasterSaga : Saga<MaterialMasterSagaData>,
                                        IAmStartedByMessages<MaterialMasterCommand>,
                                        IHandleMessages<MaterialMasterResponseCommand>,
                                        IHandleTimeouts<MaterialMasterNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IMaterialMasterTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaName = typeof(MaterialMasterSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MaterialMasterSagaData> mapper)
        {
            mapper.ConfigureMapping<MaterialMasterCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<MaterialMasterResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(MaterialMasterCommand message)
        {
            const float leg = 2.0F;

            var messageType = typeof(MaterialMasterCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from File-Watcher-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"ShortItemNumber={message.ShortItemNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.ShortItemNumber = message.ShortItemNumber;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.MaterialMasterRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.ShortItemNumber, Data.Id, _sagaName, Data.SagaReferenceId);

            var materialMasterSapCommand = CreateMessage.MaterialMasterSapCommand(message);
            Bus.Send(materialMasterSapCommand);

            const int sagaSend = (int)AuditAction.SagaSendToServiceForSoap;
            var sendLocalAuditCommand = CreateMessage.MaterialMasterAuditCommand(sagaSend, messageType, _sagaName, message.ToString(), Data.ShortItemNumber, Data.SagaReferenceId, leg);
            Bus.Send(sendLocalAuditCommand);

            RequestTimeout<MaterialMasterNoResponse>(TimeSpan.FromMinutes(Constants.MaterialMasterTimeoutMinutes));
        }

        public void Handle(MaterialMasterResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"ReferenceId={Data.ShortItemNumber}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(MaterialMasterResponseCommand).FullName;

            const int responseFromServiceForSoap = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var auditCommand = CreateMessage.MaterialMasterAuditCommand(responseFromServiceForSoap, messageType, _sagaName, message.ToString(), Data.ShortItemNumber, Data.SagaReferenceId, leg);
            Bus.Send(auditCommand);

            Transition.End(Data.ShortItemNumber, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(MaterialMasterNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Material-Master! Uncool!");

            var messageType = typeof(MaterialMasterNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.ShortItemNumber, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<MaterialMasterPayload>(Data.SerializedMessageId);

            var message = new MaterialMasterSapCommand
            {
                ShortItemNumber = Data.ShortItemNumber,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int sagaRetry = (int)AuditAction.SagaReTryToServiceForSoap;
            var endAuditCommand = CreateMessage.MaterialMasterAuditCommand(sagaRetry, messageType, _sagaName, message.ToString(), Data.ShortItemNumber, Data.SagaReferenceId, leg);
            Bus.Send(endAuditCommand);

            RequestTimeout<MaterialMasterNoResponse>(TimeSpan.FromMinutes(Constants.MaterialMasterTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("No Response, Saga giving up. No point continuing!");
            const int sagaGivingUp = (int)AuditAction.SagaRetryLimitReached;
            var auditRetryEndCommand = CreateMessage.MaterialMasterAuditCommand(sagaGivingUp, messageType, _sagaName, string.Empty, Data.ShortItemNumber, Data.SagaReferenceId, legUp);
            Bus.Send(auditRetryEndCommand);
        }
    }
}