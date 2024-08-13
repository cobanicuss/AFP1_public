using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class PurchaseOrderCommitCommandHandler : IHandleMessages<PurchaseOrderCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(PurchaseOrderCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"PurchaseOrderNumber={message.PurchaseOrderAuditCommand.PurchaseOrderNumber}");
            Logger.Info($"Action={message.PurchaseOrderAuditCommand.Action}");
            Logger.Info($"Type={message.PurchaseOrderAuditCommand.Type}");

            if (message.PurchaseOrderAuditCommand.Type.ToUpper().Equals(Shared.Constants.PoChange.ToUpper()))
            {
                var purchaseOrderChange = new PurchaseOrderChange
                {
                    Id = Guid.NewGuid(),
                    PurchaseOrderNumber = message.PurchaseOrderAuditCommand.PurchaseOrderNumber,
                    SagaReferenceId = message.PurchaseOrderAuditCommand.SagaReferenceId,
                    MessageType = message.PurchaseOrderAuditCommand.MessageType,
                    FromEndpoint = message.PurchaseOrderAuditCommand.FromEndpoint,
                    Action = message.PurchaseOrderAuditCommand.Action,
                    DateTimeMessageSendToHere = message.PurchaseOrderAuditCommand.DateTimeMessageSendToHere,
                    DateTimeMessageRecieved = DateTime.Now,
                    MessageData = message.PurchaseOrderAuditCommand.MessageData,
                    Leg = message.PurchaseOrderAuditCommand.Leg,
                    LegCount = Constants.PurchaseOrderLegCount
                };

                AuditLogRepository.SaveThisAuditLog(purchaseOrderChange);
            }
            else if (message.PurchaseOrderAuditCommand.Type.ToUpper().Equals(Shared.Constants.PoCreate.ToUpper()))
            {
                var purchaseOrderCreate = new PurchaseOrderCreate
                {
                    Id = Guid.NewGuid(),
                    PurchaseOrderNumber = message.PurchaseOrderAuditCommand.PurchaseOrderNumber,
                    SagaReferenceId = message.PurchaseOrderAuditCommand.SagaReferenceId,
                    MessageType = message.PurchaseOrderAuditCommand.MessageType,
                    FromEndpoint = message.PurchaseOrderAuditCommand.FromEndpoint,
                    Action = message.PurchaseOrderAuditCommand.Action,
                    DateTimeMessageSendToHere = message.PurchaseOrderAuditCommand.DateTimeMessageSendToHere,
                    DateTimeMessageRecieved = DateTime.Now,
                    MessageData = message.PurchaseOrderAuditCommand.MessageData,
                    Leg = message.PurchaseOrderAuditCommand.Leg,
                    LegCount = Constants.PurchaseOrderLegCount
                };

                AuditLogRepository.SaveThisAuditLog(purchaseOrderCreate);
            }
            else
            {
                throw new Exception($"Do not know what kind of PurchaseOrder message this is to process. Cannot proceed!!! message.Type={message.PurchaseOrderAuditCommand.Type} Constants='{Shared.Constants.PoChange}','{Shared.Constants.PoCreate}'.");    
            }
        }
    }
}