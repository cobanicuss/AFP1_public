using System;
using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    [Serializable]
    public class GoodsPayload
    {
        public List<GoodsPayloadItem> GoodsPayloadItem { get; set; }
    }

    [Serializable]
    public class GoodsPayloadItem
    {
        public string HeadPostingDate { get; set; }
        public string HeadDocDate { get; set; }
        public string HeadRefDocNo { get; set; }
        public string HeadHeaderTxt { get; set; }
        public string Code { get; set; }
        public string ItemCreatePlant { get; set; }
        public string ItemCreateStgeLoc { get; set; }
        public string ItemCreateMoveType { get; set; }
        public string ItemCreateEntryQnt { get; set; }
        public string ItemCreateEntryUom { get; set; }
        public string ItemCreatePoNumber { get; set; }
        public string ItemCreatePoItem { get; set; }
        public string ItemCreateMvtInd { get; set; }
        public string ItemCreateNoMoreGr { get; set; }
        public string TranTypeInd { get; set; }
    }

    public class GoodsPayloadAsString
    {
        public static string ToString(GoodsPayloadItem item)
        {
            var str = $@"RefDocNo = {item.HeadRefDocNo},
            HeaderTxt = {item.HeadHeaderTxt},
            Gm = {item.Code},
            Plant = {item.ItemCreatePlant},
            StgeLoc = {item.ItemCreateStgeLoc},
            MoveType = {item.ItemCreateMoveType},
            EntryQnt = {item.ItemCreateEntryQnt},
            EntryUom = {item.ItemCreateEntryUom},
            PoNumber = {item.ItemCreatePoNumber},
            PoItem = {item.ItemCreatePoItem},
            MvtInd = {item.ItemCreateMvtInd},
            NoMoreGr = {item.ItemCreateNoMoreGr},
            TranTypeInd = {item.TranTypeInd}";

            return str;
        }
    }
}