using System.Linq;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IPurchaseOrderMessageMap : IMarkAsMapper
    {
        PORDCR103 MakePurchaseOrderCreateSoapMessage(PurchaseOrderCreateSapCommand message);
        PORDCH03 MakePurchaseOrderChangeSoapMessage(PurchaseOrderChangeSapCommand message);
    }

    public class PurchaseOrderMessageMap : IPurchaseOrderMessageMap
    {
        public PORDCR103 MakePurchaseOrderCreateSoapMessage(PurchaseOrderCreateSapCommand message)
        {
            var poNumber = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPoNumber;
            var compCode = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCompCode;
            var docType = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderDocType;
            var createDate = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCreatDate;
            var createdBy = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCreatedBy;
            var itemIntvl = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderItemIntvl;
            var vendor = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderVendor;
            var purchOrg = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPurchOrg;
            var purGroup = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPurGroup;
            var currency = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCurrency;

            var returnVal = new PORDCR103
            {
                IDOC = new PORDCR1PORDCR103
                {
                    EDI_DC40 = new EDI_DC40PORDCR1PORDCR103
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DOCNUM = message.SagaReferenceId,
                        DIRECT = EDI_DC40PORDCR1PORDCR103DIRECT.Item1,
                        IDOCTYP = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        MESTYP = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        SNDPOR = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    },
                    E1PORDCR1 = new PORDCR103E1PORDCR1
                    {
                        E1BPMEPOHEADER = new PORDCR103E1BPMEPOHEADER
                        {
                            PO_NUMBER = poNumber,
                            COMP_CODE = compCode,
                            DOC_TYPE = docType,
                            CREAT_DATE = createDate,
                            CREATED_BY = createdBy,
                            ITEM_INTVL = itemIntvl,
                            VENDOR = vendor,
                            PURCH_ORG = purchOrg,
                            PUR_GROUP = purGroup,
                            CURRENCY = currency
                        },
                        E1BPMEPOHEADERX = new PORDCR103E1BPMEPOHEADERX
                        {
                            PO_NUMBER = string.IsNullOrEmpty(poNumber) ? string.Empty : "X",
                            COMP_CODE = string.IsNullOrEmpty(compCode) ? string.Empty : "X",
                            DOC_TYPE = string.IsNullOrEmpty(docType) ? string.Empty : "X",
                            CREAT_DATE = string.IsNullOrEmpty(createDate) ? string.Empty : "X",
                            CREATED_BY = string.IsNullOrEmpty(createdBy) ? string.Empty : "X",
                            ITEM_INTVL = string.IsNullOrEmpty(itemIntvl) ? string.Empty : "X",
                            VENDOR = string.IsNullOrEmpty(vendor) ? string.Empty : "X",
                            PURCH_ORG = string.IsNullOrEmpty(purchOrg) ? string.Empty : "X",
                            PUR_GROUP = string.IsNullOrEmpty(purGroup) ? string.Empty : "X",
                            CURRENCY = string.IsNullOrEmpty(currency) ? string.Empty : "X"
                        },
                        E1BPMEPOITEM = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOITEM
                        {
                            PO_ITEM = x.E1BpmePoItemPoItem,
                            DELETE_IND = x.E1BpmePoItemDeleteInd,
                            SHORT_TEXT = x.E1BpmePoItemShortText,
                            PLANT = x.E1BpmePoItemPlant,
                            STGE_LOC = x.E1BpmePoItemStgeLoc,
                            MATL_GROUP = x.E1BpmePoItemMatlGroup,
                            VEND_MAT = x.E1BpmePoItemVendMat,
                            QUANTITY = x.E1BpmePoItemQuantity,
                            PO_UNIT = x.E1BpmePoItemPoUnit,
                            ORDERPR_UN = x.E1BpmePoItemOrderPrUn,
                            NET_PRICE = x.E1BpmePoItemNetPrice,
                            PRICE_UNIT = x.E1BpmePoItemPriceUnit,
                            OVER_DLV_TOL = x.E1BpmePoItemOverDlvTol,
                            ACCTASSCAT = x.E1BpmePoItemAccTassCat,
                            PREQ_NAME = x.E1BpmePoItemPreqName
                        }).ToArray(),
                        E1BPMEPOITEMX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOITEMX
                        {
                            PO_ITEM = x.E1BpmePoItemPoItem,
                            DELETE_IND = string.IsNullOrEmpty(x.E1BpmePoItemDeleteInd) ? string.Empty : "X",
                            SHORT_TEXT = string.IsNullOrEmpty(x.E1BpmePoItemShortText) ? string.Empty : "X",
                            PLANT = string.IsNullOrEmpty(x.E1BpmePoItemPlant) ? string.Empty : "X",
                            STGE_LOC = string.IsNullOrEmpty(x.E1BpmePoItemStgeLoc) ? string.Empty : "X",
                            MATL_GROUP = string.IsNullOrEmpty(x.E1BpmePoItemMatlGroup) ? string.Empty : "X",
                            VEND_MAT = string.IsNullOrEmpty(x.E1BpmePoItemVendMat) ? string.Empty : "X",
                            QUANTITY = string.IsNullOrEmpty(x.E1BpmePoItemQuantity) ? string.Empty : "X",
                            PO_UNIT = string.IsNullOrEmpty(x.E1BpmePoItemPoUnit) ? string.Empty : "X",
                            ORDERPR_UN = string.IsNullOrEmpty(x.E1BpmePoItemOrderPrUn) ? string.Empty : "X",
                            NET_PRICE = string.IsNullOrEmpty(x.E1BpmePoItemNetPrice) ? string.Empty : "X",
                            PRICE_UNIT = string.IsNullOrEmpty(x.E1BpmePoItemPriceUnit) ? string.Empty : "X",
                            OVER_DLV_TOL = string.IsNullOrEmpty(x.E1BpmePoItemOverDlvTol) ? string.Empty : "X",
                            ACCTASSCAT = string.IsNullOrEmpty(x.E1BpmePoItemAccTassCat) ? string.Empty : "X",
                            PREQ_NAME = string.IsNullOrEmpty(x.E1BpmePoItemPreqName) ? string.Empty : "X"
                        }).ToArray(),
                        E1BPMEPOSCHEDULE = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOSCHEDULE
                        {
                            PO_ITEM = x.E1BpmePoSchedulePoItem,
                            SCHED_LINE = x.E1BpmePoScheduleSchedLine,
                            QUANTITY = x.E1BpmePoScheduleQuantity,
                            DELIVERY_DATE = x.E1BpmePoScheduleDeliveryDate
                        }).ToArray(),
                        E1BPMEPOSCHEDULX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOSCHEDULX
                        {
                            PO_ITEM = x.E1BpmePoSchedulePoItem,
                            SCHED_LINE = x.E1BpmePoScheduleSchedLine,
                            QUANTITY = string.IsNullOrEmpty(x.E1BpmePoScheduleQuantity) ? string.Empty : "X",
                            DELIVERY_DATE = string.IsNullOrEmpty(x.E1BpmePoScheduleDeliveryDate) ? string.Empty : "X"
                        }).ToArray(),
                        E1BPMEPOACCOUNT = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOACCOUNT
                        {
                            PO_ITEM = x.E1BpmePoAccountPoItem,
                            SERIAL_NO = x.E1BpmePoAccountSerialNo,
                            GL_ACCOUNT = x.E1BpmePoAccountGlAccount,
                            COSTCENTER = x.E1BpmePoAccountCostCenter
                        }).ToArray(),
                        E1BPMEPOACCOUNTX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCR103E1BPMEPOACCOUNTX
                        {
                            PO_ITEM = x.E1BpmePoAccountPoItem,
                            SERIAL_NO = x.E1BpmePoAccountSerialNo,
                            GL_ACCOUNT = string.IsNullOrEmpty(x.E1BpmePoAccountGlAccount) ? string.Empty : "X",
                            COSTCENTER = string.IsNullOrEmpty(x.E1BpmePoAccountCostCenter) ? string.Empty : "X"
                        }).ToArray()
                    }
                }
            };

            return returnVal;
        }

        public PORDCH03 MakePurchaseOrderChangeSoapMessage(PurchaseOrderChangeSapCommand message)
        {
            var poNumber = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPoNumber;
            var compCode = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCompCode;
            var docType = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderDocType;
            var createDate = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCreatDate;
            var createdBy = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCreatedBy;
            var itemIntvl = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderItemIntvl;
            var vendor = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderVendor;
            var purchOrg = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPurchOrg;
            var purGroup = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderPurGroup;
            var currency = message.Payload.PurchaseOrderPayloadItemList[0].E1BpmePoHeaderCurrency;

            var returnVal = new PORDCH03
            {
                IDOC = new PORDCHPORDCH03
                {
                    EDI_DC40 = new EDI_DC40PORDCHPORDCH03
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DOCNUM = message.SagaReferenceId,
                        DIRECT = EDI_DC40PORDCHPORDCH03DIRECT.Item1,
                        IDOCTYP = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        MESTYP = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        SNDPOR = string.Empty, //Must be here; even if not needed. Welcome to SAP//
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    },
                    E1PORDCH = new PORDCH03E1PORDCH
                    {
                        PURCHASEORDER = poNumber,
                        E1BPMEPOHEADER = new PORDCH03E1BPMEPOHEADER
                        {
                            COMP_CODE = compCode,
                            DOC_TYPE = docType,
                            CREAT_DATE = createDate,
                            CREATED_BY = createdBy,
                            ITEM_INTVL = itemIntvl,
                            VENDOR = vendor,
                            PURCH_ORG = purchOrg,
                            PUR_GROUP = purGroup,
                            CURRENCY = currency
                        },
                        E1BPMEPOHEADERX = new PORDCH03E1BPMEPOHEADERX
                        {
                            COMP_CODE = string.IsNullOrEmpty(compCode) ? string.Empty : "X",
                            DOC_TYPE = string.IsNullOrEmpty(docType) ? string.Empty : "X",
                            CREAT_DATE = string.IsNullOrEmpty(createDate) ? string.Empty : "X",
                            CREATED_BY = string.IsNullOrEmpty(createdBy) ? string.Empty : "X",
                            ITEM_INTVL = string.IsNullOrEmpty(itemIntvl) ? string.Empty : "X",
                            VENDOR = string.IsNullOrEmpty(vendor) ? string.Empty : "X",
                            PURCH_ORG = string.IsNullOrEmpty(purchOrg) ? string.Empty : "X",
                            PUR_GROUP = string.IsNullOrEmpty(purGroup) ? string.Empty : "X",
                            CURRENCY = string.IsNullOrEmpty(currency) ? string.Empty : "X"
                        },
                        E1BPMEPOITEM = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOITEM
                        {
                            PO_ITEM = x.E1BpmePoItemPoItem,
                            DELETE_IND = x.E1BpmePoItemDeleteInd,
                            SHORT_TEXT = x.E1BpmePoItemShortText,
                            PLANT = x.E1BpmePoItemPlant,
                            STGE_LOC = x.E1BpmePoItemStgeLoc,
                            MATL_GROUP = x.E1BpmePoItemMatlGroup,
                            VEND_MAT = x.E1BpmePoItemVendMat,
                            QUANTITY = x.E1BpmePoItemQuantity,
                            PO_UNIT = x.E1BpmePoItemPoUnit,
                            ORDERPR_UN = x.E1BpmePoItemOrderPrUn,
                            NET_PRICE = x.E1BpmePoItemNetPrice,
                            PRICE_UNIT = x.E1BpmePoItemPriceUnit,
                            OVER_DLV_TOL = x.E1BpmePoItemOverDlvTol,
                            NO_MORE_GR = x.E1BpmePoItemNoMoreGr,
                            ACCTASSCAT = x.E1BpmePoItemAccTassCat,
                            PREQ_NAME = x.E1BpmePoItemPreqName
                        }).ToArray(),
                        E1BPMEPOITEMX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOITEMX
                        {
                            PO_ITEM = x.E1BpmePoItemPoItem,
                            DELETE_IND = string.IsNullOrEmpty(x.E1BpmePoItemDeleteInd) ? string.Empty : "X",
                            SHORT_TEXT = string.IsNullOrEmpty(x.E1BpmePoItemShortText) ? string.Empty : "X",
                            PLANT = string.IsNullOrEmpty(x.E1BpmePoItemPlant) ? string.Empty : "X",
                            STGE_LOC = string.IsNullOrEmpty(x.E1BpmePoItemStgeLoc) ? string.Empty : "X",
                            MATL_GROUP = string.IsNullOrEmpty(x.E1BpmePoItemMatlGroup) ? string.Empty : "X",
                            VEND_MAT = string.IsNullOrEmpty(x.E1BpmePoItemVendMat) ? string.Empty : "X",
                            QUANTITY = string.IsNullOrEmpty(x.E1BpmePoItemQuantity) ? string.Empty : "X",
                            PO_UNIT = string.IsNullOrEmpty(x.E1BpmePoItemPoUnit) ? string.Empty : "X",
                            ORDERPR_UN = string.IsNullOrEmpty(x.E1BpmePoItemOrderPrUn) ? string.Empty : "X",
                            NET_PRICE = string.IsNullOrEmpty(x.E1BpmePoItemNetPrice) ? string.Empty : "X",
                            PRICE_UNIT = string.IsNullOrEmpty(x.E1BpmePoItemPriceUnit) ? string.Empty : "X",
                            OVER_DLV_TOL = string.IsNullOrEmpty(x.E1BpmePoItemOverDlvTol) ? string.Empty : "X",
                            NO_MORE_GR = string.IsNullOrEmpty(x.E1BpmePoItemNoMoreGr) ? string.Empty : "X",
                            ACCTASSCAT = string.IsNullOrEmpty(x.E1BpmePoItemAccTassCat) ? string.Empty : "X",
                            PREQ_NAME = string.IsNullOrEmpty(x.E1BpmePoItemPreqName) ? string.Empty : "X"
                        }).ToArray(),
                        E1BPMEPOSCHEDULE = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOSCHEDULE
                        {
                            PO_ITEM = x.E1BpmePoSchedulePoItem,
                            SCHED_LINE = x.E1BpmePoScheduleSchedLine,
                            QUANTITY = x.E1BpmePoScheduleQuantity,
                            DELIVERY_DATE = x.E1BpmePoScheduleDeliveryDate
                        }).ToArray(),
                        E1BPMEPOSCHEDULX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOSCHEDULX
                        {
                            PO_ITEM = x.E1BpmePoSchedulePoItem,
                            SCHED_LINE = x.E1BpmePoScheduleSchedLine,
                            QUANTITY = string.IsNullOrEmpty(x.E1BpmePoScheduleQuantity) ? string.Empty : "X",
                            DELIVERY_DATE = string.IsNullOrEmpty(x.E1BpmePoScheduleDeliveryDate) ? string.Empty : "X"
                        }).ToArray(),
                        E1BPMEPOACCOUNT = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOACCOUNT
                        {
                            PO_ITEM = x.E1BpmePoAccountPoItem,
                            GL_ACCOUNT = x.E1BpmePoAccountGlAccount,
                            COSTCENTER = x.E1BpmePoAccountCostCenter,
                            SERIAL_NO = x.E1BpmePoAccountSerialNo
                        }).ToArray(),
                        E1BPMEPOACCOUNTX = message.Payload.PurchaseOrderPayloadItemList.Select(x => new PORDCH03E1BPMEPOACCOUNTX
                        {
                            PO_ITEM = x.E1BpmePoAccountPoItem,
                            GL_ACCOUNT = string.IsNullOrEmpty(x.E1BpmePoAccountGlAccount) ? string.Empty : "X",
                            COSTCENTER = string.IsNullOrEmpty(x.E1BpmePoAccountCostCenter) ? string.Empty : "X",
                            SERIAL_NO = x.E1BpmePoAccountSerialNo
                        }).ToArray()
                    }
                }
            };

            return returnVal;
        }
    }
}