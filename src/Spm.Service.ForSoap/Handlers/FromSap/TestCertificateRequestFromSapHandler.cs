using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class TestCertificateRequestFromSapHandler : IHandleMessages<TestCertificateSapRequest>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateRequestFromSapHandler));
        private readonly IBus _bus;
        private readonly ISendResponseOnRequestToSap _sendToSap;

        public TestCertificateRequestFromSapHandler(IBus bus, ISendResponseOnRequestToSap sendToSap)
        {
            _bus = bus;
            _sendToSap = sendToSap;
        }

        public void Handle(TestCertificateSapRequest message)
        {
            const float leg1 = 1.0F;
            const float leg11 = 1.1F;

            Logger.Info("======================================");
            Logger.Info("Request received from SAP!");
            Logger.Info("TestCertificateSapRequest:");
            Logger.Info($"CertificateId={message.CertificateId}");
           
            Logger.Info("Sending audit command.");
            var sapRequestAuditCommand = new TestCertificateRequestAuditCommand
            {
                MessageType = typeof(TestCertificateSapRequest).FullName,
                CertificateId = message.CertificateId,
                InboundId = message.InboundId,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceived,
                MessageData = message.ToString(),
                Leg =  leg1
            };
            _bus.Send(sapRequestAuditCommand);

            Logger.Info("Sending message to OrrSysService.");
            var requestCommand = new TestCertificateRequestCommand
            {
                CertificateId = message.CertificateId,
                InboundId = message.InboundId,
                Payload = message.Payload
            };
            _bus.Send(requestCommand);

            Logger.Info("Sending acknowldegement response back to SAP.");
            var responseCommand = new ResponseToSapRequestCommand
            {
                NumberId = message.CertificateId,
                InboundType = EnumInboundType.TestCertificate
            };
            _sendToSap.SendSoapMessageToSap(responseCommand);

            Logger.Info("Sending audit trail of response.");
            var responseAuditCommand = new TestCertificateRequestAuditCommand
            {
                MessageType = typeof(ResponseToSapRequestCommand).FullName,
                CertificateId = message.CertificateId,
                InboundId = message.InboundId,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendResponseToSap,
                MessageData = responseCommand.ToString(),
                Leg = leg11
            };
            _bus.Send(responseAuditCommand);
        }
    }
}