using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class AuditActionTypeMap : ClassMap<AuditActionType>
    {
        public AuditActionTypeMap()
        {
            Table(Constants.AuditActionTypeTableName);
            Id(x => x.Id);
            Map(x => x.ActionId);
            Map(x => x.ActionName);
        }
    }
}