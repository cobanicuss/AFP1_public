using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class GeneralLedgerIdDto : IMarkAsDto
    {
        public string SagaReferenceId { get; set; }
        public string GeneralLedgerId { get; set; }
    }
}
