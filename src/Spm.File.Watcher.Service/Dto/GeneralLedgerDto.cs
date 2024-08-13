using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class GeneralLedgerDto : IMarkAsDto
    {
        public string HeaderTxt { get; set; }
        public string CompCode { get; set; }
        public string DocDate { get; set; }
        public string PstingDate { get; set; }
        public string RefDocNo { get; set; }
        public string AllocNmbr { get; set; }
        public string CostCentre { get; set; }
        public string GlAccount { get; set; }
        public string Currency { get; set; }
        public string AmtDoccur { get; set; }
    }
}