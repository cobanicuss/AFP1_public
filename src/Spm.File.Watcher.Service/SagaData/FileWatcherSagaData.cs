using System;
using NServiceBus.Saga;

namespace Spm.File.Watcher.Service.SagaData
{
    public class FileWatcherSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual string FileWatcherSagaInitId { get; set; }
        public virtual bool IsBusyExtractingData { get; set; }
        public virtual int CacheMapRetryOnSagaBusyCount { get; set; }
    }
}
