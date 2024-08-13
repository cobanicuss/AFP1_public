using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Validation;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MappingBusinessRules : IDoMappingBusinessRules
    {
        private readonly IDisplayErrors _error;
        private readonly IConvertDecimal _convertDecimal;
        private readonly IConvertDate _convertDate;

        public MappingBusinessRules(IDisplayErrors error,
            IConvertDecimal convertDecimal,
            IConvertDate convertDate)
        {
            _error = error;
            _convertDecimal = convertDecimal;
            _convertDate = convertDate;
        }

        public ResultDto MapCostCenter(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string plant, string lnType, string costCentre, string exInGst)
        {
            const string defaultTo = Constants.DefaultDoubleZero;

            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));
            var defaultCostCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(defaultTo));

            if (mappedMaterialGroup == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapMaterialGroup, Constants.JdeStockType, jdeStockType)
                };

            var sapCostCentre = mappedMaterialGroup.SapCostCentre;

            if (!string.IsNullOrEmpty(sapCostCentre) && !string.IsNullOrWhiteSpace(sapCostCentre))
                return new ResultDto
                {
                    IsOk = true,
                    Output = sapCostCentre.PadLeft(10, Constants.DefaultZero)
                };

            var costCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCentre));

            if (costCentreGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = costCentreGlPosting.SapCostCentre.PadLeft(10, Constants.DefaultZero)
                };

            if (defaultCostCentreGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapCostCentreGlPosting, Constants.JdeDepartmentCode, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultCostCentreGlPosting.SapCostCentre.PadLeft(10, Constants.DefaultZero)
            };
        }

        public ResultDto MapMaterialGroup(List<CacheMapMaterialGroup> materialGroupMapping, string plant, string lnType, string exInGst)
        {
            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));

            if (mappedMaterialGroup == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapMaterialGroup, Constants.JdeStockType, jdeStockType)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = mappedMaterialGroup.SapMatrialGroup
            };
        }

        public ResultDto MapMaterialGroupByPlant(string plant)
        {
            if (plant.Equals(Constants.Salisbury))
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.Xsal
                };

            if (plant.Equals(Constants.OsulivansBeach))
            {
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.Xosb
                };
            }

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotFind(Constants.MapMaterialGroup, Constants.Plant, plant)
            };
        }

        public ResultDto MapPurchaseOrderGlAccount(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string plant, string lnType, string glAccount, string exInGst)
        {
            const string defaultTo = Constants.DefaultQuadZero;

            var jdeStockType = GetJdeStockType(plant, lnType, exInGst);
            var mappedMaterialGroup = materialGroupMapping.FirstOrDefault(x => x.JdeStockType.Equals(jdeStockType));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(defaultTo));

            if (mappedMaterialGroup == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapMaterialGroup, Constants.JdeStockType, jdeStockType)
                };

            var sapGlAcc = mappedMaterialGroup.SapGlAcc;

            if (!string.IsNullOrEmpty(sapGlAcc) && !string.IsNullOrWhiteSpace(sapGlAcc))
                return new ResultDto
                {
                    IsOk = true,
                    Output = sapGlAcc.PadLeft(10, Constants.DefaultZero)
                };

            var glAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));

            if (glAccountGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = glAccountGlPosting.SapGlAccount.PadLeft(10, Constants.DefaultZero)
                };

            if (defaultGlAccountGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapGlAccountsGlPosting, Constants.JdeGlAccount, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultGlAccountGlPosting.SapGlAccount.PadLeft(10, Constants.DefaultZero)
            };
        }

        public ResultDto MapOrderPrUn(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit)
        {
            //Same as PoUnit for conversion to work.
            //Keep extra method for readability.
            return MapPoUnit(unitOfMeasureMapping, poUnit);
        }

        public ResultDto MapPoUnit(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit)
        {
            const string defaultTo = Constants.DefaultUnitOfMeasure;

            var mappedPoUnit = unitOfMeasureMapping.FirstOrDefault(x => x.JdeUom.Equals(poUnit));
            var defaultPoUnit = unitOfMeasureMapping.FirstOrDefault(x => x.JdeUom.ToUpper().Equals(defaultTo));

            if (mappedPoUnit != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedPoUnit.IsoUom
                };

            if (defaultPoUnit == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapUnitOfMeasure, Constants.JdeUom, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultPoUnit.IsoUom
            };
        }

        public ResultDto MapPlant(List<CacheMapPlant> plantMapping, string branch)
        {
            const string defaultTo = Constants.Salisbury;

            var mappedPlant = plantMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(branch));
            var defaultPlant = plantMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(defaultTo));

            if (mappedPlant != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedPlant.SapPlant
                };

            if (defaultPlant == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapPlant, Constants.JdeBranchCode, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultPlant.SapPlant
            };
        }

        public ResultDto MapPlantBranch(List<CacheMapBranch> branchMapping, string branch)
        {
            var mappedPlant = branchMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(branch));

            if (mappedPlant != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedPlant.SapPlant
                };

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotFind(Constants.MapBranch, Constants.JdeBranchCode, branch)
            };
        }

        public ResultDto MapDenominator(string input)
        {
            double inputVal;
            double.TryParse(input, out inputVal);

            var denominator = _convertDecimal.UpscaleDenominator(inputVal);

            return new ResultDto
            {
                IsOk = true,
                Output = denominator.ToString()
            };
        }

        public ResultDto MapNumerator(string input)
        {
            double inputVal;
            double.TryParse(input, out inputVal);

            var denominator = _convertDecimal.UpscaleDenominator(inputVal);
            var returnVal = Math.Round(denominator * inputVal, 0);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal.ToString(CultureInfo.InvariantCulture)
            };
        }

        public ResultDto MapPhysicalPackSize(string mcu, string gh, string gw)
        {
            if (string.Equals(mcu, Constants.OsulivansBeach))
                return new ResultDto { IsOk = true, Output = string.Empty };

            float ghOut;
            float.TryParse(gh, out ghOut);

            float gwOut;
            float.TryParse(gw, out gwOut);

            return new ResultDto
            {
                IsOk = true,
                Output = $"{Math.Round(ghOut, 0)} X {Math.Round(gwOut, 0)}"
            };
        }

        public ResultDto MapPurchaseGroup(List<CacheMapPurchaseGroup> purchaseGroupMapping, string purchaseGroup)
        {
            const string defaultTo = Constants.DefaultPurchGroup;

            var mappedHeaderPurGroup = purchaseGroupMapping.FirstOrDefault(x => x.JdePurchaseGroup.Equals(purchaseGroup));
            var defaultPurchaseGroup = purchaseGroupMapping.FirstOrDefault(x => x.JdePurchaseGroup.ToUpper().Equals(defaultTo));

            if (mappedHeaderPurGroup != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedHeaderPurGroup.SapPurchaseGroup
                };

            if (defaultPurchaseGroup == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapPurchaseGroup, Constants.JdePurchaseGroup, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultPurchaseGroup.SapPurchaseGroup
            };
        }

        public ResultDto MapDocType(List<CacheMapDocTypes> docTypeMapping, string type)
        {
            var mappedDocType = docTypeMapping.FirstOrDefault(x => x.JdeDocType.Equals(type));

            if (mappedDocType == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapDocTypes, Constants.JdeDocType, type)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = mappedDocType.SapDocType
            };
        }

        public ResultDto MapCompCode(List<CacheMapCompanyCode> companyCodeMapping, string compCode)
        {
            const string defaultTo = Constants.DefaultCompanyCode;

            var mappedCompCode = companyCodeMapping.FirstOrDefault(x => x.JdeCompanyCode.Equals(compCode));
            var defaultComCode = companyCodeMapping.FirstOrDefault(x => x.JdeCompanyCode.Equals(defaultTo));

            if (mappedCompCode != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedCompCode.SapCompanyCode
                };

            if (defaultComCode == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapCompanyCode, Constants.JdeCompanyCode, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultComCode.SapCompanyCode
            };
        }

        public ResultDto MapLocation(List<CacheMapLocation> locationMapping, string location)
        {
            const string defaultTo = Constants.DefaultLocationCode;

            var mappedLocation = locationMapping.FirstOrDefault(x => x.JdeLocationCode.Equals(location));
            var defaultLocation = locationMapping.FirstOrDefault(x => x.JdeLocationCode.Equals(defaultTo));

            if (mappedLocation != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedLocation.SapStorageLocation
                };

            if (defaultLocation == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapLocation, Constants.JdeLocationCode, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultLocation.SapStorageLocation
            };
        }

        public ResultDto MapPurchOrg(string purchOrg)
        {
            var purchOrgSap = purchOrg.PadLeft(4, Constants.DefaultZero);

            return new ResultDto
            {
                IsOk = true,
                Output = purchOrgSap
            };
        }

        public ResultDto MapGmCode(string gmCode)
        {
            var gmCodeSap = gmCode.PadLeft(2, Constants.DefaultZero);

            return new ResultDto
            {
                IsOk = true,
                Output = gmCodeSap
            };
        }

        public ResultDto MapGlCostCentre(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string glAccount, string refDocNo, string costCentre)
        {
            const string defaultTo = Constants.DefaultDoubleZero;

            if (refDocNo.Length < 2)
            {
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.ProblemWith("RefDocNo", refDocNo)
                };
            }

            var first2Char = refDocNo.Substring(0, 2);

            if (first2Char.ToUpper().Equals(Constants.RefDocStartsWithOv) &&
                !glAccount.Equals(Constants.GlAccount1) &&
                !glAccount.Equals(Constants.GlAccount2))
            {
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.SapCostCentre
                };
            }

            var sapGlTypeResult = GetSapGlType(glAccountsGlPosting, glAccount);

            string sapGlType;
            if (sapGlTypeResult.IsOk) sapGlType = sapGlTypeResult.Output;
            else return new ResultDto
            {
                IsOk = false,
                Output = sapGlTypeResult.Output
            };

            if (!sapGlType.ToUpper().Equals(Constants.SapGlTypeOfCost))
                return new ResultDto
                {
                    IsOk = true,
                    Output = string.Empty
                };

            var mappedCostCenterGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCentre));
            var defaultCostCentreGlPosting = costCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(defaultTo));

            if (mappedCostCenterGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedCostCenterGlPosting.SapCostCentre.PadLeft(10, Constants.DefaultZero)
                };

            if (defaultCostCentreGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapCostCentreGlPosting, Constants.JdeDepartmentCode, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultCostCentreGlPosting.SapCostCentre.PadLeft(10, Constants.DefaultZero)
            };
        }

        public ResultDto MapPoItem(string poItem)
        {
            if (!poItem.Contains('.'))
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.ProblemWith("PoItem", poItem)
                };

            var returnVal = poItem.Substring(0, poItem.IndexOf('.'));

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapPoNumber(string poNumber)
        {
            var returnVal = poNumber.PadLeft(10, Constants.DefaultZero);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapCreatDate(string createDate)
        {
            var returnVal = _convertDate.ConvertDateForSap(createDate);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapDeliveryDate(string deliveryDate)
        {
            var returnVal = _convertDate.ConvertDateForSapToTodayIfSmaller(deliveryDate);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapPostingDate(string postingDate)
        {
            var returnVal = _convertDate.ConvertDateForSap(postingDate);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapGlDocDate(string docDate)
        {
            var returnVal = _convertDate.ConvertDateForSap(docDate);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapGoodsDocDate(string docDate)
        {
            DateTime outDateTime;
            var isDate = DateTime.TryParse(docDate, out outDateTime);
            var isDocDateValid = !string.IsNullOrEmpty(docDate) && !string.IsNullOrWhiteSpace(docDate) && isDate;
            var docDateIsBad = !isDocDateValid;

            if (docDateIsBad)
                return new ResultDto
                {
                    /*WHATCH OUT: bad GoodsDocDate IS valid mapping, with empty output! Hooray SAP!*/
                    IsOk = true,
                    Output = string.Empty
                };

            var returnVal = _convertDate.ConvertDateForSap(docDate);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapNetPrice(string netPrice)
        {
            float netPriceAsFloat;
            float.TryParse(netPrice, out netPriceAsFloat);

            var returnVal = netPriceAsFloat < 0.01f ? Constants.DefaultNetPrice : netPrice;

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapHeaderText(string id)
        {
            var returnVal = $"{Constants.GoodsReceiptHeaderPrefix}{id}";

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapProductHierarchy(string plant)
        {
            if (plant.Equals(Constants.Salisbury))
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.Xs
                };

            if (plant.Equals(Constants.OsulivansBeach))
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.Xo
                };

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotFind(Constants.MapProductHierarchy, Constants.Plant, plant)
            };
        }

        public ResultDto MapKgPerM(string kg, string m)
        {
            const string returnValFormat = Constants.DefaultThreeDigits;

            int mOut;
            int.TryParse(m, NumberStyles.Any, CultureInfo.InvariantCulture, out mOut);

            var mOutDivBy1000 = (mOut / 1000f);

            float kgOut;
            float.TryParse(kg, NumberStyles.Any, CultureInfo.InvariantCulture, out kgOut);

            if (mOut > 0)
            {
                var returnVal = Math.Round(kgOut / mOutDivBy1000, 3).ToString(returnValFormat);

                return new ResultDto
                {
                    IsOk = true,
                    Output = returnVal
                };
            }

            return new ResultDto
            {
                IsOk = false,
                Output = returnValFormat
            };
        }

        public ResultDto MapActualWidth(string mcu, string sec1, string sec2)
        {
            if ((string.Equals(mcu, Constants.OsulivansBeach) || string.Equals(mcu, Constants.Salisbury)) && string.IsNullOrEmpty(sec2))
                return MapTwoDecimalPlacesRoundUp(sec1);

            return MapTwoDecimalPlacesRoundUp(sec2);
        }

        public ResultDto MapActualHeight(string sec1)
        {
            return MapTwoDecimalPlacesRoundUp(sec1);
        }

        public ResultDto MapProductAttribute(string mcu)
        {
            if (string.Equals(mcu, Constants.Salisbury))
                return new ResultDto
                {
                    IsOk = true,
                    Output = Constants.Ex
                };

            return new ResultDto
            {
                IsOk = true,
                Output = string.Empty
            };
        }

        public ResultDto MapThreeDecimalPlacesOnly(string input)
        {
            var returnVal = Constants.DefaultThreeDigits;

            int outVal;
            var isValidParse = int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out outVal);

            if (isValidParse) returnVal = outVal.ToString(Constants.DefaultThreeDigits);

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapRedBlueBlack(string input)
        {
            if (input.ToUpper().Equals("RED") || input.ToUpper().Equals("BLUE") || input.ToUpper().Equals("BLACK"))
                return new ResultDto
                {
                    IsOk = true,
                    Output = input.ToUpper()
                };

            return new ResultDto
            {
                IsOk = true,
                Output = string.Empty
            };
        }

        public ResultDto MapGlAccount(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount, string refDocNo)
        {
            const string doubleZero = Constants.DefaultDoubleZero;

            if (refDocNo.Length < 2)
            {
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.ProblemWith("RefDocNo", refDocNo)
                };
            }

            var first2Char = refDocNo.Substring(0, 2);

            if (first2Char.ToUpper().Equals(Constants.RefDocStartsWithOv)
                && !glAccount.Equals(Constants.GlAccount1)
                && !glAccount.Equals(Constants.GlAccount2))
            {
                return new ResultDto { IsOk = true, Output = Constants.SapGlAccount };
            }

            var mappedGlAccountsGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(doubleZero));

            if (mappedGlAccountsGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedGlAccountsGlPosting.SapGlAccount.PadLeft(10, Constants.DefaultZero)
                };

            if (defaultGlAccountGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapGlAccountsGlPosting, Constants.JdeGlAccount, doubleZero)
                };

            return new ResultDto { IsOk = true, Output = defaultGlAccountGlPosting.SapGlAccount.PadLeft(10, Constants.DefaultZero) };
        }

        public ResultDto MapProfitCentre(List<CacheMapBranch> branchMapping, string branch)
        {
            var cacheBranch = branchMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(branch));

            if (string.IsNullOrEmpty(cacheBranch?.SapProfitCentre) || string.IsNullOrWhiteSpace(cacheBranch.SapProfitCentre))
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapBranch, Constants.JdeBranchCode, branch)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = cacheBranch.SapProfitCentre
            };
        }

        public ResultDto MapStorageType(List<CacheMapBranch> branchMapping, string branch)
        {
            var cacheBranch = branchMapping.FirstOrDefault(x => x.JdeBranchCode.Equals(branch));

            if (string.IsNullOrEmpty(cacheBranch?.StorageType) || string.IsNullOrWhiteSpace(cacheBranch.StorageType))
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapBranch, Constants.JdeBranchCode, branch)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = cacheBranch.StorageType
            };
        }

        public ResultDto MapStorageSection(string mcu, string prp4)
        {
            if (string.Equals(mcu, Constants.Salisbury) && string.Equals(prp4, Constants.DefaultPrp4))
                return new ResultDto
                {
                    IsOk = true,
                    Output = "142"
                };

            if (string.Equals(mcu, Constants.Salisbury) && !string.Equals(prp4, Constants.DefaultPrp4))
                return new ResultDto
                {
                    IsOk = true,
                    Output = "141"
                };

            if (string.Equals(mcu, Constants.OsulivansBeach))
                return new ResultDto
                {
                    IsOk = true,
                    Output = "153"
                };

            return new ResultDto
            {
                IsOk = false,
                Output = _error.CannotFind($"{Constants.MapStorageSection}(No if condition found)", Constants.StorageSection, $"mcu='{mcu}',prp4='{prp4}'")
            };
        }

        public ResultDto MapGlProfitCentre(List<CacheMapGlAccountsGlPosting> glAcountsGlPosting, List<CacheMapProfitCentreGlPosting> profitCenterGlPosting, string glAccount, string costCenter)
        {
            const string doubleZero = Constants.DefaultDoubleZero;

            var sapGlTypeResult = GetSapGlType(glAcountsGlPosting, glAccount);

            string sapGlType;
            if (sapGlTypeResult.IsOk) sapGlType = sapGlTypeResult.Output;
            else return new ResultDto
            {
                IsOk = false,
                Output = sapGlTypeResult.Output
            };

            if (!sapGlType.ToUpper().Equals(Constants.SapGlTypeOfProfit))
                return new ResultDto
                {
                    IsOk = true,
                    Output = string.Empty
                };

            var mappedProfitCenterGlPosting = profitCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(costCenter));
            var defaultPorfitCentreGlPosting = profitCenterGlPosting.FirstOrDefault(x => x.JdeDepartmentCode.Equals(doubleZero));

            if (mappedProfitCenterGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedProfitCenterGlPosting.SapProfitCentre.PadLeft(10, Constants.DefaultZero)
                };

            if (defaultPorfitCentreGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapProfitCentreGlPosting, Constants.JdeDepartmentCode, doubleZero)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultPorfitCentreGlPosting.SapProfitCentre.PadLeft(10, Constants.DefaultZero)
            };
        }

        public ResultDto MapPackWeight(string packWeight)
        {
            float outPackWeightFloat;
            float.TryParse(packWeight, NumberStyles.Any, CultureInfo.InvariantCulture, out outPackWeightFloat);

            return new ResultDto
            {
                IsOk = true,
                Output = Math.Round(outPackWeightFloat, 3).ToString(Constants.DefaultThreeDigits)
            };
        }

        public ResultDto MapPrpZero(string mcu, string prp0)
        {
            //BEWARE: 'O' NOT ZERO //
            //BEWARE: ZERO NOT 'O' //
            //Thank you SAP!//
            var returnVal = string.Equals(mcu, Constants.Salisbury) ? prp0.PadLeft(2, 'O') : prp0.PadLeft(2, Constants.DefaultZero);

            return new ResultDto
            {
                IsOk = true,
                Output = $"{returnVal}M"
            };
        }

        public ResultDto MapSizeOne(string mcu, string sec1, string dsc1)
        {
            var returnValAsString = sec1;

            if (string.Equals(mcu, Constants.Salisbury) && dsc1.Contains(Constants.Dsc1SearchString))
                returnValAsString = dsc1.Substring(0, dsc1.IndexOf(Constants.Dsc1SearchString, StringComparison.InvariantCulture)).Trim();

            return MapTwoDecimalPlacesRoundUp(returnValAsString);
        }

        public ResultDto MapTdLine(string prp2Desc2, string dsc1, string dsc2)
        {
            var returnVal = $"{dsc1} {dsc2} {prp2Desc2}";

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapMaktx(string dsc12)
        {
            var returnVal = dsc12.Length >= 40 ? dsc12.Substring(0, 40) : dsc12;

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapDzeit(string srp1)
        {
            var returnVal = srp1.TrimStart('0');

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapZzdm2N(string sec1, string sec2)
        {
            var returnVal = string.IsNullOrEmpty(sec2) || string.IsNullOrWhiteSpace(sec2) ? sec1 : sec2;

            return new ResultDto
            {
                IsOk = true,
                Output = returnVal
            };
        }

        public ResultDto MapUnitCost(string kg, string unitCost)
        {
            const string returnValFormat = Constants.DefaultTwoDigits;

            float unitCostOut;
            float.TryParse(unitCost, NumberStyles.Any, CultureInfo.InvariantCulture, out unitCostOut);

            float kgOut;
            float.TryParse(kg, NumberStyles.Any, CultureInfo.InvariantCulture, out kgOut);

            if (unitCostOut > 0 && kgOut > 0)
            {
                var returnVal = Math.Round(unitCostOut / kgOut, 2).ToString(returnValFormat);

                return new ResultDto
                {
                    IsOk = true,
                    Output = returnVal
                };
            }

            return new ResultDto
            {
                IsOk = false,
                Output = _error.ProblemWith("MapUnitCost", "kg", kg)
            };
        }

        private ResultDto GetSapGlType(IList<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount)
        {
            const string defaultTo = Constants.DefaultDoubleZero;

            var mappedGlAccountsGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(glAccount));
            var defaultGlAccountGlPosting = glAccountsGlPosting.FirstOrDefault(x => x.JdeGlAccount.Equals(defaultTo));

            if (mappedGlAccountsGlPosting != null)
                return new ResultDto
                {
                    IsOk = true,
                    Output = mappedGlAccountsGlPosting.SapType
                };

            if (defaultGlAccountGlPosting == null)
                return new ResultDto
                {
                    IsOk = false,
                    Output = _error.CannotFind(Constants.MapGlAccountsGlPosting, Constants.JdeGlAccount, defaultTo)
                };

            return new ResultDto
            {
                IsOk = true,
                Output = defaultGlAccountGlPosting.SapType
            };
        }

        private static string GetJdeStockType(string plant, string lnType, string exInGst)
        {
            var gstAdd = exInGst.ToUpper().Equals("N") ? "EX" : "GST";

            var jdeStockType = $"{lnType}-{plant}-{gstAdd}";
            return jdeStockType;
        }

        private ResultDto MapTwoDecimalPlacesRoundUp(string input)
        {
            float outVal;
            var isValidParse = float.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out outVal);

            if (isValidParse)
            {
                var returnVal = Math.Round(outVal, 2).ToString(Constants.DefaultTwoDigits);

                return new ResultDto { IsOk = true, Output = returnVal };
            }

            return new ResultDto { IsOk = false, Output = _error.ProblemWith("MapTwoDecimalPlacesRoundUp", Constants.Input, input) };
        }
    }
}