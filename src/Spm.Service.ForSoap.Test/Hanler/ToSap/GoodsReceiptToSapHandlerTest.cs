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
    public class GoodsReceiptToSapHandlerTest
    {
        private Mock<ISendGoodsReceiptToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendGoodsReceiptToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<GoodsReceiptSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Goods-Receipt to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendGoodsReceiptAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendGoodsReceiptAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new GoodsReceiptToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<GoodsReceiptAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new GoodsReceiptSapCommand { SagaReferenceId = Constants.SagaReferenceId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<GoodsReceiptSapCommand>()), Times.Once());
        }
    }
}