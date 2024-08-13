using System;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Domain
{
    public class PurchaseOrderBaseFileData
    {
        public virtual Guid DataId { get; set; }
        public virtual DateTime DateTimeFileUploaded { get; set; }
        public virtual string FileName { get; set; }
        public virtual string SagaReferenceId { get; set; }

        public virtual string PoNumber { get; set; }
        public virtual string CompCode { get; set; }
        public virtual string DocType { get; set; }
        public virtual string CreatDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ItemIntvl { get; set; }
        public virtual string Vendor { get; set; }
        public virtual string PurchOrg { get; set; }
        public virtual string PurGroup { get; set; }
        public virtual string Currency { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string PoItem { get; set; }
        public virtual string DeleteInd { get; set; }
        public virtual string ShortText { get; set; }
        public virtual string Plant { get; set; }
        public virtual string StgeLoc { get; set; }
        public virtual string MatlGroup { get; set; }
        public virtual string VendMat { get; set; }
        public virtual string Quantity { get; set; }
        public virtual string PoUnit { get; set; }
        public virtual string OrderprUn { get; set; }
        public virtual string NetPrice { get; set; }
        public virtual string PriceUnit { get; set; }
        public virtual string OverDlvTol { get; set; }
        public virtual string NoMoreGr { get; set; }
        public virtual string Acctasscat { get; set; }
        public virtual string PreqName { get; set; }
        public virtual string SerialNo { get; set; }
        public virtual string GlAccount { get; set; }
        public virtual string Costcenter { get; set; }
        public virtual string DeliveryDate { get; set; }
        public virtual string LnType { get; set; }
        public virtual string Taxable { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class PurchaseOrderChangeFileData : PurchaseOrderBaseFileData, IMarkAsDomain{}

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class PurchaseOrderCreateFileData : PurchaseOrderBaseFileData, IMarkAsDomain{}   
}