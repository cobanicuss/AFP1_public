using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class GeneralLedgerMap : ClassMap<GeneralLedger>
    {
        public GeneralLedgerMap()
        {
            Table(Constants.GeneralLedgerTableName);
            Id(x => x.Id);
            Map(x => x.GeneralLedgerId);
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