namespace Spm.OrrSys.Service
{
    public class Constants
    {
        public const string OrrSysServiceHandlers = @"SPM.ORRSYS.SERVICE.HANDLERS";
        public const string OrrSysServiceSagas = @"SPM.ORRSYS.SERVICE.SAGAS";

        public const string TableNameF3460Z1 = "OrrSys_F3460Z1";
        public const string TableNameF3460Z1History = "OrrSys_F3460Z1_History";
        public const string TableNameF554801Z = "OrrSysF554801Z";
        public const string TableNameF554801ZHistory = "OrrSysF554801Z_History";
        public const string TableNameOrrSysProcessVariable = "OrrSysProcessVariable";

        public const int PlannedOrderHistoryKeptDays = -35;
        public const int ProductionOrderHistoryKeptDays = -35;

        public const string TableNameTestCert = "DespatchedPacksByCustomerOrder";

        public const uint OrrSysSchedulerSendPeriodMinutes = 10;

        public const int CertificateFunctionCode = 9;
        public const string SellerPartyIdScheme = "LS";
        public const string CustomerPartyIdentifier = "BSLDistribution";
        public const string FileType = "PDF";
        public const string EncodingType = "Base64";
        public const string MaterialReferenceTypeCode = "SN";
        public const string FileIdentifier = "TC";

        public const string TestCertificateFolderDev = @"C:\LogFiles\SPM\TestCert\";
        public const string TestCertificateBakupFolderDev = @"C:\LogFiles\SPM\TestCertBakup\";

        public const string TestCertificateFolderTest = TestCertificateFolderDev;
        public const string TestCertificateBackupFolderTest = TestCertificateBakupFolderDev;

        public const string TestCertificateFolderProd = @"R:\Data\Quality\Test Certificate\Manufacturing Test Certificates\Archived Test Certificates\";
        public const string TestCertificateBakupFolderProd = TestCertificateBakupFolderDev;

        public const int TestCertificateBufferSizeDev = 300;
        public const int TestCertificateBufferSizeTest = 2000;
        public const int TestCertificateBufferSizeProd = 10000;
    }
}