using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapUnitOfMeasureMap : ClassMap<MapUnitOfMeasure>
    {
        public MapUnitOfMeasureMap()
        {
            Table("MapUnitOfMeasure");
            Id(x => x.Id);
            Map(x => x.JdeUom);
            Map(x => x.JdeDescription);
            Map(x => x.IsoUom);
            Map(x => x.IsoUomDescription);
            Map(x => x.SapUom);
            Map(x => x.SapDescription);
        }
    }
}
