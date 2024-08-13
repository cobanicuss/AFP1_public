using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.ToSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.ToSap
{
    [TestFixture]
    public class GeneralLedgerToSapHandlerTest
    {
        private Mock<ISendGeneralLedgerToSap> _toSapMock;
        
        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendGeneralLedgerToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<GeneralLedgerSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("General-Ledger to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendGeneralLedgerAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }
        
        private void HandlerMustSendGeneralLedgerAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new GeneralLedgerToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<GeneralLedgerAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new GeneralLedgerSapCommand { SagaReferenceId = Constants.SagaReferenceId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<GeneralLedgerSapCommand>()), Times.Once());
        }
    }
}