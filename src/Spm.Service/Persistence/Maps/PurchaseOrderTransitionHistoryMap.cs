using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class PurchaseOrderTransitionHistoryMap : ClassMap<PurchaseOrderTransitionHistory>
    {
        public PurchaseOrderTransitionHistoryMap()
        {
            Table("PurchaseOrderTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.PurchaseOrderNumber);
            Map(x => x.PurchaseOrderActionType);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}