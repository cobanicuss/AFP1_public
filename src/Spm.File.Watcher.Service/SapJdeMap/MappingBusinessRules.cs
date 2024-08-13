using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public class MappingBusinessRules : IDoMappingBusinessRules
    {
        private readonly IDoErrorConditionsForMappingBusinessRules _errorConditions;

        public MappingBusinessRules(IDoErrorConditionsForMappingBusinessRules errorConditions)
        {
            _errorConditions = errorConditions;
        }

        public MappingOutputDto MapCostCenter(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string plant, string lnType, string costCentre, string exInGst)
        {
            const string defaultTo = MapDefaults.DefaultDoubleZero;

            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));
            var defaultCostCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(defaultTo));

            if (mappedMaterialGroup == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ConnotFindJdeStockType(jdeStockType) };

            var sapCostCentre = mappedMaterialGroup.SapCostCentre;

            if (!string.IsNullOrEmpty(sapCostCentre) && !string.IsNullOrWhiteSpace(sapCostCentre))
                return new MappingOutputDto { IsMappingOk = true, Output = sapCostCentre.PadLeft(10, '0') };

            var costCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCentre));

            if (costCentreGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = costCentreGlPosting.SapCostCentre.PadLeft(10, '0') };

            if (defaultCostCentreGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeDepartmentCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultCostCentreGlPosting.SapCostCentre.PadLeft(10, '0') };
        }

        public MappingOutputDto MapMaterialGroup(List<CacheMapMaterialGroup> materialGroupMapping, string plant, string lnType, string exInGst)
        {
            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));

            if (mappedMaterialGroup == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ConnotFindJdeStockType(jdeStockType) };

            return new MappingOutputDto { IsMappingOk = true, Output = mappedMaterialGroup.SapMatrialGroup };
        }

        public MappingOutputDto MapPurchaseOrderGlAccount(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string plant, string lnType, string glAccount, string exInGst)
        {
            const string defaultTo = MapDefaults.DefaultQuadZero;

            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(defaultTo));

            if (mappedMaterialGroup == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ConnotFindJdeStockType(jdeStockType) };

            var sapGlAcc = mappedMaterialGroup.SapGlAcc;

            if (!string.IsNullOrEmpty(sapGlAcc) && !string.IsNullOrWhiteSpace(sapGlAcc))
                return new MappingOutputDto { IsMappingOk = true, Output = sapGlAcc.PadLeft(10, '0') };

            var glAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));

            if (glAccountGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = glAccountGlPosting.SapGlAccount.PadLeft(10, '0') };

            if (defaultGlAccountGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeGlAccountCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultGlAccountGlPosting.SapGlAccount.PadLeft(10, '0') };
        }

        public MappingOutputDto MapOrderPrUn(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit)
        {
            return MapPoUnit(unitOfMeasureMapping, poUnit); //Same as PoUnit for conversion to work//
        }

        public MappingOutputDto MapPoUnit(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit)
        {
            const string defaultTo = MapDefaults.DefaultUnitOfMeasure;

            var mappedPoUnit = unitOfMeasureMapping.FirstOrDefault(x => x.JdeUom.Equals(poUnit));
            var defaultPoUnit = unitOfMeasureMapping.FirstOrDefault(x => x.JdeUom.ToUpper().Equals(defaultTo));

            if (mappedPoUnit != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedPoUnit.IsoUom };

            if (defaultPoUnit == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeUnitOfMeasure(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultPoUnit.IsoUom };
        }

        public MappingOutputDto MapPlant(List<CacheMapPlant> plantMapping, string plant)
        {
            const string defaultTo = MapDefaults.DefaultBranch;

            var mappedPlant = plantMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(plant));
            var defaultPlant = plantMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(defaultTo));

            if (mappedPlant != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedPlant.SapPlant };

            if (defaultPlant == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeBranchCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultPlant.SapPlant };
        }

        public MappingOutputDto MapPurchaseGroup(List<CacheMapPurchaseGroup> purchaseGroupMapping, string purchaseGroup)
        {
            const string defaultTo = MapDefaults.DefaultPurchGroup;

            var mappedHeaderPurGroup = purchaseGroupMapping.FirstOrDefault(x => x.JdePurchaseGroup.Equals(purchaseGroup));
            var defaultPurchaseGroup = purchaseGroupMapping.FirstOrDefault(x => x.JdePurchaseGroup.ToUpper().Equals(defaultTo));

            if (mappedHeaderPurGroup != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedHeaderPurGroup.SapPurchaseGroup };

            if (defaultPurchaseGroup == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdePurchaseGroup(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultPurchaseGroup.SapPurchaseGroup };
        }

        public MappingOutputDto MapDocType(List<CacheMapDocTypes> docTypeMapping, string docType)
        {
            var mappedDocType = docTypeMapping.FirstOrDefault(x => x.JdeDocType.Equals(docType));

            if (mappedDocType == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindJdeDocType(docType) };

            return new MappingOutputDto { IsMappingOk = true, Output = mappedDocType.SapDocType };
        }

        public MappingOutputDto MapCompCode(List<CacheMapCompanyCode> companyCodeMapping, string compCode)
        {
            const string defaultTo = MapDefaults.DefaultCompanyCode;

            var mappedCompCode = companyCodeMapping.FirstOrDefault(x => x.JdeCompanyCode.Equals(compCode));
            var defaultComCode = companyCodeMapping.FirstOrDefault(x => x.JdeCompanyCode.Equals(defaultTo));

            if (mappedCompCode != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedCompCode.SapCompanyCode };

            if (defaultComCode == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeCompanyCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultComCode.SapCompanyCode };
        }

        public MappingOutputDto MapLocation(List<CacheMapLocation> locationMapping, string location)
        {
            const string defaultTo = MapDefaults.DefaultLocationCode;

            var mappedLocation = locationMapping.FirstOrDefault(x => x.JdeLocationCode.Equals(location));
            var defaultLocation = locationMapping.FirstOrDefault(x => x.JdeLocationCode.Equals(defaultTo));

            if (mappedLocation != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedLocation.SapStorageLocation };

            if (defaultLocation == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeLocation(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultLocation.SapStorageLocation };
        }

        public MappingOutputDto MapPurchOrg(string purchOrg)
        {
            var purchOrgSap = purchOrg.PadLeft(4, '0');
            return new MappingOutputDto { IsMappingOk = true, Output = purchOrgSap };
        }

        public MappingOutputDto MapGmCode(string gmCode)
        {
            var gmCodeSap = gmCode.PadLeft(2, '0');
            return new MappingOutputDto { IsMappingOk = true, Output = gmCodeSap };
        }

        public MappingOutputDto MapGlCostCentre(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string glAccount, string refDocNo, string costCentre)
        {
            const string defaultTo = MapDefaults.DefaultDoubleZero;

            var first2Char = refDocNo.Substring(0, 2);

            if (first2Char.ToUpper().Equals(MapDefaults.RefDocStartsWithOv) &&
                !glAccount.Equals(MapDefaults.GlAccount1) &&
                !glAccount.Equals(MapDefaults.GlAccount2))
            {
                return new MappingOutputDto { IsMappingOk = true, Output = MapDefaults.SapCostCentre };
            }

            var sapGlTypeResult = GetSapGlType(glAccountsGlPosting, glAccount);

            string sapGlType;
            if (sapGlTypeResult.IsMappingOk) sapGlType = sapGlTypeResult.Output;
            else return new MappingOutputDto { IsMappingOk = false, Output = sapGlTypeResult.Output };

            if (!sapGlType.ToUpper().Equals(MapDefaults.SapGlTypeOfCost))
                return new MappingOutputDto { IsMappingOk = true, Output = string.Empty };

            var mappedCostCenterGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCentre));
            var defaultCostCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(defaultTo));

            if (mappedCostCenterGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedCostCenterGlPosting.SapCostCentre.PadLeft(10, '0') };

            if (defaultCostCentreGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeDepartmentCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultCostCentreGlPosting.SapCostCentre.PadLeft(10, '0') };
        }

        public MappingOutputDto MapPoItem(string poItem)
        {
            var returnVal = poItem.Substring(0, poItem.IndexOf('.'));

            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapPoNumber(string poNumber)
        {
            var returnVal = poNumber.PadLeft(10, '0');

            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapCreatDate(string createDate)
        {
            if (string.IsNullOrEmpty(createDate) || string.IsNullOrWhiteSpace(createDate))
                return new MappingOutputDto { IsMappingOk = true, Output = string.Empty };

            var returnVal = ConvertDateForSap(createDate);
            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapDeliveryDate(string deliveryDate)
        {
            if (string.IsNullOrEmpty(deliveryDate) || string.IsNullOrWhiteSpace(deliveryDate))
                return new MappingOutputDto { IsMappingOk = true, Output = string.Empty };

            var returnVal = ConvertDateForSapToTodayIfSmaller(deliveryDate);

            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapPostingDate(string postingDate)
        {
            var isDate = IsDateValid(postingDate);
            if (!isDate)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ProblemWithPostingDate(postingDate) };

            var returnVal = ConvertDateForSap(postingDate);
            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapGlDocDate(string docDate)
        {
            var isDate = IsDateValid(docDate);
            if (!isDate)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ProblemWithDocDate(docDate) };

            var returnVal = ConvertDateForSap(docDate);
            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapGoodsDocDate(string docDate)
        {
            var isDate = IsDateValid(docDate);
            if (!isDate)
                return new MappingOutputDto { IsMappingOk = true, Output = string.Empty };

            var returnVal = ConvertDateForSap(docDate);
            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapVendor(string vendor)
        {
            if (string.IsNullOrEmpty(vendor) || string.IsNullOrWhiteSpace(vendor))
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ProblemWithVendor(vendor) };

            return new MappingOutputDto { IsMappingOk = true, Output = vendor };
        }

        public MappingOutputDto MapNetPrice(string netPrice)
        {
            if (string.IsNullOrEmpty(netPrice) || string.IsNullOrWhiteSpace(netPrice))
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ProblemWithNetPriceEmpty(netPrice) };

            float netPriceAsFloat;
            var isValidNetPrice = float.TryParse(netPrice, out netPriceAsFloat);
            var cannotBeParsed = !isValidNetPrice;

            if (cannotBeParsed)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.ProblemWithNetPriceFormat(netPrice) };

            var returnVal = netPriceAsFloat < 0.01f ? MapDefaults.DefaultNetPrice : netPrice;

            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapHeaderText(string id)
        {
            var returnVal = string.Format("{0}{1}", MapDefaults.GoodsReceiptHeaderPrefix, id);

            return new MappingOutputDto { IsMappingOk = true, Output = returnVal };
        }

        public MappingOutputDto MapGlAccount(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount, string refDocNo)
        {
            const string doubleZero = MapDefaults.DefaultDoubleZero;
            const string quadZero = MapDefaults.DefaultQuadZero;

            var first2Char = refDocNo.Substring(0, 2);

            if (first2Char.ToUpper().Equals(MapDefaults.RefDocStartsWithOv)
                && !glAccount.Equals(MapDefaults.GlAccount1)
                && !glAccount.Equals(MapDefaults.GlAccount2))
            {
                return new MappingOutputDto { IsMappingOk = true, Output = MapDefaults.SapGlAccount };
            }

            var mappedGlAccountsGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(doubleZero));

            if (mappedGlAccountsGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedGlAccountsGlPosting.SapGlAccount.PadLeft(10, '0') };

            if (defaultGlAccountGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeGlAccountCode(quadZero) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultGlAccountGlPosting.SapGlAccount.PadLeft(10, '0') };
        }

        public MappingOutputDto MapGlProfitCentre(List<CacheMapGlAccountsGlPosting> glAcountsGlPosting, List<CacheMapProfitCentreGlPosting> profitCenterGlPosting, string glAccount, string costCenter)
        {
            var sapGlTypeResult = GetSapGlType(glAcountsGlPosting, glAccount);

            string sapGlType;
            if (sapGlTypeResult.IsMappingOk) sapGlType = sapGlTypeResult.Output;
            else return new MappingOutputDto { IsMappingOk = false, Output = sapGlTypeResult.Output };

            if (!sapGlType.ToUpper().Equals(MapDefaults.SapGlTypeOfProfit))
                return new MappingOutputDto { IsMappingOk = true, Output = string.Empty };

            var mappedProfitCenterGlPosting = profitCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCenter));
            var defaultPorfitCentreGlPosting = profitCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(MapDefaults.DefaultDoubleZero));

            if (mappedProfitCenterGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedProfitCenterGlPosting.SapProfitCentre.PadLeft(10, '0') };

            if (defaultPorfitCentreGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeDepartmentCode(MapDefaults.DefaultDoubleZero) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultPorfitCentreGlPosting.SapProfitCentre.PadLeft(10, '0') };
        }

        private MappingOutputDto GetSapGlType(IList<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount)
        {
            const string defaultTo = MapDefaults.DefaultDoubleZero;

            var mappedGlAccountsGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(defaultTo));

            if (mappedGlAccountsGlPosting != null)
                return new MappingOutputDto { IsMappingOk = true, Output = mappedGlAccountsGlPosting.SapType };

            if (defaultGlAccountGlPosting == null)
                return new MappingOutputDto { IsMappingOk = false, Output = _errorConditions.CannotFindDefaultJdeGlAccountCode(defaultTo) };

            return new MappingOutputDto { IsMappingOk = true, Output = defaultGlAccountGlPosting.SapType };
        }

        private static string GetJdeStockType(string plant, string lnType, string exInGst)
        {
            var gstAdd = exInGst.ToUpper().Equals("N") ? "EX" : "GST";

            var jdeStockType = string.Format("{0}-{1}-{2}", lnType, plant, gstAdd);
            return jdeStockType;
        }

        private static bool IsDateValid(string postingDate)
        {
            DateTime outDateTime;
            var isDate = DateTime.TryParse(postingDate, out outDateTime);

            return !string.IsNullOrEmpty(postingDate) && !string.IsNullOrWhiteSpace(postingDate) && isDate;
        }

        private static string ConvertDateForSap(string createDate)
        {
            return DateTime.ParseExact(createDate, Constants.JdeExtractFileDateFormat, new DateTimeFormatInfo()).ToString(Constants.SapDateFormat);
        }

        private static string ConvertDateForSapToTodayIfSmaller(string createDate)
        {
            var date = DateTime.ParseExact(createDate, Constants.JdeExtractFileDateFormat, new DateTimeFormatInfo()).Date;

            var returnVal = date < DateTime.Today.Date ? DateTime.Today.Date.ToString(Constants.SapDateFormat) : date.ToString(Constants.SapDateFormat);

            return returnVal;
        }
    }
}