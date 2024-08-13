using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapPurchaseGroupMap : ClassMap<MapPurchaseGroup>
    {
        public MapPurchaseGroupMap()
        {
            Table("MapPurchaseGroup");
            Id(x => x.Id);
            Map(x => x.JdePurchaseGroup);
            Map(x => x.JdeDescription);
            Map(x => x.SapPurchaseGroup);
            Map(x => x.SapDescription);
        }
    }
}
