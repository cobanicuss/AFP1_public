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
    public class PurchaseOrderChangeToSapHandlerTest
    {
        private Mock<ISendPurchaseOrderChangeToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendPurchaseOrderChangeToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<PurchaseOrderChangeSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Purchase-Order-Change to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendPurchaseOrderChangeSapCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendPurchaseOrderChangeSapCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new PurchaseOrderChangeToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<PurchaseOrderAuditCommand>(command=> command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new PurchaseOrderChangeSapCommand { SagaReferenceId = Constants.SagaReferenceId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<PurchaseOrderChangeSapCommand>()), Times.Once());
        }
    }
}
