using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class TestCertificateAuditCommand : AuditBaseCommand
    {
        public string CertificateId { get; set; }
        public string SagaReferenceId { get; set; }
        public string InboundId { get; set; }
        public int MessageIndex { get; set; }
        public int MessageCount { get; set; }
    }

    public class TestCertificateCommitCommand : ICommand
    {
        public TestCertificateAuditCommand TestCertificateAuditCommand { get; set; }
    }
}