using System.Collections.Generic;
using NServiceBus;

namespace Spm.File.Watcher.Messages
{
    public class CacheMapResponseCommand : ICommand
    {
        public string FileWatcherSagaId {get; set; }
        public List<MapBranch> MapBranchList { get; set; }
        public List<MapCompanyCode> MapCompanyCodeList { get; set; }
        public List<MapCostCentreGlPosting> MapCostCentreGlPostingList { get; set; }
        public List<MapDocTypes> MapDocTypesList { get; set; }
        public List<MapGlAccountsGlPosting> MapGlAccountsGlPostingList { get; set; }
        public List<MapLocation> MapLocationList { get; set; }
        public List<MapMaterialGroup> MapMaterialGroupList { get; set; }
        public List<MapProfitCentreGlPosting> MapProfitCentreGlPostingList { get; set; }
        public List<MapPurchaseGroup> MapPurchaseGroupList { get; set; }
        public List<MapUnitOfMeasure> MapUnitOfMeasureList { get; set; }
        public List<MapPlant> MapPlantList { get; set; }
    }

    public class MapBranch
    {
        public int Id { get; set; }
        public string SapPlant { get; set; }
        public string SapDescription { get; set; }
        public string JdeBranchCode { get; set; }
        public string JdeDescription { get; set; }
        public string SapProfitCentre { get; set; }
        public string SapProfitCentreDescription { get; set; }
        public string StorageType { get; set; }
        public string StorageTypeDescription { get; set; }
    }

    public class MapCompanyCode
    {
        public int Id { get; set; }
        public string JdeCompanyCode { get; set; }
        public string JdeDescription { get; set; }
        public string SapCompanyCode { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapCostCentreGlPosting
    {
        public int Id { get; set; }
        public string JdeDepartmentCode { get; set; }
        public string JdeDescription { get; set; }
        public string JdeBranch { get; set; }
        public string SapCostCentre { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapDocTypes
    {
        public int Id { get; set; }
        public string JdeDocType { get; set; }
        public string JdeDescription { get; set; }
        public string SapDocType { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapGlAccountsGlPosting
    {
        public int Id { get; set; }
        public string JdeGlAccount { get; set; }
        public string JdeDescription { get; set; }
        public string JdeReport { get; set; }
        public string JdeGroup { get; set; }
        public string SapGlAccount { get; set; }
        public string SapDescription { get; set; }
        public string SapType { get; set; }
    }

    public class MapLocation
    {
        public int Id { get; set; }
        public string JdePlant { get; set; }
        public string JdeLocationCode { get; set; }
        public string JdeDescription { get; set; }
        public string SapPlant { get; set; }
        public string SapStorageLocation { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapMaterialGroup
    {
        public int Id { get; set; }
        public string JdeStockType { get; set; }
        public string JdeDescription { get; set; }
        public string SapMatrialGroup { get; set; }
        public string SapDescription { get; set; }
        public string SapGlAcc { get; set; }
        public string SapCostCentre { get; set; }
    }

    public class MapProfitCentreGlPosting
    {
        public int Id { get; set; }
        public string JdeDepartmentCode { get; set; }
        public string JdeDescription { get; set; }
        public string JdeBranch { get; set; }
        public string SapProfitCentre { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapPurchaseGroup
    {
        public int Id { get; set; }
        public string JdePurchaseGroup { get; set; }
        public string JdeDescription { get; set; }
        public string SapPurchaseGroup { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapUnitOfMeasure
    {
        public int Id { get; set; }
        public string JdeUom { get; set; }
        public string JdeDescription { get; set; }
        public string IsoUom { get; set; }
        public string IsoUomDescription { get; set; }
        public string SapUom { get; set; }
        public string SapDescription { get; set; }
    }

    public class MapPlant
    {
        public int Id { get; set; }
        public string SapPlant { get; set; }
        public string SapDescription { get; set; }
        public string JdeBranchCode { get; set; }
        public string JdeDescription { get; set; }
    }
}