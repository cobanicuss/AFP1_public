using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class OrrSysF554801ZMap : ClassMap<OrrSysF554801Z>
    {
        public OrrSysF554801ZMap()
        {
            Table(Constants.TableNameF554801Z);
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
        }
    }
}