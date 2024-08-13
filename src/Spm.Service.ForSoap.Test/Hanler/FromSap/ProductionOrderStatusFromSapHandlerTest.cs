using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class ProductionOrderStatusFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<ProductionOrderStatusFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new ProductionOrderStatusFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Production-Order-Status from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendProductAchievementAuditCommandMessage())
                    .And(_ => HandlerMustSendProductAchievementResponseCommandSendToSpmService())

                .BDDfy();
        }

        private void HandlerMustSendProductAchievementAuditCommandMessage()
        {
            _handler.ExpectSend<ProductionOrderStatusAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendProductAchievementResponseCommandSendToSpmService()
        {
            _handler.ExpectSend<ProductionOrderStatusResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new ProductionOrderStatusSapResponse(new BaseResponseIdoc()) { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}