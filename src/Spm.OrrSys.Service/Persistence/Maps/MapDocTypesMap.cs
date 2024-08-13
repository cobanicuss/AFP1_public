using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapDocTypesMap : ClassMap<MapDocTypes>
    {
        public MapDocTypesMap()
        {
            Table("MapDocTypes");
            Id(x => x.Id);
            Map(x => x.JdeDocType);
            Map(x => x.JdeDescription);
            Map(x => x.SapDocType);
            Map(x => x.SapDescription);
        }
    }
}
