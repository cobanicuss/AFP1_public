using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class PurchaseOrderCreateCommand : CommandBase
    {
        public string PurchaseOrderNumber { get; set; }

        public PurchaseOrderPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.PurchaseOrderPayloadItemList != null)
                rowCount = Payload.PurchaseOrderPayloadItemList.Count;

            var str = $@"PurchaseOrderNumber={PurchaseOrderNumber},SagaReferenceId={SagaReferenceId},PayloadItems={rowCount}.";

            return str;
        }
    }
}