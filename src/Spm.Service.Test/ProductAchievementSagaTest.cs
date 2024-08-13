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
using Spm.Service.Validation;
using Spm.Shared;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.Service.Test
{
    [TestFixture]
    public class ProductAchievementSagaTest
    {
        private NServiceBus.Testing.Saga<ProductAchievementSaga> _sagaUnderTest;
        private Mock<ICreateMessage> _createMessage;
        private Mock<IProductAchievementTransitions> _transition;
        private Mock<ISerializeMessage> _serializer;
        private Mock<IValidateProductAchievement> _validate;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _createMessage = new Mock<ICreateMessage>();
            _serializer = new Mock<ISerializeMessage>();
            _transition = new Mock<IProductAchievementTransitions>();
            _validate = new Mock<IValidateProductAchievement>();

            _sagaUnderTest = NServiceBus.Testing.Test.Saga<ProductAchievementSaga>();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenStarted()
        {
            this.Given("Product-Achievement saga")
                .When(_ => SagaDoesStartSequence())
                .Then(_ => CommandMessagesForStartSequenceMustBeSend())
                    .And(_ => TimeoutMustBeRequested())
                    .And(_ => MustCallMethodForCreateMessageForSapCommand())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => SagaDataMustBeSetUpCorrectly())

                .BDDfy();
        }

        [Test]
        public void SagaMustClearIfLotNumberHasAlreadyBeenProcessed()
        {
            this.Given("Product-Achievement saga")
                .When(_ => SagaDoesStartWhenLotNumberExist())
                    .And(_ => SagaStartAsNormal())
                .Then(_ => SagaMustComplete())
                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicOnResponseIsReceived()
        {
            this.Given("Product-Achievement saga")
                .When(_ => SagaResponseSequence())
                .Then(_ => ResponseCommandMessagesMustBeSend())
                    .And(_ => SagaMustComplete())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallTransitionEnd())
                    .And(_ => MustCallDeleteSerialization())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursWithinRetryLimit()
        {
            this.Given("Product-Achievement saga")
                .When(_ => SagaDealsWithTimeOut())
                .Then(_ => CommandMessagesForStartSequenceMustBeSend())
                    .And(_ => HandlerMustRequestTimeoutForNormalOperations())
                    .And(_ => MustCallMethodForCreateMessageForSapCommand())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallDeSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => MustCallTransitionNoResponse())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursAndRetryLimitWasExceeded()
        {
            this.Given("Product-Achievement saga")
                .When(_ => SagaDealsWithTimeOutAndRetryLimitIsReached())
                .Then(_ => HandlerIsWorkingWithTimeOutRetryExeeded())
                    .And(_ => MustCallMethodForCreateMessageForSapCommand())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())

                .BDDfy();
        }

        private void SagaDoesStartSequence()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaSendToServiceForSoap);

            MethodCallForSapCommand();

            _validate.Setup(x => x.HasPreviouselyBeenCreated(It.IsAny<string>())).Returns(false);

            _serializer.Setup(x => x.Serialize(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

            _transition.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
                x.Validate = _validate.Object;
            });
        }

        private void SagaDoesStartWhenLotNumberExist()
        {
            _validate.Setup(x => x.HasPreviouselyBeenCreated(It.IsAny<string>())).Returns(true);

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
                x.Validate = _validate.Object;
            });
        }

        private void SagaResponseSequence()
        {
            MethodCallForAuditCommand((int)AuditAction.ResponseReceivedFromServiceForSoap);

            _serializer.Setup(x => x.DeleteSerialization(It.IsAny<Guid>()));

            _transition.Setup(x => x.End(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
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

        private void SagaDealsWithTimeOut()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaReTryToServiceForSoap);

            _serializer.Setup(x => x.DeSerialize<InventoryMovementPayload>(It.IsAny<Guid>()));

            _transition.Setup(x => x.NoResponse(
              It.IsAny<string>(),
              It.IsAny<Guid>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>()));

            SagaDoesStartSequence();
        }

        private void SagaDealsWithTimeOutAndRetryLimitIsReached()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaRetryLimitReached);

            SagaDoesStartSequence();
        }

        private void HandlerMustRequestTimeoutForNormalOperations()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<ProductAchievementNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.ProductAchievementTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessage());
                    x.Data.SagaRetry = 2;
                })
                .ExpectSend<ProductAchievementSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<ProductAchievementAuditCommand>(command => command.Action == (int)AuditAction.SagaReTryToServiceForSoap)
                .WhenSagaTimesOut();
        }

        private void SagaStartAsNormal()
        {
            _sagaUnderTest
                .When(x =>
                {
                    x.Handle(GetCommandMessage());
                });
        }

        private void TimeoutMustBeRequested()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<ProductAchievementNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.ProductAchievementTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessage());
                });
        }

        private void CommandMessagesForStartSequenceMustBeSend()
        {
            _sagaUnderTest
                .ExpectSend<ProductAchievementSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<ProductAchievementAuditCommand>(command => command.Action == (int)AuditAction.SagaSendToServiceForSoap);
        }

        private void HandlerIsWorkingWithTimeOutRetryExeeded()
        {
            CommandMessagesForStartSequenceMustBeSend();

            _sagaUnderTest
                .When(x =>
                {
                    x.Handle(GetCommandMessage());
                    x.Data.SagaRetry = 1;
                })
                .ExpectSend<ProductAchievementAuditCommand>(command => command.Action == (int)AuditAction.SagaRetryLimitReached)
                .WhenSagaTimesOut();
        }

        private void ResponseCommandMessagesMustBeSend()
        {
            _sagaUnderTest.ExpectSend<ProductAchievementAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .When(x => { x.Handle(new ProductAchievementResponseCommand()); });
        }

        private static ProductAchievementCommand GetCommandMessage()
        {
            return new ProductAchievementCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                LotNumber = Constants.LotNumber
            };
        }

        private void SagaMustComplete()
        {
            _sagaUnderTest.AssertSagaCompletionIs(true);
        }

        private void MethodCallForAuditCommand(int action)
        {
            _createMessage.Setup(x => x.ProductAchievementAuditCommand(
                It.Is<int>(a => a == action),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>()))
                .Returns(
                new ProductAchievementAuditCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    LotNumber = Constants.LotNumber,
                    Action = action
                });
        }

        private void MethodCallForSapCommand()
        {
            _createMessage.Setup(x => x.ProductAchievementSapCommand(It.IsAny<ProductAchievementCommand>())).Returns(
                new ProductAchievementSapCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    LotNumber = Constants.LotNumber
                });
        }

        private void MustCallDeleteSerialization()
        {
            _serializer.Verify(x => x.DeleteSerialization(It.IsAny<Guid>()), Times.Once());
        }

        private void MustCallTransitionEnd()
        {
            _transition.Verify(x => x.End(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void MustCallCreateMessageForAuditCommandTwice()
        {
            _createMessage.Verify(x => x.ProductAchievementAuditCommand(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>()),
                Times.Exactly(2));
        }

        private void MustCallMethodForAuditCommandOnce()
        {
            _createMessage.Verify(x => x.ProductAchievementAuditCommand(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>()),
                Times.Once());
        }

        private void MustCallMethodForCreateMessageForSapCommand()
        {
            _createMessage.Verify(x => x.ProductAchievementSapCommand(It.IsAny<ProductAchievementCommand>()), Times.Once());
        }

        private void MustCallDeSerialize()
        {
            _serializer.Verify(x => x.DeSerialize<InventoryMovementPayload>(It.IsAny<Guid>()), Times.Once());
        }

        private void MustCallMethodForSerialize()
        {
            _serializer.Verify(x => x.Serialize(It.IsAny<InventoryMovementPayload>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        private void MustCallMethodForTransitionStart()
        {
            _transition.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        private void MustCallTransitionNoResponse()
        {
            _transition.Verify(x => x.NoResponse(
                It.IsAny<string>(),
                It.IsAny<Guid>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void SagaDataMustBeSetUpCorrectly()
        {
            _sagaUnderTest.When(x =>
            {
                Assert.AreEqual(x.Data.SagaReferenceId, Constants.SagaReferenceId);
                Assert.AreEqual(x.Data.LotNumber, Constants.LotNumber);
                Assert.AreEqual(string.IsNullOrEmpty(x.Data.SerializedMessageId.ToString()), false);
                Assert.AreEqual(x.Data.SagaState, SagaStates.Started.ToString());
                Assert.AreEqual(x.Data.SagaRetry, Service.Constants.ProductAchievementRetry);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Date, DateTime.Now.Date);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Hour, DateTime.Now.Hour);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Minute, DateTime.Now.Minute);
            });
        }
    }
}