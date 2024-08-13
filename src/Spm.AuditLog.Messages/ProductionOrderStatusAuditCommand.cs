using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class ProductionOrderStatusAuditCommand : AuditBaseCommand
    {
        public string ProductionOrderId { get; set; }
        public string SagaReferenceId { get; set; }
    }

    public class ProductionOrderStatusCommitCommand : ICommand
    {
        public ProductionOrderStatusAuditCommand ProductionOrderStatusAuditCommand { get; set; }
    }
}