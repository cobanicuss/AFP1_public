using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public interface IDoMappingBusinessRules : IMarkAsBusinessRule
    {
        MappingOutputDto MapCostCenter(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string plant, string lnType, string costCentre, string exInGst);
        MappingOutputDto MapMaterialGroup(List<CacheMapMaterialGroup> materialGroupMapping, string plant, string lnType, string exInGst);
        MappingOutputDto MapPurchaseOrderGlAccount(List<CacheMapMaterialGroup> materialGroupMapping, List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string plant, string lnType, string glAccount, string exInGst);
        MappingOutputDto MapOrderPrUn(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit);
        MappingOutputDto MapPoUnit(List<CacheMapUnitOfMeasure> unitOfMeasureMapping, string poUnit);
        MappingOutputDto MapPlant(List<CacheMapPlant> plantMapping, string plant);
        MappingOutputDto MapPurchaseGroup(List<CacheMapPurchaseGroup> purchaseGroupMapping, string purchaseGroup);
        MappingOutputDto MapDocType(List<CacheMapDocTypes> docTypeMapping, string docType);
        MappingOutputDto MapCompCode(List<CacheMapCompanyCode> companyCodeMapping, string compCode);
        MappingOutputDto MapLocation(List<CacheMapLocation> locationMapping, string location);
        MappingOutputDto MapPurchOrg(string purchOrg);
        MappingOutputDto MapGmCode(string gmCode);
        MappingOutputDto MapGlAccount(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, string glAccount, string refDocNo);
        MappingOutputDto MapGlProfitCentre(List<CacheMapGlAccountsGlPosting> glAcountsGlPosting, List<CacheMapProfitCentreGlPosting> profitCenterGlPosting, string glAccount, string costCenter);
        MappingOutputDto MapGlCostCentre(List<CacheMapGlAccountsGlPosting> glAccountsGlPosting, List<CacheMapCostCentreGlPosting> costCenterGlPosting, string glAccount, string refDocNo, string costCentre);
        MappingOutputDto MapPoItem(string poItem);
        MappingOutputDto MapPoNumber(string poNumber);
        MappingOutputDto MapCreatDate(string createDate);
        MappingOutputDto MapDeliveryDate(string deliveryDate);
        MappingOutputDto MapPostingDate(string postingDate);
        MappingOutputDto MapGlDocDate(string docDate);
        MappingOutputDto MapGoodsDocDate(string docDate);
        MappingOutputDto MapVendor(string vendor);
        MappingOutputDto MapNetPrice(string netPrice);
        MappingOutputDto MapHeaderText(string id);
    }
}