using System;
using System.Collections.Generic;
using System.Text;

namespace Spm.Shared
{
    [Serializable]
    public class GeneralLedgerPayload
    {
        public List<GeneralLedgerPayloadItem> GeneralLedgerPayloadItem { get; set; }
    }

    [Serializable]
    public class GeneralLedgerPayloadItem
    {
        public string E1BpAche09UserName { get; set; }
        public string E1BpAche09HeaderTxt { get; set; }
        public string E1BpAche09CompanyCode { get; set; }
        public string E1BpAche09DocDate { get; set; }
        public string E1BpAche09PstngDate { get; set; }
        public string E1BpAche09DocType { get; set; }
        public string E1BpAche09RefDocNo { get; set; }

        public string E1BpAcGl09ItemNoAcc { get; set; }
        public string E1BpAcGl09GlAccount { get; set; }
        public string E1BpAcGl09ItemText { get; set; }
        public string E1BpAcGl09AllocNmbr { get; set; }
        public string E1BpAcGl09CostCentre { get; set; }
        public string E1BpAcGl09ProfitCentre { get; set; }

        public string E1BpAccr09ItemNoAcc { get; set; }
        public string E1BpAccr09Currency { get; set; }
        public string E1BpAccr09AmtDoccur { get; set; }
    }

    public class GeneralLedgerPayloadAsString
    {
        public static string ToString(GeneralLedgerPayloadItem item)
        {
            var sb = new StringBuilder();
            sb.Append("UserName={0},");
            sb.Append("HeaderTxt={1},");
            sb.Append("CompanyCode={2},");
            sb.Append("DocDate={3},");
            sb.Append("PstngDate={4},");
            sb.Append("DocType={5},");
            sb.Append("RefDocNo={6},");
            sb.Append("ItemNoAcc={7},");
            sb.Append("GlAccount={8},");
            sb.Append("ItemText={9},");
            sb.Append("AllocNmbr={10},");
            sb.Append("CostCentre={11},");
            sb.Append("ProfitCentre={12},");
            sb.Append("ItemNoAcc={13},");
            sb.Append("Currency={14},");
            sb.Append("AmtDoccur={15},");

            var str = string.Format(sb.ToString(),
                item.E1BpAche09UserName,
                item.E1BpAche09HeaderTxt,
                item.E1BpAche09CompanyCode,
                item.E1BpAche09DocDate,
                item.E1BpAche09PstngDate,
                item.E1BpAche09DocType,
                item.E1BpAche09RefDocNo,
                item.E1BpAcGl09ItemNoAcc,
                item.E1BpAcGl09GlAccount,
                item.E1BpAcGl09ItemText,
                item.E1BpAcGl09AllocNmbr,
                item.E1BpAcGl09CostCentre,
                item.E1BpAcGl09ProfitCentre,
                item.E1BpAccr09ItemNoAcc,
                item.E1BpAccr09Currency,
                item.E1BpAccr09AmtDoccur
                );

            return str;
        }
    }
}