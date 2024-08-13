using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class PlannedOrderAuditCommand : AuditBaseCommand
    {
        public string InboundId { get; set; }
    }

    public class PlannedOrderCommitCommand : ICommand
    {
        public PlannedOrderAuditCommand PlannedOrderAuditCommand { get; set; }
    }
}
