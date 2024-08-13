using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapCompanyCodeMap : ClassMap<MapCompanyCode>
    {
        public MapCompanyCodeMap()
        {
            Table("MapCompanyCode");
            Id(x => x.Id);
            Map(x => x.JdeCompanyCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapCompanyCode);
            Map(x => x.SapDescription);
        }
    }
}
