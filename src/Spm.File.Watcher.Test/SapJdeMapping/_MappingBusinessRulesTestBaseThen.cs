using System;
using System.Linq;
using NUnit.Framework;
using Spm.File.Watcher.Service;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public partial class MappingBusinessRulesTestBase
    {
        /// <summary>
        /// All the THEN parts are done here
        /// </summary>

        protected void MappingIsBad(ResultDto dto)
        {
            Assert.False(dto.IsOk);
        }

        protected void MappingIsGood(ResultDto dto)
        {
            Assert.True(dto.IsOk);
        }

        protected void ErrorDetailsProvidedForStockType(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapMaterialGroup));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeStockType));
        }

        protected void ErrorDetailsProvidedForDefaultJdeGlAccountCode2(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapGlAccountsGlPosting));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeGlAccount));
        }

        protected void ErrorDetailsProvidedForDefaultJdeGlAccountCode1(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapProfitCentreGlPosting));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeDepartmentCode));
        }

        protected void ErrorDetailsProvidedForDefaultJdeDepartmentCode(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapCostCentreGlPosting));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeDepartmentCode));
        }

        protected void SapMaterialGroupIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapMatrialGroup);
        }

        protected void SapGlAccountIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapGlAccountOutput);
        }

        protected void SapCostCenterIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapCostCentreOutput);
        }

        protected void DefaultUnitOfMeasureIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DefaultIsoUom);
        }

        protected void UnitOfMeasureIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, IsoUom);
        }

        protected void ErrorDetailsProvidedForUnitOfMeasure(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapUnitOfMeasure));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeUom));
            Assert.IsTrue(dto.Output.Contains(Constants.DefaultUnitOfMeasure));
        }

        protected void DefaultPlantIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.DefaultSapPlant);
        }

        protected void PlantIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapPlant);
        }

        protected void CorrectPhysicalPackSizeIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "10 X 11");
        }

        protected void ErrorDetailsProvidedForJdeBranchCode(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapPlant));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeBranchCode));
        }

        protected void ErrorDetailsProvidedForMissingPlantBranchCode(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapBranch));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeBranchCode));
        }

        protected void CorrectDenominatorIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DenominatorOutput);
        }

        protected void CorrectNumeratorIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, NumeratorOutput);
        }

        protected void DefaultPurchaseGroupIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DefaultSapPurchaseGroup);
        }

        protected void PurchaseGroupIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapPurchaseGroup);
        }

        protected void ErrorDetailsProvidedForJdePurchaseGroup(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapPurchaseGroup));
            Assert.IsTrue(dto.Output.Contains(Constants.JdePurchaseGroup));
        }

        protected void DocTypeIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapDocType);
        }

        protected void ErrorDetailsProvidedForJdeDocType(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapDocTypes));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeDocType));
        }

        protected void DefaultCompanyCodeIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DefaultSapCompanyCode);
        }

        protected void CompanyCodeIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapCompanyCode);
        }

        protected void ErrorDetailsProvidedForJdeCompanyCode(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapCompanyCode));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeCompanyCode));
        }

        protected void DefaultLocationIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DefaultSapStorageLocation);
        }

        protected void LocationIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapStorageLocation);
        }

        protected void ErrorDetailsProvidedForJdeLocation(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapLocation));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeLocationCode));
            Assert.IsTrue(dto.Output.Contains(Constants.DefaultLocationCode));
        }

        protected void PurchaseOrganizationOutputMustHaveLeadingZeros(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, PurchOrgOutput);
        }

        protected void GmCodeOutputMustHaveLeadingZeros(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, GmCodeOutput);
        }

        protected void DefaultSapCostCentreIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.SapCostCentre);
        }

        protected void PoItemOutputMustBeStripped(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, PoItemOutput);
        }

        protected void PoNumberOutputMustHaveLeadingZeros(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, PoNumberOutput);
        }

        protected void CreateDateMustBeConverted(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, CreateDateOutput);
        }

        protected void ConvertedDeliveryDateMustBeToday(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DateTime.Today.Date.ToString(Constants.SapDateFormat));
        }

        protected void ConvertedDeliveryDateMustBeInTheFuture(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, DateTime.Today.AddMonths(1).Date.ToString(Constants.SapDateFormat));
        }

        protected void PostingDateMustBeConverted(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, PostingDateOutput);
        }

        protected void DefaultSapGlAccountIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.SapGlAccount);
        }

        protected void SapProfitCenterIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapProfitCentreOutput);
        }

        protected void DefaultNetPriceIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.DefaultNetPrice);
        }

        protected void NetPriceIsValid(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, NetPrice);
        }

        protected void HeaderTextIsProperlyFormated(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, HeaderText);
        }

        protected void GlDocDateMustBeConverted(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, GlDocDateOutput);
        }

        protected void GoodsDocDateMustBeConverted(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, GoodsDocDateOutput);
        }

        protected void MustHaveThreeDigitsAfterComma(ResultDto dto)
        {
            var commaIndex = dto.Output.IndexOf('.');
            var placesAfterComma = dto.Output.Substring(commaIndex).Length - 1;

            Assert.AreEqual(placesAfterComma, 3);
        }

        protected void ThreeDigitsRoundedUp(ResultDto dto, string input)
        {
            var isRoundUp = false;
            var outVal = dto.Output;

            var inPos4AfterComma = input.Substring(input.IndexOf('.')).Length - 1 < 4
                ? 0
                : int.Parse(input.Substring(input.IndexOf('.') + 4, 1));

            var inPos3AfterComma = input.Substring(input.IndexOf('.')).Length - 1 < 3
                ? 0
                : int.Parse(input.Substring(input.IndexOf('.') + 3, 1));


            var outPos3AfterComma = outVal.Substring(outVal.IndexOf('.')).Length - 1 < 3
                ? 0
                : int.Parse(outVal.Substring(outVal.IndexOf('.') + 3, 1));


            if ((inPos4AfterComma == 0) || (inPos4AfterComma >= 5 && outPos3AfterComma - inPos3AfterComma == 1))
                isRoundUp = true;

            Assert.IsTrue(isRoundUp);
        }

        protected void ZeroStillRemains(ResultDto dto)
        {
            var zeroInFrontOfComma = dto.Output.Substring(0, 1);

            Assert.AreEqual(zeroInFrontOfComma, "0");
        }

        protected void DivisionWithIntMustBeRoundedUp(ResultDto dto)
        {
            //99.77552674 rounds up to//
            const string returnVal = "99.776";

            Assert.AreEqual(returnVal, dto.Output);
        }

        protected void DivisionByZeroMustReturn3DigitZeroString(ResultDto dto)
        {
            Assert.AreEqual(Constants.DefaultThreeDigits, dto.Output);
        }

        protected void Dsc1IsRoundedCorrectly(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "1235.00");
        }

        protected void Sec1IsRoundedCorrectly(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "5.56");
        }

        protected void Sec2IsRoundedCorrectly(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "5.56");
        }

        protected void CorrectMaterialGroupIsReturnedForSalisbury(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.Xsal);
        }

        protected void CorrectMaterialGroupIsReturnedForOsulivansBeach(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.Xosb);
        }

        protected void ErrorConditionForMaterialGroupByPlantIsReturned(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapMaterialGroup));
            Assert.IsTrue(dto.Output.Contains(Constants.Plant));
        }

        protected void CorrectProductHierarchyIsReturnedForSalisbury(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.Xs);
        }

        protected void CorrectProductHierarchyIsReturnedForOsulivansBeach(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.Xo);
        }

        protected void ErrorConditionForProductHierarchyIsReturned(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapProductHierarchy));
            Assert.IsTrue(dto.Output.Contains(Constants.Plant));
        }

        protected void CorrectRoundingWith2PlacesAfterComma(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "123.13");
        }

        protected void CorrectProductAttributeIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.Ex);
        }

        protected void EmptyStringIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, string.Empty);
        }

        protected void CorrectThreeDecimalResultIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "1234.000");
        }

        protected void StringWithZeroAndThreeDecimalPalcesIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.DefaultThreeDigits);
        }

        protected void CorrectColorFormatingAndTextIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, dto.Output.ToUpper());
        }

        protected void ErrorDetailsProvidedForEmptyBranchCode(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapBranch));
            Assert.IsTrue(dto.Output.Contains(Constants.JdeBranchCode));
        }

        protected void CorrectSapProfitCenreIsReturnedForGivenBranchCode(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, SapProfitCentre);
        }

        protected void CorrectStorageTypeIsReturnedForGivenBranchCode(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, StorageType);
        }

        protected void CorrectStorageSectionIsReturnedForSalisburyAndPrp(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.StorageSection1);
        }

        protected void CorrectStorageSectionIsReturnedForSalisburyAndPrpEmpty(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.StorageSection2);
        }

        protected void CorrectStorageSectionIsReturnedForOsulivansBeach(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Constants.StorageSection3);
        }

        protected void ErrorDetailsProvidedForNontFindingStorageSection(ResultDto dto)
        {
            Assert.IsTrue(dto.Output.Contains(Constants.MapStorageSection));
            Assert.IsTrue(dto.Output.Contains(Constants.StorageSection));
        }

        protected void PrpShouldBeFormattedCorrectlyForSalisbury(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "OaM");
        }

        protected void PrpShouldBeFormattedCorrectlyForNonSalisburyInput(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "0aM");
        }

        protected void CorrectStringWithTwoConcatenatedSpacesIsReturned(ResultDto dto)
        {
            Assert.AreEqual(dto.Output.Count(x => x == ' '), 2);
        }

        protected void ReturnUpToAndIncludingFirstFortyCharacters(ResultDto dto)
        {
            Assert.AreEqual(dto.Output.Length, 40);
        }

        protected void ReturnUpToAndIncludingFirstNCharacters(ResultDto dto)
        {
            Assert.AreEqual(dto.Output.Length, 10);
        }

        protected void AllLeadingZerosAreGone(ResultDto dto)
        {
            var hasNoZeros = !dto.Output.Contains('0');
            Assert.IsTrue(hasNoZeros);
        }

        protected void Sec1IsReturnedForZzdm2N(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Sec1);
        }

        protected void Sec2IsReturnedForZzdm2N(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, Sec2);
        }

        protected void CorrectValueIsRetunedForUnitCostCalculation(ResultDto dto)
        {
            Assert.AreEqual(dto.Output, "3.14");
        }
    }
}