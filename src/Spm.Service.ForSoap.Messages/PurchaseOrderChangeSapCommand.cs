using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class PurchaseOrderChangeSapCommand : ICommand
    {
        public string PurchaseOrderNumber { get; set; }
        public string SagaReferenceId { get; set; }

        public PurchaseOrderPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.PurchaseOrderPayloadItemList != null)
                rowCount = Payload.PurchaseOrderPayloadItemList.Count;

            var str = $@"PurchaseOrderNumber={PurchaseOrderNumber},SagaReferenceId={SagaReferenceId},PayloadItems={rowCount}";

            return str;
        }
    }
}