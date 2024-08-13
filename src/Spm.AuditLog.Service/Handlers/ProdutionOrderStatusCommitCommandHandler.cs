using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class ProdutionOrderStatusCommitCommandHandler : IHandleMessages<ProductionOrderStatusCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProdutionOrderStatusCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(ProductionOrderStatusCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"ProductionOrderId={message.ProductionOrderStatusAuditCommand.ProductionOrderId}");
            Logger.Info($"Action={message.ProductionOrderStatusAuditCommand.Action}");

            var productionOrderStatus = new ProductionOrderStatus
            {
                Id = Guid.NewGuid(),
                ProductionOrderId = message.ProductionOrderStatusAuditCommand.ProductionOrderId,
                SagaReferenceId = message.ProductionOrderStatusAuditCommand.SagaReferenceId,
                MessageType = message.ProductionOrderStatusAuditCommand.MessageType,
                FromEndpoint = message.ProductionOrderStatusAuditCommand.FromEndpoint,
                Action = message.ProductionOrderStatusAuditCommand.Action,
                DateTimeMessageSendToHere = message.ProductionOrderStatusAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.ProductionOrderStatusAuditCommand.MessageData,
                Leg = message.ProductionOrderStatusAuditCommand.Leg,
                LegCount = Constants.ProductionOrderStatusLegCount
            };

            AuditLogRepository.SaveThisAuditLog(productionOrderStatus);
        }
    }
}