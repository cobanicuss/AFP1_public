using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class TestCertificateResponseCommand : ICommand
    {
        public string InboundId { get; set; }
        public string CertificateId { get; set; }
        public string SagaReferenceId { get; set; }
        public int MessageIndex { get; set; }
        public int MessageCount { get; set; }
        public string[] LotNumberList { get; set; }
    }
}