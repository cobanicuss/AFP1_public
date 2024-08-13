using NServiceBus;
using Spm.OrrSys.Messages;

namespace Spm.OrrSys.Service.Scheduler
{
    public class OrrSysScheduler : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus _bus;

        public OrrSysScheduler(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            var intit = new OrrSysSchedulerSagaInit
            {
                OrrSysSchedulerSagaInitId = OrrSysSagaInitConst.OrrSysSchedulerSagaId
            };

            _bus.SendLocal(intit);
        }

        public void Stop() { }
    }
}