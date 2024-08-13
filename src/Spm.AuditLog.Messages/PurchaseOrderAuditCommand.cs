using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class PurchaseOrderAuditCommand : AuditBaseCommand
    {
        public string PurchaseOrderNumber { get; set; }
        public string SagaReferenceId { get; set; }
        public string Type { get; set; }
    }

    public class PurchaseOrderCommitCommand : ICommand
    {
        public PurchaseOrderAuditCommand PurchaseOrderAuditCommand { get; set; }
    }
}