using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class PurchaseOrderChangeFromSapHandler : IHandleMessages<PurchaseOrderChangeSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementFromSapHandler));
        private readonly IBus _bus;

        public PurchaseOrderChangeFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PurchaseOrderChangeSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("PurchaseOrderChangeSapResponse:");
            Logger.Info("Sending message to SAGA in response.");

            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var purchaseOrderAuditCommand = new PurchaseOrderAuditCommand
            {
                MessageType = typeof(PurchaseOrderChangeSapResponse).FullName,
                SagaReferenceId = message.SagaReferenceId,
                PurchaseOrderNumber = Shared.Constants.NotAvailable,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg,
                Type = Shared.Constants.PoChange
            };
            _bus.Send(purchaseOrderAuditCommand);

            var sagaCompleteCommand = new PurchaseOrderChangeResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}