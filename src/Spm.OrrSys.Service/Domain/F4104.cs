using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class F4104 : IMarkAsDomain
    {
        public virtual int IVITM { get; set; }
        public virtual string IVLITM { get; set; }
        public virtual string IVAITM { get; set; }
        public virtual string IVCITM { get; set; }
        public virtual string IVXRT { get; set; }
    }
}
