using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class MaterialMasterUpdateFromSapHandler : IHandleMessages<MaterialMasterUpdateSapRequest>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterUpdateFromSapHandler));
        private readonly IBus _bus;
        private readonly ISendResponseOnRequestToSap _sendToSap;

        public MaterialMasterUpdateFromSapHandler(IBus bus, ISendResponseOnRequestToSap sendToSap)
        {
            _bus = bus;
            _sendToSap = sendToSap;
        }

        public void Handle(MaterialMasterUpdateSapRequest message)
        {
            const float leg1 = 1.0F;
            const float leg11 = 1.1F;

            Logger.Info("======================================");
            Logger.Info("Request received from SAP!");
            Logger.Info("MaterialMasterUpdateSapRequest:");
            Logger.Info($"ReferenceId={message.ShortItemNumber}");
           
            Logger.Info("Sending audit command.");
            var sapRequestAuditCommand = new MaterialMasterUpdateAuditCommand
            {
                ShortItemNumber = message.ShortItemNumber,
                InboundId = message.InboundId,
                MessageType = typeof(MaterialMasterUpdateSapRequest).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceived,
                MessageData = message.ToString(),
                Leg = leg1
            };
            _bus.Send(sapRequestAuditCommand);

            Logger.Info("Sending message to OrrSysService.");
            var requestCommand = new MaterialMasterUpdateRequestCommand
            {
                InboundId = message.InboundId,
                ShortItemNumber = message.ShortItemNumber,
                Payload = message.Payload
            };
            _bus.Send(requestCommand);

            Logger.Info("Sending acknowldegement response back to SAP.");
            var responseCommand = new ResponseToSapRequestCommand
            {
                NumberId = message.ShortItemNumber,
                InboundType = EnumInboundType.MaterialMasterUpdate
            };
            _sendToSap.SendSoapMessageToSap(responseCommand);

            Logger.Info("Sending audit trail of response.");
            var responseAuditCommand = new MaterialMasterUpdateAuditCommand
            {
                ShortItemNumber = message.ShortItemNumber,
                InboundId = message.InboundId,
                MessageType = typeof(ResponseToSapRequestCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendResponseToSap,
                MessageData = responseCommand.ToString(),
                Leg = leg11
            };
            _bus.Send(responseAuditCommand);
        }
    }
}