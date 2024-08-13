using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.ToSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.ToSap
{
    [TestFixture]
    public class TestCertificateToSapHandlerTest
    {
        private Mock<ISendTestCertificateToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendTestCertificateToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<TestCertificateSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Test-Certificate to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendTestCertificateAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendTestCertificateAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new TestCertificateToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<TestCertificateAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new TestCertificateSapCommand
                {
                    Inboundid = Constants.InboundId,
                    SagaReferenceId = Constants.SagaReferenceId,
                    MessageIndex = Constants.MessageIndex,
                    MessageCount = Constants.MessageIndex,
                    Payload = new TestCertificateOutboundPayload { CertificateNumber = Constants.CertificateId }
                });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<TestCertificateSapCommand>()), Times.Once());
        }
    }
}