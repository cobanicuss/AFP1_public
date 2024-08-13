using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class TestCertificateCommitCommandHandler : IHandleMessages<TestCertificateCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(TestCertificateCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"CertificateId={message.TestCertificateAuditCommand.CertificateId}");
            Logger.Info($"InboundId={message.TestCertificateAuditCommand.InboundId}");
            Logger.Info($"Action={message.TestCertificateAuditCommand.Action}");

            var testCertificate = new TestCertificate
            {
                Id = Guid.NewGuid(),
                InboundId = message.TestCertificateAuditCommand.InboundId,
                SagaReferenceId = message.TestCertificateAuditCommand.SagaReferenceId,
                CertificateId = message.TestCertificateAuditCommand.CertificateId,
                MessageIndex = message.TestCertificateAuditCommand.MessageIndex,
                MessageCount = message.TestCertificateAuditCommand.MessageCount,
                MessageType = message.TestCertificateAuditCommand.MessageType,
                FromEndpoint = message.TestCertificateAuditCommand.FromEndpoint,
                Action = message.TestCertificateAuditCommand.Action,
                DateTimeMessageSendToHere = message.TestCertificateAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.TestCertificateAuditCommand.MessageData,
                Leg = message.TestCertificateAuditCommand.Leg,
                LegCount = Constants.TestCertificateLegCount
            };

            AuditLogRepository.SaveThisAuditLog(testCertificate);
        }
    }
}