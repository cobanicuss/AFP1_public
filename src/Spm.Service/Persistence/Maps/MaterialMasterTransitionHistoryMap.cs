using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class MaterialMasterTransitionHistoryMap : ClassMap<MaterialMasterTransitionHistory>
    {
        public MaterialMasterTransitionHistoryMap()
        {
            Table("MaterialMasterTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.ShortItemNumber);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}