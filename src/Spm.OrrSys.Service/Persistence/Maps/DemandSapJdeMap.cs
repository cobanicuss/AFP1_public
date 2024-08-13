using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class DemandSapJdeMap : ClassMap<DemandSapJde>
    {
        public DemandSapJdeMap()
        {
            Table("DemandSAPJDE");
            Id(x => x.Id);
            Map(x => x.Material);
            Map(x => x.Ibitm);
            Map(x => x.Ibmcu);
            Map(x => x.ForcastType);
            Map(x => x.Numa);
            Map(x => x.CyclePlannedBefore);
            Map(x => x.Qyy);
        }
    }
}
