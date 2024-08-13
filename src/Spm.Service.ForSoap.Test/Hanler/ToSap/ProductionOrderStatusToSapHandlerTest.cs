using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.ToSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.ToSap
{
    [TestFixture]
    public class ProductionOrderStatusToSapHandlerTest
    {
        private Mock<ISendProductionOrderStatusToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendProductionOrderStatusToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<ProductionOrderStatusSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Production-OrderStatus to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendProductionOrderStatusAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendProductionOrderStatusAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new ProductionOrderStatusToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<ProductionOrderStatusAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new ProductionOrderStatusSapCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    ProductionOrderId = "ab",
                    Payload = new ProductionOrderStatusPayload
                    {
                        ProductionOrderStatusPayloadItem = new List<ProductionOrderStatusPayloadItem>
                        {
                            new ProductionOrderStatusPayloadItem()
                        }
                    }
                });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<ProductionOrderStatusSapCommand>()), Times.Once());
        }
    }
}