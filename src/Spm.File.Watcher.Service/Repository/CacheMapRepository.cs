using System.Collections.Generic;
using NHibernate;
using Spm.File.Watcher.Messages;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Repository
{
    public class CacheMapRepository : ICacheMapRepository
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void LoadNewCacheMapping(CacheMapResponseCommand cache)
        {
            using (var transaction = Session.BeginTransaction())
            {
                ReloadCacheMapBranch(cache.MapBranchList);
                ReloadCacheMapCompanyCode(cache.MapCompanyCodeList);
                ReloadCacheMapCostCentreGlPosting(cache.MapCostCentreGlPostingList);
                ReloadCacheMapDocTypes(cache.MapDocTypesList);
                ReloadCacheMapGlAccountsGlPosting(cache.MapGlAccountsGlPostingList);
                ReloadCacheMapLocation(cache.MapLocationList);
                ReloadCacheMapMaterialGroup(cache.MapMaterialGroupList);
                ReloadCacheMapProfitCentreGlPosting(cache.MapProfitCentreGlPostingList);
                ReloadCacheMapPurchaseGroup(cache.MapPurchaseGroupList);
                ReloadCacheMapUnitOfMeasure(cache.MapUnitOfMeasureList);
                ReloadCacheMapPlant(cache.MapPlantList);

                transaction.Commit();
                Session.Flush();
            }
        }

        private void ReloadCacheMapUnitOfMeasure(IEnumerable<MapUnitOfMeasure> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapUnitOfMeasure]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapUnitOfMeasure
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    IsoUom = item.IsoUom,
                    IsoUomDescription = item.IsoUomDescription,
                    JdeUom = item.JdeUom,
                    SapUom = item.SapUom
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapPurchaseGroup(IEnumerable<MapPurchaseGroup> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapPurchaseGroup]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapPurchaseGroup
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    JdePurchaseGroup = item.JdePurchaseGroup,
                    SapPurchaseGroup = item.SapPurchaseGroup
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapProfitCentreGlPosting(IEnumerable<MapProfitCentreGlPosting> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapProfitCentreGlPosting]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapProfitCentreGlPosting
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    JdeDepartmentCode = item.JdeDepartmentCode,
                    JdeBranch = item.JdeBranch,
                    SapProfitCentre = item.SapProfitCentre
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapMaterialGroup(IEnumerable<MapMaterialGroup> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapMaterialGroup]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapMaterialGroup
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    JdeStockType = item.JdeStockType,
                    SapDescription = item.SapDescription,
                    SapGlAcc = item.SapGlAcc,
                    SapCostCentre = item.SapCostCentre,
                    SapMatrialGroup = item.SapMatrialGroup,
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapLocation(IEnumerable<MapLocation> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapLocation]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapLocation
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    SapPlant = item.SapPlant,
                    SapStorageLocation = item.SapStorageLocation,
                    JdeLocationCode = item.JdeLocationCode,
                    JdePlant = item.JdePlant
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapGlAccountsGlPosting(IEnumerable<MapGlAccountsGlPosting> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapGlAccountsGlPosting]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapGlAccountsGlPosting
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    SapGlAccount = item.SapGlAccount,
                    JdeGlAccount = item.JdeGlAccount,
                    JdeReport = item.JdeReport,
                    JdeGroup = item.JdeGroup,
                    SapType = item.SapType
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapDocTypes(IEnumerable<MapDocTypes> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapDocTypes]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapDocTypes
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    SapDocType = item.SapDocType,
                    JdeDocType = item.JdeDocType
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapCostCentreGlPosting(IEnumerable<MapCostCentreGlPosting> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapCostCentreGlPosting]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapCostCentreGlPosting
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    JdeDepartmentCode = item.JdeDepartmentCode,
                    JdeBranch = item.JdeBranch,
                    SapCostCentre = item.SapCostCentre
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapCompanyCode(IEnumerable<MapCompanyCode> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapCompanyCode]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapCompanyCode
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapCompanyCode = item.SapCompanyCode,
                    SapDescription = item.SapDescription,
                    JdeCompanyCode = item.JdeCompanyCode
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapBranch(IEnumerable<MapBranch> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapBranch]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapBranch
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    SapPlant = item.SapPlant,
                    JdeBranchCode = item.JdeBranchCode,
                    SapProfitCentre = item.SapProfitCentre,
                    SapProfitCentreDescription = item.SapProfitCentreDescription,
                    StorageType = item.StorageType,
                    StorageTypeDescription = item.StorageTypeDescription
                };
                Session.Save(newItem);
            }
        }

        private void ReloadCacheMapPlant(IEnumerable<MapPlant> cache)
        {
            Session.CreateSQLQuery("delete from [SPM.FileWatcher].[dbo].[CacheMapPlant]").ExecuteUpdate();
            foreach (var item in cache)
            {
                var newItem = new CacheMapPlant
                {
                    Id = item.Id,
                    JdeDescription = item.JdeDescription,
                    SapDescription = item.SapDescription,
                    SapPlant = item.SapPlant,
                    JdeBranchCode = item.JdeBranchCode
                };
                Session.Save(newItem);
            }
        }

        public List<CacheMapCompanyCode> GetCompanyCodeMapping()
        {
            var companyCodeListMap = Session.QueryOver<CacheMapCompanyCode>().List();
            return (List<CacheMapCompanyCode>)companyCodeListMap;
        }

        public List<CacheMapDocTypes> GetDocTypeMapping()
        {
            var docTypeseListMap = Session.QueryOver<CacheMapDocTypes>().List();
            return (List<CacheMapDocTypes>)docTypeseListMap;
        }

        public List<CacheMapMaterialGroup> GetMaterialGroupMapping()
        {
            var materialGroupListMap = Session.QueryOver<CacheMapMaterialGroup>().List();
            return (List<CacheMapMaterialGroup>)materialGroupListMap;
        }

        public List<CacheMapPurchaseGroup> GetPurchaseGroupMapping()
        {
            var purchaseGroupListMap = Session.QueryOver<CacheMapPurchaseGroup>().List();
            return (List<CacheMapPurchaseGroup>)purchaseGroupListMap;
        }

        public List<CacheMapUnitOfMeasure> GetUnitOfMeasureMapping()
        {
            var unitOfMeasureListMap = Session.QueryOver<CacheMapUnitOfMeasure>().List();
            return (List<CacheMapUnitOfMeasure>)unitOfMeasureListMap;
        }

        public List<CacheMapBranch> GetBranchMapping()
        {
            var brancheListMap = Session.QueryOver<CacheMapBranch>().List();
            return (List<CacheMapBranch>)brancheListMap;
        }

        public List<CacheMapCostCentreGlPosting> GetCostCentreGlPostingMapping()
        {
            var costCentreGlPostingListMap = Session.QueryOver<CacheMapCostCentreGlPosting>().List();
            return (List<CacheMapCostCentreGlPosting>)costCentreGlPostingListMap;
        }

        public List<CacheMapLocation> GetLocationMapping()
        {
            var locationListMap = Session.QueryOver<CacheMapLocation>().List();
            return (List<CacheMapLocation>)locationListMap;
        }

        public List<CacheMapGlAccountsGlPosting> GetGlAccountsGlPostingMapping()
        {
            var glAccountsGlPostingListMap = Session.QueryOver<CacheMapGlAccountsGlPosting>().List();
            return (List<CacheMapGlAccountsGlPosting>)glAccountsGlPostingListMap;
        }

        public List<CacheMapProfitCentreGlPosting> GetProfitCentreGlPostingMapping()
        {
            var profitCentreGlPostingListMap = Session.QueryOver<CacheMapProfitCentreGlPosting>().List();
            return (List<CacheMapProfitCentreGlPosting>)profitCentreGlPostingListMap;
        }

        public List<CacheMapPlant> GetPlantMapping()
        {
            var profitCentreGlPostingListMap = Session.QueryOver<CacheMapPlant>().List();
            return (List<CacheMapPlant>)profitCentreGlPostingListMap;
        }
    }
}