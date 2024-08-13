using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.File.Watcher.Service.Repository;

namespace Spm.File.Watcher.Service.Validation
{
    public class ImplementMapping : IImplementMapping
    {
        private readonly IDoMappingBusinessRules _mapping;
        private readonly IValidate _validate;

        private readonly List<CacheMapCompanyCode> _companyCodeMapping;
        private readonly List<CacheMapDocTypes> _docTypeMapping;
        private readonly List<CacheMapMaterialGroup> _materialGroupMapping;
        private readonly List<CacheMapPurchaseGroup> _purchaseGroupMapping;
        private readonly List<CacheMapUnitOfMeasure> _unitOfMeasureMapping;
        private readonly List<CacheMapGlAccountsGlPosting> _glAccountsGlPostingMapping;
        private readonly List<CacheMapPlant> _plantMapping;
        private readonly List<CacheMapBranch> _branchMapping;
        private readonly List<CacheMapLocation> _locationMapping;
        private readonly List<CacheMapGlAccountsGlPosting> _glAcountsGlPosting;
        private readonly List<CacheMapProfitCentreGlPosting> _profitCenterGlPosting;
        private readonly List<CacheMapCostCentreGlPosting> _costCenterGlPosting;

        public ImplementMapping(ICacheMapRepository cacheMapRepository,
            IDoMappingBusinessRules mapping,
            IValidate validate)
        {
            _mapping = mapping;
            _validate = validate;

            _docTypeMapping = cacheMapRepository.GetDocTypeMapping();
            _companyCodeMapping = cacheMapRepository.GetCompanyCodeMapping();
            _materialGroupMapping = cacheMapRepository.GetMaterialGroupMapping();
            _purchaseGroupMapping = cacheMapRepository.GetPurchaseGroupMapping();
            _unitOfMeasureMapping = cacheMapRepository.GetUnitOfMeasureMapping();
            _glAccountsGlPostingMapping = cacheMapRepository.GetGlAccountsGlPostingMapping();
            _plantMapping = cacheMapRepository.GetPlantMapping();
            _branchMapping = cacheMapRepository.GetBranchMapping();
            _locationMapping = cacheMapRepository.GetLocationMapping();
            _glAcountsGlPosting = cacheMapRepository.GetGlAccountsGlPostingMapping();
            _profitCenterGlPosting = cacheMapRepository.GetProfitCentreGlPostingMapping();
            _costCenterGlPosting = cacheMapRepository.GetCostCentreGlPostingMapping();
        }

