using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapMaterialGroupMap : ClassMap<CacheMapMaterialGroup>
    {
        public CacheMapMaterialGroupMap()
        {
            Table("CacheMapMaterialGroup");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeStockType);
            Map(x => x.JdeDescription);
            Map(x => x.SapMatrialGroup);
            Map(x => x.SapDescription);
            Map(x => x.SapGlAcc);
            Map(x => x.SapCostCentre);
        }
    }
}