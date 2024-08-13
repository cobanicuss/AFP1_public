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
    public class GeneralLedgerMessageMapTest
    {
        private IGeneralLedgerMessageMap _classUnderTest;
        private GeneralLedgerSapCommand _nserviceBusMessage;
        private ACC_DOCUMENT03 _soapMessage;

        private const string SagaReferenceId = "a";
        private const string UserName = "c";
        private const string HeaderTxt = "d";
        private const string CompanyCode = "e";
        private const string DocDate = "f";
        private const string PstngDate = "g";
        private const string DocType = "h";
        private const string RefDocNo = "i";
        private const string AcItemNoAcc = "j";
        private const string ItemText = "k";
        private const string AllocNmbr = "l";
        private const string CostCentre = "m";
        private const string GlAccount = "n";
        private const string ProfitCentre = "o";
        private const string Currency = "q";
        private const string AmtDoccur = "r";
        private const EDI_DC40ACC_DOCUMENTACC_DOCUMENT03DIRECT Direct = EDI_DC40ACC_DOCUMENTACC_DOCUMENT03DIRECT.Item1;
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

            _nserviceBusMessage = new GeneralLedgerSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Payload = new GeneralLedgerPayload 
                { 
                    GeneralLedgerPayloadItem = new List<GeneralLedgerPayloadItem>
                    {
                        new GeneralLedgerPayloadItem
                        {
                            UserName = UserName,
                            HeaderTxt = HeaderTxt,
                            CompanyCode = CompanyCode,
                            DocDate = DocDate,
                            PstngDate = PstngDate,
                            DocType = DocType,
                            RefDocNo = RefDocNo,
                            AcItemNoAcc = AcItemNoAcc,
                            ItemText = ItemText,
                            AllocNmbr = AllocNmbr,
                            CostCentre = CostCentre,
                            Account = GlAccount,
                            ProfitCentre = ProfitCentre,
                            Currency = Currency,
                            Doccur = AmtDoccur
                        }
                    } 
                }
            };

            _classUnderTest = new GeneralLedgerMessageMap();
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

            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.USERNAME, UserName);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.HEADER_TXT, HeaderTxt);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.COMP_CODE, CompanyCode);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.DOC_DATE, DocDate);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.PSTNG_DATE, PstngDate);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.DOC_TYPE, DocType);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACHE09.REF_DOC_NO, RefDocNo);

            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].ITEMNO_ACC, AcItemNoAcc);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].ITEM_TEXT, ItemText);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].ALLOC_NMBR, AllocNmbr);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].COSTCENTER, CostCentre);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].GL_ACCOUNT, GlAccount);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACGL09[0].PROFIT_CTR, ProfitCentre);

            Assert.AreEqual(_soapMessage.IDOC.E1BPACCR09[0].ITEMNO_ACC, AcItemNoAcc);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACCR09[0].CURRENCY, Currency);
            Assert.AreEqual(_soapMessage.IDOC.E1BPACCR09[0].AMT_DOCCUR, AmtDoccur);
        }
    }
}