        public string ForPoNumber(string poNumber, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("Po-Number", poNumber);

            if (result.IsOk) result = _mapping.MapPoNumber(poNumber);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForDeliveryDate(string deliveryDate, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDate(Constants.JdeToSapDate, Constants.DeliveryDateName, deliveryDate);

            if (result.IsOk) result = _mapping.MapDeliveryDate(deliveryDate);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForCostCenter(string plant, string lnType, string costCenter, string taxable,
            ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString(Constants.MapCostCentreGlPosting, "plant", plant, "lnType", lnType, "taxable", taxable);

            if (result.IsOk) result = _mapping.MapCostCenter(_materialGroupMapping, _costCenterGlPosting, plant, lnType, costCenter, taxable);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForGlAccount(string glAccount, string refDocNo, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapGlAccount(_glAcountsGlPosting, glAccount, refDocNo);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForGlAccount(string plant, string lnType, string glAccount, string taxable, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString(Constants.MapGlAccountsGlPosting, "plant", plant, "lnType", lnType, "taxable", taxable);

            if (result.IsOk) result = _mapping.MapPurchaseOrderGlAccount(_materialGroupMapping,
                _glAccountsGlPostingMapping,
                plant,
                lnType,
                glAccount,
                taxable);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForNetPrice(string netPrice, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsFloat("netPrice", netPrice);

            if (result.IsOk) result = _mapping.MapNetPrice(netPrice);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForOrderPrUn(string orderprUn, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapOrderPrUn(_unitOfMeasureMapping, orderprUn);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPoUnit(string poUnit, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapPoUnit(_unitOfMeasureMapping, poUnit);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForMaterialGroup(string plant, string lnType, string taxable, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString(Constants.MapMaterialGroup, "plant", plant, "lnType", lnType, "taxable", taxable);

            if (result.IsOk) result = _mapping.MapMaterialGroup(_materialGroupMapping, plant, lnType, taxable);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPlant(string plant, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString(Constants.MapPlant, "plant", plant);

            if (result.IsOk) result = _mapping.MapPlant(_plantMapping, plant);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPlantBranch(string mcu, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString(Constants.MapBranch, "mcu", mcu);

            if (result.IsOk) result = _mapping.MapPlantBranch(_branchMapping, mcu);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPoItem(string poItem, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("poItem", poItem);

            if (result.IsOk) result = _mapping.MapPoItem(poItem);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForPurGroup(string purGroup, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required//

            var result = _mapping.MapPurchaseGroup(_purchaseGroupMapping, purGroup);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPurchaseOrg(string purchOrg)
        {
            //No Validation Required

            var result = _mapping.MapPurchOrg(purchOrg);
            return result.Output;
        }

        public string ForVendor(string vendor, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Mapping Required

            var result = _validate.AsString("vendor", vendor);

            if (result.IsOk) return vendor;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForCreateDate(string creatDate, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDate(Constants.JdeToSapDate, Constants.CreateDateName, creatDate);

            if (result.IsOk) result = _mapping.MapCreatDate(creatDate);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForDocType(string type, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var docTypeResult = _mapping.MapDocType(_docTypeMapping, type);

            if (docTypeResult.IsOk) return docTypeResult.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = docTypeResult.Output });

            return string.Empty;
        }

        public string ForCompCode(string compCode, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapCompCode(_companyCodeMapping, compCode);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPhysicalPackSize(string mcu, string ghMm, string gwMm, ICollection<ProblemDto> problemList, int rowNumber)
        {
            ResultDto result;
            var result1 = _validate.AsString(Constants.MapPhysicalPackSize, "mcu", mcu);
            var result2 = _validate.ParseAsFloat(Constants.MapPhysicalPackSize, "ghMm", ghMm, "gwMm", gwMm);

            if (result1.IsOk && result2.IsOk) result = _mapping.MapPhysicalPackSize(mcu, ghMm, gwMm);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result1.Output });
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result2.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForRedBlueBlack(string srp4Desc)
        {
            //No Validation Required

            var result = _mapping.MapRedBlueBlack(srp4Desc);
            return result.Output;
        }

        public string ForThreeDecimalPlacesOnly(string mt)
        {
            //No Validation Required

            var result = _mapping.MapThreeDecimalPlacesOnly(mt);
            return result.Output;
        }

        public string ForActualWidth(string mcu, string sec1, string sec2, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("GetActualWidth", "mcu", mcu, "sec1", sec1);

            if (result.IsOk) result = _mapping.MapActualWidth(mcu, sec1, sec2);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForActualHeight(string sec1, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsFloat("sec1", sec1);

            if (result.IsOk) result = _mapping.MapActualHeight(sec1);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForProductHierachy(string mcu, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("mcu", mcu);

            if (result.IsOk) result = _mapping.MapProductHierarchy(mcu);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForMaterialGroupByPlant(string mcu, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("mcu", mcu);

            if (result.IsOk) result = _mapping.MapMaterialGroupByPlant(mcu);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForStorageSection(string mcu, string prp4, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("GetStorageSection", "mcu", mcu, "prp4", prp4);

            if (result.IsOk) result = _mapping.MapStorageSection(mcu, prp4);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForStorageType(string mcu, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("mcu", mcu);

            if (result.IsOk) result = _mapping.MapStorageType(_branchMapping, mcu);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForProfitCenre(string mcu, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("mcu", mcu);

            if (result.IsOk) result = _mapping.MapProfitCentre(_branchMapping, mcu);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForNumerator(string input, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDouble("input", input);

            if (result.IsOk) result = _mapping.MapNumerator(input);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForDenominator(string input, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDouble("input", input);

            if (result.IsOk) result = _mapping.MapDenominator(input);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForKgDivMt(string kg, string mt, ICollection<ProblemDto> problemList, int rowNumber)
        {
            ResultDto result;
            var result1 = _validate.ParseAsFloat("kg", kg);
            var result2 = _validate.ParseAsInteger("mt", mt);

            if (result1.IsOk && result2.IsOk) result = _mapping.MapKgPerM(kg, mt);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result1.Output });
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result2.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForPackWeight(string bwck, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsFloat("bwck", bwck);

            if (result.IsOk) result = _mapping.MapPackWeight(bwck);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForLocation(string stgeLoc, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapLocation(_locationMapping, stgeLoc);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForGmCode(string gmCode)
        {
            //No Validation Required

            var result = _mapping.MapGmCode(gmCode);
            return result.Output;
        }

        public string ForHeaderText(string id, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("id", id);

            if (result.IsOk) result = _mapping.MapHeaderText(id);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForDocDate(string docDate)
        {
            //No validation required
            /*WHATCH OUT: bad GoodsDocDate IS valid mapping, with empty output! Hooray SAP!*/

            var result = _mapping.MapGoodsDocDate(docDate);
            return result.Output;
        }

        public string ForPostingDate(string pstngDate, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDate(Constants.JdeToSapDate, Constants.PostingDateName, pstngDate);

            if (result.IsOk) result = _mapping.MapPostingDate(pstngDate);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForGlProfitCentre(string glAccount, string costCentre, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapGlProfitCentre(_glAcountsGlPosting, _profitCenterGlPosting, glAccount, costCentre);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForGlCostCenter(string glAccount, string refDocNo, string costCentre, ICollection<ProblemDto> problemList, int rowNumber)
        {
            //No Validation Required

            var result = _mapping.MapGlCostCentre(_glAcountsGlPosting, _costCenterGlPosting, glAccount, refDocNo, costCentre);

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForGlDocDate(string docDate, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsDate(Constants.JdeToSapDate, Constants.GlDocDateName, docDate);

            if (result.IsOk) result = _mapping.MapGlDocDate(docDate);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForPrp(string mcu, string prp0, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("GetPrp", "mcu", mcu, "prp0", prp0);

            if (result.IsOk) result = _mapping.MapPrpZero(mcu, prp0);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            return result.Output;
        }

        public string ForProductAttribute(string mcu)
        {
            //No Validation Required

            var mapPrat = _mapping.MapProductAttribute(mcu);
            return mapPrat.Output;
        }

        public string ForSizeOne(string mcu, string sec1, string dsc1, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.AsString("GetSizeOne", "mcu", mcu, "sec1", sec1, "dsc1", dsc1);

            if (result.IsOk) result = _mapping.MapSizeOne(mcu, sec1, dsc1);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }

        public string ForTdLine(string prp2Desc2, string dsc1, string dsc2)
        {
            //No Validation Required

            var result = _mapping.MapTdLine(prp2Desc2, dsc1, dsc2);
            return result.Output;
        }

        public string ForMaktx(string dsc12)
        {
            //No Validation Required

            var result = _mapping.MapMaktx(dsc12);
            return result.Output;
        }

        public string ForDzeit(string srp1)
        {
            //No Validation Required

            var result = _mapping.MapDzeit(srp1);
            return result.Output;
        }

        public string ForZzdm2N(string sec1, string sec2)
        {
            //No Validation Required

            var result = _mapping.MapZzdm2N(sec1, sec2);
            return result.Output;
        }

        public string ForUnitCost(string kg, string unitCost, ICollection<ProblemDto> problemList, int rowNumber)
        {
            var result = _validate.ParseAsFloat("UnitCost", "kg", kg, "unitCost", unitCost);

            if (result.IsOk) result = _mapping.MapUnitCost(kg, unitCost);
            else
            {
                problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });
                return string.Empty;
            }

            if (result.IsOk) return result.Output;

            problemList.Add(new ProblemDto { RowNumber = rowNumber, Result = result.Output });

            return string.Empty;
        }
    }
}