namespace Spm.AuditLog.Service.Repository
{
    public interface IImplementAuditLogRollOver
    {
        void RollOverGeneralLedgerAuditLog();
        void RollOverGoodsReceiptAuditLog();
        void RollOverGoodsReversalAuditLog();
        void RollOverMaterialMasterAuditLog();
        void RollOverMaterialMasterUpdateAuditLog();
        void RollOverProductAchievementAuditLog();
        void RollOverProductionOrderStatusAuditLog();
        void RollOverPurchaseOrderChangeAuditLog();
        void RollOverPurchaseOrderCreateAuditLog();
        void RollOverTestCertifcateRequestAuditLog();
        void RollOverTestCertifcateAuditLog();
        void RollOverPlannedOrderAuditLog();
        void RollOverProductionOrderAuditLog();
        void ReloadAuditActionType();
    }
}