using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class MaterialMasterUpdateAuditCommand : AuditBaseCommand
    {
        public string ShortItemNumber { get; set; }
        public string InboundId { get; set; }
    }

    public class MaterialMasterUpdateCommitCommand : ICommand
    {
        public MaterialMasterUpdateAuditCommand MaterialMasterUpdateAuditCommand { get; set; }
    }
}