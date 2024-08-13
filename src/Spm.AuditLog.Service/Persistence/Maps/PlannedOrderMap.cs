using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class PlannedOrderMap : ClassMap<PlannedOrder>
    {
        public PlannedOrderMap()
        {
            Table(Constants.PlannedOrderTableName);
            Id(x => x.Id);
            Map(x => x.InboundId);
            Map(x => x.MessageType);
            Map(x => x.FromEndpoint);
            Map(x => x.Action);
            Map(x => x.Leg);
            Map(x => x.LegCount);
            Map(x => x.DateTimeMessageRecieved);
            Map(x => x.DateTimeMessageSendToHere);
            Map(x => x.MessageData);
        }
    }
}