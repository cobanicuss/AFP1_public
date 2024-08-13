using System.Collections.Generic;
using NUnit.Framework;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.SoapMessageMap
{
    [TestFixture]
    public class PurchaseOrderMessageMapTest
    {
        private IPurchaseOrderMessageMap _classUnderTest;
        
        private PurchaseOrderCreateSapCommand _nserviceBusMessageCreate;
        private PurchaseOrderChangeSapCommand _nserviceBusMessageChange;

        private PORDCR103 _soapMessageCreate;
        private PORDCH03 _soapMessageChange;

        private const string SagaReferenceId = "a";

        private const string EdiDc40Docnum = "b";

        private const string E1BpmePoHeaderPoNumber = "c";
        private const string E1BpmePoHeaderCompCode = "d";
        private const string E1BpmePoHeaderDocType = "e";
        private const string E1BpmePoHeaderCreatDate = "f";
        private const string E1BpmePoHeaderCreatedBy = "g";
        private const string E1BpmePoHeaderItemIntvl = "h";
        private const string E1BpmePoHeaderVendor = "i";
        private const string E1BpmePoHeaderPurchOrg = "j";
        private const string E1BpmePoHeaderPurGroup = "k";
        private const string E1BpmePoHeaderCurrency = "l";
        private const string E1BpmePoHeaderDocDate = "m";

        private const string E1BpmePoItemPoItem = "n";
        private const string E1BpmePoItemDeleteInd = "o";
        private const string E1BpmePoItemShortText = "p";
        private const string E1BpmePoItemPlant = "q";
        private const string E1BpmePoItemStgeLoc = "r";
        private const string E1BpmePoItemMatlGroup = "s";
        private const string E1BpmePoItemVendMat = "t";
        private const string E1BpmePoItemQuantity = "u";
        private const string E1BpmePoItemPoUnit = "v";
        private const string E1BpmePoItemOrderPrUn = "w";
        private const string E1BpmePoItemNetPrice = "x";
        private const string E1BpmePoItemPriceUnit = "y";
        private const string E1BpmePoItemOverDlvTol = "z";
        private const string E1BpmePoItemAccTassCat = "1a";
        private const string E1BpmePoItemPreqName = "1b";
        private const string E1BpmePoItemNoMoreGr = "1c";

        private const string E1BpmePoSchedulePoItem = "1d";
        private const string E1BpmePoScheduleSchedLine = "1e";
        private const string E1BpmePoScheduleQuantity = "1f";
        private const string E1BpmePoScheduleDeliveryDate = "1g";

        private const string E1BpmePoAccountPoItem = "1h";
        private const string E1BpmePoAccountSerialNo = "1i";
        private const string E1BpmePoAccountGlAccount = "1j";
        private const string E1BpmePoAccountCostCenter = "1k";

        private const EDI_DC40PORDCR1PORDCR103DIRECT DirectCreate = EDI_DC40PORDCR1PORDCR103DIRECT.Item1;
        private const EDI_DC40PORDCHPORDCH03DIRECT DirectChange = EDI_DC40PORDCHPORDCH03DIRECT.Item1;
        
        private const string Sndprn = DefaultSapVariables.OrrSysDevAndTest;

        [Test]
        public void MappingNsbToSoapMustBeCorrectlyForPoCreate()
        {
            this.Given(_ => NServiceBusMessageToSoapIsRequiredPoCreate())
           .When(_ => MappingMessagesCreate())
           .Then(_ => SoapMessageMustBeMappedCorrectlyCreate())
           .BDDfy();
        }

        [Test]
        public void MappingNsbToSoapMustBeCorrectlyPoChange()
        {
            this.Given(_ => NServiceBusMessageToSoapIsRequiredPoChange())
           .When(_ => MappingMessagesChange())
           .Then(_ => SoapMessageMustBeMappedCorrectlyChange())
           .BDDfy();
        }

        private void NServiceBusMessageToSoapIsRequiredPoCreate()
        {
            ProfileConfigVariables.SndPrn = Sndprn;

            _nserviceBusMessageCreate = new PurchaseOrderCreateSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Payload = CreatePayload()
            };

            _classUnderTest = new PurchaseOrderMessageMap();
        }

        private void NServiceBusMessageToSoapIsRequiredPoChange()
        {
            ProfileConfigVariables.SndPrn = Sndprn;

            _nserviceBusMessageChange = new PurchaseOrderChangeSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Payload = CreatePayload()
            };

            _classUnderTest = new PurchaseOrderMessageMap();
        }

        private static PurchaseOrderPayload CreatePayload()
        {
            return new PurchaseOrderPayload
            {
                PurchaseOrderPayloadItemList = new List<PurchaseOrderPayloadItem>
                {
                    new PurchaseOrderPayloadItem
                    {
                        EdiDc40Docnum = EdiDc40Docnum,
                        E1BpmePoHeaderPoNumber = E1BpmePoHeaderPoNumber,
                        E1BpmePoHeaderCompCode = E1BpmePoHeaderCompCode,
                        E1BpmePoHeaderDocType = E1BpmePoHeaderDocType,
                        E1BpmePoHeaderCreatDate = E1BpmePoHeaderCreatDate,
                        E1BpmePoHeaderCreatedBy = E1BpmePoHeaderCreatedBy,
                        E1BpmePoHeaderItemIntvl = E1BpmePoHeaderItemIntvl,
                        E1BpmePoHeaderVendor = E1BpmePoHeaderVendor,
                        E1BpmePoHeaderPurchOrg = E1BpmePoHeaderPurchOrg,
                        E1BpmePoHeaderPurGroup = E1BpmePoHeaderPurGroup,
                        E1BpmePoHeaderCurrency = E1BpmePoHeaderCurrency,
                        E1BpmePoHeaderDocDate = E1BpmePoHeaderDocDate,
                        E1BpmePoItemPoItem = E1BpmePoItemPoItem,
                        E1BpmePoItemDeleteInd = E1BpmePoItemDeleteInd,
                        E1BpmePoItemShortText = E1BpmePoItemShortText,
                        E1BpmePoItemPlant = E1BpmePoItemPlant,
                        E1BpmePoItemStgeLoc = E1BpmePoItemStgeLoc,
                        E1BpmePoItemMatlGroup = E1BpmePoItemMatlGroup,
                        E1BpmePoItemVendMat = E1BpmePoItemVendMat,
                        E1BpmePoItemQuantity = E1BpmePoItemQuantity,
                        E1BpmePoItemPoUnit = E1BpmePoItemPoUnit,
                        E1BpmePoItemOrderPrUn = E1BpmePoItemOrderPrUn,
                        E1BpmePoItemNetPrice = E1BpmePoItemNetPrice,
                        E1BpmePoItemPriceUnit = E1BpmePoItemPriceUnit,
                        E1BpmePoItemOverDlvTol = E1BpmePoItemOverDlvTol,
                        E1BpmePoItemAccTassCat = E1BpmePoItemAccTassCat,
                        E1BpmePoItemPreqName = E1BpmePoItemPreqName,
                        E1BpmePoItemNoMoreGr = E1BpmePoItemNoMoreGr,
                        E1BpmePoSchedulePoItem = E1BpmePoSchedulePoItem,
                        E1BpmePoScheduleSchedLine = E1BpmePoScheduleSchedLine,
                        E1BpmePoScheduleQuantity = E1BpmePoScheduleQuantity,
                        E1BpmePoScheduleDeliveryDate = E1BpmePoScheduleDeliveryDate,
                        E1BpmePoAccountPoItem = E1BpmePoAccountPoItem,
                        E1BpmePoAccountSerialNo = E1BpmePoAccountSerialNo,
                        E1BpmePoAccountGlAccount = E1BpmePoAccountGlAccount,
                        E1BpmePoAccountCostCenter = E1BpmePoAccountCostCenter
                    }
                }
            };
        }

        private void MappingMessagesCreate()
        {
            _soapMessageCreate = _classUnderTest.MakePurchaseOrderCreateSoapMessage(_nserviceBusMessageCreate);
        }

        private void MappingMessagesChange()
        {
            _soapMessageChange = _classUnderTest.MakePurchaseOrderChangeSoapMessage(_nserviceBusMessageChange);
        }

        private void SoapMessageMustBeMappedCorrectlyCreate()
        {
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.TABNAM, DefaultSapVariables.Tabnam);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.MANDT, DefaultSapVariables.Mandt);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.DOCNUM, SagaReferenceId);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.DIRECT, DirectCreate);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.IDOCTYP, string.Empty);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.MESTYP, string.Empty);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.SNDPOR, string.Empty);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.SNDPRT, DefaultSapVariables.SndPrt);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.SNDPRN, DefaultSapVariables.OrrSysDevAndTest);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.RCVPOR, DefaultSapVariables.RcvPor);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.RCVPRT, DefaultSapVariables.RcvPrt);
            Assert.AreEqual(_soapMessageCreate.IDOC.EDI_DC40.RCVPRN, DefaultSapVariables.RcvPrn);

            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.PO_NUMBER, E1BpmePoHeaderPoNumber);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.COMP_CODE, E1BpmePoHeaderCompCode);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.DOC_TYPE, E1BpmePoHeaderDocType);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.CREAT_DATE, E1BpmePoHeaderCreatDate);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.CREATED_BY, E1BpmePoHeaderCreatedBy);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.ITEM_INTVL, E1BpmePoHeaderItemIntvl);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.VENDOR, E1BpmePoHeaderVendor);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.PURCH_ORG, E1BpmePoHeaderPurchOrg);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.PUR_GROUP, E1BpmePoHeaderPurGroup);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOHEADER.CURRENCY, E1BpmePoHeaderCurrency);

            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].PO_ITEM, E1BpmePoItemPoItem);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].DELETE_IND, E1BpmePoItemDeleteInd);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].SHORT_TEXT, E1BpmePoItemShortText);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].PLANT, E1BpmePoItemPlant);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].STGE_LOC, E1BpmePoItemStgeLoc);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].MATL_GROUP, E1BpmePoItemMatlGroup);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].VEND_MAT, E1BpmePoItemVendMat);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].QUANTITY, E1BpmePoItemQuantity);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].PO_UNIT, E1BpmePoItemPoUnit);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].ORDERPR_UN, E1BpmePoItemOrderPrUn);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].NET_PRICE, E1BpmePoItemNetPrice);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].PRICE_UNIT, E1BpmePoItemPriceUnit);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].OVER_DLV_TOL, E1BpmePoItemOverDlvTol);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].ACCTASSCAT, E1BpmePoItemAccTassCat);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOITEM[0].PREQ_NAME, E1BpmePoItemPreqName);

            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOSCHEDULE[0].PO_ITEM, E1BpmePoSchedulePoItem);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOSCHEDULE[0].SCHED_LINE, E1BpmePoScheduleSchedLine);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOSCHEDULE[0].QUANTITY, E1BpmePoScheduleQuantity);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOSCHEDULE[0].DELIVERY_DATE, E1BpmePoScheduleDeliveryDate);

            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOACCOUNT[0].PO_ITEM, E1BpmePoAccountPoItem);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOACCOUNT[0].SERIAL_NO, E1BpmePoAccountSerialNo);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOACCOUNT[0].GL_ACCOUNT, E1BpmePoAccountGlAccount);
            Assert.AreEqual(_soapMessageCreate.IDOC.E1PORDCR1.E1BPMEPOACCOUNT[0].COSTCENTER, E1BpmePoAccountCostCenter);
        }

        private void SoapMessageMustBeMappedCorrectlyChange()
        {
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.TABNAM, DefaultSapVariables.Tabnam);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.MANDT, DefaultSapVariables.Mandt);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.DOCNUM, SagaReferenceId);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.DIRECT, DirectChange);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.IDOCTYP, string.Empty);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.MESTYP, string.Empty);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.SNDPOR, string.Empty);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.SNDPRT, DefaultSapVariables.SndPrt);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.SNDPRN, DefaultSapVariables.OrrSysDevAndTest);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.RCVPOR, DefaultSapVariables.RcvPor);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.RCVPRT, DefaultSapVariables.RcvPrt);
            Assert.AreEqual(_soapMessageChange.IDOC.EDI_DC40.RCVPRN, DefaultSapVariables.RcvPrn);

            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.COMP_CODE, E1BpmePoHeaderCompCode);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.DOC_TYPE, E1BpmePoHeaderDocType);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.CREAT_DATE, E1BpmePoHeaderCreatDate);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.CREATED_BY, E1BpmePoHeaderCreatedBy);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.ITEM_INTVL, E1BpmePoHeaderItemIntvl);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.VENDOR, E1BpmePoHeaderVendor);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.PURCH_ORG, E1BpmePoHeaderPurchOrg);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.PUR_GROUP, E1BpmePoHeaderPurGroup);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOHEADER.CURRENCY, E1BpmePoHeaderCurrency);

            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].PO_ITEM, E1BpmePoItemPoItem);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].DELETE_IND, E1BpmePoItemDeleteInd);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].SHORT_TEXT, E1BpmePoItemShortText);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].PLANT, E1BpmePoItemPlant);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].STGE_LOC, E1BpmePoItemStgeLoc);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].MATL_GROUP, E1BpmePoItemMatlGroup);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].VEND_MAT, E1BpmePoItemVendMat);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].QUANTITY, E1BpmePoItemQuantity);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].PO_UNIT, E1BpmePoItemPoUnit);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].ORDERPR_UN, E1BpmePoItemOrderPrUn);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].NET_PRICE, E1BpmePoItemNetPrice);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].PRICE_UNIT, E1BpmePoItemPriceUnit);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].OVER_DLV_TOL, E1BpmePoItemOverDlvTol);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].ACCTASSCAT, E1BpmePoItemAccTassCat);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOITEM[0].PREQ_NAME, E1BpmePoItemPreqName);

            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOSCHEDULE[0].PO_ITEM, E1BpmePoSchedulePoItem);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOSCHEDULE[0].SCHED_LINE, E1BpmePoScheduleSchedLine);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOSCHEDULE[0].QUANTITY, E1BpmePoScheduleQuantity);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOSCHEDULE[0].DELIVERY_DATE, E1BpmePoScheduleDeliveryDate);

            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOACCOUNT[0].PO_ITEM, E1BpmePoAccountPoItem);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOACCOUNT[0].SERIAL_NO, E1BpmePoAccountSerialNo);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOACCOUNT[0].GL_ACCOUNT, E1BpmePoAccountGlAccount);
            Assert.AreEqual(_soapMessageChange.IDOC.E1PORDCH.E1BPMEPOACCOUNT[0].COSTCENTER, E1BpmePoAccountCostCenter);
        }
    }
}