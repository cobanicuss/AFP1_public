using System;
using NServiceBus.Saga;

namespace Spm.AuditLog.Service.SagaData
{
    public class AuditLogSchedulerSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual string AuditLogSchedulerSagaInitId { get; set; }
        public virtual bool IsDoingRollOver { get; set; }
    }
}