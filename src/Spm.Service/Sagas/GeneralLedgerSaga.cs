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
    public class GeneralLedgerSaga : Saga<GeneralLedgerSagaData>,
                                IAmStartedByMessages<GeneralLedgerCommand>,
                                IHandleMessages<GeneralLedgerResponseCommand>,
                                IHandleTimeouts<GeneralLedgerNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IGeneralLedgerTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaName = typeof(GeneralLedgerSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GeneralLedgerSagaData> mapper)
        {
            mapper.ConfigureMapping<GeneralLedgerCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<GeneralLedgerResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(GeneralLedgerCommand message)
        {
            const float leg = 2.0F;
            var messageType = typeof(GeneralLedgerCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from File-Watcher-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"GeneralLedgerId={message.GeneralLedgerId}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.GeneralLedgerId = message.GeneralLedgerId;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.GeneralLedgerRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.GeneralLedgerId, Data.Id, _sagaName, Data.SagaReferenceId);

            var generalLedgerSapCommand = CreateMessage.GeneralLedgerSapCommand(message);
            Bus.Send(generalLedgerSapCommand);

            const int sagaSend = (int)AuditAction.SagaSendToServiceForSoap;
            var sendLocalAuditCommand = CreateMessage.GeneralLedgerAuditCommand(sagaSend, messageType, _sagaName, message.ToString(), Data.GeneralLedgerId, Data.SagaReferenceId, leg);
            Bus.Send(sendLocalAuditCommand);

            RequestTimeout<GeneralLedgerNoResponse>(TimeSpan.FromMinutes(Constants.GeneralLedgerTimeoutMinutes));
        }

        public void Handle(GeneralLedgerResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"GeneralLedgerId={Data.GeneralLedgerId}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(GeneralLedgerResponseCommand).FullName;

            const int responseFromServiceForSoap = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var auditCommand = CreateMessage.GeneralLedgerAuditCommand(responseFromServiceForSoap, messageType, _sagaName, message.ToString(), Data.GeneralLedgerId, Data.SagaReferenceId, leg);
            Bus.Send(auditCommand);

            Transition.End(Data.GeneralLedgerId, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(GeneralLedgerNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. General-Ledger! Uncool!");

            var messageType = typeof(GeneralLedgerNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.GeneralLedgerId, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<GeneralLedgerPayload>(Data.SerializedMessageId);
            var message = new GeneralLedgerSapCommand
            {
                GeneralLedgerId = Data.GeneralLedgerId,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int sagaRetry = (int)AuditAction.SagaReTryToServiceForSoap;
            var endAuditCommand = CreateMessage.GeneralLedgerAuditCommand(sagaRetry, messageType, _sagaName, message.ToString(), Data.GeneralLedgerId, Data.SagaReferenceId, leg);
            Bus.Send(endAuditCommand);

            RequestTimeout<GeneralLedgerNoResponse>(TimeSpan.FromMinutes(Constants.GeneralLedgerTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("No Response, Saga giving up. No point continuing!");
            const int sagaGivingUp = (int)AuditAction.SagaRetryLimitReached;
            var auditRetryEndCommand = CreateMessage.GeneralLedgerAuditCommand(sagaGivingUp, messageType, _sagaName, string.Empty, Data.GeneralLedgerId, Data.SagaReferenceId, legUp);
            Bus.Send(auditRetryEndCommand);
        }
    }
}