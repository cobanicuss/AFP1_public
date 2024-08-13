using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class F4101 : IMarkAsDomain
    {
        public virtual int IMITM { get; set; }
        public virtual string IMLITM { get; set; }
        public virtual string IMAITM { get; set; }
    }
}