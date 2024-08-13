using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Handlers;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class TestCertificateOutboundResponseCommandHandlerTest
    {
        private Mock<IDoTestCertificateBusiness> _business;
        private NServiceBus.Testing.Handler<TestCertificateOutboundResponseCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _business = new Mock<IDoTestCertificateBusiness>();
            _business.Setup(x => x.CreateOutboundTestCertificateResponse(It.IsAny<string[]>()));
                
            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new TestCertificateOutboundResponseCommandHandler(bus, _business.Object));
        }

        [Test]
        public void ResponseMessageReceivedAfterOutboundTestCertifcateRequestCommandMessageWasSend()
        {
            this.Given("Test-Certificate-Resonse from Sap on outbound request")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => CreateResponseTestCertifcate())

                .BDDfy();
        }

        private void CreateResponseTestCertifcate()
        {
            _business.Verify(x => x.CreateOutboundTestCertificateResponse(It.IsAny<string[]>()), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<TestCertificateAuditCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.CertificateId == Constants.CertificateId
                    )
                .OnMessage(new TestCertificateResponseCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    CertificateId = Constants.CertificateId
                });
        }
    }
}