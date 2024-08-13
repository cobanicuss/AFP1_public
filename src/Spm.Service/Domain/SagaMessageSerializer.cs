using System;
using Spm.Shared;

namespace Spm.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class SagaMessageSerializer : IMarkAsDomain
    {
        public virtual Guid Id { get; set; }
        public virtual Guid SagaId { get; set; }
        public virtual Guid MessageId { get; set; }
        public virtual string Message { get; set; }
    }
}
