using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class ProductionOrderTransitionHistoryMap : ClassMap<ProductionOrderTransitionHistory>
    {
        public ProductionOrderTransitionHistoryMap()
        {
            Table("ProductionOrderTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.ProductionOrderId);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}
