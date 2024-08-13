namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MaterialMasterUpdate : AuditBase
    {
        public virtual string ShortItemNumber { get; set; }
        public virtual string InboundId { get; set; }
    }
}