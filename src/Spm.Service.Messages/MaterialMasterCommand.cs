using Spm.Shared.Payloads;

namespace Spm.Service.Messages
{
    public class MaterialMasterCommand : CommandBase
    {
        public string ShortItemNumber { get; set; }
        public MaterialMasterPayload Payload { get; set; }

        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.MaterialMasterPayloadItem != null)
                rowCount = Payload.MaterialMasterPayloadItem.Count;

            var str = $@"ShortItemNumber={ShortItemNumber},SagaReferenceId={SagaReferenceId},PayloadItems={rowCount}.";

            return str;
        }
    }
}