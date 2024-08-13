using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class GeneralLedgerTransitionHistoryMap : ClassMap<GeneralLedgerTransitionHistory>
    {
        public GeneralLedgerTransitionHistoryMap()
        {
            Table("GeneralLedgerTransitionHistory");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.SagaName);
            Map(x => x.SagaReferenceId);
            Map(x => x.GeneralLedgerId);
            Map(x => x.TransitionFrom);
            Map(x => x.TransitionTo);
            Map(x => x.DateTimeOfTransition);
        }
    }
}