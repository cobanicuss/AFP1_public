using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IDoMappingBusinessRules : IMarkAsBusinessRule
    {
        ResultDto MapCostCenter(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string plant, string lnType, string costCentre, string exInGst);
        ResultDto MapMaterialGroup(List<CacheMapMaterialGroup> materialGroupMapping, string plant, string lnType, string exInGst);
        ResultDto MapMaterialGroupByPlant(string plant);
        ResultDto MapPurchaseOrderGlAccount(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string plant, string lnType, string glAccount, string exInGst);
        ResultDto MapOrderPrUn(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit);
        ResultDto MapPoUnit(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit);
        ResultDto MapPlant(List<CacheMapPlant> plantMapping, string branch);
        ResultDto MapPlantBranch(List<CacheMapBranch> branchMapping, string branch);
        ResultDto MapPurchaseGroup(List<CacheMapPurchaseGroup> purchaseGroupMapping, string purchaseGroup);
        ResultDto MapDocType(List<CacheMapDocTypes> docTypeMapping, string type);
        ResultDto MapCompCode(List<CacheMapCompanyCode> companyCodeMapping, string compCode);
        ResultDto MapLocation(List<CacheMapLocation> locationMapping, string location);
        ResultDto MapPurchOrg(string purchOrg);
        ResultDto MapGmCode(string gmCode);
        ResultDto MapGlAccount(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount, string refDocNo);
        ResultDto MapProfitCentre(List<CacheMapBranch> branchMapping, string branch);
        ResultDto MapStorageType(List<CacheMapBranch> branchMapping, string branch);
        ResultDto MapStorageSection(string mcu, string prp4);
        ResultDto MapGlProfitCentre(List<CacheMapGlAccountsGlPosting> glAcountsGlPosting, List<CacheMapProfitCentreGlPosting> profitCenterGlPosting, string glAccount, string costCenter);
        ResultDto MapGlCostCentre(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string glAccount, string refDocNo, string costCentre);
        ResultDto MapPoItem(string poItem);
        ResultDto MapPoNumber(string poNumber);
        ResultDto MapCreatDate(string createDate);
        ResultDto MapDeliveryDate(string deliveryDate);
        ResultDto MapPostingDate(string postingDate);
        ResultDto MapGlDocDate(string docDate);
        ResultDto MapGoodsDocDate(string docDate);
        ResultDto MapNetPrice(string netPrice);
        ResultDto MapHeaderText(string id);
        ResultDto MapProductHierarchy(string plant);
        ResultDto MapKgPerM(string kg, string m);
        ResultDto MapThreeDecimalPlacesOnly(string input);
        ResultDto MapRedBlueBlack(string input);
        ResultDto MapDenominator(string input);
        ResultDto MapNumerator(string input);
        ResultDto MapPhysicalPackSize(string mcu, string gh, string gw);
        ResultDto MapPackWeight(string packWeight);
        ResultDto MapPrpZero(string mcu, string prp0);
        ResultDto MapActualWidth(string mcu, string sec1, string sec2);
        ResultDto MapActualHeight(string sec1);
        ResultDto MapProductAttribute(string mcu);
        ResultDto MapSizeOne(string mcu, string sec1, string dsc1);
        ResultDto MapTdLine(string prp2Desc2, string dsc1, string dsc2);
        ResultDto MapMaktx(string dsc12);
        ResultDto MapDzeit(string srp1);
        ResultDto MapZzdm2N(string sec1, string sec2);
        ResultDto MapUnitCost(string kg, string unitCost);
    }
}