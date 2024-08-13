using NServiceBus;

namespace Spm.File.Watcher.Messages
{
    public class FileWatcherSagaInit : ICommand
    {
        public string FileWatcherSagaInitId { get; set;}
    }
}
