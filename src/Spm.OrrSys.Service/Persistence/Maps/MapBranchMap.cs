using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapBranchMap : ClassMap<MapBranch>
    {
        public MapBranchMap()
        {
            Table("MapBranch");
            Id(x => x.Id);
            Map(x => x.SapPlant);
            Map(x => x.SapDescription);
            Map(x => x.JdeBranchCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapProfitCentre);
            Map(x => x.SapProfitCentreDescription);
            Map(x => x.StorageType);
            Map(x => x.StorageTypeDescription);
        }
    }
}