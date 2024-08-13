using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Handlers;
using Spm.OrrSys.Service.Map;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class TestCertificateOutboundCommandHandlerTest
    {
        private Mock<IDoTestCertificateBusiness> _business;
        private Mock<IMapTestCertificateMessage> _map;
        private NServiceBus.Testing.Handler<TestCertificateOutboundTriggerCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _business = new Mock<IDoTestCertificateBusiness>();

            _business.Setup(x => x.GetOutboundTestCertificateData())
                .Returns(new List<OutboundTestCertificateDto>
                {
                    new OutboundTestCertificateDto()
                });

            _business.Setup(x => x.GetByUniqueReportGroup(
                It.IsAny<List<OutboundTestCertificateDto>>()))
                .Returns(new List<int> { 1 });

            _business.Setup(x => x.DeletePreviousTestCertificates(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<int>()));

            _map = new Mock<IMapTestCertificateMessage>();

            _map.Setup(x => x.CreateTestCertificateFileCommandMessage(
                It.IsAny<IList<OutboundTestCertificateDto>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .Returns(new TestCertificateFileRequestCommand
                {
                    InboundId = Constants.InboundId
                });

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new TestCertificateOutboundTriggerCommandHandler(bus, _business.Object, _map.Object));
        }

        [Test]
        public void OutboundTestCertificateCommandNeedsToBeSendToSap()
        {
            this.Given("Outbound Test-Certificate-Command request received from SSIS")
                .When("Handler is called")
                .Then(_ => TestCertificateCommandMustBeSend())
                    .And(_ => AuditCommandMustBeSend())
                    .And(_ => BusinessRulesMustBeImplemented())

                .BDDfy();
        }

        private void TestCertificateCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSendLocal<TestCertificateFileRequestCommand>(command =>
                    command.InboundId == Constants.InboundId
                );
        }

        private void BusinessRulesMustBeImplemented()
        {
            _business.Verify(x => x.GetOutboundTestCertificateData(), Times.Once());

            _business.Verify(x => x.GetByUniqueReportGroup(It.IsAny<List<OutboundTestCertificateDto>>()), Times.Once());

            _business.Verify(x => x.DeletePreviousTestCertificates(
                It.IsAny<bool>(),
                It.IsAny<string>(),
                It.IsAny<int>()),
                Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<TestCertificateAuditCommand>(command =>
                    command.SagaReferenceId == Shared.Constants.NotAvailable
                    && command.CertificateId == Shared.Constants.NotAvailable
                    && command.InboundId == Constants.InboundId
                    )
                .OnMessage(new TestCertificateTriggerCommand
                {
                    InboundId = Constants.InboundId
                });
        }
    }
}