using System;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class ProductionOrderStatusCommandHandler : IHandleMessages<ProductionOrderStatusCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderStatusCommandHandler));
        private readonly IBus _bus;

        public ProductionOrderStatusCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ProductionOrderStatusCommand message)
        {
            const float leg = 1.0F;

            Logger.Info("======================================");
            Logger.Info("Message received from either:");
            Logger.Info("OrrSys or Spm.OrrSys.Service's Scheduler.");
            Logger.Info("Now sending message from Spm.OrrSys.Service to Saga.");
            Logger.Info($"ProductionOrderId={message.ProductionOrderId}.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}.");

            var messageName = typeof (ProductionOrderStatusCommand).FullName;

            if (message.Payload?.ProductionOrderStatusPayloadItem == null || !message.Payload.ProductionOrderStatusPayloadItem.Any())
            {
                Logger.Warn($"No data in {messageName}.");
                Logger.Warn("The message payload is null OR");
                Logger.Warn("the message has no items in its payload.");
                Logger.Warn("No point in sending message to Saga.");
                Logger.Warn("Ignoring this command.");

                return;
            }

            var sagaStartMessage = new Spm.Service.Messages.ProductionOrderStatusCommand
            {
                SagaReferenceId = message.SagaReferenceId,
                ProductionOrderId = message.ProductionOrderId,
                Payload = message.Payload
            };
            _bus.Send(sagaStartMessage);

            var productionOrderAuditCommand = new ProductionOrderStatusAuditCommand
            {
                ProductionOrderId = message.ProductionOrderId,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductionOrderStatusCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendToSaga,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(productionOrderAuditCommand);

            Logger.Info("Message send successfully.");
        }
    }
}
