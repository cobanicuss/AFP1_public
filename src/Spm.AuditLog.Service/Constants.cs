namespace Spm.AuditLog.Service
{
    public class Constants
    {
        public const string AuditLogServiceHandlers = @"SPM.AUDITLOG.SERVICE.HANDLERS";
        public const string AuditLogServiceSagas = @"SPM.AUDITLOG.SERVICE.SAGAS";

        public const string OutputPath = @"\\stlsalfp01.steel.bhpsteel.net\JDE_INT\SPM\";

        public const string HistoryForDev = @"AuditLogDev\";
        public const string HistoryForTest = @"AuditLogTest\";
        public const string HistoryForProd = @"AuditLogProd\";
        
        public static string GeneralLedgerFolder = @"GeneralLedger\";
        public static string GeneralLedgerBcpFormatFileName = "GeneralLedgerFormat.xml";
        public static string GeneralLedgerBcpDataFileName = "GeneralLedgerData.bcp";
        public static string GeneralLedgerErrorFileName = "GeneralLedgerError.log";
        public static string GeneralLedgerLogFileName = "GeneralLedgerLog.log";
        public static string GeneralLedgerTableName = "GeneralLedgerAuditLog";

        public static string GoodsReceiptFolder = @"GoodsReceipt\";
        public static string GoodsReceiptBcpFormatFileName = "GoodsReceiptFormat.xml";
        public static string GoodsReceiptBcpDataFileName = "GoodsReceiptData.bcp";
        public static string GoodsReceiptErrorFileName = "GoodsReceiptError.log";
        public static string GoodsReceiptLogFileName = "GoodsReceiptLog.log";
        public static string GoodsReceiptTableName = "GoodsReceiptAuditLog";

        public static string GoodsReversalFolder = @"GoodsReversal\";
        public static string GoodsReversalBcpFormatFileName = "GoodsReversalFormat.xml";
        public static string GoodsReversalBcpDataFileName = "GoodsReversalData.bcp";
        public static string GoodsReversalErrorFileName = "GoodsReversalError.log";
        public static string GoodsReversalLogFileName = "GoodsReversalLog.log";
        public static string GoodsReversalTableName = "GoodsReversalAuditLog";

        public static string MaterialMasterFolder = @"MaterialMaster\";
        public static string MaterialMasterBcpFormatFileName = "MaterialMasterFormat.xml";
        public static string MaterialMasterBcpDataFileName = "MaterialMasterData.bcp";
        public static string MaterialMasterErrorFileName = "MaterialMasterError.log";
        public static string MaterialMasterLogFileName = "MaterialMasterLog.log";
        public static string MaterialMasterTableName = "MaterialMasterAuditLog";

        public static string ProductAchievementFolder = @"ProductAchievement\";
        public static string ProductAchievementBcpFormatFileName = "ProductAchievementFormat.xml";
        public static string ProductAchievementBcpDataFileName = "ProductAchievementData.bcp";
        public static string ProductAchievementErrorFileName = "ProductAchievementError.log";
        public static string ProductAchievementLogFileName = "ProductAchievementLog.log";
        public static string ProductAchievementTableName = "ProductAchievementAuditLog";

        public static string PurchaseOrderChangeFolder = @"PurchaseOrderChange\";
        public static string PurchaseOrderChangeBcpFormatFileName = "PurchaseOrderChangeFormat.xml";
        public static string PurchaseOrderChangeBcpDataFileName = "PurchaseOrderChangeData.bcp";
        public static string PurchaseOrderChangeErrorFileName = "PurchaseOrderChangeError.log";
        public static string PurchaseOrderChangeLogFileName = "PurchaseOrderChangeLog.log";
        public static string PurchaseOrderChangeTableName = "PurchaseOrderChangeAuditLog";

        public static string PurchaseOrderCreateFolder = @"PurchaseOrderCreate\";
        public static string PurchaseOrderCreateBcpFormatFileName = "PurchaseOrderCreateFormat.xml";
        public static string PurchaseOrderCreateBcpDataFileName = "PurchaseOrderCreateData.bcp";
        public static string PurchaseOrderCreateErrorFileName = "PurchaseOrderCreateError.log";
        public static string PurchaseOrderCreateLogFileName = "PurchaseOrderCreateLog.log";
        public static string PurchaseOrderCreateTableName = "PurchaseOrderCreateAuditLog";

        public static string ProductionOrderStatusFolder = @"ProductionOrderStatus\";
        public static string ProductionOrderStatusBcpFormatFileName = "ProductionOrderStatusFormat.xml";
        public static string ProductionOrderStatusBcpDataFileName = "ProductionOrderStatusData.bcp";
        public static string ProductionOrderStatusErrorFileName = "ProductionOrderStatusError.log";
        public static string ProductionOrderStatusLogFileName = "ProductionOrderStatusLog.log";
        public static string ProductionOrderStatusTableName = "ProductionOrderStatusAuditLog";

        public static string TestCertificateRequestFolder = @"TestCertificateRequest\";
        public static string TestCertificateRequestBcpFormatFileName = "TestCertificateRequestFormat.xml";
        public static string TestCertificateRequestBcpDataFileName = "TestCertificateRequestData.bcp";
        public static string TestCertificateRequestErrorFileName = "TestCertificateRequestError.log";
        public static string TestCertificateRequestLogFileName = "TestCertificateRequestLog.log";
        public static string TestCertificateRequestTableName = "TestCertificateRequestAuditLog";

        public static string TestCertificateFolder = @"TestCertificate\";
        public static string TestCertificateBcpFormatFileName = "TestCertificateFormat.xml";
        public static string TestCertificateBcpDataFileName = "TestCertificateData.bcp";
        public static string TestCertificateErrorFileName = "TestCertificateError.log";
        public static string TestCertificateLogFileName = "TestCertificateLog.log";
        public static string TestCertificateTableName = "TestCertificateAuditLog";

        public static string MaterialMasterUpdateFolder = @"MaterialMasterUpdate\";
        public static string MaterialMasterUpdateBcpFormatFileName = "MaterialMasterUpdateFormat.xml";
        public static string MaterialMasterUpdateBcpDataFileName = "MaterialMasterUpdateData.bcp";
        public static string MaterialMasterUpdateErrorFileName = "MaterialMasterUpdateError.log";
        public static string MaterialMasterUpdateLogFileName = "MaterialMasterUpdateLog.log";
        public static string MaterialMasterUpdateTableName = "MaterialMasterUpdateAuditLog";

        public static string PlannedOrderFolder = @"PlannedOrder\";
        public static string PlannedOrderBcpFormatFileName = "PlannedOrderFormat.xml";
        public static string PlannedOrderBcpDataFileName = "PlannedOrderData.bcp";
        public static string PlannedOrderErrorFileName = "PlannedOrderError.log";
        public static string PlannedOrderLogFileName = "PlannedOrderLog.log";
        public static string PlannedOrderTableName = "PlannedOrderAuditLog";

        public static string ProductionOrderFolder = @"ProductionOrder\";
        public static string ProductionOrderBcpFormatFileName = "ProductionOrderFormat.xml";
        public static string ProductionOrderBcpDataFileName = "ProductionOrderData.bcp";
        public static string ProductionOrderErrorFileName = "ProductionOrderError.log";
        public static string ProductionOrderLogFileName = "ProductionOrderLog.log";
        public static string ProductionOrderTableName = "ProductionOrderAuditLog";

        public static string AuditActionTypeTableName = "AuditActionType";

        public const float GeneralLedgerLegCount = 5.0F;
        public const float GoodsReceitpLegCount = 5.0F;
        public const float MaterialMasterLegCount = 5.0F;
        public const float MaterialMasterUpdateLegCount = 2.0F;
        public const float PurchaseOrderLegCount = 5.0F;
        public const float ProductAchievementLegCount = 6.0F;
        public const float ProductionOrderStatusLegCount = 5.0F;
        public const float TestCertificateRequestLegCount = 2.0F;
        public const float TestCertificateLegCount = 6.0F;
        public const float PlanndedOrderLegCount = 2.0F;
        public const float ProductionOrderLegCount = 2.0F;

        //public const int GeneralLedgerAuditLogCount = 60000;
        //public const int GoodsReceiptAuditLogCount = 150000;
        //public const int GoodsReversalAuditLogCount = 10000;
        //public const int MaterialMasterAuditLogCount = 10000;
        //public const int ProductAchievementAuditLogCount = 900000;
        //public const int ProductionOrderStatusAuditLogCount = 100000;
        //public const int PurchaseOrderChangeAuditLogCount = 30000;
        //public const int PurchaseOrderCreateAuditLogCount = 60000;
        //public const int TestCertificateAuditLogCount = 200000;
        //public const int TestCertificateRequestAuditLogCount = 2200;
        //public const int MaterialMasterUpdateAuditLogCount = 2000;
        //public const int PlannedOrderAuditLogCount = 1000;
        //public const int ProductionOrderAuditLogCount = 1000;

        public const int GeneralLedgerAuditLogCount = 2;
        public const int GoodsReceiptAuditLogCount = 2;
        public const int GoodsReversalAuditLogCount = 2;
        public const int MaterialMasterAuditLogCount = 2;
        public const int ProductAchievementAuditLogCount = 2;
        public const int ProductionOrderStatusAuditLogCount = 2;
        public const int PurchaseOrderChangeAuditLogCount = 2;
        public const int PurchaseOrderCreateAuditLogCount = 2;
        public const int TestCertificateAuditLogCount = 2;
        public const int TestCertificateRequestAuditLogCount = 2;
        public const int MaterialMasterUpdateAuditLogCount = 2;
        public const int PlannedOrderAuditLogCount = 2;
        public const int ProductionOrderAuditLogCount = 2;

        public const int AuditLogIsBusyDeferalPeriodMinutes = 5;
        //public const int AuditLogSchedulerDataExportPeriodMinutes = 720; //once every 12hours. 12h=720min//
        public const int AuditLogSchedulerDataExportPeriodMinutes = 10; //once every 12hours. 12h=720min//
    }
}