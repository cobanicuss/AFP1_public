using System;
using System.Collections.Generic;
using System.Text;

namespace Spm.Shared.Payloads
{
    [Serializable]
    public class PurchaseOrderPayload
    {
        public List<PurchaseOrderPayloadItem> PurchaseOrderPayloadItemList { get; set; }
    }

    [Serializable]
    public class PurchaseOrderPayloadItem
    {
        public string EdiDc40Docnum { get; set; }

        public string E1BpmePoHeaderPoNumber { get; set; }
        public string E1BpmePoHeaderCompCode { get; set; }
        public string E1BpmePoHeaderDocType { get; set; }
        public string E1BpmePoHeaderCreatDate { get; set; }
        public string E1BpmePoHeaderCreatedBy { get; set; }
        public string E1BpmePoHeaderItemIntvl { get; set; }
        public string E1BpmePoHeaderVendor { get; set; }
        public string E1BpmePoHeaderPurchOrg { get; set; }
        public string E1BpmePoHeaderPurGroup { get; set; }
        public string E1BpmePoHeaderCurrency { get; set; }
        public string E1BpmePoHeaderDocDate { get; set; }

        public string E1BpmePoItemPoItem { get; set; }
        public string E1BpmePoItemDeleteInd { get; set; }
        public string E1BpmePoItemShortText { get; set; }
        public string E1BpmePoItemPlant { get; set; }
        public string E1BpmePoItemStgeLoc { get; set; }
        public string E1BpmePoItemMatlGroup { get; set; }
        public string E1BpmePoItemVendMat { get; set; }
        public string E1BpmePoItemQuantity { get; set; }
        public string E1BpmePoItemPoUnit { get; set; }
        public string E1BpmePoItemOrderPrUn { get; set; }
        public string E1BpmePoItemNetPrice { get; set; }
        public string E1BpmePoItemPriceUnit { get; set; }
        public string E1BpmePoItemOverDlvTol { get; set; }
        public string E1BpmePoItemAccTassCat { get; set; }
        public string E1BpmePoItemPreqName { get; set; }
        public string E1BpmePoItemNoMoreGr { get; set; }

        public string E1BpmePoSchedulePoItem { get; set; }
        public string E1BpmePoScheduleSchedLine { get; set; }
        public string E1BpmePoScheduleQuantity { get; set; }
        public string E1BpmePoScheduleDeliveryDate { get; set; }

        public string E1BpmePoAccountPoItem { get; set; }
        public string E1BpmePoAccountSerialNo { get; set; }
        public string E1BpmePoAccountGlAccount { get; set; }
        public string E1BpmePoAccountCostCenter { get; set; }
        
    }

    public class PurchaseOrderPayloadAsString
    {
        public static string ToString(PurchaseOrderPayloadItem purchaseOrderPayload)
        {
            var str = $"EdiDc40Docnum={purchaseOrderPayload.EdiDc40Docnum}";

            return str;
        }
    }
}