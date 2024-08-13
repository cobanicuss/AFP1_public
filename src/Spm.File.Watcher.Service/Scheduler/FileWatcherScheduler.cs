using NServiceBus;
using Spm.File.Watcher.Messages;

namespace Spm.File.Watcher.Service.Scheduler
{
    public class FileWatcherScheduler : IWantToRunWhenBusStartsAndStops
    {
        private readonly IBus _bus;

        public FileWatcherScheduler(IBus bus)
        {
            _bus = bus;
        }

        public void Start()
        {
            var intit = new FileWatcherSagaInit
            {
                FileWatcherSagaInitId = FileWatcherSagaInitConst.FileWatcherSagaId
            };

            _bus.SendLocal(intit);
        }

        public void Stop() { }
    }
}