using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.InboundTrigger
{
    public class ProductionOrderFromTriggerHandler : IHandleMessages<ProductionOrderTriggerRequest>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderFromTriggerHandler));
        private readonly IBus _bus;

        public ProductionOrderFromTriggerHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ProductionOrderTriggerRequest message)
        {
            const float leg = 1.0F;

            Logger.Info("======================================");
            Logger.Info("Request received from SAP!");
            Logger.Info("ProductionOrderSapRequest:");
            Logger.Info("Sending audit command.");

            var sapRequestAuditCommand = new ProductionOrderAuditCommand
            {
                InboundId = message.InboundId,
                MessageType = typeof(ProductionOrderTriggerRequest).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceived,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(sapRequestAuditCommand);

            Logger.Info("Sending message to OrrSysService.");
            var requestCommand = new ProductionOrderRequestCommand
            {
                InboundId = message.InboundId
            };
            _bus.Send(requestCommand);
        }
    }
}