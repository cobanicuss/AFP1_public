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
    public class ProductAchievementToSapHandlerTest
    {
        private Mock<ISendProductAchievementToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendProductAchievementToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<ProductAchievementSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Product-Achievement to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendProductAchievementAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendProductAchievementAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new ProductAchievementToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<ProductAchievementAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new ProductAchievementSapCommand { SagaReferenceId = Constants.SagaReferenceId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<ProductAchievementSapCommand>()), Times.Once());
        }
    }
}