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
    public class ProductionOrderSaga : Saga<ProductionOrderSagaData>,
                                    IAmStartedByMessages<ProductionOrderStatusCommand>,
                                    IHandleMessages<ProductionOrderStatusResponseCommand>,
                                    IHandleTimeouts<ProductionOrderStatusNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IProductionOrderTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaType = typeof(ProductionOrderSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProductionOrderSagaData> mapper)
        {
            mapper.ConfigureMapping<ProductionOrderStatusCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<ProductionOrderStatusResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(ProductionOrderStatusCommand message)
        {
            const float leg = 2.0F;
            var messageType = typeof(ProductionOrderStatusCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from OrrSys-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"ProductionOrderId={message.ProductionOrderId}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.ProductionOrderId = message.ProductionOrderId;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.ProductionOrderStatusRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.ProductionOrderId, Data.Id, _sagaType, Data.SagaReferenceId);

            var productionOrderStatusSapCommand = CreateMessage.ProductionOrderStatusSapCommand(message);
            Bus.Send(productionOrderStatusSapCommand);

            const int action = (int) AuditAction.SagaSendToServiceForSoap;
            var productionOrderStatusAuditCommand = CreateMessage.ProductionOrderAuditCommand(
                    action,
                    messageType,
                    _sagaType,
                    message.ToString(),
                    Data.ProductionOrderId,
                    Data.SagaReferenceId,
                    leg);
            Bus.Send(productionOrderStatusAuditCommand);

            RequestTimeout<ProductionOrderStatusNoResponse>(TimeSpan.FromMinutes(Constants.ProductionOrderStatusTimeoutMinutes));
        }

        public void Handle(ProductionOrderStatusResponseCommand message)
        {
            const float leg = 5.0F;
            var messageType = typeof(ProductionOrderStatusResponseCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"ProductionOrderId={Data.ProductionOrderId}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            const int action = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var productionOrderStatusAuditCommand = CreateMessage.ProductionOrderAuditCommand(
                action, 
                messageType, 
                _sagaType, 
                message.ToString(), 
                Data.ProductionOrderId, 
                Data.SagaReferenceId, 
                leg);
            Bus.Send(productionOrderStatusAuditCommand);

            Transition.End(Data.ProductionOrderId, Data.Id, _sagaType, Data.SagaState, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(ProductionOrderStatusNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Production-Order-Status! Uncool!");

            var messageType = typeof(ProductionOrderStatusNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.ProductionOrderId, Data.Id, _sagaType, Data.SagaState, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<ProductionOrderStatusPayload>(Data.SerializedMessageId);
            var message = new ProductionOrderStatusSapCommand
            {
                ProductionOrderId = Data.ProductionOrderId,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);
            
            const int action = (int)AuditAction.SagaReTryToServiceForSoap;
            var productionOrderStatusAuditCommand = CreateMessage.ProductionOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                string.Empty,
                Data.ProductionOrderId,
                Data.SagaReferenceId,
                leg);
            Bus.Send(productionOrderStatusAuditCommand);

            RequestTimeout<ProductionOrderStatusNoResponse>(TimeSpan.FromMinutes(Constants.ProductAchievementTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("Saga giving up. No point continuing!");

            const int action = (int)AuditAction.SagaRetryLimitReached;
            var productionOrderStatusAuditCommand = CreateMessage.ProductionOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                string.Empty,
                Data.ProductionOrderId,
                Data.SagaReferenceId,
                legUp);
            Bus.Send(productionOrderStatusAuditCommand);
        }
    }
}