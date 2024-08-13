using FluentNHibernate.Mapping;
using Spm.Service.Domain;

namespace Spm.Service.Persistence.Maps
{
    public class SagaMessageSerializerMap : ClassMap<SagaMessageSerializer>
    {
        public SagaMessageSerializerMap()
        {
            Table("SagaMessageForResend");
            Id(x => x.Id);
            Map(x => x.SagaId);
            Map(x => x.MessageId);
            Map(x => x.Message).Length(4001); //NVarChar(Max)//
        }
    }
}