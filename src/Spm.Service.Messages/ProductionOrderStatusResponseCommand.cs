using NServiceBus;

namespace Spm.Service.Messages
{
    public class ProductionOrderStatusResponseCommand : ICommand
    {
        public string ProductionOrderId { get; set; }
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = string.Format(@"ProductionOrderId={0},SagaReferenceId={1}", ProductionOrderId, SagaReferenceId);

            return str;
        }
    }
}
