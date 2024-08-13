using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapMaterialGroupMap : ClassMap<MapMaterialGroup>
    {
        public MapMaterialGroupMap()
        {
            Table("MapMaterialGroup");
            Id(x => x.Id);
            Map(x => x.JdeStockType);
            Map(x => x.JdeDescription);
            Map(x => x.SapMatrialGroup);
            Map(x => x.SapDescription);
            Map(x => x.SapGlAcc);
            Map(x => x.SapCostCentre);
        }
    }
}
