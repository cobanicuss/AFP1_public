using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MapDocTypes : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeDocType { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapDocType { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
