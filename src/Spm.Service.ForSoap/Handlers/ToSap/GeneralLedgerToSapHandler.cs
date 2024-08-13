using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.ToSap
{
    public class GeneralLedgerToSapHandler : IHandleMessages<GeneralLedgerSapCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerToSapHandler));
        private readonly IBus _bus;
        private readonly ISendGeneralLedgerToSap _sendSoapToSap;

        public GeneralLedgerToSapHandler(IBus bus, ISendGeneralLedgerToSap sendSoapToSap)
        {
            _bus = bus;
            _sendSoapToSap = sendSoapToSap;
        }

        public void Handle(GeneralLedgerSapCommand message)
        {
            const float leg = 3.0F;

            Logger.Info("======================================");
            Logger.Info("Have received a message from the SAGA.");
            Logger.Info("Now attempting to send a message to SAP.");

            _sendSoapToSap.SendSoapMessageToSap(message);

            Logger.Info("Message send successfully.");
            var generalLedgerAuditCommand = new GeneralLedgerAuditCommand
            {
                SagaReferenceId = message.SagaReferenceId,
                GeneralLedgerId = message.GeneralLedgerId,
                MessageType = typeof(GeneralLedgerSapCommand).FullName,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendRequestToSap,
                MessageData = message.ToString(),
                Leg = leg
            };

            _bus.Send(generalLedgerAuditCommand);
        }
    }
}