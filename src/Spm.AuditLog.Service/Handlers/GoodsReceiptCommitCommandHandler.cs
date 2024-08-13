using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Repository;

namespace Spm.AuditLog.Service.Handlers
{
    public class GoodsReceiptCommitCommandHandler : IHandleMessages<GoodsReceiptCommitCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsReceiptCommitCommandHandler));

        public IAuditLogRepository AuditLogRepository { get; set; }

        public void Handle(GoodsReceiptCommitCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Now writing an auditlog, with:");
            Logger.Info($"GoodsReceiptId={message.GoodsReceiptAuditCommand.GoodsReceiptId}");
            Logger.Info($"Action={message.GoodsReceiptAuditCommand.Action}");
            Logger.Info($"Type={message.GoodsReceiptAuditCommand.Type}");

            const float legCount = Constants.GoodsReceitpLegCount;

            if (message.GoodsReceiptAuditCommand.Type.ToUpper().Equals(Shared.Constants.GoodsReceiptType.ToUpper()))
            {
                var goodsReceipt = new GoodsReceipt
                {
                    Id = Guid.NewGuid(),
                    GoodsReceiptId = message.GoodsReceiptAuditCommand.GoodsReceiptId,
                    SagaReferenceId = message.GoodsReceiptAuditCommand.SagaReferenceId,
                    MessageType = message.GoodsReceiptAuditCommand.MessageType,
                    FromEndpoint = message.GoodsReceiptAuditCommand.FromEndpoint,
                    Action = message.GoodsReceiptAuditCommand.Action,
                    DateTimeMessageSendToHere = message.GoodsReceiptAuditCommand.DateTimeMessageSendToHere,
                    DateTimeMessageRecieved = DateTime.Now,
                    MessageData = message.GoodsReceiptAuditCommand.MessageData,
                    Leg = message.GoodsReceiptAuditCommand.Leg,
                    LegCount = legCount
                };

                AuditLogRepository.SaveThisAuditLog(goodsReceipt);
            }
            else if (message.GoodsReceiptAuditCommand.Type.ToUpper().Equals(Shared.Constants.GoodsReversalType.ToUpper()))
            {
                var goodsReversal = new GoodsReversal
                {
                    Id = Guid.NewGuid(),
                    GoodsReceiptId = message.GoodsReceiptAuditCommand.GoodsReceiptId,
                    SagaReferenceId = message.GoodsReceiptAuditCommand.SagaReferenceId,
                    MessageType = message.GoodsReceiptAuditCommand.MessageType,
                    FromEndpoint = message.GoodsReceiptAuditCommand.FromEndpoint,
                    Action = message.GoodsReceiptAuditCommand.Action,
                    DateTimeMessageSendToHere = message.GoodsReceiptAuditCommand.DateTimeMessageSendToHere,
                    DateTimeMessageRecieved = DateTime.Now,
                    MessageData = message.GoodsReceiptAuditCommand.MessageData,
                    Leg = message.GoodsReceiptAuditCommand.Leg,
                    LegCount = legCount
                };

                AuditLogRepository.SaveThisAuditLog(goodsReversal);
            }
            else
            {
                throw new Exception($"Do not know what kind of GoodsReceipt message this is to process. Cannot proceed!!! message.Type={message.GoodsReceiptAuditCommand.Type} Constants='{Shared.Constants.GoodsReceiptType}','{Shared.Constants.GoodsReversalType}'.");
            }
        }
    }
}