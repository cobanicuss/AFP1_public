using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapPlantMap : ClassMap<MapPlant>
    {
        public MapPlantMap()
        {
            Table("MapPlant");
            Id(x => x.Id);
            Map(x => x.SapPlant);
            Map(x => x.SapDescription);
            Map(x => x.JdeBranchCode);
            Map(x => x.JdeDescription);
        }
    }
}
