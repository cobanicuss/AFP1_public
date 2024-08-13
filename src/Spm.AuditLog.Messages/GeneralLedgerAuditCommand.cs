using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class GeneralLedgerAuditCommand : AuditBaseCommand
    {
        public string GeneralLedgerId { get; set; }
        public string SagaReferenceId { get; set; }
    }

    public class GeneralLedgerCommitCommand : ICommand
    {
        public GeneralLedgerAuditCommand GeneralLedgerAuditCommand { get; set; }
    }
}