using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapPurchaseGroupMap : ClassMap<CacheMapPurchaseGroup>
    {
        public CacheMapPurchaseGroupMap()
        {
            Table("CacheMapPurchaseGroup");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdePurchaseGroup);
            Map(x => x.JdeDescription);
            Map(x => x.SapPurchaseGroup);
            Map(x => x.SapDescription);
        }
    }
}
