using FluentNHibernate.Mapping;
using Spm.AuditLog.Service.Domain;

namespace Spm.AuditLog.Service.Persistence.Maps
{
    public class TestCertificateRequestMap : ClassMap<TestCertificateRequest>
    {
        public TestCertificateRequestMap()
        {
            Table(Constants.TestCertificateRequestTableName);
            Id(x => x.Id);
            Map(x => x.InboundId);
            Map(x => x.CertificateId);
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