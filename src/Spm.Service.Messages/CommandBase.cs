using NServiceBus;

namespace Spm.Service.Messages
{
    public class CommandBase : ICommand
    {
        public string SagaReferenceId { get; set; }
    }
}