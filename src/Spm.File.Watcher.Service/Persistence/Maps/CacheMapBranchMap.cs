using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapBranchMap : ClassMap<CacheMapBranch>
    {
        public CacheMapBranchMap()
        {
            Table("CacheMapBranch");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SapPlant);
            Map(x => x.SapDescription);
            Map(x => x.JdeBranchCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapProfitCentre);
            Map(x => x.SapProfitCentreDescription);
            Map(x => x.StorageType);
            Map(x => x.StorageTypeDescription);
        }
    }
}