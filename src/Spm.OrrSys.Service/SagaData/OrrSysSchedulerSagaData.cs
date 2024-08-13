using System;
using NServiceBus.Saga;

namespace Spm.OrrSys.Service.SagaData
{
    public class OrrSysSchedulerSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual string OrrSysSchedulerSagaInitId { get; set; }
    }
}