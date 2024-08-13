using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class PurchaseOrderCreateSapResponse : IMessage
    {
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = $"SagaReferenceId={SagaReferenceId}.";

            return str;
        }
    }
}