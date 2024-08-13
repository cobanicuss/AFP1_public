using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class DemandSapJdeWo : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual int Ibitm { get; set; }
        public virtual string Iblitm { get; set; }
        public virtual DateTime CyclePlannedBefore { get; set; }
        public virtual int Qty { get; set; }
        public virtual string BaseUom { get; set; }
        public virtual string Ibmcu { get; set; }
        public virtual string ProOrder { get; set; }
    }
}
