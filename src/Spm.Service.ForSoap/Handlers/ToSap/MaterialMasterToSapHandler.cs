using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class MaterialMasterToSapHandler : IHandleMessages<MaterialMasterSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterToSapHandler));
        private readonly IBus _bus;
        private readonly ISendMaterialMasterToSap _sendSoapToSap;

        public MaterialMasterToSapHandler(IBus bus, ISendMaterialMasterToSap sendSoapToSap)
        {
            _bus = bus;
            _sendSoapToSap = sendSoapToSap;
        }

        public void Handle(MaterialMasterSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");

            _sendSoapToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var materialMasterAuditCommand = new MaterialMasterAuditCommand
            {
                ReferenceId = message.ShortItemNumber,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(MaterialMasterSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg
            };

            _bus.Send(materialMasterAuditCommand);
        }
    }
}