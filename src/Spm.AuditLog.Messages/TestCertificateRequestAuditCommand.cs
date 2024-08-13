using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class TestCertificateRequestAuditCommand : AuditBaseCommand
    {
        public string CertificateId { get; set; }
        public string InboundId { get; set; }
    }

    public class TestCertificateRequestCommitCommand : ICommand
    {
        public TestCertificateRequestAuditCommand TestCertificateRequestAuditCommand { get; set; }
    }
}