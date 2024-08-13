using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Spm.OrrSys.Service.Config
{
    public class AuditQueueConfig : IProvideConfiguration<AuditConfig>
    {
        public AuditConfig GetConfiguration()
        {
            return new AuditConfig
            {
                QueueName = Shared.Constants.AuditQueueName
            };
        }
    }
}