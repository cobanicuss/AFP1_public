using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MapUnitOfMeasure : IMarkAsDomain
    {
        public virtual int Id { get; set; }
        public virtual string JdeUom { get; set; }
        public virtual string JdeDescription { get; set; }
        public virtual string IsoUom { get; set; }
        public virtual string IsoUomDescription { get; set; }
        public virtual string SapUom { get; set; }
        public virtual string SapDescription { get; set; }
    }
}
