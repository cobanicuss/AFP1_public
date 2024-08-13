namespace Spm.Shared
{
    public class Constants
    {
        public const string SapSoapDevAndTestUserName = "BSLORRC";
        public const string SapSoapDevAndTestPassword = "Orr@2017";

        public const string SapSoapProdUserName = "BSLORRC";
        public const string SapSoapProdPassword = "Bsl@2017";

        public const string LogFilePath = @"C:\LogFiles\Spm\";

        public const string SpmServiceForSoap = @"SpmServiceForSoap\";
        public const string SpmService = @"SpmService\";
        public const string SpmAuditLogService = @"SpmAuditLogService\";
        public const string SpmOrrSysService = @"SpmOrrSysService\";
        public const string SpmFileWatcherService = @"SpmFileWatcherService\";

        public const string PurchaseOrderCreate = @"PurchaseOrderCreate\";
        public const string PurchaseOrderChange = @"PurchaseOrderChange\";
        public const string GoodsReceipt = @"GoodsReceipt\";
        public const string GoodsReversal = @"GoodsReversal\";
        public const string MaterialMaster = @"MaterialMaster\";
        public const string GeneralLedger = @"GeneralLedger\";
        public const string ProductAchievement = @"ProductAchievement\";
        public const string ProductionOrderStatus = @"ProductionOrderStatus\";
        public const string TestCertificate = @"TestCertificate\";
        public const string ResponseOnRequest = @"ResponseOnRequest\";
        public const string TestCertificateInbound = @"TestCertificateInbound\";
        public const string MaterialMasterInbound = @"MaterialMasterInbound\";
        
        public const string BaseLocation = @"\\stlsalfp01.steel.bhpsteel.net\JDE_INT\SPM\";
        public const string PurchaseOrderCreateFileName = "PURCHASE_ORDER_CREATE";
        public const string PurchaseOrderChangeFileName = "PURCHASE_ORDER_CHANGE";
        public const string GoodsReceiptFileName = "GOODS_RECEIPTS_CREATE";
        public const string GoodsReversalFileName = "GOODS_RECEIPTS_CHANGE";
        public const string MaterialMasterFileName = "PRODUCT_";
        public const string GeneralLedgerFileName = "JDEGL_";
        public const string FileExtension = ".CSV";

        public static string ServiceForSoapLogFilePath = $"{LogFilePath}{SpmServiceForSoap}SpmServiceForSoap.log";
        public static string AuditLogServiceLogFilePath = $"{LogFilePath}{SpmAuditLogService}SpmAuditLogService.log";
        public static string OrrSysServiceLogFilePath = $"{LogFilePath}{SpmOrrSysService}SpmOrrSysService.log";
        public static string SpmServiceLogFilePath = $"{LogFilePath}{SpmService}SpmService.log";
        public static string FileWatcherServiceLogFilePath = $"{LogFilePath}{SpmFileWatcherService}SpmFileWatcherService.log";

        public const string PoCreate = "Create";
        public const string PoChange = "Change";

        public const string GoodsReceiptType = "Receipt";
        public const string GoodsReversalType = "Reversal";

        public const int GeneralLedgerFileCount = 5000;
        public const int GoodsFileCount = 45000;
        public const int GoodsReceiptFileCount = 25000;
        public const int GoodsReversalFileCount = 25000;
        public const int MaterailMasterFileCount = 40000;
        public const int ProductAchievementFileCount = 232500;
        public const int ProductionOrderStatusFileCount = 25500;
        public const int PurchaseOrderChangeFileCount = 13000;
        public const int PurchaseOrderCreateFileCount = 17250;
        public const int ResponseOnRequestFileCount = 500;
        public const int TestCertificateFileCount =80000;

        public const string ErrorQueueName = "error";
        public const string AuditQueueName = "audit";

        public const string NotAvailable = "na";
    }
}
