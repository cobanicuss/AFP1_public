using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapGlAccountsGlPostingMap : ClassMap<CacheMapGlAccountsGlPosting>
    {
        public CacheMapGlAccountsGlPostingMap()
        {
            Table("CacheMapGlAccountsGlPosting");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeGlAccount);
            Map(x => x.JdeDescription);
            Map(x => x.JdeReport);
            Map(x => x.JdeGroup);
            Map(x => x.SapGlAccount);
            Map(x => x.SapDescription);
            Map(x => x.SapType);
        }
    }
}
