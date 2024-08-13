using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class ProductionOrderStatusCommand : CommandBase
    {
        public string ProductionOrderId { get; set; }
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