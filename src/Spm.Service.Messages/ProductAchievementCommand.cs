using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class ProductAchievementCommand : CommandBase
    {
        public string LotNumber { get; set; }
        public InventoryMovementPayload Payload { get; set; }

        public override string ToString()
        {

            var str = $@"LotNumber={LotNumber},SagaReferenceId={SagaReferenceId},";
            var payload = Payload != null ? InventoryMovementPayloadAsString.ToString(Payload) : string.Empty;

            return str + payload;
        }
    }
}