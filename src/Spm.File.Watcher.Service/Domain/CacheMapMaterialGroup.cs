using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class CacheMapMaterialGroup : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeStockType { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string SapMatrialGroup { get; set; }
        public virtual string SapDescription { get; set; }
        public virtual string SapGlAcc { get; set; }
        public virtual string SapCostCentre { get; set; }
    }
}