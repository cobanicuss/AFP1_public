using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapDocTypesMap : ClassMap<CacheMapDocTypes>
    {
        public CacheMapDocTypesMap()
        {
            Table("CacheMapDocTypes");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeDocType);
            Map(x => x.JdeDescription);
            Map(x => x.SapDocType);
            Map(x => x.SapDescription);
        }
    }
}
