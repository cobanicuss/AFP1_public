using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.File.Watcher.Service.Validation;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public partial class MappingBusinessRulesTestBase
    {
        protected IDisplayErrors ErrorConditions { get; set; }
        protected IConvertDecimal ConvertDecimal { get; set; }
        protected IDoMappingBusinessRules ClassUnderTest { get; set; }
        protected IConvertDate ConvertDate { get; set; }

        protected List<CacheMapMaterialGroup> MapMaterialGroupList;
        protected List<CacheMapCostCentreGlPosting> MapCostCentreGlPostingList;
        protected List<CacheMapGlAccountsGlPosting> MapGlAccountsGlPostingsList;
        protected List<CacheMapUnitOfMeasure> MapUnitOfMeasureList;
        protected List<CacheMapPlant> MapPlantList;
        protected List<CacheMapBranch> MapPlantBranchList;
        protected List<CacheMapPurchaseGroup> MapPurchaseGroupList;
        protected List<CacheMapDocTypes> MapDocTypesList;
        protected List<CacheMapCompanyCode> MapCompanyCodeList;
        protected List<CacheMapLocation> MapLocationList;
        protected List<CacheMapProfitCentreGlPosting> MapProfitCentreGlPostingList;

        protected string VallidJdeStockType = $"{LnType}-{Plant}-{GstForN}";

        protected static string Plant = "a";
        protected static string LnType = "b";
        protected static string ExInGst = "N";
        protected static string GstForN = "EX";
        protected static string SapMatrialGroup = "c";
        protected static string SapCostCentre = "1";
        protected static string SapCostCentreOutput = "0000000001";
        protected static string CostCentre = "2";
        protected static string JdeDepartmentCode = "4";
        protected static string SapGlAccount = "5";
        protected static string SapGlAccountOutput = "0000000005";
        protected static string GlAccount = "6";
        protected static string PoUnit = "d";
        protected static string IsoUom = "e";
        protected static string DefaultIsoUom = "f";
        protected static string SapPlant = "g";
        protected static string PurchaseGroup = "i";
        protected static string SapPurchaseGroup = "j";
        protected static string DefaultSapPurchaseGroup = "k";
        protected static string DocType = "l";
        protected static string SapDocType = "m";
        protected static string CompanyCode = "n";
        protected static string SapCompanyCode = "o";
        protected static string DefaultSapCompanyCode = "p";
        protected static string Location = "q";
        protected static string SapStorageLocation = "r";
        protected static string DefaultSapStorageLocation = "s";
        protected static string PurchOrgInput = "7";
        protected static string PurchOrgOutput = "0007";
        protected static string GmCodeInput = "8";
        protected static string GmCodeOutput = "08";
        protected static string RefDocNum = "zzz9";
        protected static string SapType = "t";
        protected static string PoItem = "123.000";
        protected static string PoItemOutput = "123";
        protected static string PoNumber = "7";
        protected static string PoNumberOutput = "0000000007";
        protected static string CreateDate = "29/02/16";
        protected static string CreateDateOutput = "20160229";
        protected static string DeliveryDate = string.Empty;
        protected static string PostingDate = string.Empty;
        protected static string PostingDateOutput = "20160203";
        protected static string SapProfitCentre = "8";
        protected static string SapProfitCentreOutput = "0000000008";
        protected static string NetPrice = string.Empty;
        protected static string Id = "u";
        protected static string HeaderText = $"{Service.Constants.GoodsReceiptHeaderPrefix}{Id}";
        protected static string GlDocDate = string.Empty;
        protected static string GlDocDateOutput = "20151222";
        protected static string GoodsDocDate = string.Empty;
        protected static string GoodsDocDateOutput = "20160201";
        protected static string Vendor = "v";
        protected static string JdePackWeight = string.Empty;
        protected static string Kg = string.Empty;
        protected static string M = string.Empty;
        protected static string DenominatorInput = string.Empty;
        protected static string DenominatorOutput = "10000";
        protected static string NumeratorInput = string.Empty;
        protected static string NumeratorOutput = "3333";
        protected static string Height = string.Empty;
        protected static string Width = string.Empty;
        protected static string PhysicalPackSizeOutput = string.Empty;
        protected static string Mcu = string.Empty;
        protected static string Sec1 = string.Empty;
        protected static string Sec2 = string.Empty;
        protected static string Dsc1 = string.Empty;
        protected static string Dsc2 = string.Empty;
        protected static string MaterailGroupPlant = string.Empty;
        protected static string ProductHierarcyPlant = string.Empty;
        protected static string ActualHeightInput = string.Empty;
        protected static string ProductAttributeInput = string.Empty;
        protected static string ThreeDecimalPlacesInput = string.Empty;
        protected static string ColorTextInput = string.Empty;
        protected static string BranchCode = string.Empty;
        protected static string StorageType = "w";
        protected static string Prp4 = string.Empty;
        protected static string Prp0 = string.Empty;
        protected static string Prp2Desc2 = string.Empty;
        protected static string Dsc12 = string.Empty;
        protected static string Spr1 = string.Empty;
        protected static string UnitCost = string.Empty;

        protected MappingBusinessRulesTestBase()
        {
            ErrorConditions = new ErrorDisplay();
            ConvertDecimal = new ConvertDecimal();
            ConvertDate = new ConvertDate();
            ClassUnderTest = new MappingBusinessRules(ErrorConditions, ConvertDecimal, ConvertDate);
        }

        protected virtual void ExecutingMapping() { }

        protected static void ResetRefDocNumAndGlAccount()
        {
            GlAccount = "6";
            RefDocNum = "zzz9";
        }
    }
}