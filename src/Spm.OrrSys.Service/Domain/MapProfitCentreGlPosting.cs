using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MapProfitCentreGlPosting : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeDepartmentCode { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string JdeBranch { get; set; }
        public virtual string SapProfitCentre { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
