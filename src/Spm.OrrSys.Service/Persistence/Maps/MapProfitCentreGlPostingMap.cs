using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapProfitCentreGlPostingMap : ClassMap<MapProfitCentreGlPosting>
    {
        public MapProfitCentreGlPostingMap()
        {
            Table("MapProfitCentreGlPosting");
            Id(x => x.Id);
            Map(x => x.JdeDepartmentCode);
            Map(x => x.JdeDescription);
            Map(x => x.JdeBranch);
            Map(x => x.SapProfitCentre);
            Map(x => x.SapDescription);
        }
    }
}
