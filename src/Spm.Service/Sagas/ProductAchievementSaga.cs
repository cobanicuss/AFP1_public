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
using Spm.Service.Validation;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.Service.Sagas
{
    public class ProductAchievementSaga : Saga<ProductAchievementSagaData>,
                                        IAmStartedByMessages<ProductAchievementCommand>,
                                        IHandleMessages<ProductAchievementResponseCommand>,
                                        IHandleTimeouts<ProductAchievementNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IProductAchievementTransitions Transition { get; set; }
        public IValidateProductAchievement Validate { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaName = typeof(ProductAchievementSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProductAchievementSagaData> mapper)
        {
            mapper.ConfigureMapping<ProductAchievementCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<ProductAchievementResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(ProductAchievementCommand message)
        {
            const float leg = 2.0F;

            if (Validate.HasPreviouselyBeenCreated(message.LotNumber))
            {
                Logger.Info("Have already processed this LotNumber.");
                MarkAsComplete();
                return;
            }

            var messageType = typeof(ProductAchievementCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from OrrSys-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"Unique Id: LotNumber={message.LotNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.LotNumber = message.LotNumber;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.ProductAchievementRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.LotNumber, Data.Id, _sagaName, Data.SagaReferenceId);

            var productAchievementSapCommand = CreateMessage.ProductAchievementSapCommand(message);
            Bus.Send(productAchievementSapCommand);

            const int sagaSend = (int)AuditAction.SagaSendToServiceForSoap;
            var sendLocalAuditCommand = CreateMessage.ProductAchievementAuditCommand(sagaSend, messageType, _sagaName, message.ToString(), message.LotNumber, Data.SagaReferenceId, leg);
            Bus.Send(sendLocalAuditCommand);

            RequestTimeout<ProductAchievementNoResponse>(TimeSpan.FromMinutes(Constants.ProductAchievementTimeoutMinutes));
        }

        public void Handle(ProductAchievementResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"LotNumber={Data.LotNumber}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(ProductAchievementResponseCommand).FullName;

            const int fromServiceForSoap = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var auditCommand = CreateMessage.ProductAchievementAuditCommand(fromServiceForSoap, messageType, _sagaName, message.ToString(), Data.LotNumber, Data.SagaReferenceId, leg);
            Bus.Send(auditCommand);

            Transition.End(Data.LotNumber, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            var orrSysSapResponse = new OrrSys.Messages.ProductAchievementResponseCommand
            {
                LotNumber = Data.LotNumber,
                SagaReferenceId = Data.SagaReferenceId
            };
            Bus.Send(orrSysSapResponse);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(ProductAchievementNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Product-Achievement! Uncool!");

            var messageType = typeof(ProductAchievementNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.LotNumber, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<InventoryMovementPayload>(Data.SerializedMessageId);
            var message = new ProductAchievementSapCommand
            {
                LotNumber = Data.LotNumber,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int sagaRetry = (int)AuditAction.SagaReTryToServiceForSoap;
            var endAuditCommand = CreateMessage.ProductAchievementAuditCommand(sagaRetry, messageType, _sagaName, message.ToString(), Data.LotNumber, Data.SagaReferenceId, leg);
            Bus.Send(endAuditCommand);

            RequestTimeout<ProductAchievementNoResponse>(TimeSpan.FromMinutes(Constants.ProductAchievementTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("No Response, Saga giving up. No point continuing!");
            const int sagaGivingUp = (int)AuditAction.SagaRetryLimitReached;
            var auditRetryEndCommand = CreateMessage.ProductAchievementAuditCommand(sagaGivingUp, messageType, _sagaName, string.Empty, Data.LotNumber, Data.SagaReferenceId, legUp);
            Bus.Send(auditRetryEndCommand);
        }
    }
}