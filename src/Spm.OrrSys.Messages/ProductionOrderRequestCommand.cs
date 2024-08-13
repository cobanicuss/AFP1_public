using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class ProductionOrderRequestCommand : ICommand
    {
        public string InboundId { get; set; }
    }
}