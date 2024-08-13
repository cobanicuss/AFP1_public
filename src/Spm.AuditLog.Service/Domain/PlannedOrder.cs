namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class PlannedOrder : AuditInboundBase
    {
        public virtual string InboundId { get; set; }
    }
}