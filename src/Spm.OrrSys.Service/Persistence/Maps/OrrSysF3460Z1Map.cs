using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class OrrSysF3460Z1Map : ClassMap<OrrSysF3460Z1>
    {
        public OrrSysF3460Z1Map()
        {
            Table(Constants.TableNameF3460Z1);
            CompositeId()
                .KeyProperty(x => x.FTEDUS)
                .KeyProperty(x => x.FTEDBT)
                .KeyProperty(x => x.FTEDTN)
                .KeyProperty(x => x.FTEDLN);
            Map(x => x.FTTYTN);
            Map(x => x.FTEDDT);
            Map(x => x.FTDRIN);
            Map(x => x.FTEDSP);
            Map(x => x.FTTNAC);
            Map(x => x.FTITM);
            Map(x => x.FTLITM);
            Map(x => x.FTAITM);
            Map(x => x.FTMCU);
            Map(x => x.FTDRQJ);
            Map(x => x.FTUORG);
            Map(x => x.FTFQT);
            Map(x => x.FTTYPF);
            Map(x => x.FTDCTO);
        }
    }
}