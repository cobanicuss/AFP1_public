using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class DemandSapJdeWoMap : ClassMap<DemandSapJdeWo>
    {
        public DemandSapJdeWoMap()
        {
            Table("DemandSAPJDEWO");
            Id(x => x.Id);
            Map(x => x.Ibitm);
            Map(x => x.Iblitm);
            Map(x => x.CyclePlannedBefore);
            Map(x => x.Qty);
            Map(x => x.BaseUom);
            Map(x => x.Ibmcu);
            Map(x => x.ProOrder);
        }
    }
}
