using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Handlers;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class TestCertificateInboundRequestCommandHandlerTest
    {
        private Mock<IDoTestCertificateBusiness> _business;
        private NServiceBus.Testing.Handler<TestCertificateInboundRequestCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _business = new Mock<IDoTestCertificateBusiness>();
            _business.Setup(x => x.CreateInboundTestCertificate(It.IsAny<TestCertificateRequestCommand>()));

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new TestCertificateInboundRequestCommandHandler(bus, _business.Object));
        }

        [Test]
        public void InboundTestCertificateRequestCommandRecievedFromSap()
        {
            this.Given("Test-Certificate-Inbound-Request from SAP")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => TestCertificateMustBeCreated())

                .BDDfy();
        }

        private void TestCertificateMustBeCreated()
        {
            _business.Verify(x => x.CreateInboundTestCertificate(It.IsAny<TestCertificateRequestCommand>()), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<TestCertificateRequestAuditCommand>(command =>
                    command.InboundId == Constants.InboundId
                    && command.CertificateId == Constants.CertificateId
                    && Math.Abs(command.Leg - 2.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(TestCertificateRequestCommand).FullName
                    && command.Action == (int)AuditAction.RequestReceivedFromServiceForSoap
                    && !string.IsNullOrEmpty(command.MessageData)
                    )
                .OnMessage(new TestCertificateRequestCommand
                {
                    InboundId = Constants.InboundId,
                    CertificateId = Constants.CertificateId
                });
        }
    }
}