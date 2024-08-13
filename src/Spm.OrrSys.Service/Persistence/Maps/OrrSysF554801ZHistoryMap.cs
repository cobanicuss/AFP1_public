using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class OrrSysF554801ZHistoryMap : ClassMap<OrrSysF554801ZHistory>
    {
        public OrrSysF554801ZHistoryMap()
        {
            Table(Constants.TableNameF554801ZHistory);
            CompositeId()
                .KeyProperty(x => x.SYEDUS)
                .KeyProperty(x => x.SYEDBT)
                .KeyProperty(x => x.SYEDTN)
                .KeyProperty(x => x.SYEDLN);
            Map(x => x.SYEDCT);
            Map(x => x.SYTYTN);
            Map(x => x.SYEDDT);
            Map(x => x.SYDRIN);
            Map(x => x.SYEDDL);
            Map(x => x.SYEDSP);
            Map(x => x.SYTNAC);
            Map(x => x.SYDCTO);
            Map(x => x.SYITM);
            Map(x => x.SYLITM);
            Map(x => x.SYDRQJ);
            Map(x => x.SYUORG);
            Map(x => x.SYSOQS);
            Map(x => x.SYUM);
            Map(x => x.SYMCU);
            Map(x => x.SYVR01);
            Map(x => x.DateTimeImplemented);
            Map(x => x.DateRequested);
        }
    }
}