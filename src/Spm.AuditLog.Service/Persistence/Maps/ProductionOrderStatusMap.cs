using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class ProductionOrderStatusMap : ClassMap<ProductionOrderStatus>
    {
        public ProductionOrderStatusMap()
        {
            Table(Constants.ProductionOrderStatusTableName);
            Id(x => x.Id);
            Map(x => x.ProductionOrderId);
            Map(x => x.SagaReferenceId);
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