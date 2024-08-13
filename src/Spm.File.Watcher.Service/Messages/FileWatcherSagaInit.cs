using System;
using NServiceBus;

namespace Spm.File.Watcher.Service.Messages
{
    public class FileWatcherSagaInit : ICommand
    {
        public string FileWatcherSagaInitId { get; set;}
    }
}
