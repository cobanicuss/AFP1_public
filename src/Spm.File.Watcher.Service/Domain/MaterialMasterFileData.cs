using System;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MaterialMasterFileData : IMarkAsDomain
    {
        public virtual Guid DataId { get; set; }
        public virtual DateTime DateTimeFileUploaded { get; set; }
        public virtual string FileName { get; set; }
        public virtual string SagaReferenceId { get; set; }

        public virtual string Itm { get; set; }
        public virtual string Litm { get; set; }
        public virtual string Aitm { get; set; }
        public virtual string Dsc1 { get; set; }
        public virtual string Dsc2 { get; set; }
        public virtual string Dsc12 { get; set; }
        public virtual string LongDsc { get; set; }
        public virtual string Mcu { get; set; }
        public virtual string Stkt { get; set; }
        public virtual string Draw { get; set; }
        public virtual string Srp3Desc { get; set; }
        public virtual string Srp4Desc { get; set; }
        public virtual string Srp4Desc2 { get; set; }
        public virtual string Srp5Desc { get; set; }
        public virtual string Srp6Desc { get; set; }
        public virtual string Srp7Desc { get; set; }
        public virtual string Srp8Desc { get; set; }
        public virtual string Prp0 { get; set; }
        public virtual string Prp0M { get; set; }
        public virtual string Prp1Desc { get; set; }
        public virtual string Prp1Desc2 { get; set; }
        public virtual string Prp2Desc { get; set; }
        public virtual string Prp3Desc { get; set; }
        public virtual string Sec1 { get; set; }
        public virtual string Sec2 { get; set; }
        public virtual string Prp4Desc { get; set; }
        public virtual string Prp5Desc { get; set; }
        public virtual string Prp6Desc { get; set; }
        public virtual string Imprp3 { get; set; }
        public virtual string Imprp3Desc { get; set; }
        public virtual string Imprp4 { get; set; }
        public virtual string Imprp4Desc { get; set; }
        public virtual string Imuom1 { get; set; }
        public virtual string Bq { get; set; }
        public virtual string BwCk { get; set; }
        public virtual string Kgs { get; set; }
        public virtual string Mt { get; set; }
        public virtual string GhMm { get; set; }
        public virtual string GwMm { get; set; }
        public virtual string Kg { get; set; }
    }
}