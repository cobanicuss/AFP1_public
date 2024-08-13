using NServiceBus;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Messages
{
    public class TestCertificateRequestCommand : ICommand
    {
        public string CertificateId { get; set; }
        public string InboundId { get; set; }
        public TestCertificateRequestPayload Payload { get; set; }   
    
        public override string ToString()
        {
            var rowCount = 0;

            if (Payload?.TestCertificatePayloadItemList != null)
                rowCount = Payload.TestCertificatePayloadItemList.Count;

            var str = $@"CertificateId={CertificateId},InboundId={InboundId},PayloadItems={rowCount}.";

            return str;
        }
    }
}