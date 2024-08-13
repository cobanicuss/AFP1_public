using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Soap;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class ProductAchievementResponseCommandHandler : IHandleMessages<ProductAchievementResponseCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementResponseCommandHandler));
        private readonly IBus _bus;
        private readonly IDoProductAchievementCommunication _productAchievement;

        public ProductAchievementResponseCommandHandler(IBus bus, IDoProductAchievementCommunication productAchievement)
        {
            _bus = bus;
            _productAchievement = productAchievement;
        }

        public void Handle(ProductAchievementResponseCommand message)
        {
            const float leg = 6.0F;

            Logger.Info("======================================");
            Logger.Info("Received a message from SAGA.");
            Logger.Info("A response from SAP received.");
            Logger.Info("Updating the db using:");
            Logger.Info($"LotNumber={message.LotNumber}.");

            Logger.Info("Send SOAP message to OrrSys.DataInterfacingService.");
            _productAchievement.SendSoapMessageForProductAchievement(message.LotNumber);

            var updateAuditCommand = new ProductAchievementAuditCommand
            {
                LotNumber = message.LotNumber,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductAchievementResponseCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.MessageImplemented,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(updateAuditCommand);

            Logger.Info("All done. All good.");
        }
    }
}