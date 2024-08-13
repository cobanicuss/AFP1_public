using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class PurchaseOrderChangeFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<PurchaseOrderChangeFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new PurchaseOrderChangeFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Purchase-Order-Change from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustPurchaseOrderAuditCommandMessage())
                    .And(_ => HandlerMustSendPurchaseOrderChangeResponseCommand())

                .BDDfy();
        }

        private void HandlerMustPurchaseOrderAuditCommandMessage()
        {
            _handler.ExpectSend<PurchaseOrderAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendPurchaseOrderChangeResponseCommand()
        {
            _handler.ExpectSend<PurchaseOrderChangeResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new PurchaseOrderChangeSapResponse { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}