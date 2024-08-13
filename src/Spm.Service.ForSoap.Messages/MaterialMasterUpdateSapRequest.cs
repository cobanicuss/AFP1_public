using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Messages
{
    public class MaterialMasterUpdateSapRequest : IMessage
    {
        public string ShortItemNumber { get; set; }
        public string InboundId { get; set; }
        public MaterialMasterUpdatePayload Payload { get; set; }
    }
}