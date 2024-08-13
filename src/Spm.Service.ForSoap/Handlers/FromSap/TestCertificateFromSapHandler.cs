using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class TestCertificateFromSapHandler : IHandleMessages<TestCertificateSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateFromSapHandler));
        private readonly IBus _bus;

        public TestCertificateFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(TestCertificateSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("TestCertificateSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var testCertificateAuditCommand = new TestCertificateAuditCommand
            {
                CertificateId = Shared.Constants.NotAvailable,
                SagaReferenceId = message.SagaReferenceId,
                InboundId = Shared.Constants.NotAvailable,
                MessageIndex = -1,
                MessageCount = -1,
                MessageType = typeof(TestCertificateSapResponse).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(testCertificateAuditCommand);

            var sagaCompleteCommand = new TestCertificateResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}