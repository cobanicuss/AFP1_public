namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class TestCertificate : AuditBase
    {
        public virtual string CertificateId { get; set; }
        public virtual string InboundId { get; set; }
        public virtual int MessageIndex { get; set; }
        public virtual int MessageCount { get; set; }
    }
}