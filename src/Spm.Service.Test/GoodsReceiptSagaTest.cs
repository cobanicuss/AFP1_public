using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.CreateMessage;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Service.Sagas;
using Spm.Service.SagaTransitions;
using Spm.Service.Serialization;
using Spm.Shared;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.Service.Test
{
    [TestFixture]
    public class GoodsReceiptSagaTest
    {
        private NServiceBus.Testing.Saga<GoodsReceiptSaga> _sagaUnderTest;
        private Mock<ICreateMessage> _createMessage;
        private Mock<IGoodsTransitions> _transition;
        private Mock<ISerializeMessage> _serializer;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _createMessage = new Mock<ICreateMessage>();

            _sagaUnderTest = NServiceBus.Testing.Test.Saga<GoodsReceiptSaga>();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenStarted()
        {
            this.Given("Goods-Receipt saga")
                .When(_ => SagaHandlesGoodsReceiptCommand())
                .Then(_ => HandlerMustSendGoodsReceiptSapCommandMessage())
                    .And(_ => HandlerMustRequestTimeout())
                    .And(_ => HandlerMustCallMethodForCreateMessageForSapCommand())
                    .And(_ => HandlerMustCallMethodForCreateGoodsReceiptAuditCommandOnce())
                    .And(_ => HandlerMustCallMethodForSerialize())
                    .And(_ => HandlerMustCallMethodForTransitionStart())
                    .And(_ => SagaDataMustBeSetUpCorrectly())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenResponseIsReceived()
        {
            this.Given("Goods-Receipt saga")
                .When(_ => SagaHandlesGoodsReceiptResponseCommand())
                .Then(_ => HandlerIsWorkingWithGoodsReceiptResponseCommand())
                    .And(_ => SagaMustComplete())
                    .And(_ => HandlerMustCallMethodForCreateGoodsReceiptAuditCommand())
                    .And(_ => HandlerMustCallTransitionEnd())
                    .And(_ => HandlerMustCallDeleteSerialization())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursWithinRetryLimit()
        {
            this.Given("Goods-Receipt saga")
                .When(_ => SagaHandlesGoodsReceiptCommand1())
                .Then(_ => HandlerMustSendGoodsReceiptSapCommandMessage())
                    .And(_ => HandlerMustRequestTimeoutForNormalOperations())
                    .And(_ => HandlerMustCallMethodForCreateMessageForSapCommand())
                    .And(_ => HandlerMustCallCreateMessageForAuditCommandTwice())
                    .And(_ => HandlerMustCallMethodForSerialize())
                    .And(_ => HandlerMustCallDeSerialize())
                    .And(_ => HandlerMustCallMethodForTransitionStart())
                    .And(_ => HandlerMustCallTransitionNoResponse())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursAndRetryLimitWasExceeded()
        {
            this.Given("Goods-Receipt saga")
                .When(_ => SagaHandlesGoodsReceiptCommand())
                .Then(_ => HandlerIsWorkingWithTimeOutRetryExeeded())
                    .And(_ => HandlerMustCallMethodForCreateMessageForSapCommand())
                    .And(_ => HandlerMustCallCreateMessageForAuditCommandTwice())
                    .And(_ => HandlerMustCallMethodForSerialize())
                    .And(_ => HandlerMustCallMethodForTransitionStart())

                .BDDfy();
        }

        private void SagaHandlesGoodsReceiptResponseCommand()
        {
            MethodCallForAuditCommand((int)AuditAction.ResponseReceivedFromSap);
            MethodCallForAuditCommand((int)AuditAction.ResponseReceivedFromServiceForSoap);

            _serializer = new Mock<ISerializeMessage>();
            _serializer.Setup(x => x.DeleteSerialization(It.IsAny<Guid>()));

            _transition = new Mock<IGoodsTransitions>();
            _transition.Setup(x => x.End(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
            });
        }

        private void HandlerMustCallDeleteSerialization()
        {
            _serializer.Verify(x => x.DeleteSerialization(It.IsAny<Guid>()), Times.Once());
        }

        private void HandlerMustCallTransitionEnd()
        {
            _transition.Verify(x => x.End(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void HandlerMustRequestTimeoutForNormalOperations()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<GoodsReceiptNoResponse>(
                (state, span) => span == TimeSpan.FromMinutes(Service.Constants.GoodsReceiptTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetGoodsReceiptCommand());
                    x.Data.SagaRetry = 2;
                })
                .ExpectSend<GoodsReceiptSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<GoodsReceiptAuditCommand>(command => command.Action == (int)AuditAction.SagaReTryToServiceForSoap)
                .WhenSagaTimesOut();
        }

        private void HandlerMustRequestTimeout()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<GoodsReceiptNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.GoodsReceiptTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetGoodsReceiptCommand());
                });
        }

        private void SagaHandlesGoodsReceiptCommand1()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaSendToServiceForSoap);
            MethodCallForAuditCommand((int)AuditAction.SagaReTryToServiceForSoap);

            MethodCallForSapCommand();

            _serializer = new Mock<ISerializeMessage>();
            _serializer.Setup(x => x.Serialize(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()));
            _serializer.Setup(x => x.DeSerialize<GoodsPayload>(It.IsAny<Guid>()));

            _transition = new Mock<IGoodsTransitions>();
            _transition.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _transition.Setup(x => x.NoResponse(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
            });
        }

        private void SagaHandlesGoodsReceiptCommand()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaSendToServiceForSoap);
            MethodCallForAuditCommand((int)AuditAction.SagaRetryLimitReached);

            MethodCallForSapCommand();

            _serializer = new Mock<ISerializeMessage>();
            _serializer.Setup(x => x.Serialize(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

            _transition = new Mock<IGoodsTransitions>();
            _transition.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
            });
        }

        private void HandlerMustCallMethodForCreateGoodsReceiptAuditCommandOnce()
        {
            _createMessage.Verify(x => x.GoodsReceiptAuditCommand(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void HandlerMustCallMethodForCreateGoodsReceiptAuditCommand()
        {
            _createMessage.Verify(x => x.GoodsReceiptAuditCommand(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<string>()),
                Times.Exactly(2));
        }

        private void HandlerMustCallMethodForCreateMessageForSapCommand()
        {
            _createMessage.Verify(x => x.GoodsReceiptSapCommand(It.IsAny<GoodsCommand>()), Times.Once());
        }

        private void HandlerMustCallDeSerialize()
        {
            _serializer.Verify(x => x.DeSerialize<GoodsPayload>(It.IsAny<Guid>()), Times.Once());
        }

        private void HandlerMustCallMethodForSerialize()
        {
            _serializer.Verify(x => x.Serialize(It.IsAny<GoodsPayload>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        private void HandlerMustCallMethodForTransitionStart()
        {
            _transition.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        private void HandlerMustCallTransitionNoResponse()
        {
            _transition.Verify(x => x.NoResponse(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void HandlerMustSendGoodsReceiptSapCommandMessage()
        {
            _sagaUnderTest
                .ExpectSend<GoodsReceiptSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<GoodsReceiptAuditCommand>(command => command.Action == (int) AuditAction.SagaSendToServiceForSoap);
        }

        private void HandlerIsWorkingWithTimeOutRetryExeeded()
        {
            _sagaUnderTest
                .ExpectSend<GoodsReceiptSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<GoodsReceiptAuditCommand>(command => command.Action == (int)AuditAction.SagaSendToServiceForSoap)
                .When(x =>
                {
                    x.Handle(GetGoodsReceiptCommand());
                    x.Data.SagaRetry = 1;
                })
                .ExpectSend<GoodsReceiptAuditCommand>(command=> command.Action == (int)AuditAction.SagaRetryLimitReached)
                .WhenSagaTimesOut();
        }

        private void HandlerIsWorkingWithGoodsReceiptResponseCommand()
        {
            _sagaUnderTest
                .ExpectSend<GoodsReceiptAuditCommand>(command=> command.Action == (int)AuditAction.ResponseReceivedFromSap)
                .ExpectSend<GoodsReceiptAuditCommand>(command => command.Action == (int)AuditAction.ResponseReceivedFromServiceForSoap)
                .When(x =>
                {
                    x.Data.Type = Shared.Constants.GoodsReceiptType;

                    x.Handle(new GoodsReceiptResponseCommand
                    {
                        SagaReferenceId = Constants.SagaReferenceId
                    });
                });
        }

        private void HandlerMustCallCreateMessageForAuditCommandTwice()
        {
            _createMessage.Verify(x => x.GoodsReceiptAuditCommand(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<string>()),
                Times.Exactly(2));
        }

        private static GoodsCommand GetGoodsReceiptCommand()
        {
            return new GoodsCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                GoodsReceiptId = Constants.GoodsReceiptId
            };
        }

        private void SagaDataMustBeSetUpCorrectly()
        {
            _sagaUnderTest.When(x =>
            {
                Assert.AreEqual(x.Data.SagaReferenceId, Constants.SagaReferenceId);
                Assert.AreEqual(x.Data.GoodsReceiptId, Constants.GoodsReceiptId);
                Assert.AreEqual(string.IsNullOrEmpty(x.Data.SerializedMessageId.ToString()), false);
                Assert.AreEqual(x.Data.SagaState, SagaStates.Started.ToString());
                Assert.AreEqual(x.Data.SagaRetry, Service.Constants.GoodsReceiptRetry);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Date, DateTime.Now.Date);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Hour, DateTime.Now.Hour);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Minute, DateTime.Now.Minute);
            });
        }

        private void SagaMustComplete()
        {
            _sagaUnderTest.AssertSagaCompletionIs(true);
        }

        private void MethodCallForAuditCommand(int action)
        {
            _createMessage.Setup(x => x.GoodsReceiptAuditCommand(
            It.Is<int>(a => a == action),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<float>(),
            It.IsAny<string>()))
            .Returns(
            new GoodsReceiptAuditCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                GoodsReceiptId = Constants.GoodsReceiptId,
                Action = action,
                Type = Shared.Constants.GoodsReceiptType
            });
        }

        private void MethodCallForSapCommand()
        {
            _createMessage.Setup(x => x.GoodsReceiptSapCommand(It.IsAny<GoodsCommand>())).Returns(
            new GoodsReceiptSapCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                GoodsReceiptId = Constants.GeneralLedgerId
            });
        }
    }
}