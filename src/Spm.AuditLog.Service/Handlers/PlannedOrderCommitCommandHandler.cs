using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class PlannedOrderCommitCommandHandler : IHandleMessages<PlannedOrderCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlannedOrderCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(PlannedOrderCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"InboundId={message.PlannedOrderAuditCommand.InboundId}");
            Logger.Info($"Action={message.PlannedOrderAuditCommand.Action}");

            var plannedOrder = new PlannedOrder
            {
                Id = Guid.NewGuid(),
                InboundId = message.PlannedOrderAuditCommand.InboundId,
                MessageType = message.PlannedOrderAuditCommand.MessageType,
                FromEndpoint = message.PlannedOrderAuditCommand.FromEndpoint,
                Action = message.PlannedOrderAuditCommand.Action,
                DateTimeMessageSendToHere = message.PlannedOrderAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.PlannedOrderAuditCommand.MessageData,
                Leg = message.PlannedOrderAuditCommand.Leg,
                LegCount = Constants.PlanndedOrderLegCount
            };

            AuditLogRepository.SaveThisAuditLog(plannedOrder);
        }
    }
}