using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Map;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class TestCertificateOutboundFileRequestCommandHandler : IHandleMessages<TestCertificateFileRequestCommand>
    {
        private readonly IBus _bus;
        private readonly IDoTestCertificateBusiness _testCertificate;
        private readonly IMapTestCertificateMessage _map;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateOutboundFileRequestCommandHandler));

        public TestCertificateOutboundFileRequestCommandHandler(
            IBus bus,
            IDoTestCertificateBusiness testCertificate,
            IMapTestCertificateMessage map)
        {
            _bus = bus;
            _testCertificate = testCertificate;
            _map = map;
        }

        public void Handle(TestCertificateFileRequestCommand message)
        {
            const float leg = 1.0F;

            Logger.Info("Creating Test-Certificate-Outbound message.");
            var payload = _testCertificate.CreatePayloadForOutboundTestCertificateRequest(message.GroupedDtoList);
            var sagaMessage = _map.CreateCommandMessage(payload, message.InboundId, message.CurrentMessageCount, message.TotalMessages);

            Logger.Info("Now sending Test-Certificate-Outbound message to Saga.");
            Logger.Info($"SagaReferenceId={sagaMessage.SagaReferenceId}.");

            Logger.Info($"Sending message {message.CurrentMessageCount} of {message.TotalMessages}.");
            _bus.Send(sagaMessage);

            Logger.Info("Now sending Test-Certificate-Outbound audit trail.");
            var testCertificateAuditCommand = new TestCertificateAuditCommand
            {
                InboundId = message.InboundId,
                CertificateId = sagaMessage.Payload.CertificateNumber,
                SagaReferenceId = sagaMessage.SagaReferenceId,
                MessageIndex = message.CurrentMessageCount,
                MessageCount = message.TotalMessages,
                MessageType = typeof(Spm.Service.Messages.TestCertificateCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendToSaga,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(testCertificateAuditCommand);
        }
    }
}