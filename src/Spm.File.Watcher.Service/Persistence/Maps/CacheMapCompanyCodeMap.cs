using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class CacheMapCompanyCodeMap : ClassMap<CacheMapCompanyCode>
    {
        public CacheMapCompanyCodeMap()
        {
            Table("CacheMapCompanyCode");
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.JdeCompanyCode);
            Map(x => x.JdeDescription);
            Map(x => x.SapCompanyCode);
            Map(x => x.SapDescription);
        }
    }
}
