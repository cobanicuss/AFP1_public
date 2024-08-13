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
    public class PlannedOrderFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<PlannedOrderFromTriggerHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new PlannedOrderFromTriggerHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Planned-Order from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendPlannedOrderAuditCommandMessage())
                    .And(_ => HandlerMustSendPlannedOrderRequest())

                .BDDfy();
        }

        private void HandlerMustSendPlannedOrderAuditCommandMessage()
        {
            _handler.ExpectSend<PlannedOrderAuditCommand>(command => command.InboundId == Constants.InboundId);
        }

        private void HandlerMustSendPlannedOrderRequest()
        {
            _handler.ExpectSend<PlannedOrderRequestCommand>(command => command.InboundId == Constants.InboundId)
                .OnMessage(new PlannedOrderTriggerRequest { InboundId = Constants.InboundId });
        }
    }
}