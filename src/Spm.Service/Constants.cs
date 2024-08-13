namespace Spm.Service
{
    public class Constants
    {
        public const string SpmServiceSagas = @"SPM.SERVICE.SAGAS";

        public const int PurchaseOrderCreateTimeoutMinutes = 5;
        public const int PurchaseOrderCreateRetry = 1;

        public const int PurchaseOrderChangeTimeoutMinutes = 5;
        public const int PurchaseOrderChangeRetry = 1;

        public const int ProductAchievementTimeoutMinutes = 5;
        public const int ProductAchievementRetry = 1;

        public const int ProductionOrderStatusTimeoutMinutes = 5;
        public const int ProductionOrderStatusRetry = 1;

        public const int GoodsReceiptTimeoutMinutes = 5;
        public const int GoodsReceiptRetry = 1;

        public const int GeneralLedgerTimeoutMinutes = 5;
        public const int GeneralLedgerRetry = 1;

        public const int MaterialMasterTimeoutMinutes = 5;
        public const int MaterialMasterRetry = 1;

        public const int TestCertificateTimeoutMinutes = 5;
        public const int TestCertificateRetry = 1;
    }
}