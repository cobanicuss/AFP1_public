using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class CacheMapLocation : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdePlant { get; set; }
        public virtual string JdeLocationCode { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapPlant { get; set; }
        public virtual string SapStorageLocation { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
