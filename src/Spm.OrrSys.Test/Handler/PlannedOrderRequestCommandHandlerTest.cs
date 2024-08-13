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
    public class PlannedOrderRequestCommandHandlerTest
    {
        private Mock<IDoPlannedOrderBusiness> _business;
        private NServiceBus.Testing.Handler<PlannedOrderRequestCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _business = new Mock<IDoPlannedOrderBusiness>();
            _business.Setup(x => x.CreatePlannedOrders());

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new PlannedOrderRequestCommandHandler(bus, _business.Object));
        }

        [Test]
        public void InboundPlannedOrderRequestCommandWasSendBySsis()
        {
            this.Given("Planned-Order-Request from SSIS")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => PlannedOrdersMustBeCreated())

                .BDDfy();
        }

        private void PlannedOrdersMustBeCreated()
        {
            _business.Verify(x => x.CreatePlannedOrders(), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<PlannedOrderAuditCommand>(command =>
                    command.InboundId == Constants.InboundId
                    && Math.Abs(command.Leg - 2.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(PlannedOrderRequestCommand).FullName
                    && command.Action == (int)AuditAction.RequestReceivedFromServiceForSoap
                    && command.MessageData == Shared.Constants.NotAvailable
                    )
                .OnMessage(new PlannedOrderRequestCommand
                {
                    InboundId = Constants.InboundId
                });
        }
    }
}