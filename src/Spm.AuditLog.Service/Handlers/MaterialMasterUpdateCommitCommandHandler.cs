using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class MaterialMasterUpdateCommitCommandHandler : IHandleMessages<MaterialMasterUpdateCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterUpdateCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(MaterialMasterUpdateCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"ShortItemNumber={message.MaterialMasterUpdateAuditCommand.ShortItemNumber}");
            Logger.Info($"InboundId={message.MaterialMasterUpdateAuditCommand.InboundId}");
            Logger.Info($"Action={message.MaterialMasterUpdateAuditCommand.Action}");

            var materialMasterUpdate = new MaterialMasterUpdate
            {
                Id = Guid.NewGuid(),
                ShortItemNumber = message.MaterialMasterUpdateAuditCommand.ShortItemNumber,
                InboundId = message.MaterialMasterUpdateAuditCommand.InboundId,
                MessageType = message.MaterialMasterUpdateAuditCommand.MessageType,
                FromEndpoint = message.MaterialMasterUpdateAuditCommand.FromEndpoint,
                Action = message.MaterialMasterUpdateAuditCommand.Action,
                DateTimeMessageSendToHere = message.MaterialMasterUpdateAuditCommand.DateTimeMessageSendToHere,
                DateTimeMessageRecieved = DateTime.Now,
                MessageData = message.MaterialMasterUpdateAuditCommand.MessageData,
                Leg = message.MaterialMasterUpdateAuditCommand.Leg,
                LegCount = Constants.MaterialMasterUpdateLegCount
            };

            AuditLogRepository.SaveThisAuditLog(materialMasterUpdate);
        }
    }
}