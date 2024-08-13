using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class GoodsReceiptAuditCommand : AuditBaseCommand
    {
        public string GoodsReceiptId { get; set; }
        public string SagaReferenceId { get; set; }
        public string Type { get; set; }
    }

    public class GoodsReceiptCommitCommand : ICommand
    {
        public GoodsReceiptAuditCommand GoodsReceiptAuditCommand { get; set; }
    }
}