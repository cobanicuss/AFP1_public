using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Spm.File.Watcher.Messages;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IOrrSysMappingRepository
    {
        List<MapCompanyCode> GetCompanyCodeMapping();
        List<MapDocTypes> GetDocTypeMapping();
        List<MapMaterialGroup> GetMaterialGroupMapping();
        List<MapPurchaseGroup> GetPurchaseGroupMapping();
        List<MapUnitOfMeasure> GetUnitOfMeasureMapping();
        List<MapBranch> GetBranchMapping();
        List<MapCostCentreGlPosting> GetCostCentreGlPostingMapping();
        List<MapLocation> GetLocationMapping();
        List<MapGlAccountsGlPosting> GetGlAccountsGlPostingMapping();
        List<MapProfitCentreGlPosting> GetProfitCentreGlPostingMapping();
        List<MapPlant> GetPlantMapping();
    }

    public class OrrSysMappingRepository : IOrrSysMappingRepository
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public List<MapCompanyCode> GetCompanyCodeMapping()
        {
            var companyCodeListMap = Session.QueryOver<Domain.MapCompanyCode>().List();

            var returnVal = companyCodeListMap.Select(x => new MapCompanyCode
            {
                Id = x.Id,
                JdeCompanyCode = x.JdeCompanyCode,
                SapCompanyCode = x.SapCompanyCode,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapDocTypes> GetDocTypeMapping()
        {
            var docTypeseListMap = Session.QueryOver<Domain.MapDocTypes>().List();

            var returnVal = docTypeseListMap.Select(x => new MapDocTypes
            {
                Id = x.Id,
                SapDocType = x.SapDocType,
                JdeDocType = x.JdeDocType,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapMaterialGroup> GetMaterialGroupMapping()
        {
            var materialGroupListMap = Session.QueryOver<Domain.MapMaterialGroup>().List();

            var returnVal = materialGroupListMap.Select(x => new MapMaterialGroup
            {
                Id = x.Id,
                JdeStockType = x.JdeStockType,
                JdeDescription = x.JdeDescription,
                SapMatrialGroup = x.SapMatrialGroup,
                SapDescription = x.SapDescription,
                SapGlAcc = x.SapGlAcc,
                SapCostCentre = x.SapCostCentre
            });

            return returnVal.ToList();
        }

        public List<MapPurchaseGroup> GetPurchaseGroupMapping()
        {
            var purchaseGroupListMap = Session.QueryOver<Domain.MapPurchaseGroup>().List();

            var returnVal = purchaseGroupListMap.Select(x => new MapPurchaseGroup
            {
                Id = x.Id,
                SapPurchaseGroup = x.SapPurchaseGroup,
                JdePurchaseGroup = x.JdePurchaseGroup,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapUnitOfMeasure> GetUnitOfMeasureMapping()
        {
            var unitOfMeasureListMap = Session.QueryOver<Domain.MapUnitOfMeasure>().List();

            var returnVal = unitOfMeasureListMap.Select(x => new MapUnitOfMeasure
            {
                Id = x.Id,
                SapUom = x.SapUom,
                JdeUom = x.JdeUom,
                IsoUom = x.IsoUom,
                IsoUomDescription = x.IsoUomDescription,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapBranch> GetBranchMapping()
        {
            var brancheListMap = Session.QueryOver<Domain.MapBranch>().List();

            var returnVal = brancheListMap.Select(x => new MapBranch
            {
                Id = x.Id,
                SapPlant = x.SapPlant,
                JdeBranchCode = x.JdeBranchCode,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription,
                SapProfitCentre = x.SapProfitCentre,
                SapProfitCentreDescription = x.SapProfitCentreDescription,
                StorageType = x.StorageType,
                StorageTypeDescription = x.StorageTypeDescription
            });

            return returnVal.ToList();
        }

        public List<MapCostCentreGlPosting> GetCostCentreGlPostingMapping()
        {
            var costCentreGlPostingListMap = Session.QueryOver<Domain.MapCostCentreGlPosting>().List();

            var returnVal = costCentreGlPostingListMap.Select(x => new MapCostCentreGlPosting
            {
                Id = x.Id,
                JdeDepartmentCode = x.JdeDepartmentCode,
                SapCostCentre = x.SapCostCentre,
                JdeBranch = x.JdeBranch,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapLocation> GetLocationMapping()
        {
            var locationListMap = Session.QueryOver<Domain.MapLocation>().List();

            var returnVal = locationListMap.Select(x => new MapLocation
            {
                Id = x.Id,
                JdePlant = x.JdePlant,
                JdeLocationCode = x.JdeLocationCode,
                JdeDescription = x.JdeDescription,
                SapPlant = x.SapPlant,
                SapStorageLocation = x.SapStorageLocation,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapGlAccountsGlPosting> GetGlAccountsGlPostingMapping()
        {
            var glAccountsGlPostingListMap = Session.QueryOver<Domain.MapGlAccountsGlPosting>().List();

            var returnVal = glAccountsGlPostingListMap.Select(x => new MapGlAccountsGlPosting
            {
                Id = x.Id,
                JdeGlAccount = x.JdeGlAccount,
                JdeDescription = x.JdeDescription,
                JdeGroup = x.JdeGroup,
                JdeReport = x.JdeReport,
                SapGlAccount = x.SapGlAccount,
                SapType = x.SapType,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapProfitCentreGlPosting> GetProfitCentreGlPostingMapping()
        {
            var profitCentreGlPostingListMap = Session.QueryOver<Domain.MapProfitCentreGlPosting>().List();

            var returnVal = profitCentreGlPostingListMap.Select(x => new MapProfitCentreGlPosting
            {
                Id = x.Id,
                JdeBranch = x.JdeBranch,
                JdeDepartmentCode = x.JdeDepartmentCode,
                JdeDescription = x.JdeDescription,
                SapProfitCentre = x.SapProfitCentre,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }

        public List<MapPlant> GetPlantMapping()
        {
            var plantListMap = Session.QueryOver<Domain.MapPlant>().List();

            var returnVal = plantListMap.Select(x => new MapPlant
            {
                Id = x.Id,
                SapPlant = x.SapPlant,
                JdeBranchCode = x.JdeBranchCode,
                JdeDescription = x.JdeDescription,
                SapDescription = x.SapDescription
            });

            return returnVal.ToList();
        }
    }
}