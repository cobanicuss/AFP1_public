using NUnit.Framework;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class GoodsReceiptFromSapHandlerTest
    {
        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Goods-Receipt from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendGeneralLedgerAuditCommandMessage())

                .BDDfy();
        }

        private void HandlerMustSendGeneralLedgerAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new GoodsReceiptFromSapHandler(bus))
                .ExpectSend<GoodsReceiptResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new GoodsReceiptSapResponse { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}