using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class TestCertificateFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<TestCertificateFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new TestCertificateFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Test-Certificate from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustTestCertificateAuditCommandMessage())
                    .And(_ => HandlerMustSendTestCertificateResponseCommand())

                .BDDfy();
        }

        private void HandlerMustTestCertificateAuditCommandMessage()
        {
            _handler.ExpectSend<TestCertificateAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendTestCertificateResponseCommand()
        {
            _handler.ExpectSend<TestCertificateResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new TestCertificateSapResponse(new BaseResponseIdoc()) { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}