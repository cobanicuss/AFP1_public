using System;
using System.Collections.Generic;
using System.Text;

namespace Spm.Shared
{
    [Serializable]
    public class GoodsPayload
    {
        public List<GoodsPayloadItem> GoodsPayloadItem { get; set; }
    }

    [Serializable]
    public class GoodsPayloadItem
    {
        public string E2Bp2017GmHeadPostingDate { get; set; }
        public string E2Bp2017GmHeadDocDate { get; set; }
        public string E2Bp2017GmHeadRefDocNo { get; set; }
        public string E2Bp2017GmHeadHeaderTxt { get; set; }
        public string E2Bp2017GmCode { get; set; }
        public string E2Bp2017GmItemCreatePlant { get; set; }
        public string E2Bp2017GmItemCreateStgeLoc { get; set; }
        public string E2Bp2017GmItemCreateMoveType { get; set; }
        public string E2Bp2017GmItemCreateEntryQnt { get; set; }
        public string E2Bp2017GmItemCreateEntryUom { get; set; }
        public string E2Bp2017GmItemCreatePoNumber { get; set; }
        public string E2Bp2017GmItemCreatePoItem { get; set; }
        public string E2Bp2017GmItemCreateMvtInd { get; set; }
        public string E2Bp2017GmItemCreateNoMoreGr { get; set; }
        public string TranTypeInd { get; set; }
    }

    public class GoodsPayloadAsString
    {
        public static string ToString(GoodsPayloadItem item)
        {
            var sb = new StringBuilder();
            sb.Append("RefDocNo={0},");
            sb.Append("HeaderTxt={1},");
            sb.Append("Gm={2},");
            sb.Append("Plant={3},");
            sb.Append("StgeLoc={4},");
            sb.Append("MoveType={5},");
            sb.Append("EntryQnt={6},");
            sb.Append("EntryUom={7},");
            sb.Append("PoNumber={8},");
            sb.Append("PoItem={9},");
            sb.Append("MvtInd={10},");
            sb.Append("NoMoreGr={11},");
            sb.Append("TranTypeInd={12},");

            var str = string.Format(sb.ToString(),
                item.E2Bp2017GmHeadRefDocNo,
                item.E2Bp2017GmHeadHeaderTxt,
                item.E2Bp2017GmCode,
                item.E2Bp2017GmItemCreatePlant,
                item.E2Bp2017GmItemCreateStgeLoc,
                item.E2Bp2017GmItemCreateMoveType,
                item.E2Bp2017GmItemCreateEntryQnt,
                item.E2Bp2017GmItemCreateEntryUom,
                item.E2Bp2017GmItemCreatePoNumber,
                item.E2Bp2017GmItemCreatePoItem,
                item.E2Bp2017GmItemCreateMvtInd,
                item.E2Bp2017GmItemCreateNoMoreGr,
                item.TranTypeInd
                );

            return str;
        }
    }
}