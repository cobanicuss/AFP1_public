using System;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Config;
using Spm.OrrSys.Service.Map;
using Spm.Shared;

namespace Spm.OrrSys.Service.Handlers
{
    public class TestCertificateOutboundTriggerCommandHandler : IHandleMessages<Messages.TestCertificateTriggerCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateOutboundTriggerCommandHandler));
        private readonly IBus _bus;
        private readonly IDoTestCertificateBusiness _testCertificate;
        private readonly IMapTestCertificateMessage _map;

        public TestCertificateOutboundTriggerCommandHandler(
            IBus bus,
            IDoTestCertificateBusiness testCertificate,
            IMapTestCertificateMessage map)
        {
            _bus = bus;
            _testCertificate = testCertificate;
            _map = map;
        }

        public void Handle(Messages.TestCertificateTriggerCommand message)
        {
            const float leg = 0.1F;

            Logger.Info("======================================");
            Logger.Info("Message received from external source");
            Logger.Info("via SpmWebService (inbound SOAP).");

            Logger.Info("Getting Test-Certificate-Outbound data.");
            var dataDtoList = _testCertificate.GetOutboundTestCertificateData();

            Logger.Info("Test-Certificate grouped by unique OutboundGroupId.");
            var uniqueReportGroupIdList = _testCertificate.GetByUniqueReportGroup(dataDtoList);

            var total = uniqueReportGroupIdList.Count;
            var current = 0;

            _testCertificate.DeletePreviousTestCertificates(
                ProfileConfigVariables.RestrictTestCertFiles,
                ProfileConfigVariables.TestCertBakupFolder,
                ProfileConfigVariables.TestCertificateBufferSize);

            var auditCommand = new TestCertificateAuditCommand
            {
                InboundId = message.InboundId,
                CertificateId = Shared.Constants.NotAvailable,
                SagaReferenceId = Shared.Constants.NotAvailable,
                MessageIndex = 0,
                MessageCount = 0,
                MessageType = typeof(Messages.TestCertificateTriggerCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.RequestReceived,
                MessageData = Shared.Constants.NotAvailable,
                Leg = leg
            };
            _bus.Send(auditCommand);

            Logger.Info($"Total trigger messages = {total}.");
            foreach (var reportGroupId in uniqueReportGroupIdList)
            {
                current++;

                var dtoList = dataDtoList.Where(x => x.ReportGroup == reportGroupId).ToList();

                var testCertificateMessage = _map.CreateTestCertificateFileCommandMessage(dtoList, current, total, message.InboundId);

                _bus.SendLocal(testCertificateMessage);
            }

            Logger.Info("All Outbound TestCertificates triggered.");
        }
    }
}