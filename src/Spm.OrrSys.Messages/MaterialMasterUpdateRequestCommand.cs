using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Messages
{
    public class MaterialMasterUpdateRequestCommand : ICommand
    {
        public string InboundId { get; set; }
        public string ShortItemNumber { get; set; }
        public MaterialMasterUpdatePayload Payload { get; set; }

        public override string ToString()
        {
            var str = $@"ShortItemNumber={ShortItemNumber}";

            return str;
        }
    }
}