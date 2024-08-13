using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Handlers;
using Spm.Shared;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class ProductionOrderStatusCommandHandlerTest
    {
        private NServiceBus.Testing.Handler<ProductionOrderStatusCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new ProductionOrderStatusCommandHandler(bus));
        }

        [Test]
        public void MessageReceivedFromOrrSysOrSpmOrrSysServicesScheduler()
        {
            this.Given("Production-Order-Status-Command from OrrSys or SpmOrrSysService Scheduler")
                .When("Handler is called and message contains data")
                .Then(_ => MessageMustBeSendToAuditCommand())
                    .And(_ => MessageMustBeSendToSaga())

                .BDDfy();
        }

        [Test]
        public void HandlerMustNotSendAnyMessagesToSagaIfMessageConstainsNoData()
        {
            this.Given("Production-Order-Status-Command from OrrSys or SpmOrrSysService Scheduler")
                .When("Handler is called with message containing no data")
                .Then(_ => NoMessageSendForAuditCommand())
                    .And(_ => NoMessageSendToSaga())

                .BDDfy();
        }

        private void MessageMustBeSendToAuditCommand()
        {
            _handlerUnderTest
                .ExpectSend<ProductionOrderStatusAuditCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.ProductionOrderId == Constants.ProductionOrderId
                    && Math.Abs(command.Leg - 1.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmOrrSysService
                    && command.MessageType == typeof(ProductionOrderStatusCommand).FullName
                    && command.Action == (int)AuditAction.SendToSaga
                    && !string.IsNullOrEmpty(command.MessageData)
                );
        }

        private void MessageMustBeSendToSaga()
        {
            _handlerUnderTest
                .ExpectSend<Spm.Service.Messages.ProductionOrderStatusCommand>(command =>
                    command.SagaReferenceId == Constants.SagaReferenceId
                    && command.ProductionOrderId == Constants.ProductionOrderId
                    && command.Payload != null
                )
                .OnMessage(new ProductionOrderStatusCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    ProductionOrderId = Constants.ProductionOrderId,
                    Payload = new ProductionOrderStatusPayload
                    {
                        ProductionOrderStatusPayloadItem = new List<ProductionOrderStatusPayloadItem>
                        {
                            new ProductionOrderStatusPayloadItem()
                        }
                    }
                });
        }

        private void NoMessageSendForAuditCommand()
        {
            _handlerUnderTest
                .ExpectNotSend<Spm.Service.Messages.ProductionOrderStatusCommand>(
                    x => x.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void NoMessageSendToSaga()
        {
            _handlerUnderTest
                .ExpectNotSend<ProductionOrderStatusAuditCommand>(x => x.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new ProductionOrderStatusCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    ProductionOrderId = Constants.ProductionOrderId,
                    Payload = null
                });
        }
    }
}