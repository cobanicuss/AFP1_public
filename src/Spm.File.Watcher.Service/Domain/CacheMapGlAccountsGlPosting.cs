using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class CacheMapGlAccountsGlPosting : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeGlAccount { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string JdeReport { get; set; }
        public virtual string JdeGroup { get; set; }
        public virtual string SapGlAccount { get; set; }
        public virtual string SapDescription { get; set; }
        public virtual string SapType { get; set; }
    }
}
