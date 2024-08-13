using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Spm.AuditLog.Service.Config
{
    public class ErrorQueueConfig : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = Shared.Constants.ErrorQueueName
            };
        }
    }
}