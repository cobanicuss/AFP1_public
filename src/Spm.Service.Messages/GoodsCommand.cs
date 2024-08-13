using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class GoodsCommand : CommandBase
    {
        public string GoodsReceiptId { get; set; }
        public string Type { get; set; }
        public GoodsPayload Payload { get; set; }

        public override string ToString()
        {
            var str = $@"GoodsReceiptId={GoodsReceiptId},SagaReferenceId={SagaReferenceId}";

            return str;
        }
    }
}