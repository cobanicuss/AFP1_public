using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class F4101Map : ClassMap<F4101>
    {
        public F4101Map()
        {
            Table("F4101");
            Id(x => x.IMITM);
            Map(x => x.IMAITM);
            Map(x => x.IMLITM);
        }
    }
}
