using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class TestCertificateInboundRequestCommandHandler : IHandleMessages<TestCertificateRequestCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateInboundRequestCommandHandler));
        private readonly IBus _bus;
        private readonly IDoTestCertificateBusiness _testCertificateBusiness;

        public TestCertificateInboundRequestCommandHandler(IBus bus, IDoTestCertificateBusiness testCertificateBusiness)
        {
            _bus = bus;
            _testCertificateBusiness = testCertificateBusiness;
        }

        public void Handle(TestCertificateRequestCommand message)
        {
            const float leg = 2.0F;

            Logger.Info("======================================");
            Logger.Info("A request received from ServiceForSoap.");
            Logger.Info("TestCertificateRequestCommand");
            Logger.Info($"CertificateId={message.CertificateId}.");

            Logger.Info("Implementing TestCertificate business logic.");
            _testCertificateBusiness.CreateInboundTestCertificate(message);
            
            var productAchievementAuditCommand = new TestCertificateRequestAuditCommand
            {
                MessageType = typeof(TestCertificateRequestCommand).FullName,
                CertificateId = message.CertificateId,
                InboundId = message.InboundId,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceivedFromServiceForSoap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(productAchievementAuditCommand);

            Logger.Info("All done. All good.");
        }
    }
}