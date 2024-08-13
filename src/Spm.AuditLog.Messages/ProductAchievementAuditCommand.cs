using NServiceBus;

namespace Spm.AuditLog.Messages
{
    public class ProductAchievementAuditCommand : AuditBaseCommand
    {
        public string LotNumber { get; set; }
        public string SagaReferenceId { get; set; }
    }

    public class ProductAchievementCommitCommand : ICommand
    {
        public ProductAchievementAuditCommand ProductAchievementAuditCommand { get; set; }
    }
}