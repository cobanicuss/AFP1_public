namespace Spm.File.Watcher.Service.SapJdeMap
{
    public static class MapDefaults
    {
        public const string DefaultSchedLine = "0001";
        public const string DefaultStgeLoc = "0001";
        public const string DefaultDoubleZero = "00";
        public const string DefaultQuadZero = "0000";
        public const string DefaultUnitOfMeasure = "ML";
        public const string DefaultBranch = "4510";
        public const string DefaultPurchGroup = "S";
        public const string DefaultGlDocType = "GJ";
        public const string DefaultCompanyCode = "BD008";
        public const string DefaultLocationCode = "";
        public const string DefaultUserName = "JDE_GL";
        public const string DefaultCompCode = "0070";
        public const string DefaultCurrency = "AUD";
        public const string DefaultItemIntvlCreate = "00000";
        public const string DefaultNetPrice = "0.0100";

        public static readonly string DefaultItemIntvlChange = string.Empty;

        public const string GoodsReceiptHeaderPrefix = "JDE_";

        public const string PurchaseOrderCreate = "POCreate";
        public const string PurchaseOrderChange = "POChange";

        public const string GoodsReceiptType = "101";
        public const string GoodsReversalType = "102";

        public const string SapGlAccount = "0000782500";
        public const string SapCostCentre = "0007045130";

        public const string RefDocStartsWithOv = "OV";
        public const string GlAccount1 = "94000";
        public const string GlAccount2 = "94050";

        public const string SapGlTypeOfProfit = "PROFIT";
        public const string SapGlTypeOfCost = "COST";
        
        public const string PurchaseOrderChangeSerialNo = "01";
    }
}