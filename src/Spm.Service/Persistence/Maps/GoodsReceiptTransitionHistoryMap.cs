using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class GoodsReceiptTransitionHistoryMap : ClassMap<GoodsReceiptTransitionHistory>
    {
        public GoodsReceiptTransitionHistoryMap()
        {
            Table("GoodsReceiptTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.GoodsReceiptId);
            Map(x => x.Type);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}
