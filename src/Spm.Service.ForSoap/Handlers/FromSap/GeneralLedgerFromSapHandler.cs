using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class GeneralLedgerFromSapHandler : IHandleMessages<GeneralLedgerSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerFromSapHandler));
        private readonly IBus _bus;

        public GeneralLedgerFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(GeneralLedgerSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("GeneralLedgerSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var generalLedgerAuditCommand = new GeneralLedgerAuditCommand
            {
                MessageType = typeof(GeneralLedgerSapResponse).FullName,
                SagaReferenceId = message.SagaReferenceId,
                GeneralLedgerId = Shared.Constants.NotAvailable,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(generalLedgerAuditCommand);

            var sagaCompleteCommand = new GeneralLedgerResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}