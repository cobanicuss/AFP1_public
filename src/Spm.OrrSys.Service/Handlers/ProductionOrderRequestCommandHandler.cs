using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class ProductionOrderRequestCommandHandler : IHandleMessages<ProductionOrderRequestCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderRequestCommandHandler));
        private readonly IBus _bus;
        private readonly IDoProductionOrderBusiness _productionOrderBusiness;

        public ProductionOrderRequestCommandHandler(IBus bus, IDoProductionOrderBusiness productionOrderBusiness)
        {
            _bus = bus;
            _productionOrderBusiness = productionOrderBusiness;
        }

        public void Handle(ProductionOrderRequestCommand message)
        {
            const float leg = 2.0F;

            Logger.Info("======================================");
            Logger.Info("Have received ProductionOrderRequestCommand.");
            Logger.Info($"From {EndPointName.SpmServiceForSoap}.");
            
            Logger.Info("Sending Auditlog.");
            var sapRequestAuditCommand = new ProductionOrderAuditCommand
            {
                InboundId = message.InboundId,
                MessageType = typeof(ProductionOrderRequestCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceivedFromServiceForSoap,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(sapRequestAuditCommand);
            
            Logger.Info("Now implementing business logic.");
            _productionOrderBusiness.CreateProductionOrders();
            Logger.Info("All done. All good.");
        }
    }
}