using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class GoodsReceiptToSapHandler : IHandleMessages<GoodsReceiptSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsReceiptToSapHandler));
        private readonly IBus _bus;
        private readonly ISendGoodsReceiptToSap _sendSoapToSap;

        public GoodsReceiptToSapHandler(IBus bus, ISendGoodsReceiptToSap sendSoapToSap)
        {
            _bus = bus;
            _sendSoapToSap = sendSoapToSap;
        }

        public void Handle(GoodsReceiptSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");

            _sendSoapToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var goodsReceiptAuditCommand = new GoodsReceiptAuditCommand
            {
                GoodsReceiptId = message.GoodsReceiptId,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(GoodsReceiptSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg,
                Type = message.Type
            };

            _bus.Send(goodsReceiptAuditCommand);
        }
    }
}