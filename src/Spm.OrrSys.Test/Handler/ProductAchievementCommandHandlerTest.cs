using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Handlers;
using Spm.Shared;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class ProductAchievementCommandHandlerTest
    {
        private Mock<IDoProductAchievementBusiness> _soap;
        private NServiceBus.Testing.Handler<ProductAchievementCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _soap = new Mock<IDoProductAchievementBusiness>();
            _soap.Setup(x => x.SetInProgress(It.IsAny<string>()));

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new ProductAchievementCommandHandler(bus, _soap.Object));
        }

        [Test]
        public void OutboundProductAchievementCommandReceived()
        {
            this.Given("Product-Achievement-Command recieved from either OrrSys or ScannerService")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => ProductAchievementCommandMustBeSend())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void SoapMessageMustBeSend()
        {
            _soap.Verify(x => x.SetInProgress(It.IsAny<string>()), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<ProductAchievementAuditCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.LotNumber == Constants.LotNumber
                    && Math.Abs(command.Leg - 1.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(ProductAchievementCommand).FullName
                    && command.Action == (int)AuditAction.SendToSaga
                    && !string.IsNullOrEmpty(command.MessageData)
                    );
        }

        private void ProductAchievementCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<Spm.Service.Messages.ProductAchievementCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.LotNumber == Constants.LotNumber
                    && command.Payload != null
                )
                .OnMessage(new ProductAchievementCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    LotNumber = Constants.LotNumber,
                    Payload = new InventoryMovementPayload()
                });
        }
    }
}