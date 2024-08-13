using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class PurchaseOrderCreateToSapHandler : IHandleMessages<PurchaseOrderCreateSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PurchaseOrderCreateToSapHandler));
        private readonly IBus _bus;
        private readonly ISendPurchaseOrderCreateToSap _sendToSap;

        public PurchaseOrderCreateToSapHandler(IBus bus, ISendPurchaseOrderCreateToSap sendToSap)
        {
            _bus = bus;
            _sendToSap = sendToSap;
        }

        public void Handle(PurchaseOrderCreateSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");

            _sendToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var purchaseOrderAuditCommand = new PurchaseOrderAuditCommand
            {
                PurchaseOrderNumber = message.PurchaseOrderNumber,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(PurchaseOrderCreateSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Type = Shared.Constants.PoCreate,
                Leg = leg
            };
            _bus.Send(purchaseOrderAuditCommand);
        }
    }
}