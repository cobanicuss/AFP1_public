using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Handlers;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class ProductionOrderRequestCommandHandlerTest
    {
        private Mock<IDoProductionOrderBusiness> _business;
        private NServiceBus.Testing.Handler<ProductionOrderRequestCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _business = new Mock<IDoProductionOrderBusiness>();
            _business.Setup(x => x.CreateProductionOrders());

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new ProductionOrderRequestCommandHandler(bus, _business.Object));
        }

        [Test]
        public void InboundProductionOrderRequestCommandWasSendBySsis()
        {
            this.Given("Production-Order-Request from SSIS")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => PlannedOrdersMustBeCreated())

                .BDDfy();
        }

        private void PlannedOrdersMustBeCreated()
        {
            _business.Verify(x => x.CreateProductionOrders(), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<ProductionOrderAuditCommand>(command =>
                    command.InboundId == Constants.InboundId
                    && Math.Abs(command.Leg - 2.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(ProductionOrderRequestCommand).FullName
                    && command.Action == (int)AuditAction.RequestReceivedFromServiceForSoap
                    && command.MessageData == Shared.Constants.NotAvailable
                    )
                .OnMessage(new ProductionOrderRequestCommand
                {
                    InboundId = Constants.InboundId
                });
        }
    }
}