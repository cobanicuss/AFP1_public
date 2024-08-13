using System;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Spm.AuditLog.Messages;
using Spm.AuditLog.Service.Repository;
using Spm.AuditLog.Service.SagaData;

namespace Spm.AuditLog.Service.Sagas
{
    public class AuditLogSchedulerSaga : Saga<AuditLogSchedulerSagaData>,
        IAmStartedByMessages<AuditLogSchedulerSagaInit>,
        IAmStartedByMessages<ProductAchievementAuditCommand>,
        IAmStartedByMessages<GeneralLedgerAuditCommand>,
        IAmStartedByMessages<GoodsReceiptAuditCommand>,
        IAmStartedByMessages<MaterialMasterAuditCommand>,
        IAmStartedByMessages<ProductionOrderStatusAuditCommand>,
        IAmStartedByMessages<PurchaseOrderAuditCommand>,
        IAmStartedByMessages<TestCertificateAuditCommand>,
        IAmStartedByMessages<TestCertificateRequestAuditCommand>,
        IAmStartedByMessages<MaterialMasterUpdateAuditCommand>,
        IAmStartedByMessages<PlannedOrderAuditCommand>,
        IAmStartedByMessages<ProductionOrderAuditCommand>,
        IHandleTimeouts<TimeToExportAuditLogToFile>
    {
        //IMPORTANT: SagaId is a constant (single instance) and is passed INTO the Saga.//
        //This is deliberately abstracted away for maintainability.//

        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuditLogSchedulerSaga));

        public IImplementAuditLogRollOver AuditLogRollOver { get; set; }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AuditLogSchedulerSagaData> mapper)
        {
            mapper.ConfigureMapping<AuditLogSchedulerSagaInit>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<ProductAchievementAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<GeneralLedgerAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<GoodsReceiptAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<PurchaseOrderAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<MaterialMasterAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<ProductionOrderStatusAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<TestCertificateAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<TestCertificateRequestAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<MaterialMasterUpdateAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<PlannedOrderAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
            mapper.ConfigureMapping<ProductionOrderAuditCommand>(m => m.AuditLogSchedulerSagaInitId).ToSaga(s => s.AuditLogSchedulerSagaInitId);
        }

        public void Handle(AuditLogSchedulerSagaInit message)
        {
            Logger.Info("======================================");
            Logger.Info("Saga started.");
            Logger.Info($"SagaId={message.AuditLogSchedulerSagaInitId}");

            Data.AuditLogSchedulerSagaInitId = message.AuditLogSchedulerSagaInitId;
            Data.IsDoingRollOver = false;

            SetNextTimeoutForAuditLogExportToFile();

            Logger.Info("Refreshing AuditActionType table.");
            AuditLogRollOver.ReloadAuditActionType();
        }

        public void Timeout(TimeToExportAuditLogToFile state)
        {
            Logger.Info("======================================");
            Logger.Info("Time to process ExportAuditLogToFile.");

            Data.IsDoingRollOver = true;
            RollAudtiLogOverIfNeeded();
            Data.IsDoingRollOver = false;

            SetNextTimeoutForAuditLogExportToFile();
        }

        private void RollAudtiLogOverIfNeeded()
        {
            Logger.Info("======================================");
            Logger.Info("Export to file if limit reached.");

            Logger.Info("RollOverGeneralLedgerAuditLog");
            AuditLogRollOver.RollOverGeneralLedgerAuditLog();

            Logger.Info("RollOverGoodsReceiptAuditLog");
            AuditLogRollOver.RollOverGoodsReceiptAuditLog();

            Logger.Info("RollOverGoodsReversalAuditLog");
            AuditLogRollOver.RollOverGoodsReversalAuditLog();

            Logger.Info("RollOverMaterialMasterAuditLog");
            AuditLogRollOver.RollOverMaterialMasterAuditLog();

            Logger.Info("RollOverProductAchievementAuditLog");
            AuditLogRollOver.RollOverProductAchievementAuditLog();

            Logger.Info("RollOverProductionOrderStatusAuditLog");
            AuditLogRollOver.RollOverProductionOrderStatusAuditLog();

            Logger.Info("RollOverPurchaseOrderChangeAuditLog");
            AuditLogRollOver.RollOverPurchaseOrderChangeAuditLog();

            Logger.Info("RollOverPurchaseOrderCreateAuditLog");
            AuditLogRollOver.RollOverPurchaseOrderCreateAuditLog();

            Logger.Info("RollOverTestCertificateRequestAuditLog");
            AuditLogRollOver.RollOverTestCertifcateRequestAuditLog();

            Logger.Info("RollOverTestCertificateAuditLog");
            AuditLogRollOver.RollOverTestCertifcateAuditLog();

            Logger.Info("RollOverProductionOrderAuditLog");
            AuditLogRollOver.RollOverProductionOrderAuditLog();

            Logger.Info("RollOverPlannedOrderAuditLog");
            AuditLogRollOver.RollOverPlannedOrderAuditLog();

            Logger.Info("RollOverMaterialMasterUpdateAuditLog");
            AuditLogRollOver.RollOverMaterialMasterUpdateAuditLog();
        }

        public void Handle(ProductAchievementAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle ProductAchievementAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new ProductAchievementCommitCommand { ProductAchievementAuditCommand = message });
        }

        public void Handle(GeneralLedgerAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle GeneralLedgerAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new GeneralLedgerCommitCommand { GeneralLedgerAuditCommand = message });
        }

        public void Handle(GoodsReceiptAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle GoodsReceiptAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new GoodsReceiptCommitCommand { GoodsReceiptAuditCommand = message });
        }

        public void Handle(MaterialMasterAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle MaterialMasterAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new MaterialMasterCommitCommand { MaterialMasterAuditCommand = message });
        }

        public void Handle(ProductionOrderStatusAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle ProductionOrderAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new ProductionOrderStatusCommitCommand { ProductionOrderStatusAuditCommand = message });
        }

        public void Handle(PurchaseOrderAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle PurchaseOrderAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new PurchaseOrderCommitCommand { PurchaseOrderAuditCommand = message });
        }

        public void Handle(TestCertificateRequestAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle TestCertificateRequestAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new TestCertificateRequestCommitCommand { TestCertificateRequestAuditCommand = message });
        }

        public void Handle(TestCertificateAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle TestCertificateAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new TestCertificateCommitCommand { TestCertificateAuditCommand = message });
        }

        public void Handle(MaterialMasterUpdateAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle MaterialMasterUpdateAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new MaterialMasterUpdateCommitCommand { MaterialMasterUpdateAuditCommand = message });
        }

        public void Handle(PlannedOrderAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle PlannedOrderAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new PlannedOrderCommitCommand { PlannedOrderAuditCommand = message });
        }

        public void Handle(ProductionOrderAuditCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Handle ProductionOrderAuditCommand");

            InitSagaIfNotInitialized(message.AuditLogSchedulerSagaInitId);

            if (Data.IsDoingRollOver)
            {
                Logger.Info("Is doing Roll-Over. Retry later.");
                Bus.Defer(TimeSpan.FromMinutes(Constants.AuditLogIsBusyDeferalPeriodMinutes), message);
                return;
            }

            Logger.Info("Send local to handle message.");
            Bus.SendLocal(new ProductionOrderCommitCommand { ProductionOrderAuditCommand = message });
        }

        private void InitSagaIfNotInitialized(string auditLogSchedulerSagaInitId)
        {
            if (!string.IsNullOrEmpty(Data.AuditLogSchedulerSagaInitId)) return;

            Data.AuditLogSchedulerSagaInitId = auditLogSchedulerSagaInitId;
            Data.IsDoingRollOver = false;
        }

        private void SetNextTimeoutForAuditLogExportToFile()
        {
            const int everyXminutes = Constants.AuditLogSchedulerDataExportPeriodMinutes;
            const float onceEveryXhours = everyXminutes / 60f;

            Logger.Info("AuditLog Roll Over:");
            Logger.Info($"Scheduled to run {everyXminutes} minutes from now. {onceEveryXhours} Hours.");
            RequestTimeout<TimeToExportAuditLogToFile>(TimeSpan.FromMinutes(everyXminutes));
        }
    }
}