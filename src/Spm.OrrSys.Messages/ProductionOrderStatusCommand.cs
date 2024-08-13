using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Messages
{
    public class ProductionOrderStatusCommand : ICommand
    {
        public string ProductionOrderId { get; set; }
        public string SagaReferenceId { get; set; }
        public ProductionOrderStatusPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.ProductionOrderStatusPayloadItem != null)
                rowCount = Payload.ProductionOrderStatusPayloadItem.Count;

            var str = $@"ProductionOrderId={ProductionOrderId},SagaReferenceId={SagaReferenceId},PayloadItems={rowCount}.";

            return str;
        }
    }
}