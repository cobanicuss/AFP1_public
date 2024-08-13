using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class ProductAchievementFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<ProductAchievementFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new ProductAchievementFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Product-Achievement from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendProductAchievementAuditCommandMessage())
                    .And(_ => HandlerMustSendProductAchievementResponseCommandSendToSpmService())

                .BDDfy();
        }

        private void HandlerMustSendProductAchievementAuditCommandMessage()
        {
            _handler.ExpectSend<ProductAchievementAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendProductAchievementResponseCommandSendToSpmService()
        {
            _handler.ExpectSend<ProductAchievementResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new ProductAchievementSapResponse(new BaseResponseIdoc()) { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}