using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.InboundTrigger
{
    public class PlannedOrderFromTriggerHandler : IHandleMessages<PlannedOrderTriggerRequest>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlannedOrderFromTriggerHandler));
        private readonly IBus _bus;

        public PlannedOrderFromTriggerHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PlannedOrderTriggerRequest message)
        {
            const float leg = 1.0F;

            Logger.Info("======================================");
            Logger.Info("Request message received from SAP!");
            Logger.Info("PlannedOrderSapRequest:");

            Logger.Info("Sending audit command.");
            var productAchievementAuditCommand = new PlannedOrderAuditCommand
            {
                InboundId = message.InboundId,
                MessageType = typeof(PlannedOrderTriggerRequest).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceived,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(productAchievementAuditCommand);

            Logger.Info("Sending message to OrrSysService.");
            var requestCommand = new PlannedOrderRequestCommand
            {
                InboundId = message.InboundId
            };
            _bus.Send(requestCommand);
        }
    }
}