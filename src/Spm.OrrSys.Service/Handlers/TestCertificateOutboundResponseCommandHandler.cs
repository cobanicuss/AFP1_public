using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class TestCertificateOutboundResponseCommandHandler : IHandleMessages<TestCertificateResponseCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateOutboundResponseCommandHandler));
        private readonly IBus _bus;
        private readonly IDoTestCertificateBusiness _testCertificateBusiness;

        public TestCertificateOutboundResponseCommandHandler(IBus bus, IDoTestCertificateBusiness testCertificateBusiness)
        {
            _bus = bus;
            _testCertificateBusiness = testCertificateBusiness;
        }

        public void Handle(TestCertificateResponseCommand message)
        {
            const float leg = 6.0F;

            Logger.Info("======================================");
            Logger.Info("Received a message from SAGA.");
            Logger.Info("A response from SAP received.");
            Logger.Info("TestCertificateResponseCommand");
            Logger.Info($"InboundId={message.InboundId}.");
            Logger.Info($"CertificateId={message.CertificateId}.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}.");

            Logger.Info("Updating the db.");
            _testCertificateBusiness.CreateOutboundTestCertificateResponse(message.LotNumberList);

            Logger.Info("Sending Audit-Log.");
            var testCertificateAuditCommand = new TestCertificateAuditCommand
            {
                InboundId = message.InboundId,
                CertificateId = message.CertificateId,
                SagaReferenceId = message.SagaReferenceId,
                MessageIndex = message.MessageIndex,
                MessageCount = message.MessageCount,
                MessageType = typeof(TestCertificateResponseCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.MessageImplemented,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(testCertificateAuditCommand);

            Logger.Info("All done. All good.");
        }
    }
}