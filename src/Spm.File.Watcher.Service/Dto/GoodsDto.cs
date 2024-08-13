using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class GoodsDto : IMarkAsDto
    {
        public string PstngDate { get; set; }
        public string DocDate { get; set; }
        public string RefDocNo { get; set; }
        public string HeaderTxt { get; set; }
        public string GmCode { get; set; }
        public string Plant { get; set; }
        public string StgeLoc { get; set; }
        public string MoveType { get; set; }
        public string EntryQnt { get; set; }
        public string EntryUom { get; set; }
        public string PoNumber { get; set; }
        public string PoItem { get; set; }
        public string MvtInd { get; set; }
        public string NoMoreGr { get; set; }
        public string TranTypeInd { get; set; }
        public string Id { get; set; }
        public string OrderType { get; set; }
        public string DocType { get; set; }
        public string ReceiptDoc { get; set; }
    }
}