using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;

namespace Spm.Service.ForSoap.Config
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