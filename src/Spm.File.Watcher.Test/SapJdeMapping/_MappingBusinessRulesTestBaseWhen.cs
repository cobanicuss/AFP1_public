using System;
using System.Collections.Generic;
using Spm.File.Watcher.Service;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public partial class MappingBusinessRulesTestBase
    {
        /// <summary>
        /// All the WHEN parts are done here
        /// </summary>

        protected void MaterialGroupMappingContainsJdeStockTypeOnly()
        {
            MapMaterialGroupList = new List<CacheMapMaterialGroup>
            {
                new CacheMapMaterialGroup { JdeStockType = VallidJdeStockType}
            };
        }

        protected void MaterialGroupMappingContainsSapMaterialGroup()
        {
            MapMaterialGroupList = new List<CacheMapMaterialGroup>
            {
                new CacheMapMaterialGroup { JdeStockType = VallidJdeStockType, SapMatrialGroup = SapMatrialGroup }
            };
        }

        protected void MaterialGroupMappingDoesNotContainJdeStockType()
        {
            MapMaterialGroupList = new List<CacheMapMaterialGroup>();
        }

        protected void MaterialGroupMappingContainsSapGlAccount()
        {
            MapMaterialGroupList = new List<CacheMapMaterialGroup>
            {
                new CacheMapMaterialGroup { JdeStockType = VallidJdeStockType, SapGlAcc = SapGlAccount }
            };
        }

        protected void GlAccountsGlPositingMappingDoesNotContainJdeGlAccountAndNoDefault()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>();
        }

        protected void GlAccountGlPositingMappingContainsJdeGlAccount()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = GlAccount, SapGlAccount = SapGlAccount}
            };
        }

        protected void GlAccountGlPositingMappingContainsDefaultJdeGlAccount()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = Constants.DefaultDoubleZero, SapGlAccount = SapGlAccount}
            };
        }

        protected void GlAccountGlPositingMappingContainsJdeGlAccountWithSapType()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = GlAccount, SapGlAccount = SapGlAccount, SapType = SapType}
            };
        }

        protected void GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultSapType()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = GlAccount, SapGlAccount = SapGlAccount, SapType = Constants.DefaultDoubleZero}
            };
        }

        protected void GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultCostSapType()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = GlAccount, SapGlAccount = SapGlAccount, SapType = Constants.SapGlTypeOfCost}
            };
        }

        protected void GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultProfitSapType()
        {
            MapGlAccountsGlPostingsList = new List<CacheMapGlAccountsGlPosting>
            {
                new CacheMapGlAccountsGlPosting { JdeGlAccount = GlAccount, SapGlAccount = SapGlAccount, SapType = Constants.SapGlTypeOfProfit}
            };
        }

        protected void MaterialGroupMappingContainsSapCostCenter()
        {
            MapMaterialGroupList = new List<CacheMapMaterialGroup>
            {
                new CacheMapMaterialGroup { JdeStockType = VallidJdeStockType, SapCostCentre = SapCostCentre }
            };
        }

        protected void CostCenterGlPostingMappingDoesNotContainSapCostCentre()
        {
            MapCostCentreGlPostingList = new List<CacheMapCostCentreGlPosting>();
        }

        protected void CostCenterGlPostingMappingDoesHaveSapCostCenter()
        {
            MapCostCentreGlPostingList = new List<CacheMapCostCentreGlPosting>
            {
                new CacheMapCostCentreGlPosting { JdeDepartmentCode = CostCentre, SapCostCentre = SapCostCentre }
            };
        }

        protected void CostCenterGlPostingMappingHasDefaultJdeDepartmentCode()
        {
            MapCostCentreGlPostingList = new List<CacheMapCostCentreGlPosting>
            {
                new CacheMapCostCentreGlPosting { JdeDepartmentCode = Constants.DefaultDoubleZero, SapCostCentre = SapCostCentre }
            };
        }

        protected void ProfitCenterGlPostingMappingDoesNotContainJdeDepartmentCode()
        {
            MapProfitCentreGlPostingList = new List<CacheMapProfitCentreGlPosting>();
        }

        protected void ProfitCenterGlPostingMappingDoesContainDefaultJdeDepartmentCode()
        {
            MapProfitCentreGlPostingList = new List<CacheMapProfitCentreGlPosting>
            {
                new CacheMapProfitCentreGlPosting{ JdeDepartmentCode = Constants.DefaultDoubleZero, SapProfitCentre = SapProfitCentre}
            };
        }

        protected void ProfitCenterGlPostingMappingDoesContainJdeDepartmentCode()
        {
            MapProfitCentreGlPostingList = new List<CacheMapProfitCentreGlPosting>
            {
                new CacheMapProfitCentreGlPosting{ JdeDepartmentCode = CostCentre, SapProfitCentre = SapProfitCentre}
            };
        }

        protected void UnitOfMeasureMappingDoesNotContainJdeUnitOfMeasureAndNoDefault()
        {
            MapUnitOfMeasureList = new List<CacheMapUnitOfMeasure>();
        }

        protected void UnitOfMeasureMappingDoesNotContainJdeUnitOfMeasureButDefaultExist()
        {
            MapUnitOfMeasureList = new List<CacheMapUnitOfMeasure>
            {
                new CacheMapUnitOfMeasure { IsoUom = DefaultIsoUom, JdeUom = Constants.DefaultUnitOfMeasure }
            };
        }

        protected void UnitOfMeasureMappingContainsJdeUnitOfMeasure()
        {
            MapUnitOfMeasureList = new List<CacheMapUnitOfMeasure>
            {
                new CacheMapUnitOfMeasure { IsoUom = IsoUom, JdeUom = PoUnit }
            };
        }

        protected void PlantMappingDoesNotContainJdeBranchCodeAndNoDefault()
        {
            MapPlantList = new List<CacheMapPlant>();
        }

        protected void ProperFractionProvidedForDenominator()
        {
            DenominatorInput = "0.3333";
        }

        protected void UnParsableFractionForNumerator()
        {
            NumeratorInput = "aa.bb";
        }

        protected void ProperFractionProvidedForNumerator()
        {
            NumeratorInput = "0.3333";
        }

        protected void PlantBranchMappingDoesNotContainJdeBranchCodeAndNoDefault()
        {
            MapPlantBranchList = new List<CacheMapBranch>();
        }

        protected void DefaultPlantAsInputValue()
        {
            Plant = Constants.OsulivansBeach;
            Height = "NotApplicableNow";
            Width = "NotApplicableNow";
        }

        protected void ValidPlantAndHeightAndWidthIsProvided()
        {
            Plant = "a1";
            Height = "10.1";
            Width = "10.6";
        }

        protected void PlantMappingDoesNotContainJdeBranchCodeButDefaultExist()
        {
            MapPlantList = new List<CacheMapPlant>
            {
                new CacheMapPlant { SapPlant = Constants.DefaultSapPlant, JdeBranchCode = Constants.Salisbury }
            };
        }

        protected void PlantMappingContainsJdeBranchCode()
        {
            MapPlantList = new List<CacheMapPlant>
            {
                new CacheMapPlant { SapPlant = SapPlant, JdeBranchCode = Plant }
            };
        }

        protected void PlantBranchMappingContainsJdeBranchCode()
        {
            MapPlantBranchList = new List<CacheMapBranch>
            {
                new CacheMapBranch { SapPlant = SapPlant, JdeBranchCode = Plant }
            };
        }

        protected void PurchaseGroupMappingDoesNotContainJdePurchaseGroupAndNoDefault()
        {
            MapPurchaseGroupList = new List<CacheMapPurchaseGroup>();
        }

        protected void PurchaseGroupMappingDoesNotContainJdePurchaseGroupButDefaultExists()
        {
            MapPurchaseGroupList = new List<CacheMapPurchaseGroup>
            {
                new CacheMapPurchaseGroup { SapPurchaseGroup = DefaultSapPurchaseGroup, JdePurchaseGroup = Constants.DefaultPurchGroup }
            };
        }

        protected void PurchaseGroupMappingContainsJdePurchaseGroup()
        {
            MapPurchaseGroupList = new List<CacheMapPurchaseGroup>
            {
                new CacheMapPurchaseGroup { SapPurchaseGroup = SapPurchaseGroup, JdePurchaseGroup = PurchaseGroup }
            };
        }

        protected void DocTypeMappingDoesNotContainJdeDocType()
        {
            MapDocTypesList = new List<CacheMapDocTypes>();
        }

        protected void DocTypeMappingDoesContainJdeDocType()
        {
            MapDocTypesList = new List<CacheMapDocTypes>
            {
                new CacheMapDocTypes { SapDocType = SapDocType, JdeDocType = DocType }
            };
        }

        protected void CompanyCodeMappingDoesNotContainJdeCompanyCodeAndNoDefault()
        {
            MapCompanyCodeList = new List<CacheMapCompanyCode>();
        }

        protected void CompanyCodeMappingDoesNotContainJdeCompanyCodeButDefaultExists()
        {
            MapCompanyCodeList = new List<CacheMapCompanyCode>
            {
                new CacheMapCompanyCode { SapCompanyCode = DefaultSapCompanyCode, JdeCompanyCode = Constants.DefaultCompanyCode }
            };
        }

        protected void CompanyCodeMappingContainsJdeCompanyCode()
        {
            MapCompanyCodeList = new List<CacheMapCompanyCode>
            {
                new CacheMapCompanyCode { SapCompanyCode = SapCompanyCode, JdeCompanyCode = CompanyCode }
            };
        }

        protected void LocationMappingDoesNotContainJdeLocationAndNoDefault()
        {
            MapLocationList = new List<CacheMapLocation>();
        }

        protected void LocationMappingDoesNotContainJdeLocationButDefaultExists()
        {
            MapLocationList = new List<CacheMapLocation>
            {
                new CacheMapLocation { SapStorageLocation = DefaultSapStorageLocation, JdeLocationCode = Constants.DefaultLocationCode }
            };
        }

        protected void LocationMappingContainsJdeLocation()
        {
            MapLocationList = new List<CacheMapLocation>
            {
                new CacheMapLocation { SapStorageLocation = SapStorageLocation, JdeLocationCode = Location }
            };
        }

        protected void ReferenceDocumentNumberStartsWithOv()
        {
            RefDocNum = "OVzzzzzzzz";
        }

        protected void GlAccountDoesNotMatchAnyDefaults()
        {
            GlAccount = "UnMatchedGlAccount";
        }

        protected void DeliveryDateIsInThePast()
        {
            DeliveryDate = DateTime.Today.AddMonths(-1).ToString(Constants.JdeExtractFileDateFormat);
        }

        protected void DeliveryDateIsInTheFuture()
        {
            DeliveryDate = DateTime.Today.AddMonths(1).ToString(Constants.JdeExtractFileDateFormat);
        }

        protected void DeliveryDateIsToday()
        {
            DeliveryDate = DateTime.Today.ToString(Constants.JdeExtractFileDateFormat);
        }

        protected void PostingDateIsValid()
        {
            PostingDate = "03/02/16";
        }

        protected void NetPriceIsToSmall()
        {
            NetPrice = "0.001";
        }

        protected void NetPriceIsCorrect()
        {
            NetPrice = "1.2345";
        }

        protected void GlDocDateIsValid()
        {
            GlDocDate = "22/12/15";
        }

        protected void GoodsDocDateIsInvalid()
        {
            GoodsDocDate = "BadGoodsDocDate";
        }

        protected void GoodsDocDateIsValid()
        {
            GoodsDocDate = "01/02/16";
        }

        protected void PassingInValidJdePackWeightWithMoreThan3Digits()
        {
            JdePackWeight = "12345.123567";
        }

        protected void PassingInValidJdePackWeightWith3Digits()
        {
            JdePackWeight = "12345.125";
        }

        protected void PassingInValidJdePackWeightWithLessThan3Digits()
        {
            JdePackWeight = "12345.15";
        }

        protected void PassingInValidJdePackWeightWithZeroInFrontOfComma()
        {
            JdePackWeight = "0.15";
        }

        protected void PassingInValidKilogramsAndMeters()
        {
            Kg = "123.123";
            M = "1234";
        }

        protected void PassingInValidKilogramsAndZeroMeters()
        {
            Kg = "123.123";
            M = "0";
        }

        protected void CorrectInputParamertsAreProvidedForDsc1()
        {
            Mcu = Constants.Salisbury;
            Sec1 = "2.5678";
            Sec2 = string.Empty;
        }

        protected void CorrectInputParamertsAreProvidedForSec1ForActualWidth()
        {
            Mcu = Constants.Salisbury;
            Sec1 = "5.5555";
            Sec2 = string.Empty;
        }

        protected void CorrectInputParamertsAreProvidedForSec1ForSizeOne()
        {
            Mcu = Constants.Salisbury;
            Sec1 = "5.5555";
            Dsc1 = string.Empty;
        }

        protected void CorrectInputParamertsAreProvidedForSec2()
        {
            Mcu = Constants.OsulivansBeach;
            Sec1 = "-";
            Sec2 = "5.5555";
        }

        protected void CorrectInputParamertsAreProvidedForDsc1ForSizeOne()
        {
            Mcu = Constants.Salisbury;
            Sec1 = "5.5555";
            Dsc1 = $"1234.999{Constants.Dsc1SearchString}";
        }

        protected void SalisburyIsPassedInForMaterialGroup()
        {
            MaterailGroupPlant = Constants.Salisbury;
        }

        protected void OsulivansBeachIsPassedInForMaterialGroup()
        {
            MaterailGroupPlant = Constants.OsulivansBeach;
        }

        protected void BadInputForMaterialGroupByPlant()
        {
            MaterailGroupPlant = "BadPlantInput";
        }

        protected void SalisburyIsPassedInForProductHierarchy()
        {
            ProductHierarcyPlant = Constants.Salisbury;
        }

        protected void OsulivansBeachIsPassedInForProductHierarchy()
        {
            ProductHierarcyPlant = Constants.OsulivansBeach;
        }

        protected void BadInputForProductHierarchy()
        {
            ProductHierarcyPlant = "BadPlantInput";
        }

        protected void ProperInputForActualHeightIsPassedIn()
        {
            ActualHeightInput = "123.129";
        }

        protected void SalisburyIsPassedInForProductAttribute()
        {
            ProductAttributeInput = Constants.Salisbury;
        }

        protected void OtherThanSalisburyIsPassedInForProductAttribute()
        {
            ProductAttributeInput = "SomethingOtherThanSalisbury";
        }

        protected void ProperIntegerIsPassedInForThreeDecimalFormatting()
        {
            ThreeDecimalPlacesInput = "1234";
        }

        protected void BadIntegerIsPassedInForThreeDecimalFormatting()
        {
            ThreeDecimalPlacesInput = "BadInteger";
        }

        protected void ProperRedTextIsPassedIn()
        {
            ColorTextInput = "red";
        }

        protected void ProperBlueTextIsPassedIn()
        {
            ColorTextInput = "blue";
        }

        protected void ProperBlackTextIsPassedIn()
        {
            ColorTextInput = "black";
        }

        protected void BadInputColorStringIsPassedIn()
        {
            ColorTextInput = "BadColorString";
        }

        protected void BranchCodeCannotBeFoundInBranchMapping()
        {
            MapPlantBranchList = new List<CacheMapBranch>();
            BranchCode = string.Empty;
        }

        protected void BranchCodeCanBeFoundButSapProfitCenreIsEmpty()
        {
            BranchCode = "a";

            MapPlantBranchList = new List<CacheMapBranch>
            {
                new CacheMapBranch
                {
                    JdeBranchCode = BranchCode,
                    SapProfitCentre = string.Empty
                }
            };
        }

        protected void BranchCodeCanBeFoundButStorageTypeIsEmpty()
        {
            BranchCode = "a";

            MapPlantBranchList = new List<CacheMapBranch>
            {
                new CacheMapBranch
                {
                    JdeBranchCode = BranchCode,
                    StorageType = string.Empty
                }
            };
        }

        protected void CorrectBranchCodeIsPassedInForFindingSapProfitCenre()
        {
            BranchCode = "a";

            MapPlantBranchList = new List<CacheMapBranch>
            {
                new CacheMapBranch
                {
                    JdeBranchCode = BranchCode,
                    SapProfitCentre = SapProfitCentre
                }
            };
        }

        protected void CorrectBranchCodeIsPassedInForFindingStorageType()
        {
            BranchCode = "a";

            MapPlantBranchList = new List<CacheMapBranch>
            {
                new CacheMapBranch
                {
                    JdeBranchCode = BranchCode,
                    StorageType = StorageType
                }
            };
        }

        protected void CorrectValueForSalisburyAndPrpIsPassedInForFindingStorageSection()
        {
            Mcu = Constants.Salisbury;
            Prp4 = Constants.DefaultPrp4;
        }

        protected void CorrectValueForSalisburyAndPrpEmptyIsPassedInForFindingStorageSection()
        {
            Mcu = Constants.Salisbury;
            Prp4 = "Prp4IsNotEqualToConstant";
        }

        protected void CorrectValueForOsulivansBeachIsPassedInForFindingStorageSection()
        {
            Mcu = Constants.OsulivansBeach;
        }

        protected void NothingIsFoundForStorageSection()
        {
            Mcu = "BadMcuValue";
            Prp4 = "BadPrpValue";
        }

        protected void SalisburyIsPassedInForFormattingPrp()
        {
            Mcu = Constants.Salisbury;
            Prp0 = "a";
        }

        protected void SalisburyIsNotPassedInForFormattingPrp()
        {
            Mcu = "NonSalisburyValue";
            Prp0 = "a";
        }

        protected void ProperStingValuesArePassedInForTdLine()
        {
            Prp2Desc2 = "Prp2Desc2";
            Dsc1 = "Dsc1";
            Dsc2 = "Dsc2";
        }

        protected void ProperInputStingIsBiggerThanForty()
        {
            Dsc12 = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";//50
        }

        protected void ProperInputStingIsForty()
        {
            Dsc12 = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";//40
        }

        protected void ProperInputStingIsTen()
        {
            Dsc12 = "zzzzzzzzzz";//10
        }

        protected void ProperStingValuesArePassedInWihtTenLeadingZeros()
        {
            Spr1 = "0000000000123";
        }

        protected void Sec2InputIsNullOrEmptyForZzdm2N()
        {
            Sec1 = "Sec1";
            Sec2 = string.Empty;
        }

        protected void Sec2InputCoantainsProperValue()
        {
            Sec1 = string.Empty;
            Sec2 = "Sec2";
        }

        protected void InputsAreProvidedForUnitCostCalculation(string kg, string unitcost)
        {
            Kg = kg;
            UnitCost = unitcost;
        }
    }
}