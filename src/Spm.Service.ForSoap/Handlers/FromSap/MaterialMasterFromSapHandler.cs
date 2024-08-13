using System;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class MaterialMasterFromSapHandler : IHandleMessages<MaterialMasterSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterFromSapHandler));
        private readonly IBus _bus;

        public MaterialMasterFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(MaterialMasterSapResponse message)
        {
            const float leg = 4.0F;

            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("MaterialMasterSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            var generalLedgerAuditCommand = new MaterialMasterAuditCommand
            {
                MessageType = typeof(MaterialMasterSapResponse).FullName,
                SagaReferenceId = message.SagaReferenceId,
                ReferenceId = Shared.Constants.NotAvailable,
                FromEndpoint = EndPointName.SpmServiceForSoap,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.ResponseReceivedFromSap,
                MessageData = message.ToString(),
                Leg = leg
            };
            _bus.Send(generalLedgerAuditCommand);

            var sagaCompleteCommand = new MaterialMasterResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}