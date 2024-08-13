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
    public class PurchaseOrderSagaTest
    {
        private NServiceBus.Testing.Saga<PurchaseOrderSaga> _sagaUnderTest;
        private Mock<ICreateMessage> _createMessage;
        private Mock<IPurchaseOrderTransitions> _transition;
        private Mock<ISerializeMessage> _serializer;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _createMessage = new Mock<ICreateMessage>();
            _serializer = new Mock<ISerializeMessage>();
            _transition = new Mock<IPurchaseOrderTransitions>();

            _sagaUnderTest = NServiceBus.Testing.Test.Saga<PurchaseOrderSaga>();
        }

        [Test]
        public void PurchserOrderCreateSagaMustFollowSpicificLogicWhenStarted()
        {
            this.Given("Purchase-Order-Create saga")
                .When(_ => SagaDoesStartSequenceForCreate())
                .Then(_ => CommandMessagesForStartSequenceForCreateMustBeSend())
                    .And(_ => TimeoutMustBeRequestedForCreate())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForCreate())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => SagaDataMustBeSetUpCorrectly())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicOnResponseReceivedForCreate()
        {
            this.Given("Purchase-Order-Create saga")
                .When(_ => SagaDoesResponseSequence())
                .Then(_ => ResponseCommandMessagesMustBeSendForCreate())
                    .And(_ => SagaMustComplete())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallTransitionEnd())
                    .And(_ => MustCallDeleteSerialization())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursWithinRetryLimitForCreate()
        {
            this.Given("Purchase-Order-Create saga")
                .When(_ => SagaDealsWithTimeOutForCreate())
                .Then(_ => CommandMessagesForStartSequenceForCreateMustBeSend())
                    .And(_ => HandlerMustRequestTimeoutForNormalOperationsCreate())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForCreate())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallDeSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => MustCallTransitionNoResponse())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursAndRetryLimitWasExceededForCreate()
        {
            this.Given("Purchase-Order-Create saga")
                .When(_ => SagaDealsWithTimeOutAndRetryLimitIsReachedForCreate())
                .Then(_ => HandlerIsWorkingWithTimeOutRetryExeededCreate())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForCreate())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())

                .BDDfy();
        }

        [Test]
        public void PurchserOrderChangeSagaMustFollowSpicificLogicWhenStarted()
        {
            this.Given("Purchase-Order-Change saga")
                .When(_ => SagaDoesStartSequenceForChange())
                .Then(_ => CommandMessagesForStartSequenceForChangeMustBeSend())
                    .And(_ => TimeoutMustBeRequestedForChange())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForChange())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => SagaDataMustBeSetUpCorrectly())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicOnResponseReceivedForChange()
        {
            this.Given("Purchase-Order-Change saga")
                .When(_ => SagaDoesResponseSequence())
                .Then(_ => ResponseCommandMessagesMustBeSendForChange())
                    .And(_ => SagaMustComplete())
                    .And(_ => MustCallMethodForAuditCommandOnce())
                    .And(_ => MustCallTransitionEnd())
                    .And(_ => MustCallDeleteSerialization())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursWithinRetryLimitForChange()
        {
            this.Given("Purchase-Order-Change saga")
                .When(_ => SagaDealsWithTimeOutForChange())
                .Then(_ => CommandMessagesForStartSequenceForChangeMustBeSend())
                    .And(_ => HandlerMustRequestTimeoutForNormalOperationsChange())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForChange())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallDeSerialize())
                    .And(_ => MustCallMethodForTransitionStart())
                    .And(_ => MustCallTransitionNoResponse())

                .BDDfy();
        }

        [Test]
        public void SagaMustFollowSpicificLogicWhenTimeOutOccursAndRetryLimitWasExceededForChange()
        {
            this.Given("Purchase-Order-Create saga")
                .When(_ => SagaDealsWithTimeOutAndRetryLimitIsReachedForChange())
                .Then(_ => HandlerIsWorkingWithTimeOutRetryExeededChange())
                    .And(_ => MustCallMethodForCreationOfSapCommandMessageForChange())
                    .And(_ => MustCallCreateMessageForAuditCommandTwice())
                    .And(_ => MustCallMethodForSerialize())
                    .And(_ => MustCallMethodForTransitionStart())

                .BDDfy();
        }

        private void SagaDoesStartSequenceForCreate()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaSendToServiceForSoap);

            MethodCallForSapCommandForPurchaseOrderCreate();

            _serializer.Setup(x => x.Serialize(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

            _transition.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
            });
        }

        private void SagaDoesStartSequenceForChange()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaSendToServiceForSoap);

            MethodCallForSapCommandForPurchaseOrderChange();

            _serializer.Setup(x => x.Serialize(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<Guid>()));

            _transition.Setup(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            _sagaUnderTest.WithExternalDependencies(x =>
            {
                x.CreateMessage = _createMessage.Object;
                x.Serializer = _serializer.Object;
                x.Transition = _transition.Object;
            });
        }

        private void SagaDoesResponseSequence()
        {
            MethodCallForAuditCommand((int)AuditAction.ResponseReceivedFromServiceForSoap);

            _serializer.Setup(x => x.DeleteSerialization(It.IsAny<Guid>()));

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

        private void SagaDealsWithTimeOutForChange()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaReTryToServiceForSoap);

            _serializer.Setup(x => x.DeSerialize<PurchaseOrderPayload>(It.IsAny<Guid>()));

            _transition.Setup(x => x.NoResponse(
              It.IsAny<string>(),
              It.IsAny<Guid>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>()));

            SagaDoesStartSequenceForChange();
        }

        private void SagaDealsWithTimeOutForCreate()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaReTryToServiceForSoap);

            _serializer.Setup(x => x.DeSerialize<PurchaseOrderPayload>(It.IsAny<Guid>()));

            _transition.Setup(x => x.NoResponse(
              It.IsAny<string>(),
              It.IsAny<Guid>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>(),
              It.IsAny<string>()));

            SagaDoesStartSequenceForCreate();
        }

        private void SagaDealsWithTimeOutAndRetryLimitIsReachedForChange()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaRetryLimitReached);

            SagaDoesStartSequenceForChange();
        }

        private void SagaDealsWithTimeOutAndRetryLimitIsReachedForCreate()
        {
            MethodCallForAuditCommand((int)AuditAction.SagaRetryLimitReached);

            SagaDoesStartSequenceForCreate();
        }

        private void HandlerMustRequestTimeoutForNormalOperationsCreate()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<PurchaseOrderCreateNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.PurchaseOrderCreateTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessageForCreate());
                    x.Data.SagaRetry = 2;
                })
                .ExpectSend<PurchaseOrderCreateSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaReTryToServiceForSoap)
                .WhenSagaTimesOut();
        }

        private void HandlerMustRequestTimeoutForNormalOperationsChange()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<PurchaseOrderChangeNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.PurchaseOrderChangeTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessageForChange());
                    x.Data.SagaRetry = 2;
                })
                .ExpectSend<PurchaseOrderChangeSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaReTryToServiceForSoap)
                .WhenSagaTimesOut();
        }

        private void TimeoutMustBeRequestedForCreate()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<PurchaseOrderCreateNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.PurchaseOrderCreateTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessageForCreate());
                });
        }

        private void TimeoutMustBeRequestedForChange()
        {
            _sagaUnderTest.ExpectTimeoutToBeSetIn<PurchaseOrderChangeNoResponse>((state, span) =>
            span == TimeSpan.FromMinutes(Service.Constants.PurchaseOrderCreateTimeoutMinutes))
                .When(x =>
                {
                    x.Handle(GetCommandMessageForChange());
                });
        }

        private void CommandMessagesForStartSequenceForCreateMustBeSend()
        {
            _sagaUnderTest
                .ExpectSend<PurchaseOrderCreateSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaSendToServiceForSoap);
        }

        private void CommandMessagesForStartSequenceForChangeMustBeSend()
        {
            _sagaUnderTest
                .ExpectSend<PurchaseOrderChangeSapCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaSendToServiceForSoap);
        }

        private void HandlerIsWorkingWithTimeOutRetryExeededCreate()
        {
            CommandMessagesForStartSequenceForCreateMustBeSend();

            _sagaUnderTest
                .When(x =>
                {
                    x.Handle(GetCommandMessageForCreate());
                    x.Data.SagaRetry = 1;
                })
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaRetryLimitReached)
                .WhenSagaTimesOut();
        }

        private void HandlerIsWorkingWithTimeOutRetryExeededChange()
        {
            CommandMessagesForStartSequenceForChangeMustBeSend();

            _sagaUnderTest
                .When(x =>
                {
                    x.Handle(GetCommandMessageForChange());
                    x.Data.SagaRetry = 1;
                })
                .ExpectSend<PurchaseOrderAuditCommand>(command => command.Action == (int)AuditAction.SagaRetryLimitReached)
                .WhenSagaTimesOut();
        }

        private void ResponseCommandMessagesMustBeSendForCreate()
        {
            _sagaUnderTest.ExpectSend<PurchaseOrderAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .When(x =>
                {
                    x.Handle(new PurchaseOrderCreateResponseCommand());
                });
        }

        private void ResponseCommandMessagesMustBeSendForChange()
        {
            _sagaUnderTest.ExpectSend<PurchaseOrderAuditCommand>(command=> command.SagaReferenceId == Constants.SagaReferenceId)
                .When(x =>
                {
                    x.Handle(new PurchaseOrderChangeResponseCommand());
                });
        }

        private static PurchaseOrderCreateCommand GetCommandMessageForCreate()
        {
            return new PurchaseOrderCreateCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                PurchaseOrderNumber = Constants.PurchaseOrderNumber
            };
        }

        private static PurchaseOrderChangeCommand GetCommandMessageForChange()
        {
            return new PurchaseOrderChangeCommand
            {
                SagaReferenceId = Constants.SagaReferenceId,
                PurchaseOrderNumber = Constants.PurchaseOrderNumber
            };
        }

        private void SagaMustComplete()
        {
            _sagaUnderTest.AssertSagaCompletionIs(true);
        }

        private void MethodCallForAuditCommand(int action)
        {
            _createMessage.Setup(x => x.PurchaseOrderAuditCommand(
                It.Is<int>(a => a == action),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<float>(),
                It.IsAny<string>()))
                .Returns(
                new PurchaseOrderAuditCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    PurchaseOrderNumber = Constants.PurchaseOrderNumber,
                    Action = action
                });
        }

        private void MethodCallForSapCommandForPurchaseOrderCreate()
        {
            _createMessage.Setup(x => x.PurchaseOrderCreateSapCommand(It.IsAny<PurchaseOrderCreateCommand>())).Returns(
                new PurchaseOrderCreateSapCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    PurchaseOrderNumber = Constants.PurchaseOrderNumber
                });
        }

        private void MethodCallForSapCommandForPurchaseOrderChange()
        {
            _createMessage.Setup(x => x.PurchaseOrderChangeSapCommand(It.IsAny<PurchaseOrderChangeCommand>())).Returns(
                new PurchaseOrderChangeSapCommand
                {
                    SagaReferenceId = Constants.SagaReferenceId,
                    PurchaseOrderNumber = Constants.PurchaseOrderNumber
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
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Once());
        }

        private void MustCallCreateMessageForAuditCommandTwice()
        {
            _createMessage.Verify(x => x.PurchaseOrderAuditCommand(
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

        private void MustCallMethodForAuditCommandOnce()
        {
            _createMessage.Verify(x => x.PurchaseOrderAuditCommand(
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

        private void MustCallMethodForCreationOfSapCommandMessageForCreate()
        {
            _createMessage.Verify(x => x.PurchaseOrderCreateSapCommand(It.IsAny<PurchaseOrderCreateCommand>()), Times.Once());
        }

        private void MustCallMethodForCreationOfSapCommandMessageForChange()
        {
            _createMessage.Verify(x => x.PurchaseOrderChangeSapCommand(It.IsAny<PurchaseOrderChangeCommand>()), Times.Once());
        }

        private void MustCallDeSerialize()
        {
            _serializer.Verify(x => x.DeSerialize<PurchaseOrderPayload>(It.IsAny<Guid>()), Times.Once());
        }

        private void MustCallMethodForSerialize()
        {
            _serializer.Verify(x => x.Serialize(It.IsAny<PurchaseOrderPayload>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        }

        private void MustCallMethodForTransitionStart()
        {
            _transition.Verify(x => x.Start(It.IsAny<string>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        private void MustCallTransitionNoResponse()
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

        private void SagaDataMustBeSetUpCorrectly()
        {
            _sagaUnderTest.When(x =>
            {
                Assert.AreEqual(x.Data.SagaReferenceId, Constants.SagaReferenceId);
                Assert.AreEqual(x.Data.PurchaseOrderNumber, Constants.PurchaseOrderNumber);
                Assert.AreEqual(string.IsNullOrEmpty(x.Data.SerializedMessageId.ToString()), false);
                Assert.AreEqual(x.Data.SagaState, SagaStates.Started.ToString());
                Assert.AreEqual(x.Data.SagaRetry, Service.Constants.PurchaseOrderCreateRetry);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Date, DateTime.Now.Date);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Hour, DateTime.Now.Hour);
                Assert.AreEqual(x.Data.LastUpdatedDateTime.Minute, DateTime.Now.Minute);
            });
        }
    }
}