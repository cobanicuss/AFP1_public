using System;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class GeneralLedgerData : IMarkAsDomain
    {
        public virtual Guid DataId { get; set; }
        public virtual DateTime DateTimeFileUploaded { get; set; }
        public virtual string SagaReferenceId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string HeaderTxt { get; set; }
        public virtual string CompCode { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string PstingDate { get; set; }
        public virtual string RefDocNo { get; set; }
        public virtual string AllocNmbr { get; set; }
        public virtual string CostCentre { get; set; }
        public virtual string GlAccount { get; set; }
        public virtual string Currency { get; set; }
        public virtual string AmtDoccur { get; set; }
    }
}