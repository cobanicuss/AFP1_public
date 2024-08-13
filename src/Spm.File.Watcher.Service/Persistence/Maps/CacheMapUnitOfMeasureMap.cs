using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapUnitOfMeasureMap : ClassMap<CacheMapUnitOfMeasure>
    {
        public CacheMapUnitOfMeasureMap()
        {
            Table("CacheMapUnitOfMeasure");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeUom);
            Map(x => x.JdeDescription);
            Map(x => x.IsoUom);
            Map(x => x.IsoUomDescription);
            Map(x => x.SapUom);
            Map(x => x.SapDescription);
        }
    }
}