using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class ProductionOrderStatusToSapHandler : IHandleMessages<ProductionOrderStatusSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderStatusToSapHandler));
        private readonly IBus _bus;
        private readonly ISendProductionOrderStatusToSap _sendToSap;

        public ProductionOrderStatusToSapHandler(IBus bus, ISendProductionOrderStatusToSap sendToSap)
        {
            _bus = bus;
            _sendToSap = sendToSap;
        }

        public void Handle(ProductionOrderStatusSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("ProductionOrderStatusSapCommand.");
            Logger.Info("Now attempting to send a message to SAP.");
            Logger.Info($"PayloadItem.Count={message.Payload.ProductionOrderStatusPayloadItem.Count}");

            _sendToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var productionOrderStatusAuditCommand = new ProductionOrderStatusAuditCommand
            {
                ProductionOrderId = message.ProductionOrderId,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductionOrderStatusSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg
            };

            _bus.Send(productionOrderStatusAuditCommand);
        }
    }
}