using System;
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
    public class ProductionOrderMesageMapTest
    {
        private IProductionOrderMesageMap _classUnderTest;
        private ProductionOrderStatusSapCommand _nserviceBusMessage;
        private ZPP_CHNG _soapMessage;
        
        private const string SagaReferenceId = "a";

        public string ProductionOrderNumber  = "b";
        public string ReleaseFlag  = "c";
        public string CompleteFlag  = "d";
        public double? OrderQuantityIn = 1.1d;
        public string OrderQuantityOut = "1.1";
        public string OrderQuantityUom   = "e";
        public DateTime? FinishDateIn = DateTime.Today;
        public string FinishDateOut = DateTime.Today.ToString("yyyyMMdd");

        private const EDI_DC40ZPP_CHNGZPP_CHNGDIRECT Direct = EDI_DC40ZPP_CHNGZPP_CHNGDIRECT.Item1;
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

            _nserviceBusMessage = new ProductionOrderStatusSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Payload = new ProductionOrderStatusPayload
                {
                    ProductionOrderStatusPayloadItem = new List<ProductionOrderStatusPayloadItem>
                    {
                        new ProductionOrderStatusPayloadItem
                        {
                            ProductionOrderNumber = ProductionOrderNumber,
                            ReleaseFlag = ReleaseFlag,
                            CompleteFlag = CompleteFlag,
                            OrderQuantity = OrderQuantityIn,
                            OrderQuantityUom = OrderQuantityUom,
                            FinishDate = FinishDateIn
                        }
                    }
                }
            };

            _classUnderTest = new ProductionOrderMesageMap();
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

            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].AUFNR, ProductionOrderNumber);
            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].CFLAG, CompleteFlag);
            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].GAMNG, OrderQuantityOut);
            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].GLTRI, FinishDateOut);
            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].GMEIN, OrderQuantityUom);
            Assert.AreEqual(_soapMessage.IDOC.Z1PLINE[0].RFLAG, ReleaseFlag);
            
        }
    }
}