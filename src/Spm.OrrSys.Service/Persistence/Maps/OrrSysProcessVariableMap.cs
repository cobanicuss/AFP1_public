using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class OrrSysProcessVariableMap : ClassMap<OrrSysProcessVariable>
    {
        public OrrSysProcessVariableMap()
        {
            Table(Constants.TableNameOrrSysProcessVariable);
            Id(x => x.Id);
            Map(x => x.RunProductionOrderStatusOnTime);
        }
    }
}