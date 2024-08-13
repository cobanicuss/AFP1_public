using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapProfitCentreGlPostingMap : ClassMap<CacheMapProfitCentreGlPosting>
    {
        public CacheMapProfitCentreGlPostingMap()
        {
            Table("CacheMapProfitCentreGlPosting");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeDepartmentCode);
            Map(x => x.JdeDescription);
            Map(x => x.JdeBranch);
            Map(x => x.SapProfitCentre);
            Map(x => x.SapDescription);
        }
    }
}
