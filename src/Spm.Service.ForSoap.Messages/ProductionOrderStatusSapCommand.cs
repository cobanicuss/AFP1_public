using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class ProductionOrderStatusSapCommand : ICommand
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