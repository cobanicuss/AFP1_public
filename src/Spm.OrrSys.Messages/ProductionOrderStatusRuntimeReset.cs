using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class ProductionOrderStatusRuntimeReset : ICommand
    {
        public string OrrSysSchedulerSagaInitId { get; set; }
    }
}