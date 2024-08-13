using NServiceBus;
using Spm.AuditLog.Messages;

namespace Spm.AuditLog.Service.Scheduler
{
    public class AuditLogScheduler : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus _bus;

        public AuditLogScheduler(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            var intit = new AuditLogSchedulerSagaInit
            {
                AuditLogSchedulerSagaInitId = AuditLogSchedulerSagaInitConst.AuditLogSchedulerSagaId
            };

            _bus.SendLocal(intit);

        }

        public void Stop() { }
    }
}