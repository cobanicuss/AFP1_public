namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class GoodsReceipt : AuditBase
    {
        public virtual string GoodsReceiptId { get; set; }
    }
}