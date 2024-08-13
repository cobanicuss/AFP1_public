using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class ProductionOrderCommitCommandHandler : IHandleMessages<ProductionOrderCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(ProductionOrderCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"InboundId={message.ProductionOrderAuditCommand.InboundId}");
            Logger.Info($"Action={message.ProductionOrderAuditCommand.Action}");

            var productionOrder = new ProductionOrder
            {
                Id = Guid.NewGuid(),
                InboundId = message.ProductionOrderAuditCommand.InboundId,
                MessageType = message.ProductionOrderAuditCommand.MessageType,
                FromEndpoint = message.ProductionOrderAuditCommand.FromEndpoint,
                Action = message.ProductionOrderAuditCommand.Action,
                DateTimeMessageSendToHere = message.ProductionOrderAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.ProductionOrderAuditCommand.MessageData,
                Leg = message.ProductionOrderAuditCommand.Leg,
                LegCount = Constants.ProductionOrderLegCount
            };

            AuditLogRepository.SaveThisAuditLog(productionOrder);
        }
    }
}