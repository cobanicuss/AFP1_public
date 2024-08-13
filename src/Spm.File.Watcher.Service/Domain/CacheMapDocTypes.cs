using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class CacheMapDocTypes : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeDocType { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapDocType { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
