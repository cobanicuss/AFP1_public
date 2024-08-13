using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class MaterialMasterUpdateRequestCommandHandler : IHandleMessages<MaterialMasterUpdateRequestCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterUpdateRequestCommandHandler));
        private readonly IBus _bus;
        private readonly IDoMaterialMasterCommunication _materialMaster;

        public MaterialMasterUpdateRequestCommandHandler(IBus bus, IDoMaterialMasterCommunication materialMaster)
        {
            _bus = bus;
            _materialMaster = materialMaster;
        }

        public void Handle(MaterialMasterUpdateRequestCommand message)
        {
            const float leg = 2.0F;

            Logger.Info("======================================");
            Logger.Info("A request received from ServiceForSoap.");
            Logger.Info("MaterialMasterUpdateRequestCommand");
            Logger.Info("Updating the db using:");
            Logger.Info($"ReferenceId={message.ShortItemNumber}.");

            Logger.Info("Send SOAP message to OrrSys.DataInterfacingService.");
            _materialMaster.SendSoapMessageForMatrialMasterUpdate(message);

            Logger.Info("Sending audit command.");
            var sapRequestAuditCommand = new MaterialMasterUpdateAuditCommand
            {
                ShortItemNumber = message.ShortItemNumber,
                InboundId = message.InboundId,
                MessageType = typeof(MaterialMasterUpdateRequestCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.MessageImplemented,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(sapRequestAuditCommand);

            Logger.Info("All done. All good.");
        }
    }
}