using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class TestCertificateTriggerCommand : ICommand
    {
        public string InboundId { get; set; }
    }
}