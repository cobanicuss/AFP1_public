using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class ProductAchievementSapCommand : ICommand
    {
        public string LotNumber { get; set; }
        public string SagaReferenceId { get; set; }
        public InventoryMovementPayload Payload { get; set; }

        public override string ToString()
        {
            var str = $@"LotNumber={LotNumber},SagaReferenceId={SagaReferenceId}";
            var payload = Payload != null ? InventoryMovementPayloadAsString.ToString(Payload) : string.Empty;

            return str + payload;
        }
    }
}