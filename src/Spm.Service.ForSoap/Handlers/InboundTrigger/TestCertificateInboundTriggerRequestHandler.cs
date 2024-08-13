using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.InboundTrigger
{
    public class TestCertificateInboundTriggerRequestHandler : IHandleMessages<TestCertificateTriggerRequest>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateInboundTriggerRequestHandler));
        private readonly IBus _bus;

        public TestCertificateInboundTriggerRequestHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(TestCertificateTriggerRequest message)
        {
            const float leg = 0.0F;

            Logger.Info("======================================");
            Logger.Info("Request received from external source!");
            Logger.Info("TestCertificateTriggerRequest:");
            Logger.Info($"InboundId={message.InboundId}.");
            Logger.Info("Sending audit command.");

            var auditCommand = new TestCertificateAuditCommand
            {
                InboundId = message.InboundId,
                CertificateId = Shared.Constants.NotAvailable,
                SagaReferenceId = Shared.Constants.NotAvailable,
                MessageIndex = 0,
                MessageCount = 0,
                MessageType = typeof(TestCertificateTriggerRequest).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ExternalTriggerReceived,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(auditCommand);

            Logger.Info("Sending message to OrrSysService.");
            var requestCommand = new TestCertificateTriggerCommand
            {
                InboundId = message.InboundId
            };
            _bus.Send(requestCommand);
        }
    }
}