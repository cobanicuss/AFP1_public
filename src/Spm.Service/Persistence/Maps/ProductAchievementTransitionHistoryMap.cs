using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class ProductAchievementTransitionHistoryMap : ClassMap<ProductAchievementTransitionHistory>
    {
        public ProductAchievementTransitionHistoryMap()
        {
            Table("ProductAchievementTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.LotNumber);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}
