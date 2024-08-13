using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapPlantMap : ClassMap<CacheMapPlant>
    {
        public CacheMapPlantMap()
        {
            Table("CacheMapPlant");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SapPlant);
            Map(x => x.SapDescription);
            Map(x => x.JdeBranchCode);
            Map(x => x.JdeDescription);
        }
    }
}
