using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class TestCertificateRequestCommitCommandHandler : IHandleMessages<TestCertificateRequestCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateRequestCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(TestCertificateRequestCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"CertificateId={message.TestCertificateRequestAuditCommand.CertificateId}");
            Logger.Info($"InboundId={message.TestCertificateRequestAuditCommand.InboundId}");
            Logger.Info($"Action={message.TestCertificateRequestAuditCommand.Action}");

            var testCertificate = new TestCertificateRequest
            {
                Id = Guid.NewGuid(),
                CertificateId = message.TestCertificateRequestAuditCommand.CertificateId,
                InboundId = message.TestCertificateRequestAuditCommand.InboundId,
                MessageType = message.TestCertificateRequestAuditCommand.MessageType,
                FromEndpoint = message.TestCertificateRequestAuditCommand.FromEndpoint,
                Action = message.TestCertificateRequestAuditCommand.Action,
                DateTimeMessageSendToHere = message.TestCertificateRequestAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.TestCertificateRequestAuditCommand.MessageData,
                Leg = message.TestCertificateRequestAuditCommand.Leg,
                LegCount = Constants.TestCertificateRequestLegCount
            };

            AuditLogRepository.SaveThisAuditLog(testCertificate);
        }
    }
}