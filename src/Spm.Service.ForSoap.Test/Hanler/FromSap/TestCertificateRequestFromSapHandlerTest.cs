using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class TestCertificateRequestFromSapHandlerTest
    {
        private Mock<ISendResponseOnRequestToSap> _toSapMock;
        private NServiceBus.Testing.Handler<TestCertificateRequestFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendResponseOnRequestToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<ResponseToSapRequestCommand>()));

            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new TestCertificateRequestFromSapHandler(bus, _toSapMock.Object));
        }

        [Test]
        public void HandlerMustSendAuditlogTwice()
        {
            this.Given("Test-Certificate request from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendTestCertificateRequestAuditCommandMessage())

                .BDDfy();
        }

        [Test]
        public void HandlerMustImplementThisLogic()
        {
            this.Given("TestCertificateRequestFromSapHandler")
                .When("Handler is called")
                .Then(_ => RequestCommandMustBeSendToOrrSysService())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendTestCertificateRequestAuditCommandMessage()
        {
            _handler.ExpectSend<TestCertificateRequestAuditCommand>(command => command.Action == (int)AuditAction.RequestReceived)
                .ExpectSend<TestCertificateRequestAuditCommand>(command=> command.Action == (int)AuditAction.SendResponseToSap)
                .OnMessage(new TestCertificateSapRequest { CertificateId = Constants.CertificateId });
        }

        private void RequestCommandMustBeSendToOrrSysService()
        {
            _handler.ExpectSend<TestCertificateRequestCommand>(command => command.CertificateId == Constants.CertificateId)
                .OnMessage(new TestCertificateSapRequest { CertificateId = Constants.CertificateId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<ResponseToSapRequestCommand>()), Times.Once());
        }
    }
}