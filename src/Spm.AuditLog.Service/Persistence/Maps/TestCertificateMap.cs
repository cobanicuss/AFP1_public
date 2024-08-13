using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class TestCertificateMap : ClassMap<TestCertificate>
    {
        public TestCertificateMap()
        {
            Table(Constants.TestCertificateTableName);
            Id(x => x.Id);
            Map(x => x.InboundId);
            Map(x => x.SagaReferenceId);
            Map(x => x.CertificateId);
            Map(x => x.MessageIndex);
            Map(x => x.MessageCount);
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