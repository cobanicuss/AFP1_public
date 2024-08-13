using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class PlannedOrderRequestCommand : ICommand
    {
        public string InboundId { get; set; }
    }
}