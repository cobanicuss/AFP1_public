using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class ProductAchievementCommandHandler : IHandleMessages<ProductAchievementCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementCommandHandler));
        private readonly IBus _bus;
        private readonly IDoProductAchievementBusiness _productAchievement;

        public ProductAchievementCommandHandler(IBus bus, IDoProductAchievementBusiness productAchievement)
        {
            _bus = bus;
            _productAchievement = productAchievement;
        }

        public void Handle(ProductAchievementCommand message)
        {
            const float leg = 1.0F;

            Logger.Info("======================================");
            Logger.Info("Message received from OrrSys/ScannerService.");
            Logger.Info($"With LotNumber={message.LotNumber}.");
            Logger.Info($"With SagaReferenceId={message.SagaReferenceId}.");

            Logger.Info("Setting F4801Z1 ProcessedInSAP='I'.");
            _productAchievement.SetInProgress(message.LotNumber);

            Logger.Info("Now sending message from OrrSys-Service to Saga.");
            var sagaStartMessage = new Spm.Service.Messages.ProductAchievementCommand
            {
                SagaReferenceId = message.SagaReferenceId,
                LotNumber = message.LotNumber,
                Payload = message.Payload
            };
            _bus.Send(sagaStartMessage);

            var productAchievementAuditCommand = new ProductAchievementAuditCommand
            {
                LotNumber = message.LotNumber,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductAchievementCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendToSaga,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(productAchievementAuditCommand);

            Logger.Info("Message send successfully.");
        }
    }
}