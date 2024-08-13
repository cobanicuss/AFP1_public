using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class ProductionOrderStatusFromSapHandler : IHandleMessages<ProductionOrderStatusSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderStatusFromSapHandler));
        private readonly IBus _bus;

        public ProductionOrderStatusFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ProductionOrderStatusSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("ProductionOrderStatusSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var productionOrderAuditCommand = new ProductionOrderStatusAuditCommand
            {
                ProductionOrderId = Shared.Constants.NotAvailable,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductionOrderStatusSapResponse).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(productionOrderAuditCommand);

            var sagaCompleteCommand = new ProductionOrderStatusResponseCommand
            {
                SagaReferenceId = message.SagaReferenceId,
                ProductionOrderId = message.ProductionOrderId
            };
            _bus.Send(sagaCompleteCommand);
        }
    }
}