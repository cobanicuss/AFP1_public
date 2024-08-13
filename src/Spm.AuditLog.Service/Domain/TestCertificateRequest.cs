namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class TestCertificateRequest : AuditInboundBase
    {
        public virtual string CertificateId { get; set; }
        public virtual string InboundId { get; set; }
    }
}