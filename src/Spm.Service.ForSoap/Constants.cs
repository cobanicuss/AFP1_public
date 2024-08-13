namespace Spm.Service.ForSoap
{
    public class Constants
    {
        public static string PurchaseOrderCreateOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.PurchaseOrderCreate}";
        public static string PurchaseOrderChangeOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.PurchaseOrderChange}";
        public static string GoodsOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.GoodsReceipt}";
        public static string MaterialMasterOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.MaterialMaster}";
        public static string GeneralLedgerOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.GeneralLedger}";
        public static string ProductAchievementOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.ProductAchievement}";
        public static string ProductionOrderStatusOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.ProductionOrderStatus}";
        public static string TestCertificateOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.TestCertificate}";
        public static string ResponseOnRequestOutputFolder = $@"{Shared.Constants.LogFilePath}{SoapOutputFolderName}{Shared.Constants.ResponseOnRequest}";
        public static string TestCertificateInboundOutputFolder = $@"{ResponseOnRequestOutputFolder}{Shared.Constants.TestCertificateInbound}";
        public static string MaterialMasterUpdateInboundOutputFolder = $@"{ResponseOnRequestOutputFolder}{Shared.Constants.MaterialMasterInbound}";

        public const string ServiceForSoapHandlers = @"SPM.SERVICE.FORSOAP.HANDLERS";

        public const string DuplicateCheck = "&MessageId=";

        public const string EmptySoapBodyOn200ResponseIsOk = "ONE-WAY OPERATION RETURNED A NON-NULL MESSAGE WITH ACTION=''";

        public const string SoapOutputFolderName = @"XmlSendToSap\";
    }
}