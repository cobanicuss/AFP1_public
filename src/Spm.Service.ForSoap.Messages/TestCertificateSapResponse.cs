using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class TestCertificateSapResponse : BaseResponseIdoc, IMessage
    {
        public TestCertificateSapResponse(BaseResponseIdoc baseResponseIdoc) : base(baseResponseIdoc) { }

        public string SagaReferenceId { get; set; }
    }
}