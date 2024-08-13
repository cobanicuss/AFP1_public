using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class PurchaseOrderCreateFromSapHandler : IHandleMessages<PurchaseOrderCreateSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementFromSapHandler));
        private readonly IBus _bus;

        public PurchaseOrderCreateFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(PurchaseOrderCreateSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("PurchaseOrderCreateSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var purchaseOrderAuditCommand = new PurchaseOrderAuditCommand
            {
                MessageType = typeof(PurchaseOrderCreateSapResponse).FullName,
                SagaReferenceId = message.SagaReferenceId,
                PurchaseOrderNumber = Shared.Constants.NotAvailable,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg,
                Type = Shared.Constants.PoCreate
            };
            _bus.Send(purchaseOrderAuditCommand);

            var sagaCompleteCommand = new PurchaseOrderCreateResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}