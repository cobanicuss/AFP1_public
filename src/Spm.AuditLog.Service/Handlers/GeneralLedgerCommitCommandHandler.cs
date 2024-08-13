using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class GeneralLedgerCommitCommandHandler : IHandleMessages<GeneralLedgerCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerCommitCommandHandler));
        
        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(GeneralLedgerCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"GeneralLedgerId={message.GeneralLedgerAuditCommand.GeneralLedgerId}");
            Logger.Info($"Action={message.GeneralLedgerAuditCommand.Action}");

            var generalLedger = new GeneralLedger
            {
                Id = Guid.NewGuid(),
                GeneralLedgerId = message.GeneralLedgerAuditCommand.GeneralLedgerId,
                SagaReferenceId = message.GeneralLedgerAuditCommand.SagaReferenceId,
                MessageType = message.GeneralLedgerAuditCommand.MessageType,
                FromEndpoint = message.GeneralLedgerAuditCommand.FromEndpoint,
                Action = message.GeneralLedgerAuditCommand.Action,
                DateTimeMessageSendToHere = message.GeneralLedgerAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.GeneralLedgerAuditCommand.MessageData,
                Leg = message.GeneralLedgerAuditCommand.Leg,
                LegCount = Constants.GeneralLedgerLegCount
            };

            AuditLogRepository.SaveThisAuditLog(generalLedger);
        }
    }
}