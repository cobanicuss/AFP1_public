using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class TestCertificateCommand : CommandBase
    {
        public string InboundId { get; set; }
        public string[] LotNumberList { get; set; }
        public int MessageIndex { get; set; }
        public int MessageCount { get; set; }
        public TestCertificateOutboundPayload Payload { get; set; }
    }
}