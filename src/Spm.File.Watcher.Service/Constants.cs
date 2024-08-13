namespace Spm.File.Watcher.Service
{
    public class Constants
    {
        public const string SpmFileWatcherServiceSagas = @"SPM.FILE.WATCHER.SERVICE.SAGAS";

        public const string FileWentMissingDuringProcessing = @"The file was NOT found on the server. It was in the intial file list when reading the directory but now it is misssing!!! Deleted/Renamed/Moved. Cannot supply the original file for error. Uncool. CANNOT proceed!";
        public const string FileMissing = "File was deleted, renamed or moved during processing!!! Uncool. CANNOT proceed.";
        public const string FileIsMissingEarly = "File is ignored because file is missing. Hmm... Continue processing.";
        public const string FileIsLocked = "File is ignored because it is LOCKED. Whatever, continue processing.";
        public const string FileDataListIsNull = "File is ignored because it contains NO DATA (List IsNull). Weired?. Continue processing.";

        public const int MaterialMasterColumnCount = 46;
        public const int MaterialMasterSagaColumnCount = MaterialMasterColumnCount + 1;
        public const int GoodsColumnCount = 19;
        public const int GoodsSagaColumnCount = GoodsColumnCount + 1;
        public const int PurchaseOrderColumnCount = 33;
        public const int GeneralLedgerColumnCount = 10;

        public const char DefaultZero = '0';
        public const string DefaultTwoDigits = "0.00";
        public const string DefaultThreeDigits = "0.000";
        public const string DefaultDoubleZero = "00";
        public const string DefaultQuadZero = "0000";
        public const string DefaultPentaZero = "00000";
        public const string Default3Zeros1 = "0001";
        public const string Default5Zeros1 = "000001";
        public const string Default5Zeros2 = "000002";
        public const string Default7Zeros1 = "00000001";
        public const string Default2Zeros5 = "005";
        public const string Default2Zeros9 = "009";
        public const string Default2Zeros20 = "0020";
        public const string Default2Zeros70 = "0070";
        public const string Default1 = "1";
        public const string DefaultThreeHundred = "300";
        public const string DefaultOneThousand = "1000";

        public const string Salisbury = "420";
        public const string OsulivansBeach = "520";

        public const string Xsal = "TU-XSAL";
        public const string Xosb = "TU-XOSB";
        public const string Xs = "STTUXS";
        public const string Xo = "STTUXO";

        public const string DefaultUnitOfMeasure = "ML";
        public const string DefaultSapPlant = "4510";
        public const string DefaultPurchGroup = "S";

        public const string DefaultCompanyCode = "BD008";
        public const string DefaultLocationCode = "";
        public const string DefaultUserName = "JDE_GL";
        public const string DefaultCurrency = "AUD";
        public const string DefaultNetPrice = "0.0100";
        public const string DefaultMara = "MARA";
        public const string DefaultLength = "LENGTH";
        public const string DefaultLengthPerKgM = "C_LENGTH_KG_PER_METRE";
        public const string DefaultLengthActualHeight = "C_LENGTH_ACTUAL_HEIGHT";
        public const string DefaultLengthActualWidth = "C_LENGTH_ACTUAL_WIDTH";
        public const string DefaultLengthNrOfProcesses = "C_LENGTH_NO_OF_PROCESSES";

        public const string DefaultMvke = "MVKE";
        public const string DefaultEn = "EN";
        public const string DefaultTx = "TX";
        public const string DefaultStar = "*";
        public const string DefaultA = "A";
        public const string DefaultMaterial = "MATERIAL";
        public const string DefaultBest = "BEST";
        public const string DefaultPk = "PK";
        public const string DefaultM = "M";
        public const string DefaultEa = "EA";
        public const string DefaultTn = "TN";
        public const string DefaultPrp4 = "M06";
        public const string DefaultZg = "ZG";
        public const string DefaultKilograms = "KG";
        public const string DefaultKgp = "KGP";

        public const string Ex = "X";

        public const string Dsc1SearchString = "NB";

        public const string GoodsReceiptHeaderPrefix = "JDE_";

        public const string PurchaseOrderCreate = "POCreate";
        public const string PurchaseOrderChange = "POChange";
        public const string GenralLedger = "GenralLedger";

        public const string GoodsReceiptType = "101";
        public const string GoodsReversalType = "102";

        public const string SapGlAccount = "0000782500";
        public const string SapCostCentre = "0007045130";

        public const string RefDocStartsWithOv = "OV";

        public const string GlAccount1 = "94000";
        public const string GlAccount2 = "94050";

        public const string SapGlTypeOfProfit = "PROFIT";
        public const string SapGlTypeOfCost = "COST";

        public const string SalesTxtName1 = "007001";
        public const string SalesTxtName2 = "002001";

        public const string PurchaseOrderChangeSerialNo = "01";

        public static readonly string DefaultItemIntvlChange = string.Empty;

        public const string CreateDateName = "Create-Date";
        public const string DeliveryDateName = "Delivery-Date";
        public const string PostingDateName = "Posting-Date";
        public const string GlDocDateName = "Gl-Doc-Date";

        public const string MapMaterialGroup = "MapMaterialGroup";
        public const string JdeStockType = "JdeStockType";
        public const string MapGlAccountsGlPosting = "MapGlAccountsGlPosting";
        public const string JdeGlAccount = "JdeGlAccount";
        public const string MapProfitCentreGlPosting = "MapProfitCentreGlPosting";
        public const string JdeDepartmentCode = "JdeDepartmentCode";
        public const string MapCostCentreGlPosting = "MapCostCentreGlPosting";
        public const string MapUnitOfMeasure = "MapUnitOfMeasure";
        public const string JdeUom = "JdeUom";
        public const string JdeBranchCode = "JdeBranchCode";
        public const string MapPhysicalPackSize = "MapPhysicalPackSize";
        public const string MapBranch = "MapBranch";
        public const string MapPurchaseGroup = "MapPurchaseGroup";
        public const string JdePurchaseGroup = "JdePurchaseGroup";
        public const string MapDocTypes = "MapDocTypes";
        public const string JdeDocType = "JdeDocType";
        public const string MapCompanyCode = "MapCompanyCode";
        public const string JdeCompanyCode = "JdeCompanyCode";
        public const string MapLocation = "MapLocation";
        public const string JdeLocationCode = "JdeLocationCode";
        public const string JdeToSapDate = "Jde-Date to Sap-Date";
        public const string MapPlant = "MapPlant";
        public const string Plant = "plant";
        public const string MapProductHierarchy = "MapProductHierarchy";
        public const string MapStorageSection = "MapStorageSection";

        public const string StorageSection = "StorageSection";
        public const string StorageSection1 = "142";
        public const string StorageSection2 = "141";
        public const string StorageSection3 = "153";

        public const string Input = "input";

        public const int FileWatcherUploadPeriodMinutes = 1;
        public const int FileWatcherUploadBatchSize = 100;

        public const int MaxCacheMapRetryOnSagaBusy = 10;

        public const string LocalDestnDir = @"FilesProcessed\";

        public const string JdeExtractFileDateFormat = "d/M/yy";

        public const string SapDateFormat = "yyyyMMdd";

        public static string BadFileFormat(
            string lineNumber, 
            string fileName, 
            string expectedColumns,
            string receivedColumns)
        {
            return $"The file is BADLY formatted, UNCOOL!!!! Wrong column count in LineNumber={lineNumber}. FileName={fileName}, ExpectedColumns={expectedColumns}, ReceivedColumns={receivedColumns}.";
        }

        public const string GeneralLedgerTableName = "GeneralLedgerFileData";
        public const string GoodsReceiptTableName = "GoodsReceiptFileData";
        public const string GoodsReversalTableName = "GoodsReversalFileData";
        public const string MaterialMasterTableName = "MaterialMasterFileData";
        public const string PurchaseOrderChangeTableName = "PurchaseOrderChangeFileData";
        public const string PurchaseOrderCreateTableName = "PurchaseOrderCreateFileData";
    }
}