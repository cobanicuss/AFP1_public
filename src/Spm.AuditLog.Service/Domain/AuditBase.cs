using System;
using Spm.Shared;

namespace Spm.AuditLog.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class AuditBase : AuditInboundBase
    {
        public virtual string SagaReferenceId { get; set; }
    }

    public class AuditInboundBase : IMarkAsDomain
    {
        public virtual Guid Id { get; set; }
        public virtual string MessageType { get; set; }
        public virtual string FromEndpoint { get; set; }
        public virtual int Action { get; set; }
        public virtual string MessageData { get; set; }
        public virtual float Leg { get; set; }
        public virtual float LegCount { get; set; }
        public virtual DateTime DateTimeMessageRecieved { get; set; }
        public virtual DateTime DateTimeMessageSendToHere { get; set; }
    }
}
