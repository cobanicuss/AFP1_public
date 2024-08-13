using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class OrrSysSchedulerSagaInit : ICommand
    {
        public string OrrSysSchedulerSagaInitId { get; set; }
    }
}
