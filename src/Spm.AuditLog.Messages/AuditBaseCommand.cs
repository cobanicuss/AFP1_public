using System;
using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class AuditBaseCommand : ICommand
    {
        public string MessageType { get; set; }
        public string FromEndpoint { get; set; }
        public DateTime DateTimeMessageSendToHere { get; set; }
        public int Action { get; set; }
        public string MessageData { get; set; }
        public float Leg { get; set; }
        public string AuditLogSchedulerSagaInitId => AuditLogSchedulerSagaInitConst.AuditLogSchedulerSagaId;
    }
}