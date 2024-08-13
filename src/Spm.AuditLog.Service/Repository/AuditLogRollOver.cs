using NServiceBus.Logging;
using Spm.AuditLog.Service.Config;
using Spm.AuditLog.Service.Dto;

namespace Spm.AuditLog.Service.Repository
{
    public class AuditLogRollOver : IImplementAuditLogRollOver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuditLogRepository));
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogRollOver(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        
        public void RollOverGeneralLedgerAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.GeneralLedgerTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.GeneralLedgerAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath =$"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.GeneralLedgerFolder}",
                BcpFormatFileName = Constants.GeneralLedgerBcpFormatFileName,
                BcpDataFileName = Constants.GeneralLedgerBcpDataFileName,
                ErrorFileName = Constants.GeneralLedgerErrorFileName,
                LogFileName = Constants.GeneralLedgerLogFileName,
                TableName = Constants.GeneralLedgerTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.GeneralLedgerTableName);
        }

        public void RollOverGoodsReceiptAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.GoodsReceiptTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.GoodsReceiptAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.GoodsReceiptFolder}",
                BcpFormatFileName = Constants.GoodsReceiptBcpFormatFileName,
                BcpDataFileName = Constants.GoodsReceiptBcpDataFileName,
                ErrorFileName = Constants.GoodsReceiptErrorFileName,
                LogFileName = Constants.GoodsReceiptLogFileName,
                TableName = Constants.GoodsReceiptTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.GoodsReceiptTableName);
        }

        public void RollOverGoodsReversalAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.GoodsReversalTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.GoodsReversalAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.GoodsReversalFolder}",
                BcpFormatFileName = Constants.GoodsReversalBcpFormatFileName,
                BcpDataFileName = Constants.GoodsReversalBcpDataFileName,
                ErrorFileName = Constants.GoodsReversalErrorFileName,
                LogFileName = Constants.GoodsReversalLogFileName,
                TableName = Constants.GoodsReversalTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.GoodsReversalTableName);
        }

        public void RollOverMaterialMasterAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.MaterialMasterTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.MaterialMasterAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.MaterialMasterFolder}",
                BcpFormatFileName = Constants.MaterialMasterBcpFormatFileName,
                BcpDataFileName = Constants.MaterialMasterBcpDataFileName,
                ErrorFileName = Constants.MaterialMasterErrorFileName,
                LogFileName = Constants.MaterialMasterLogFileName,
                TableName = Constants.MaterialMasterTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.MaterialMasterTableName);
        }

        public void RollOverProductAchievementAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.ProductAchievementTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.ProductAchievementAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.ProductAchievementFolder}",
                BcpFormatFileName = Constants.ProductAchievementBcpFormatFileName,
                BcpDataFileName = Constants.ProductAchievementBcpDataFileName,
                ErrorFileName = Constants.ProductAchievementErrorFileName,
                LogFileName = Constants.ProductAchievementLogFileName,
                TableName = Constants.ProductAchievementTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.ProductAchievementTableName);
        }

        public void RollOverProductionOrderStatusAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.ProductionOrderStatusTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.ProductionOrderStatusAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.ProductionOrderStatusFolder}",
                BcpFormatFileName = Constants.ProductionOrderStatusBcpFormatFileName,
                BcpDataFileName = Constants.ProductionOrderStatusBcpDataFileName,
                ErrorFileName = Constants.ProductionOrderStatusErrorFileName,
                LogFileName = Constants.ProductionOrderStatusLogFileName,
                TableName = Constants.ProductionOrderStatusTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.ProductionOrderStatusTableName);
        }

        public void RollOverPurchaseOrderChangeAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.PurchaseOrderChangeTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.PurchaseOrderChangeAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.PurchaseOrderChangeFolder}",
                BcpFormatFileName = Constants.PurchaseOrderChangeBcpFormatFileName,
                BcpDataFileName = Constants.PurchaseOrderChangeBcpDataFileName,
                ErrorFileName = Constants.PurchaseOrderChangeErrorFileName,
                LogFileName = Constants.PurchaseOrderChangeLogFileName,
                TableName = Constants.PurchaseOrderChangeTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.PurchaseOrderChangeTableName);
        }

        public void RollOverPurchaseOrderCreateAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.PurchaseOrderCreateTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.PurchaseOrderCreateAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.PurchaseOrderCreateFolder}",
                BcpFormatFileName = Constants.PurchaseOrderCreateBcpFormatFileName,
                BcpDataFileName = Constants.PurchaseOrderCreateBcpDataFileName,
                ErrorFileName = Constants.PurchaseOrderCreateErrorFileName,
                LogFileName = Constants.PurchaseOrderCreateLogFileName,
                TableName = Constants.PurchaseOrderCreateTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.PurchaseOrderCreateTableName);
        }

        public void RollOverTestCertifcateRequestAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.TestCertificateRequestTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.TestCertificateRequestAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.TestCertificateRequestFolder}",
                BcpFormatFileName = Constants.TestCertificateRequestBcpFormatFileName,
                BcpDataFileName = Constants.TestCertificateRequestBcpDataFileName,
                ErrorFileName = Constants.TestCertificateRequestErrorFileName,
                LogFileName = Constants.TestCertificateRequestLogFileName,
                TableName = Constants.TestCertificateRequestTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.TestCertificateRequestTableName);
        }

        public void RollOverTestCertifcateAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.TestCertificateTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.TestCertificateAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.TestCertificateFolder}",
                BcpFormatFileName = Constants.TestCertificateBcpFormatFileName,
                BcpDataFileName = Constants.TestCertificateBcpDataFileName,
                ErrorFileName = Constants.TestCertificateErrorFileName,
                LogFileName = Constants.TestCertificateLogFileName,
                TableName = Constants.TestCertificateTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.TestCertificateTableName);
        }

        public void RollOverPlannedOrderAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.PlannedOrderTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.PlannedOrderAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.PlannedOrderFolder}",
                BcpFormatFileName = Constants.PlannedOrderBcpFormatFileName,
                BcpDataFileName = Constants.PlannedOrderBcpDataFileName,
                ErrorFileName = Constants.PlannedOrderErrorFileName,
                LogFileName = Constants.PlannedOrderLogFileName,
                TableName = Constants.PlannedOrderTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.PlannedOrderTableName);
        }

        public void RollOverProductionOrderAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.ProductionOrderTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.ProductionOrderAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.ProductionOrderFolder}",
                BcpFormatFileName = Constants.ProductionOrderBcpFormatFileName,
                BcpDataFileName = Constants.ProductionOrderBcpDataFileName,
                ErrorFileName = Constants.ProductionOrderErrorFileName,
                LogFileName = Constants.ProductionOrderLogFileName,
                TableName = Constants.ProductionOrderTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.ProductionOrderTableName);
        }

        public void ReloadAuditActionType()
        {
            _auditLogRepository.CreateAllNewAuditActionTypes();
        }

        public void RollOverMaterialMasterUpdateAuditLog()
        {
            Logger.Info("Getting Count");

            var count = _auditLogRepository.GetCountOfThisAuditLogTable(Constants.MaterialMasterUpdateTableName);
            Logger.Info($"Count={count}");

            if (count < Constants.MaterialMasterUpdateAuditLogCount) return;

            Logger.Info("Proceeding with export.");
            var dto = new ExportSprocDto
            {
                OutputPath = $"{Constants.OutputPath}{ProfileConfigVariables.FolderContainingHistory}{Constants.MaterialMasterUpdateFolder}",
                BcpFormatFileName = Constants.MaterialMasterUpdateBcpFormatFileName,
                BcpDataFileName = Constants.MaterialMasterUpdateBcpDataFileName,
                ErrorFileName = Constants.MaterialMasterUpdateErrorFileName,
                LogFileName = Constants.MaterialMasterUpdateLogFileName,
                TableName = Constants.MaterialMasterUpdateTableName
            };

            Logger.Info("Exporting data to BCP file.");
            _auditLogRepository.ExportAuditLogToFile(dto);

            Logger.Info("Truncating the table.");
            _auditLogRepository.TruncateThisAuditLogTable(Constants.MaterialMasterUpdateTableName);
        }
    }
}