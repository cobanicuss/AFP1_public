using System;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Spm.File.Watcher.Service.Config
{
    public class ProvideConfiguration : IProvideConfiguration<SecondLevelRetriesConfig>
    {
        public SecondLevelRetriesConfig GetConfiguration()
        {
            return new SecondLevelRetriesConfig
            {
                Enabled = true,
                NumberOfRetries = 1,
                TimeIncrease = TimeSpan.FromSeconds(2)
            };
        }
    }
}