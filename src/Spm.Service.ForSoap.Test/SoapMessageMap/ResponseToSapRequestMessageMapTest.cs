using NUnit.Framework;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.SoapMessageMap
{
    [TestFixture]
    public class ResponseToSapRequestMessageMapTest
    {
        private IResponseToSapRequestMessageMap _classUnderTest;
        private ResponseToSapRequestCommand _nserviceBusMessage;
        private SYSTAT01 _soapMessage;

        private const string NumberId = "a";

        private const EDI_DC40STATUSSYSTAT01DIRECT Direct = EDI_DC40STATUSSYSTAT01DIRECT.Item1;
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

            _nserviceBusMessage = new ResponseToSapRequestCommand
            {
                NumberId = NumberId
            };

            _classUnderTest = new ResponseToSapRequestMessageMap();
        }

        private void MappingMessages()
        {
            _soapMessage = _classUnderTest.CreateResponseToSapMessage(_nserviceBusMessage);
        }

        private void SoapMessageMustBeMappedCorrectly()
        {
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.TABNAM, DefaultSapVariables.Tabnam);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.MANDT, DefaultSapVariables.Mandt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.DIRECT, Direct);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.IDOCTYP, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.MESTYP, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPOR, string.Empty);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPRT, DefaultSapVariables.SndPrt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.SNDPRN, DefaultSapVariables.OrrSysDevAndTest);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPOR, DefaultSapVariables.RcvPor);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPRT, DefaultSapVariables.RcvPrt);
            Assert.AreEqual(_soapMessage.IDOC.EDI_DC40.RCVPRN, DefaultSapVariables.RcvPrn);

            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].DOCNUM, NumberId);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].LOGDAT, DefaultSapVariables.DateNow);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].LOGTIM, DefaultSapVariables.TimeNow);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].STATUS, DefaultSapVariables.Status);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].UNAME, DefaultSapVariables.Uname);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].REPID, DefaultSapVariables.RepId);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].STACOD, DefaultSapVariables.StaCod);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].STATXT, DefaultSapVariables.StaTxt);
            Assert.AreEqual(_soapMessage.IDOC.E1STATS[0].STATYP, DefaultSapVariables.StaTyp);
        }
    }
}