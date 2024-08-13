using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class DemandSapJde : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string Material { get; set; }
        public virtual int Ibitm { get; set; }
        public virtual string Ibmcu { get; set; }
        public virtual string ForcastType { get; set; }
        public virtual DateTime CyclePlannedBefore { get; set; }
        public virtual string Numa { get; set; }
        public virtual double Qyy { get; set; }
    }
}
