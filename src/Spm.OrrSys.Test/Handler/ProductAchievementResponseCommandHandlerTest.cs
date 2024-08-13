using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Handlers;
using Spm.OrrSys.Service.Soap;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class ProductAchievementResponseCommandHandlerTest
    {
        private Mock<IDoProductAchievementCommunication> _soap;
        private NServiceBus.Testing.Handler<ProductAchievementResponseCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _soap = new Mock<IDoProductAchievementCommunication>();
            _soap.Setup(x => x.SendSoapMessageForProductAchievement(It.IsAny<string>()));

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new ProductAchievementResponseCommandHandler(bus, _soap.Object));
        }

        [Test]
        public void ResponseCommandReceivedFromSapAfterOutboundProductAchievementCommandWasSend()
        {
            this.Given("Product-Achievement-Response from SAP")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void SoapMessageMustBeSend()
        {
            _soap.Verify(x => x.SendSoapMessageForProductAchievement(It.IsAny<string>()), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<ProductAchievementAuditCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.LotNumber == Constants.LotNumber
                    && Math.Abs(command.Leg - 6.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(ProductAchievementResponseCommand).FullName
                    && command.Action == (int)AuditAction.MessageImplemented
                    && !string.IsNullOrEmpty(command.MessageData)
                    )
                .OnMessage(new ProductAchievementResponseCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    LotNumber = Constants.LotNumber
                });
        }
    }
}