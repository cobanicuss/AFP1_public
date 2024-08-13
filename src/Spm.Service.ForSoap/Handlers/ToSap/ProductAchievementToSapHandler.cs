using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class ProductAchievementToSapHandler : IHandleMessages<ProductAchievementSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievementToSapHandler));
        private readonly IBus _bus;
        private readonly ISendProductAchievementToSap _sendSoapToSap;

        public ProductAchievementToSapHandler(IBus bus, ISendProductAchievementToSap sendSoapToSap)
        {
            _bus = bus;
            _sendSoapToSap = sendSoapToSap;
        }

        public void Handle(ProductAchievementSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");

            _sendSoapToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var productAchievementAuditCommand = new ProductAchievementAuditCommand
            {
                LotNumber = message.LotNumber,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductAchievementSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg
            };

            _bus.Send(productAchievementAuditCommand);
        }
    }
}
