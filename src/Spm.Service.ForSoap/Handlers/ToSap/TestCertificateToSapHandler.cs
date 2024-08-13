using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class TestCertificateToSapHandler : IHandleMessages<TestCertificateSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateToSapHandler));
        private readonly IBus _bus;
        private readonly ISendTestCertificateToSap _sendSoapToSap;

        public TestCertificateToSapHandler(IBus bus, ISendTestCertificateToSap sendSoapToSap)
        {
            _bus = bus;
            _sendSoapToSap = sendSoapToSap;
        }
        
        public void Handle(TestCertificateSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");
            Logger.Info("Test-Certificate Request.");

            _sendSoapToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");

            var testCertificateAuditCommand = new TestCertificateAuditCommand
            {
                InboundId = message.Inboundid,
                SagaReferenceId = message.SagaReferenceId,
                CertificateId = message.Payload.CertificateNumber,
                MessageIndex = message.MessageIndex,
                MessageCount = message.MessageCount,
                MessageType = typeof(TestCertificateSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg
            };

            _bus.Send(testCertificateAuditCommand);
        }
    }
}