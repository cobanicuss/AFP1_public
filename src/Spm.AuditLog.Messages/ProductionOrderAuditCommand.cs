using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class ProductionOrderAuditCommand : AuditBaseCommand
    {
        public string InboundId { get; set; }
    }

    public class ProductionOrderCommitCommand : ICommand
    {
        public ProductionOrderAuditCommand ProductionOrderAuditCommand { get; set; }
    }
}