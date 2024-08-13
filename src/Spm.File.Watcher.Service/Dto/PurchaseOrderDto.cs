using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class PurchaseOrderDto : IMarkAsDto
    {
        public string PoNumber      { get; set; }
        public string CompCode      { get; set; }
        public string DocType       { get; set; }
        public string CreateDate    { get; set; }
        public string CreatedBy     { get; set; }
        public string ItemIntvl     { get; set; }
        public string Vendor        { get; set; }
        public string PurchOrg      { get; set; }
        public string PurGroup      { get; set; }
        public string Currency      { get; set; }
        public string DocDate       { get; set; }
        public string PoItem        { get; set; }
        public string DeleteInd     { get; set; }
        public string ShortText     { get; set; }
        public string Plant         { get; set; }
        public string StgeLoc       { get; set; }
        public string MatlGroup     { get; set; }
        public string VendMat       { get; set; }
        public string Quantity      { get; set; }
        public string PoUnit        { get; set; }
        public string OrderPrUn     { get; set; }
        public string NetPrice      { get; set; }
        public string PriceUnit     { get; set; }
        public string OverDlvTol    { get; set; }
        public string NoMoreGr      { get; set; }
        public string Acctasscat    { get; set; }
        public string PreqName      { get; set; }
        public string SerialNo      { get; set; }
        public string GlAccount     { get; set; }
        public string CostCenter    { get; set; }
        public string DeliveryDate  { get; set; }
        public string LnType        { get; set; }
        public string Taxable       { get; set; }
    }
}