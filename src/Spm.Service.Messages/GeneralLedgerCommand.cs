using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class GeneralLedgerCommand : CommandBase
    {
        public string GeneralLedgerId { get; set; }
        public GeneralLedgerPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.GeneralLedgerPayloadItem != null)
                rowCount = Payload.GeneralLedgerPayloadItem.Count;

            var str = $@"GeneralLedgerId={GeneralLedgerId},SagaReferenceId={SagaReferenceId},PayloadItems={rowCount}.";

            return str;
        }
    }
}