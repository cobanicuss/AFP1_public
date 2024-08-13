namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class PurchaseOrderCreate : AuditBase
    {
        public virtual string PurchaseOrderNumber { get; set; }
    }
}
