using System;
using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    [Serializable]
    public class GeneralLedgerPayload
    {
        public List<GeneralLedgerPayloadItem> GeneralLedgerPayloadItem { get; set; }
    }

    [Serializable]
    public class GeneralLedgerPayloadItem
    {
        public string UserName { get; set; }
        public string HeaderTxt { get; set; }
        public string CompanyCode { get; set; }
        public string DocDate { get; set; }
        public string PstngDate { get; set; }
        public string DocType { get; set; }
        public string RefDocNo { get; set; }

        public string AcItemNoAcc { get; set; }
        public string Account { get; set; }
        public string ItemText { get; set; }
        public string AllocNmbr { get; set; }
        public string CostCentre { get; set; }
        public string ProfitCentre { get; set; }

        public string AccrItemNoAcc { get; set; }
        public string Currency { get; set; }
        public string Doccur { get; set; }
    }

    public class GeneralLedgerPayloadAsString
    {
        public static string ToString(GeneralLedgerPayloadItem item)
        {
            var str = $@"UserName ={item.UserName},
            HeaderTxt = {item.HeaderTxt},
            CompanyCode = {item.CompanyCode},
            DocDate ={item.DocDate},
            PstngDate = {item.PstngDate},
            DocType = {item.DocType},
            RefDocNo = {item.RefDocNo},
            ItemNoAcc = {item.AcItemNoAcc},
            GlAccount = {item.Account},
            ItemText = {item.ItemText},
            AllocNmbr = {item.AllocNmbr},
            CostCentre = {item.CostCentre},
            ProfitCentre = {item.ProfitCentre},
            ItemNoAcc = {item.AccrItemNoAcc},
            Currency = {item.Currency},
            AmtDoccur = {item.Doccur}";

            return str;
        }
    }
}