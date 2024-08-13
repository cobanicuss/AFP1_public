using System.Collections.Generic;
using Spm.File.Watcher.Messages;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Repository
{
    public interface ICacheMapRepository
    {
        void LoadNewCacheMapping(CacheMapResponseCommand cache);

        List<CacheMapCompanyCode> GetCompanyCodeMapping();
        List<CacheMapDocTypes> GetDocTypeMapping();
        List<CacheMapMaterialGroup> GetMaterialGroupMapping();
        List<CacheMapPurchaseGroup> GetPurchaseGroupMapping();
        List<CacheMapUnitOfMeasure> GetUnitOfMeasureMapping();
        List<CacheMapBranch> GetBranchMapping();
        List<CacheMapCostCentreGlPosting> GetCostCentreGlPostingMapping();
        List<CacheMapLocation> GetLocationMapping();
        List<CacheMapGlAccountsGlPosting> GetGlAccountsGlPostingMapping();
        List<CacheMapProfitCentreGlPosting> GetProfitCentreGlPostingMapping();
        List<CacheMapPlant> GetPlantMapping();
    }
}