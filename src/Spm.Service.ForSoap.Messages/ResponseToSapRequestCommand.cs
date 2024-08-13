using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class ResponseToSapRequestCommand : ICommand
    {
        public string NumberId { get; set; }
        public EnumInboundType InboundType { get; set; }

        public override string ToString()
        {
            var str = $@"NumberId={NumberId}_|[Response after SAP request received].";

            return str;
        }
    }
}