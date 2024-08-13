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
    public class GoodsMessageMapTest
    {
        private IGoodsMessageMap _classUnderTest;
        private GoodsReceiptSapCommand _nserviceBusMessage;
        private ZMBGMCR02_EXTND _soapMessage;

        private const string SagaReferenceId = "a";
        private const string Type = "b";
        private const string HeadPostingDate = "c";
        private const string HeadDocDate = "d";
        private const string HeadRefDocNo = "e";
        private const string HeadHeaderTxt = "f";
        private const string Code = "g";
        private const string ItemCreatePlant = "h";
        private const string ItemCreateStgeLoc = "i";
        private const string ItemCreateMoveType = "j";
        private const string ItemCreateEntryQnt = "k";
        private const string ItemCreateEntryUom = "l";
        private const string ItemCreatePoNumber = "m";
        private const string ItemCreatePoItem = "n";
        private const string ItemCreateMvtInd = "o";
        private const string ItemCreateNoMoreGr = "p";
        private const string TranTypeInd = "q";

        private const EDI_DC40ZMBGMCRZMBGMCR02_EXTNDDIRECT Direct = EDI_DC40ZMBGMCRZMBGMCR02_EXTNDDIRECT.Item1;
        private const string Sndprn = DefaultSapVariables.OrrSysDevAndTest;

        [Test]
        public void MappingNsbToSoapMustBeCorrectly()
        {
            this.Given(_ => NServiceBusMessageToSoapIsRequired())
           .When(_ => MappingMessages())
           .Then(_ => SoapMessageMustBeMappedCorrectly())
           .BDDfy();
        }

        private void NServiceBusMessageToSoapIsRequired()
        {
            ProfileConfigVariables.SndPrn = Sndprn;

            _nserviceBusMessage = new GoodsReceiptSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Type = Type,
                Payload = new GoodsPayload
                {
                    GoodsPayloadItem = new List<GoodsPayloadItem>
                    {
                        new GoodsPayloadItem
                        {
                            HeadPostingDate = HeadPostingDate,
                            HeadDocDate = HeadDocDate,
                            HeadRefDocNo = HeadRefDocNo,
                            HeadHeaderTxt = HeadHeaderTxt,
                            Code = Code,
                            ItemCreatePlant = ItemCreatePlant,
                            ItemCreateStgeLoc = ItemCreateStgeLoc,
                            ItemCreateMoveType = ItemCreateMoveType,
                            ItemCreateEntryQnt = ItemCreateEntryQnt,
                            ItemCreateEntryUom = ItemCreateEntryUom,
                            ItemCreatePoNumber = ItemCreatePoNumber,
                            ItemCreatePoItem = ItemCreatePoItem,
                            ItemCreateMvtInd = ItemCreateMvtInd,
                            ItemCreateNoMoreGr = ItemCreateNoMoreGr,
                            TranTypeInd = TranTypeInd 
                        }
                    }
                }
            };

            _classUnderTest = new GoodsMessageMap();
        }

        private void MappingMessages()
        {
            _soapMessage = _classUnderTest.MakeSoapMessage(_nserviceBusMessage);
        }

        private void SoapMessageMustBeMappedCorrectly()
        {
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.TABNAM, DefaultSapVariables.Tabnam);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.MANDT, DefaultSapVariables.Mandt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.DOCNUM, SagaReferenceId);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.DIRECT, Direct);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.IDOCTYP, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.MESTYP, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPOR, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPRT, DefaultSapVariables.SndPrt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPRN, DefaultSapVariables.OrrSysDevAndTest);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPOR, DefaultSapVariables.RcvPor);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPRT, DefaultSapVariables.RcvPrt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPRN, DefaultSapVariables.RcvPrn);

            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].PLANT, ItemCreatePlant);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].STGE_LOC, ItemCreateStgeLoc);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].MOVE_TYPE, ItemCreateMoveType);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].ENTRY_QNT, ItemCreateEntryQnt);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].ENTRY_UOM, ItemCreateEntryUom);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].PO_NUMBER, ItemCreatePoNumber);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].PO_ITEM, ItemCreatePoItem);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].MVT_IND, ItemCreateMvtInd);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_ITEM_CREATE[0].NO_MORE_GR, ItemCreateNoMoreGr);

            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_HEAD_01.PSTNG_DATE, HeadPostingDate);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_HEAD_01.DOC_DATE, HeadDocDate);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_HEAD_01.REF_DOC_NO, HeadRefDocNo);
            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_HEAD_01.HEADER_TXT, HeadHeaderTxt);

            Assert.AreEqual(_soapMessage.IDOC.E1BP2017_GM_CODE.GM_CODE, Code);
        }
    }
}
