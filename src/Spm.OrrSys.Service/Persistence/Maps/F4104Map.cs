using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class F4104Map : ClassMap<F4104>
    {
        public F4104Map()
        {
            Table("F4104");
            Id(x => x.IVCITM);
            Map(x => x.IVAITM);
            Map(x => x.IVITM);
            Map(x => x.IVLITM);
            Map(x => x.IVXRT);
        }
    }
}
