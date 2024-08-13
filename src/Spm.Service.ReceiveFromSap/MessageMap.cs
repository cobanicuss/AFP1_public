using System.Linq;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ReceiveFromSap.SoapMessages;
using Spm.Shared.Payloads;

namespace Spm.Service.ReceiveFromSap
{
    public interface IMapSoapMessage
     {
        ProductAchievementSapResponse ToProductAchievementSapResponse(SYSTAT01IDOC messageBody);
        PurchaseOrderCreateSapResponse ToPurchaseOrderCreateSapResponse(SYSTAT01IDOC messageBody);
        PurchaseOrderChangeSapResponse ToPurchaseOrderChangeSapResponse(SYSTAT01IDOC messageBody);
        ProductionOrderStatusSapResponse ToProductionOrderStatusSapResponse(SYSTAT01IDOC messageBody);
        GoodsReceiptSapResponse ToGoodsReceiptSapResponse(SYSTAT01IDOC messageBody);
        MaterialMasterSapResponse ToMaterialMasterSapResponse(SYSTAT01IDOC messageBody);
        GeneralLedgerSapResponse ToGeneralLedgerSapResponse(SYSTAT01IDOC messageBody);
        TestCertificateSapResponse ToTestCertificateSapResponse(SYSTAT01IDOC messageBody);

        MaterialMasterUpdateSapRequest ToMatarialMasterUpdateSapRequest(MATMASMATMAS05ZMATMAS5 messageBody);
        TestCertificateSapRequest ToTestCertificateSapRequest(ZOBTCZOBTC01 messageBody);
     }

    public class MapSoapMessage : IMapSoapMessage
    {
        public ProductAchievementSapResponse ToProductAchievementSapResponse(SYSTAT01IDOC messageBody)
        {
            var baseIdoc = GetBaseIdoc(messageBody);

            var message = new ProductAchievementSapResponse(baseIdoc)
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public PurchaseOrderCreateSapResponse ToPurchaseOrderCreateSapResponse(SYSTAT01IDOC messageBody)
        {
            var message = new PurchaseOrderCreateSapResponse
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public PurchaseOrderChangeSapResponse ToPurchaseOrderChangeSapResponse(SYSTAT01IDOC messageBody)
        {
            var message = new PurchaseOrderChangeSapResponse
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public ProductionOrderStatusSapResponse ToProductionOrderStatusSapResponse(SYSTAT01IDOC messageBody)
        {
            var baseIdoc = GetBaseIdoc(messageBody);

            var message = new ProductionOrderStatusSapResponse(baseIdoc)
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public GoodsReceiptSapResponse ToGoodsReceiptSapResponse(SYSTAT01IDOC messageBody)
        {
            var message = new GoodsReceiptSapResponse
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public MaterialMasterSapResponse ToMaterialMasterSapResponse(SYSTAT01IDOC messageBody)
        {
            var message = new MaterialMasterSapResponse
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public GeneralLedgerSapResponse ToGeneralLedgerSapResponse(SYSTAT01IDOC messageBody)
        {
            var message = new GeneralLedgerSapResponse
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public TestCertificateSapResponse ToTestCertificateSapResponse(SYSTAT01IDOC messageBody)
        {
            var baseIdoc = GetBaseIdoc(messageBody);

            var message = new TestCertificateSapResponse(baseIdoc)
            {
                SagaReferenceId = messageBody.E1STATS.DOCNUM
            };

            return message;
        }

        public MaterialMasterUpdateSapRequest ToMatarialMasterUpdateSapRequest(MATMASMATMAS05ZMATMAS5 messageBody)
        {
            var mappedList = messageBody.E1MARAM.Select(x => new MaterialMasterUpdatePayloadItem
            {
                SapMaterialNumber = x.MATNR,
                OldMaterialNumber = x.BISMT,
                PrimeGtin = x.EAN11,
                RejectGtin = x.Z1MARAM != null && x.Z1MARAM.Any() ? x.Z1MARAM[0].ZZREJTEAN : string.Empty,
                RejectCode = x.Z1MARAM != null && x.Z1MARAM.Any() ? x.Z1MARAM[0].ZZREJT : string.Empty,
                SprasLanguage0 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[0].SPRAS : string.Empty,
                SapDescription0 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[0].MAKTX : string.Empty,
                SprasIsoLanguage0 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[0].SPRAS_ISO : string.Empty,
                SprasLanguage1 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[1].SPRAS : string.Empty,
                SapDescription1 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[1].MAKTX : string.Empty,
                SprasIsoLanguage1 = x.E1MAKTM != null && x.E1MAKTM.Any() ? x.E1MAKTM[1].SPRAS_ISO : string.Empty
            }).ToList();
            
            var message = new MaterialMasterUpdateSapRequest
            {
                ShortItemNumber = messageBody.EDI_DC40.DOCNUM,
                Payload = new MaterialMasterUpdatePayload
                {
                    MaterialMasterUpdatePayloadItem = mappedList
                }
            };

            return message;
        }

        public TestCertificateSapRequest ToTestCertificateSapRequest(ZOBTCZOBTC01 messageBody)
        {
            var mappedList = messageBody.Z1TCLIN.Select(x => new TestCertificateRequestPayloadItem
            {
                SapMaterialNumber = x.MATNR,
                SalesOrderNumber = x.VBELV,
                SaleInvoiceNumber = x.VBELN,
                CustomerName = x.NAME1,
                CustomerAccountNumber = x.KUNNR,
                PackNumber = x.HEAT,
                DateUpdated = x.WADAT_IST,
                ShipTo = x.SHIPTO,
                EmailAddress = x.Z1TCEML[0].EMAIL, //SAP shall always populate: hopefully//
                PurchaseOrder = x.PURCHASEORDER
            }).ToList();

            var message = new TestCertificateSapRequest
            {
                CertificateId = messageBody.EDI_DC40.DOCNUM,
                Payload = new TestCertificateRequestPayload
                {
                    TestCertificatePayloadItemList = mappedList
                }
            };

            return message;
        }

        private static BaseResponseIdoc GetBaseIdoc(SYSTAT01IDOC messageBody)
        {
            var message = new BaseResponseIdoc
            {
                Tabnam = messageBody.EDI_DC40.TABNAM,
                Direct = messageBody.EDI_DC40.DIRECT,
                Idoctyp = messageBody.EDI_DC40.IDOCTYP,
                Mestyp = messageBody.EDI_DC40.MESTYP,
                Sndpor = messageBody.EDI_DC40.SNDPOR,
                Sndprt = messageBody.EDI_DC40.SNDPRT,
                Sndprn = messageBody.EDI_DC40.SNDPRN,
                Rcvpor = messageBody.EDI_DC40.RCVPOR,
                Rcvprt = messageBody.EDI_DC40.RCVPRT,
                Rcvprn = messageBody.EDI_DC40.RCVPRN,
                Docnum = messageBody.E1STATS.DOCNUM,
                Logdat = messageBody.E1STATS.LOGDAT,
                Logtim = messageBody.E1STATS.LOGTIM,
                Status = messageBody.E1STATS.STATUS,
                Uname = messageBody.E1STATS.UNAME,
                Repid = messageBody.E1STATS.REPID,
                Stacod = messageBody.E1STATS.STACOD,
                Statxt = messageBody.E1STATS.STATXT,
                Statyp = messageBody.E1STATS.STATYP
            };
            return message;
        }
    }
}