using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class PlannedOrderTriggerRequest : IMessage
    {
        public string InboundId { get; set; }

        public override string ToString()
        {
            var str = $"InboundId={InboundId}";
            return str;
        }
    }
}