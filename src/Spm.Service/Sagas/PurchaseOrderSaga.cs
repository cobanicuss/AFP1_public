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
    public class PurchaseOrderSaga : Saga<PurchaseOrderSagaData>,
                                        IAmStartedByMessages<PurchaseOrderCreateCommand>,
                                        IHandleMessages<PurchaseOrderCreateResponseCommand>,
                                        IHandleTimeouts<PurchaseOrderCreateNoResponse>,
                                        IAmStartedByMessages<PurchaseOrderChangeCommand>,
                                        IHandleMessages<PurchaseOrderChangeResponseCommand>,
                                        IHandleTimeouts<PurchaseOrderChangeNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PurchaseOrderSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IPurchaseOrderTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaType = typeof(PurchaseOrderSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<PurchaseOrderSagaData> mapper)
        {
            mapper.ConfigureMapping<PurchaseOrderCreateCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<PurchaseOrderCreateResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);

            mapper.ConfigureMapping<PurchaseOrderChangeCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<PurchaseOrderChangeResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(PurchaseOrderCreateCommand message)
        {
            const float leg = 2.0F;

            var messageType = typeof(PurchaseOrderCreateCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from File-Watcher-Service.");
            Logger.Info("Saga is now STARTED.");

            var purchaseOrderCreateNumber = message.PurchaseOrderNumber;
            Logger.Info($"PurchaseOrderCreateNumber={purchaseOrderCreateNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.PurchaseOrderNumber = purchaseOrderCreateNumber;
            Data.PurchaseOrderActionType = Shared.Constants.PoCreate;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.PurchaseOrderCreateRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(Data.PurchaseOrderNumber, Data.Id, _sagaType, Shared.Constants.PoCreate, Data.SagaReferenceId);

            var purchaseOrderCreateSapCommand = CreateMessage.PurchaseOrderCreateSapCommand(message);
            Bus.Send(purchaseOrderCreateSapCommand);


            const int action = (int) AuditAction.SagaSendToServiceForSoap;
            const string type = Shared.Constants.PoCreate;
            var purchaseOrderCreateAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg,
                type);
            Bus.Send(purchaseOrderCreateAuditCommand);

            RequestTimeout<PurchaseOrderCreateNoResponse>(TimeSpan.FromMinutes(Constants.PurchaseOrderCreateTimeoutMinutes));
        }

        public void Handle(PurchaseOrderCreateResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"PurchaseOrderCreateNumber={Data.PurchaseOrderNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var messageType = typeof(PurchaseOrderCreateResponseCommand).FullName;

            Transition.End(Data.PurchaseOrderNumber, Data.Id, _sagaType, Data.SagaState, Shared.Constants.PoCreate, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();

            const int action = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            const string type = Shared.Constants.PoCreate;
            var purchaseOrderCreateAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg,
                type);
            Bus.Send(purchaseOrderCreateAuditCommand);
        }

        public void Timeout(PurchaseOrderCreateNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("Purchase-Order-Create: No response received! Uncool!");

            var messageType = typeof(PurchaseOrderCreateNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp, Shared.Constants.PoCreate);
                return;
            }

            Transition.NoResponse(Data.PurchaseOrderNumber, Data.Id, _sagaType, Data.SagaState, Shared.Constants.PoCreate, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<PurchaseOrderPayload>(Data.SerializedMessageId);
            var message = new PurchaseOrderCreateSapCommand
            {
                PurchaseOrderNumber = Data.PurchaseOrderNumber,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int action = (int)AuditAction.SagaReTryToServiceForSoap;
            const string type = Shared.Constants.PoCreate;
            var purchaseOrderCreateAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg + 0.001F,
                type);
            Bus.Send(purchaseOrderCreateAuditCommand);

            RequestTimeout<PurchaseOrderCreateNoResponse>(TimeSpan.FromMinutes(Constants.PurchaseOrderCreateTimeoutMinutes));
        }

        public void Handle(PurchaseOrderChangeCommand message)
        {
            const float leg = 2.0F;

            var messageType = typeof(PurchaseOrderChangeNoResponse).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from File-Watcher-Service.");
            Logger.Info("Saga is now STARTED.");

            var purchaseOrderChangeNumber = message.PurchaseOrderNumber;
            Logger.Info($"PurchaseOrderChangeNumber={purchaseOrderChangeNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.PurchaseOrderNumber = purchaseOrderChangeNumber;
            Data.PurchaseOrderActionType = Shared.Constants.PoChange;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.PurchaseOrderChangeRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(Data.PurchaseOrderNumber, Data.Id, _sagaType, Shared.Constants.PoChange, Data.SagaReferenceId);

            var purchaseOrderChangeSapCommand = CreateMessage.PurchaseOrderChangeSapCommand(message);
            Bus.Send(purchaseOrderChangeSapCommand);

            const int action = (int)AuditAction.SagaSendToServiceForSoap;
            const string type = Shared.Constants.PoChange;
            var purchaseOrderChangeAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg,
                type);

            Bus.Send(purchaseOrderChangeAuditCommand);

            RequestTimeout<PurchaseOrderChangeNoResponse>(TimeSpan.FromMinutes(Constants.PurchaseOrderChangeTimeoutMinutes));
        }

        public void Handle(PurchaseOrderChangeResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"PurchaseOrderChangeNumber={Data.PurchaseOrderNumber}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(PurchaseOrderChangeResponseCommand).FullName;

            Transition.End(Data.PurchaseOrderNumber, Data.Id, _sagaType, Data.SagaState, Shared.Constants.PoChange, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
            
            const int action = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            const string type = Shared.Constants.PoChange;
            var purchaseOrderChangeAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg,
                type);
            Bus.Send(purchaseOrderChangeAuditCommand);
        }

        public void Timeout(PurchaseOrderChangeNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Purchase-Order-Change! Uncool!");

            var messageType = typeof(PurchaseOrderChangeNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp, Shared.Constants.PoChange);
                return;
            }

            Transition.NoResponse(Data.PurchaseOrderNumber, Data.Id, _sagaType, Data.SagaState, Shared.Constants.PoChange, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<PurchaseOrderPayload>(Data.SerializedMessageId);
            var message = new PurchaseOrderChangeSapCommand
            {
                PurchaseOrderNumber = Data.PurchaseOrderNumber,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int action = (int)AuditAction.SagaReTryToServiceForSoap;
            const string type = Shared.Constants.PoChange;
            var purchaseOrderChangeAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                message.ToString(),
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                leg + 0.001F,
                type);
            Bus.Send(purchaseOrderChangeAuditCommand);

            RequestTimeout<PurchaseOrderCreateNoResponse>(TimeSpan.FromMinutes(Constants.PurchaseOrderChangeTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp, string type)
        {
            Logger.Info("No Response, Saga giving Up. No point continuing!");

            const int action = (int)AuditAction.SagaRetryLimitReached;
            var purchaseOrderGenericAuditCommand = CreateMessage.PurchaseOrderAuditCommand(
                action,
                messageType,
                _sagaType,
                Shared.Constants.NotAvailable,
                Data.PurchaseOrderNumber,
                Data.SagaReferenceId,
                legUp,
                type);
            Bus.Send(purchaseOrderGenericAuditCommand);
        }
    }
}