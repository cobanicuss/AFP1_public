using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class TestCertificateTriggerRequest : ICommand
    {
        public string InboundId { get; set; }
    }
}