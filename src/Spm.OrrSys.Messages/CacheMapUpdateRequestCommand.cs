using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class CacheMapUpdateRequestCommand : ICommand
    {
        public string FileWatcherSagaId { get; set; }
    }
}