using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class PurchaseOrderCreateFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<PurchaseOrderCreateFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new PurchaseOrderCreateFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Purchase-Order-Create from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendPurchaseOrderAuditCommandMessage())
                    .And(_ => HandlerMustSendPurchaseOrderCreateResponseCommand())

                .BDDfy();
        }

        private void HandlerMustSendPurchaseOrderAuditCommandMessage()
        {
            _handler.ExpectSend<PurchaseOrderAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendPurchaseOrderCreateResponseCommand()
        {
            _handler.ExpectSend<PurchaseOrderCreateResponseCommand>(command=> command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new PurchaseOrderCreateSapResponse { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}