using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapCostCentreGlPostingMap : ClassMap<CacheMapCostCentreGlPosting>
    {
        public CacheMapCostCentreGlPostingMap()
        {
            Table("CacheMapCostCentreGlPosting");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeDepartmentCode);
            Map(x => x.JdeDescription);
            Map(x => x.JdeBranch);
            Map(x => x.SapCostCentre);
            Map(x => x.SapDescription);
        }
    }
}