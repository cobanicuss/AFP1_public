using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapLocationMap : ClassMap<CacheMapLocation>
    {
        public CacheMapLocationMap()
        {
            Table("CacheMapLocation");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdePlant);
            Map(x => x.JdeLocationCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapPlant);
            Map(x => x.SapStorageLocation);
            Map(x => x.SapDescription);
        }
    }
}
