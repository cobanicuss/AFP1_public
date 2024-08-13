using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class ProductAchievementCommitCommandHandler : IHandleMessages<ProductAchievementCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(ProductAchievementCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"LotNumberId={message.ProductAchievementAuditCommand.LotNumber}");
            Logger.Info($"Action={message.ProductAchievementAuditCommand.Action}");

            var productAchievementAudit = new ProductAchievement
            {
                Id = Guid.NewGuid(),
                LotNumber = message.ProductAchievementAuditCommand.LotNumber,
                SagaReferenceId = message.ProductAchievementAuditCommand.SagaReferenceId,
                MessageType = message.ProductAchievementAuditCommand.MessageType,
                FromEndpoint = message.ProductAchievementAuditCommand.FromEndpoint,
                Action = message.ProductAchievementAuditCommand.Action,
                DateTimeMessageSendToHere = message.ProductAchievementAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.ProductAchievementAuditCommand.MessageData,
                Leg = message.ProductAchievementAuditCommand.Leg,
                LegCount = Constants.ProductAchievementLegCount
            };

            AuditLogRepository.SaveThisAuditLog(productAchievementAudit);
        }
    }
}