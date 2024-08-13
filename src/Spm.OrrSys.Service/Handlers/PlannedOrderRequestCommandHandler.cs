using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class PlannedOrderRequestCommandHandler : IHandleMessages<PlannedOrderRequestCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlannedOrderRequestCommandHandler));
        private readonly IBus _bus;
        private readonly IDoPlannedOrderBusiness _plannedOrderBusiness;

        public PlannedOrderRequestCommandHandler(IBus bus, IDoPlannedOrderBusiness plannedOrderBusiness)
        {
            _bus = bus;
            _plannedOrderBusiness = plannedOrderBusiness;
        }

        public void Handle(PlannedOrderRequestCommand message)
        {
            const float leg = 2.0F;

            Logger.Info("======================================");
            Logger.Info("Have received PlannedOrderRequestCommand.");
            Logger.Info($"From {EndPointName.SpmServiceForSoap}.");
            
            Logger.Info("Sending Auditlog.");
            var sapRequestAuditCommand = new PlannedOrderAuditCommand
            {
                InboundId = message.InboundId,
                MessageType = typeof(PlannedOrderRequestCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceivedFromServiceForSoap,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(sapRequestAuditCommand);

            Logger.Info("Now implementing busines logic.");
            _plannedOrderBusiness.CreatePlannedOrders();
            Logger.Info("All done. All good.");
        }
    }
}