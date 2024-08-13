using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class GoodsReceiptSapCommand : ICommand
    {
        public string GoodsReceiptId { get; set; }
        public string SagaReferenceId { get; set; }
        public string Type { get; set; }
        public GoodsPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.GoodsPayloadItem != null)
                rowCount = Payload.GoodsPayloadItem.Count;

            var str = $@"SagaReferenceId={SagaReferenceId},GoodsReceiptId={GoodsReceiptId},PayloadItems={rowCount}.";

            return str;
        }
    }
}