using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Handlers.InboundTrigger;
using Spm.Service.ForSoap.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class ProductionOrderFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<ProductionOrderFromTriggerHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new ProductionOrderFromTriggerHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Production-Order from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendProductionOrderAuditCommandMessage())
                    .And(_ => HandlerMustSendProductionOrderRequestCommand())

                .BDDfy();
        }

        private void HandlerMustSendProductionOrderAuditCommandMessage()
        {
            _handler.ExpectSend<ProductionOrderAuditCommand>(command => command.InboundId == Constants.InboundId);
        }

        private void HandlerMustSendProductionOrderRequestCommand()
        {
            _handler.ExpectSend<ProductionOrderRequestCommand>(command => command.InboundId == Constants.InboundId)
                .OnMessage(new ProductionOrderTriggerRequest { InboundId = Constants.InboundId });
        }
    }
}