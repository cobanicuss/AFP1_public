using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class TestCertificateSapCommand : ICommand
    {
        public string Inboundid { get; set; }
        public int MessageIndex { get; set; }
        public int MessageCount { get; set; }
        public string SagaReferenceId { get; set; }
        
        public TestCertificateOutboundPayload Payload { get; set; }
    }
}