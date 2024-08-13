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
    public class GoodsReceiptSaga : Saga<GoodsReceiptSagaData>,
                                IAmStartedByMessages<GoodsCommand>,
                                IHandleMessages<GoodsReceiptResponseCommand>,
                                IHandleTimeouts<GoodsReceiptNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsReceiptSaga));
        public ICreateMessage CreateMessage { get; set; }
        public IGoodsTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaName = typeof(GoodsReceiptSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GoodsReceiptSagaData> mapper)
        {
            mapper.ConfigureMapping<GoodsCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<GoodsReceiptResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(GoodsCommand message)
        {
            const float leg = 2.0F;

            var messageType = typeof(GoodsCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from File-Watcher-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"GoodsReceiptId={message.GoodsReceiptId}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");
            Logger.Info($"Type={message.Type}");

            Data.SagaReferenceId = message.SagaReferenceId;
            Data.GoodsReceiptId = message.GoodsReceiptId;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.GoodsReceiptRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;
            Data.Type = message.Type;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.GoodsReceiptId, Data.Id, _sagaName, Data.Type, Data.SagaReferenceId);

            var goodsReceiptSapCommand = CreateMessage.GoodsReceiptSapCommand(message);
            Bus.Send(goodsReceiptSapCommand);

            const int sagaSend = (int)AuditAction.SagaSendToServiceForSoap;
            var sendLocalAuditCommand = CreateMessage.GoodsReceiptAuditCommand(sagaSend, messageType, _sagaName, message.ToString(), Data.GoodsReceiptId, Data.SagaReferenceId, leg, Data.Type);
            Bus.Send(sendLocalAuditCommand);

            RequestTimeout<GoodsReceiptNoResponse>(TimeSpan.FromMinutes(Constants.GoodsReceiptTimeoutMinutes));
        }

        public void Handle(GoodsReceiptResponseCommand message)
        {
            const float previousLeg = 4.0F;
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"GoodsReceiptId={Data.GoodsReceiptId}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(GoodsReceiptResponseCommand).FullName;

            //See Spm.Service.ForSoap <GoodsReceiptFromSapHandler> for more detail as to why this is done here and not at ServiceForSoap//
            const int auditFromPreviousLeg = (int)AuditAction.ResponseReceivedFromSap;
            var previousAuditCommand = CreateMessage.GoodsReceiptAuditCommand(auditFromPreviousLeg, messageType, _sagaName, Shared.Constants.NotAvailable, Data.GoodsReceiptId, Data.SagaReferenceId, previousLeg, Data.Type);
            Bus.Send(previousAuditCommand);

            const int responseFromServiceForSoap = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var auditCommand = CreateMessage.GoodsReceiptAuditCommand(responseFromServiceForSoap, messageType, _sagaName, message.ToString(), Data.GoodsReceiptId, Data.SagaReferenceId, leg, Data.Type);
            Bus.Send(auditCommand);

            Transition.End(Data.GoodsReceiptId, Data.Id, _sagaName, Data.SagaState, Data.Type, Data.SagaReferenceId);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(GoodsReceiptNoResponse state)
        {
            const float leg = 3.1F;
            const float legUp = 3.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Goods-Receipt! Uncool!");

            var messageType = typeof(GoodsReceiptNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.GoodsReceiptId, Data.Id, _sagaName, Data.SagaState, Data.Type, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<GoodsPayload>(Data.SerializedMessageId);

            var message = new GoodsReceiptSapCommand
            {
                GoodsReceiptId = Data.GoodsReceiptId,
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload,
                Type = Data.Type
            };
            Bus.Send(message);

            const int sagaRetry = (int)AuditAction.SagaReTryToServiceForSoap;
            var endAuditCommand = CreateMessage.GoodsReceiptAuditCommand(sagaRetry, messageType, _sagaName, message.ToString(), Data.GoodsReceiptId, Data.SagaReferenceId, leg, Data.Type);
            Bus.Send(endAuditCommand);

            RequestTimeout<GoodsReceiptNoResponse>(TimeSpan.FromMinutes(Constants.GoodsReceiptTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("No Response, Saga giving up. No point continuing!");
            const int sagaGivingUp = (int)AuditAction.SagaRetryLimitReached;
            var auditRetryEndCommand = CreateMessage.GoodsReceiptAuditCommand(sagaGivingUp, messageType, _sagaName, string.Empty, Data.GoodsReceiptId, Data.SagaReferenceId, legUp, Data.Type);
            Bus.Send(auditRetryEndCommand);
        }
    }
}