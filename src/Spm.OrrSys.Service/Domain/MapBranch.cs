using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MapBranch : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string SapPlant { get; set; }
        public virtual string SapDescription { get; set; }
        public virtual string JdeBranchCode { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapProfitCentre { get; set; }
        public virtual string SapProfitCentreDescription { get; set; }
        public virtual string StorageType { get; set; }
        public virtual string StorageTypeDescription { get; set; }
    }
}