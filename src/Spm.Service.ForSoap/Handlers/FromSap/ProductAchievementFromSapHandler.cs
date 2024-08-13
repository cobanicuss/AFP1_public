using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class ProductAchievementFromSapHandler : IHandleMessages<ProductAchievementSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementFromSapHandler));
        private readonly IBus _bus;

        public ProductAchievementFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(ProductAchievementSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("ProductAchievementSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var productAchievementAuditCommand = new ProductAchievementAuditCommand
            {
                LotNumber = Shared.Constants.NotAvailable,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductAchievementSapResponse).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(productAchievementAuditCommand);

            var sagaCompleteCommand = new ProductAchievementResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}