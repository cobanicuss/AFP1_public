using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class MaterialMasterCommitCommandHandler : IHandleMessages<MaterialMasterCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(MaterialMasterCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"ReferenceId={message.MaterialMasterAuditCommand.ReferenceId}");
            Logger.Info($"Action={message.MaterialMasterAuditCommand.Action}");

            var materialMaster = new MaterialMaster
            {
                Id = Guid.NewGuid(),
                ShortItemNumber = message.MaterialMasterAuditCommand.ReferenceId,
                SagaReferenceId = message.MaterialMasterAuditCommand.SagaReferenceId,
                MessageType = message.MaterialMasterAuditCommand.MessageType,
                FromEndpoint = message.MaterialMasterAuditCommand.FromEndpoint,
                Action = message.MaterialMasterAuditCommand.Action,
                DateTimeMessageSendToHere = message.MaterialMasterAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.MaterialMasterAuditCommand.MessageData,
                Leg = message.MaterialMasterAuditCommand.Leg,
                LegCount = Constants.MaterialMasterLegCount
            };

            AuditLogRepository.SaveThisAuditLog(materialMaster);
        }
    }
}