using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class MapGlAccountsGlPostingMap : ClassMap<MapGlAccountsGlPosting>
    {
        public MapGlAccountsGlPostingMap()
        {
            Table("MapGlAccountsGlPosting");
            Id(x => x.Id);
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
