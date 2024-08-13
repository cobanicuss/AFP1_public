using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class MaterialMasterAuditCommand : AuditBaseCommand
    {
        public string ReferenceId { get; set; }
        public string SagaReferenceId { get; set; }
    }

    public class MaterialMasterCommitCommand : ICommand
    {
        public MaterialMasterAuditCommand MaterialMasterAuditCommand { get; set; }
    }
}