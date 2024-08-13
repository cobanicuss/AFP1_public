using System;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Spm.Service.CreateMessage;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Service.SagaData;
using Spm.Service.SagaTransitions;
using Spm.Service.Serialization;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.Service.Sagas
{
    public class TestCertificateSaga : Saga<TestCertificateSagaData>,
        IAmStartedByMessages<TestCertificateCommand>,
        IHandleMessages<TestCertificateResponseCommand>,
        IHandleTimeouts<TestCertificateNoResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateSaga));
        public ICreateMessage CreateMessage { get; set; }
        public ITestCertificateTransitions Transition { get; set; }
        public ISerializeMessage Serializer { get; set; }
        private readonly string _sagaName = typeof(TestCertificateSaga).FullName;

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TestCertificateSagaData> mapper)
        {
            mapper.ConfigureMapping<TestCertificateCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
            mapper.ConfigureMapping<TestCertificateResponseCommand>(m => m.SagaReferenceId).ToSaga(s => s.SagaReferenceId);
        }

        public void Handle(TestCertificateCommand message)
        {
            const float leg = 2.0F;

            var messageType = typeof(TestCertificateCommand).FullName;

            Logger.Info("======================================");
            Logger.Info("Message received from OrrSys-Service.");
            Logger.Info("Saga is now STARTED.");
            Logger.Info($"InboundId={message.InboundId}");
            Logger.Info($"CertificateId={message.Payload.CertificateNumber}");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            Data.InboundId = message.InboundId;
            Data.SagaReferenceId = message.SagaReferenceId;
            Data.CertificateId = message.Payload.CertificateNumber;
            Data.LotNumberList = string.Join(",", message.LotNumberList);
            Data.MessageIndex = message.MessageIndex;
            Data.MessageCount = message.MessageCount;
            Data.SagaState = SagaStates.Started.ToString();
            Data.SagaRetry = Constants.TestCertificateRetry;
            Data.SerializedMessageId = Guid.NewGuid();
            Data.LastUpdatedDateTime = DateTime.Now;

            Serializer.Serialize(message.Payload, Data.SerializedMessageId, Data.Id);

            Transition.Start(message.Payload.CertificateNumber, Data.Id, _sagaName, Data.SagaReferenceId);

            var testCertificateSapCommand = CreateMessage.TestCertificateSapCommand(message);
            Bus.Send(testCertificateSapCommand);

            const int sagaSend = (int)AuditAction.SagaSendToServiceForSoap;
            var sendAuditCommand = CreateMessage.TestCertificateAuditCommand(
                sagaSend, 
                messageType, 
                _sagaName, 
                message.ToString(), 
                message.Payload.CertificateNumber, 
                Data.SagaReferenceId, 
                Data.InboundId, 
                Data.MessageIndex,
                Data.MessageCount,
                leg);
            Bus.Send(sendAuditCommand);

            RequestTimeout<TestCertificateNoResponse>(TimeSpan.FromMinutes(Constants.TestCertificateTimeoutMinutes));
        }

        public void Handle(TestCertificateResponseCommand message)
        {
            const float leg = 5.0F;

            Logger.Info("======================================");
            Logger.Info("ALL GOOD: Message received from SAP via ServiceForSoap.");
            Logger.Info("Saga is now complete. Will be deleted from db.");
            Logger.Info($"InboundId={Data.InboundId}");
            Logger.Info($"CertificateId={Data.CertificateId}");
            Logger.Info($"SagaReferenceId={Data.SagaReferenceId}");

            var messageType = typeof(TestCertificateResponseCommand).FullName;

            const int fromServiceForSoap = (int)AuditAction.ResponseReceivedFromServiceForSoap;
            var auditCommand = CreateMessage.TestCertificateAuditCommand(
                fromServiceForSoap, 
                messageType, 
                _sagaName, 
                message.ToString(), 
                Data.CertificateId, 
                Data.SagaReferenceId, 
                Data.InboundId, 
                Data.MessageIndex,
                Data.MessageCount,
                leg);
            Bus.Send(auditCommand);

            Transition.End(Data.CertificateId, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            var testCertificateSapResponse = new OrrSys.Messages.TestCertificateResponseCommand
            {
                InboundId = Data.InboundId,
                SagaReferenceId = Data.SagaReferenceId,
                CertificateId = Data.CertificateId,
                MessageIndex = Data.MessageIndex,
                MessageCount = Data.MessageCount,
                LotNumberList = Data.LotNumberList.Split(',')
            };
            Bus.Send(testCertificateSapResponse);

            Serializer.DeleteSerialization(Data.SerializedMessageId);

            MarkAsComplete();
        }

        public void Timeout(TestCertificateNoResponse state)
        {
            const float leg = 2.1F;
            const float legUp = 2.999F;

            Logger.Info("======================================");
            Logger.Info("No response received. Test-Certificate! Uncool!");

            var messageType = typeof(TestCertificateNoResponse).FullName;

            var retries = Data.SagaRetry;
            retries = retries - 1;
            Data.SagaRetry = retries;

            if (retries <= 0)
            {
                SagaGivingUp(messageType, legUp);
                return;
            }

            Transition.NoResponse(Data.CertificateId, Data.Id, _sagaName, Data.SagaState, Data.SagaReferenceId);

            Data.SagaState = SagaStates.NoResponse.ToString();
            Data.LastUpdatedDateTime = DateTime.Now;

            var payload = Serializer.DeSerialize<TestCertificateOutboundPayload>(Data.SerializedMessageId);
            var message = new TestCertificateSapCommand
            {
                SagaReferenceId = Data.SagaReferenceId,
                Payload = payload
            };
            Bus.Send(message);

            const int sagaRetry = (int)AuditAction.SagaReTryToServiceForSoap;
            var endAuditCommand = CreateMessage.TestCertificateAuditCommand(
                sagaRetry,
                messageType,
                _sagaName,
                message.ToString(),
                Data.CertificateId,
                Data.SagaReferenceId,
                Data.InboundId,
                Data.MessageIndex,
                Data.MessageCount,
                leg);
            Bus.Send(endAuditCommand);

            RequestTimeout<TestCertificateNoResponse>(TimeSpan.FromMinutes(Constants.TestCertificateTimeoutMinutes));
        }

        private void SagaGivingUp(string messageType, float legUp)
        {
            Logger.Info("No Response, Saga giving up. No point continuing!");

            const int sagaGivingUp = (int)AuditAction.SagaRetryLimitReached;
            var auditRetryEndCommand = CreateMessage.TestCertificateAuditCommand(
                sagaGivingUp,
                messageType,
                _sagaName,
                string.Empty,
                Data.CertificateId,
                Data.SagaReferenceId,
                Data.InboundId,
                Data.MessageIndex,
                Data.MessageCount,
                legUp);
            Bus.Send(auditRetryEndCommand);
        }
    }
}