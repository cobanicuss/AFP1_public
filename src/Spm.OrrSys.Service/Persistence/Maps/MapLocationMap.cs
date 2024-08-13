using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapLocationMap : ClassMap<MapLocation>
    {
        public MapLocationMap()
        {
            Table("MapLocation");
            Id(x => x.Id);
            Map(x => x.JdePlant);
            Map(x => x.JdeLocationCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapPlant);
            Map(x => x.SapStorageLocation);
            Map(x => x.SapDescription);
        }
    }
}
