using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class AuditLogSchedulerSagaInit : ICommand
    {
        public string AuditLogSchedulerSagaInitId { get; set; }
    }
}