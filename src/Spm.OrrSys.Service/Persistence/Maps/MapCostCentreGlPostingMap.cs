using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapCostCentreGlPostingMap : ClassMap<MapCostCentreGlPosting>
    {
        public MapCostCentreGlPostingMap()
        {
            Table("MapCostCentreGlPosting");
            Id(x => x.Id);
            Map(x => x.JdeDepartmentCode);
            Map(x => x.JdeDescription);
            Map(x => x.JdeBranch);
            Map(x => x.SapCostCentre);
            Map(x => x.SapDescription);
        }
    }
}