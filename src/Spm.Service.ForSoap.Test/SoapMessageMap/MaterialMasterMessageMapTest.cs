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
    public class MaterialMasterMessageMapTest
    {
        private IMaterialMasterMessageMap _classUnderTest;
        private MaterialMasterSapCommand _nserviceBusMessage;
        private ZMATMAS5 _soapMessage;

        private const string SagaReferenceId = "a";

        private const string E1MaramBismt = "b";
        private const string E1MarcmWerks = "c";
        private const string E1MarcmMsgfn = "d";
        private const string E1MaktmMaktx = "e";
        private const string E1MaramMatkl = "f";
        private const string E1MaramPrdha = "g";
        private const string E1MaramGroes = "h";
        private const string E1MarcmDzeit = "i";

        //E1Cucfg[E1Cuins[0]]\\
        private const string E1CucfgPosex0 = "j";
        private const string E1CucfgConfigId0 = "k";
        private const string E1CucfgRootId0 = "l";
        private const string E1CuinsInstId0 = "m";
        private const string E1CuinsObjType0 = "n";
        private const string E1CuinsClassType0 = "o";
        private const string E1CuinsObjKey0 = "p";

        //E1Cucfg[E1Cuins[1]]\\
        private const string E1CucfgPosex1 = "q";
        private const string E1CucfgConfigId1 = "r";
        private const string E1CucfgRootId1 = "s";
        private const string E1CuinsInstId1 = "t";
        private const string E1CuinsObjType1 = "u";
        private const string E1CuinsClassType1 = "v";
        private const string E1CuinsObjKey1 = "w";

        //E1Cuval[0]\\
        private const string E1CuvalInstId0 = "x";
        private const string E1CuvalCharc0 = "y";
        private const string E1CuvalValue0 = "z";

        //E1Cuval[1]\\
        private const string E1CuvalInstId1 = "1a";
        private const string E1CuvalCharc1 = "1b";
        private const string E1CuvalValue1 = "1c";

        //E1Cuval[2]\\
        private const string E1CuvalInstId2 = "1d";
        private const string E1CuvalCharc2 = "1e";
        private const string E1CuvalValue2 = "1g";

        //E1Cuval[3]\\
        private const string E1CuvalInstId3 = "1h";
        private const string E1CuvalCharc3 = "1i";
        private const string E1CuvalValue3 = "1j";

        //E1Cuval[4]\\
        private const string E1CuvalInstId4 = "1k";
        private const string E1CuvalCharc4 = "1l";
        private const string E1CuvalValue4 = "1m";

        //E1Cuval[5]\\
        private const string E1CuvalInstId5 = "1n";
        private const string E1CuvalCharc5 = "1o";
        private const string E1CuvalValue5 = "1p";

        //E1Cuval[6]\\
        private const string E1CuvalInstId6 = "1q";
        private const string E1CuvalCharc6 = "1r";
        private const string E1CuvalValue6 = "1s";

        //E1Cuval[7]\\
        private const string E1CuvalInstId7 = "1t";
        private const string E1CuvalCharc7 = "1u";
        private const string E1CuvalValue7 = "1v";

        //E1Mvkem[0]\\
        private const string E1MvkemMsgfn0 = "1w";
        private const string E1MvkemVkorg0 = "1x";
        private const string E1MvkemProdh0 = "1y";
        private const string E1MvkemPrat10 = "1z";
        private const string E1MvkemPrat40 = "2a";

        //E1Mvkem[1]\\
        private const string E1MvkemMsgfn1 = "2b";
        private const string E1MvkemVkorg1 = "2c";
        private const string E1MvkemProdh1 = "2d";
        private const string E1MvkemPrat11 = "2e";
        private const string E1MvkemPrat41 = "2f";

        private const string E1MarcmPrctr = "2g";

        //E1Mtxhm[E1Mtxlm[0]]\\
        private const string E1MtxhmMsgfn0 = "2h";
        private const string E1MtxhmTdobject0 = "2i";
        private const string E1MtxhmTdid0 = "2j";
        private const string E1MtxhmTdspras0 = "2k";
        private const string E1MtxhmTdtexttype0 = "2l";
        private const string E1MtxhmTdname0 = "2m";
        private const string E1MtxlmMsgfn0 = "2n";
        private const string E1MtxlmTdformat0 = "2o";
        private const string E1MtxlmTdline0 = "2p";

        //E1Mtxhm[E1Mtxlm[1]]\\
        private const string E1MtxhmMsgfn1 = "2q";
        private const string E1MtxhmTdobject1 = "2r";
        private const string E1MtxhmTdid1 = "2s";
        private const string E1MtxhmTdspras1 = "2t";
        private const string E1MtxhmTdtexttype1 = "2u";
        private const string E1MtxhmTdname1 = "2v";
        private const string E1MtxlmMsgfn1 = "2w";
        private const string E1MtxlmTdformat1 = "2x";
        private const string E1MtxlmTdline1 = "2y";

        //E1Mtxhm[E1Mtxlm[2]]\\
        private const string E1MtxhmMsgfn2 = "2z";
        private const string E1MtxhmTdobject2 = "3a";
        private const string E1MtxhmTdid2 = "3b";
        private const string E1MtxhmTdspras2 = "3c";
        private const string E1MtxhmTdtexttype2 = "3d";
        private const string E1MtxhmTdname2 = "3e";
        private const string E1MtxlmMsgfn2 = "3f";
        private const string E1MtxlmTdformat2 = "3g";
        private const string E1MtxlmTdline2 = "3h";

        private const string E1MarcmEkgrp = "3i";
        private const string E1MarcmMaabc = "3j";
        private const string E1MarcmDispr = "3k";
        private const string E1MarcmBstrf = "3l";
        private const string E1MlgnmMsgfn = "3m";
        private const string E1MlgnmLgnum = "3n";
        private const string E1MlgnmLgbkz = "3o";
        private const string E1MlgnmLhmg1 = "3p";
        private const string E1MlgtmKober = "3q";
        private const string E1MlgtmNsmng = "3r";
        private const string E1MbewmMsgfn = "3s";
        private const string E1MbewmBwkey = "3t";
        private const string Z1MaramZzsan = "3u";
        private const string Z1MaramZzquln = "3v";
        private const string Z1MaramZzpcsn = "3w";
        private const string Z1MaramZzdm1N = "3x";
        private const string Z1MaramZzdm2N = "3y";
        private const string Z1MaramZzdm5N = "3z";
        private const string Z1MaramZzcol1 = "4a";
        private const string Z1MaramZzprfn = "4b";
        private const string Z1MaramZzstol = "4c";
        private const string Z1MaramZzroll = "4d";
        private const string Z1MaramZzmtwc = "4e";
        private const string Z1MaramZzpkdm = "4f";

        //E1Marmm[0]\\
        private const string E1MarmmMsgfn0 = "4g";
        private const string E1MarmmMeinh0 = "4h";
        private const string E1MarmmUmrez0 = "4i";
        private const string E1MarmmUmren0 = "4j";

        //E1Marmm[1]\\
        private const string E1MarmmMsgfn1 = "4k";
        private const string E1MarmmMeinh1 = "4l";
        private const string E1MarmmUmrez1 = "4m";
        private const string E1MarmmUmren1 = "4n";

        //E1Marmm[2]\\
        private const string E1MarmmMsgfn2 = "4o";
        private const string E1MarmmMeinh2 = "4p";
        private const string E1MarmmUmrez2 = "4q";
        private const string E1MarmmUmren2 = "4r";

        //E1Marmm[3]\\
        private const string E1MarmmMsgfn3 = "4s";
        private const string E1MarmmMeinh3 = "4t";
        private const string E1MarmmUmrez3 = "4u";
        private const string E1MarmmUmren3 = "4v";

        //E1Marmm[4]\\
        private const string E1MarmmMsgfn4 = "4w";
        private const string E1MarmmMeinh4 = "4x";
        private const string E1MarmmUmren4 = "4y";
        private const string E1MarmmUmrez4 = "4z";
        private const string E1MarmmNumtp4 = "5a";
        private const string E1MarmmVolume4 = "5b";
        private const string E1MarmmVoleh4 = "5c";
        private const string E1MarmmBrgew4 = "5d";
        private const string E1MarmmGewei = "5e";


        private const EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT Direct = EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT.Item1;
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

            _nserviceBusMessage = new MaterialMasterSapCommand
            {
                SagaReferenceId = SagaReferenceId,
                Payload = new MaterialMasterPayload
                {
                    MaterialMasterPayloadItem = new List<MaterialMasterPayloadItem>
                    {
                        new MaterialMasterPayloadItem
                        {
                            E1MaramBismt = E1MaramBismt,
                            E1MarcmWerks = E1MarcmWerks,
                            E1MarcmMsgfn = E1MarcmMsgfn,
                            E1MaktmMaktx = E1MaktmMaktx,
                            E1MaramMatkl = E1MaramMatkl,
                            E1MaramPrdha = E1MaramPrdha,
                            E1MaramGroes = E1MaramGroes,
                            E1MarcmDzeit = E1MarcmDzeit,

                            //E1Cucfg[E1Cuins[0]]\\
                            E1CucfgPosex0 = E1CucfgPosex0,

                            E1CucfgConfigId0 = E1CucfgConfigId0,
                            E1CucfgRootId0 = E1CucfgRootId0,
                            E1CuinsInstId0 = E1CuinsInstId0,
                            E1CuinsObjType0 = E1CuinsObjType0,
                            E1CuinsClassType0 = E1CuinsClassType0,
                            E1CuinsObjKey0 = E1CuinsObjKey0,

                            //E1Cucfg[E1Cuins[1]]\\
                            E1CucfgPosex1 = E1CucfgPosex1,
                            E1CucfgConfigId1 = E1CucfgConfigId1,
                            E1CucfgRootId1 = E1CucfgRootId1,
                            E1CuinsInstId1 = E1CuinsInstId1,
                            E1CuinsObjType1 = E1CuinsObjType1,
                            E1CuinsClassType1 = E1CuinsClassType1,
                            E1CuinsObjKey1 = E1CuinsObjKey1,

                            //E1Cuval[0]\\
                            E1CuvalInstId0 = E1CuvalInstId0,
                            E1CuvalCharc0 = E1CuvalCharc0,
                            E1CuvalValue0 = E1CuvalValue0,

                            //E1Cuval[1]\\
                            E1CuvalInstId1 = E1CuvalInstId1,
                            E1CuvalCharc1 = E1CuvalCharc1,
                            E1CuvalValue1 = E1CuvalValue1,

                            //E1Cuval[2]\\
                            E1CuvalInstId2 = E1CuvalInstId2,
                            E1CuvalCharc2 = E1CuvalCharc2,
                            E1CuvalValue2 = E1CuvalValue2,

                            //E1Cuval[3]\\
                            E1CuvalInstId3 = E1CuvalInstId3,
                            E1CuvalCharc3 = E1CuvalCharc3,
                            E1CuvalValue3 = E1CuvalValue3,

                            //E1Cuval[4]\\
                            E1CuvalInstId4 = E1CuvalInstId4,
                            E1CuvalCharc4 = E1CuvalCharc4,
                            E1CuvalValue4 = E1CuvalValue4,

                            //E1Cuval[5]\\
                            E1CuvalInstId5 = E1CuvalInstId5,
                            E1CuvalCharc5 = E1CuvalCharc5,
                            E1CuvalValue5 = E1CuvalValue5,

                            //E1Cuval[6]\\
                            E1CuvalInstId6 = E1CuvalInstId6,
                            E1CuvalCharc6 = E1CuvalCharc6,
                            E1CuvalValue6 = E1CuvalValue6,

                            //E1Cuval[7]\\
                            E1CuvalInstId7 = E1CuvalInstId7,
                            E1CuvalCharc7 = E1CuvalCharc7,
                            E1CuvalValue7 = E1CuvalValue7,

                            //E1Mvkem[0]\\
                            E1MvkemMsgfn0 = E1MvkemMsgfn0,
                            E1MvkemVkorg0 = E1MvkemVkorg0,
                            E1MvkemProdh0 = E1MvkemProdh0,
                            E1MvkemPrat10 = E1MvkemPrat10,
                            E1MvkemPrat40 = E1MvkemPrat40,

                            //E1Mvkem[1]\\
                            E1MvkemMsgfn1 = E1MvkemMsgfn1,
                            E1MvkemVkorg1 = E1MvkemVkorg1,
                            E1MvkemProdh1 = E1MvkemProdh1,
                            E1MvkemPrat11 = E1MvkemPrat11,
                            E1MvkemPrat41 = E1MvkemPrat41,

                            E1MarcmPrctr = E1MarcmPrctr,

                            //E1Mtxhm[E1Mtxlm[0]]\\
                            E1MtxhmMsgfn0 = E1MtxhmMsgfn0,
                            E1MtxhmTdobject0 = E1MtxhmTdobject0,
                            E1MtxhmTdid0 = E1MtxhmTdid0,
                            E1MtxhmTdspras0 = E1MtxhmTdspras0,
                            E1MtxhmTdtexttype0 = E1MtxhmTdtexttype0,
                            E1MtxhmTdname0 = E1MtxhmTdname0,
                            E1MtxlmMsgfn0 = E1MtxlmMsgfn0,
                            E1MtxlmTdformat0 = E1MtxlmTdformat0,
                            E1MtxlmTdline0 = E1MtxlmTdline0,

                            //E1Mtxhm[E1Mtxlm[1]]\\
                            E1MtxhmMsgfn1 = E1MtxhmMsgfn1,
                            E1MtxhmTdobject1 = E1MtxhmTdobject1,
                            E1MtxhmTdid1 = E1MtxhmTdid1,
                            E1MtxhmTdspras1 = E1MtxhmTdspras1,
                            E1MtxhmTdtexttype1 = E1MtxhmTdtexttype1,
                            E1MtxhmTdname1 = E1MtxhmTdname1,
                            E1MtxlmMsgfn1 = E1MtxlmMsgfn1,
                            E1MtxlmTdformat1 = E1MtxlmTdformat1,
                            E1MtxlmTdline1 = E1MtxlmTdline1,

                            //E1Mtxhm[E1Mtxlm[2]]\\
                            E1MtxhmMsgfn2 = E1MtxhmMsgfn2,
                            E1MtxhmTdobject2 = E1MtxhmTdobject2,
                            E1MtxhmTdid2 = E1MtxhmTdid2,
                            E1MtxhmTdspras2 = E1MtxhmTdspras2,
                            E1MtxhmTdtexttype2 = E1MtxhmTdtexttype2,
                            E1MtxhmTdname2 = E1MtxhmTdname2,
                            E1MtxlmMsgfn2 = E1MtxlmMsgfn2,
                            E1MtxlmTdformat2 = E1MtxlmTdformat2,
                            E1MtxlmTdline2 = E1MtxlmTdline2,

                            E1MarcmEkgrp = E1MarcmEkgrp,
                            E1MarcmMaabc = E1MarcmMaabc,
                            E1MarcmDispr = E1MarcmDispr,
                            E1MarcmBstrf = E1MarcmBstrf,
                            E1MlgnmMsgfn = E1MlgnmMsgfn,
                            E1MlgnmLgnum = E1MlgnmLgnum,
                            E1MlgnmLgbkz = E1MlgnmLgbkz,
                            E1MlgnmLhmg1 = E1MlgnmLhmg1,
                            E1MlgtmKober = E1MlgtmKober,
                            E1MlgtmNsmng = E1MlgtmNsmng,
                            E1MbewmMsgfn = E1MbewmMsgfn,
                            E1MbewmBwkey = E1MbewmBwkey,
                            Z1MaramZzsan = Z1MaramZzsan,
                            Z1MaramZzquln = Z1MaramZzquln,
                            Z1MaramZzpcsn = Z1MaramZzpcsn,
                            Z1MaramZzdm1N = Z1MaramZzdm1N,
                            Z1MaramZzdm2N = Z1MaramZzdm2N,
                            Z1MaramZzdm5N = Z1MaramZzdm5N,
                            Z1MaramZzcol1 = Z1MaramZzcol1,
                            Z1MaramZzprfn = Z1MaramZzprfn,
                            Z1MaramZzstol = Z1MaramZzstol,
                            Z1MaramZzroll = Z1MaramZzroll,
                            Z1MaramZzmtwc = Z1MaramZzmtwc,
                            Z1MaramZzpkdm = Z1MaramZzpkdm,

                            //E1Marmm[0]\\
                            E1MarmmMsgfn0 = E1MarmmMsgfn0,
                            E1MarmmMeinh0 = E1MarmmMeinh0,
                            E1MarmmUmrez0 = E1MarmmUmrez0,
                            E1MarmmUmren0 = E1MarmmUmren0,

                            //E1Marmm[1]\\
                            E1MarmmMsgfn1 = E1MarmmMsgfn1,
                            E1MarmmMeinh1 = E1MarmmMeinh1,
                            E1MarmmUmrez1 = E1MarmmUmrez1,
                            E1MarmmUmren1 = E1MarmmUmren1,

                            //E1Marmm[2]\\
                            E1MarmmMsgfn2 = E1MarmmMsgfn2,
                            E1MarmmMeinh2 = E1MarmmMeinh2,
                            E1MarmmUmrez2 = E1MarmmUmrez2,
                            E1MarmmUmren2 = E1MarmmUmren2,

                            //E1Marmm[3]\\
                            E1MarmmMsgfn3 = E1MarmmMsgfn3,
                            E1MarmmMeinh3 = E1MarmmMeinh3,
                            E1MarmmUmrez3 = E1MarmmUmrez3,
                            E1MarmmUmren3 = E1MarmmUmren3,

                            //E1Marmm4]\\
                            E1MarmmMsgfn4 = E1MarmmMsgfn4,
                            E1MarmmMeinh4 = E1MarmmMeinh4,
                            E1MarmmUmren4 = E1MarmmUmren4,
                            E1MarmmUmrez4 = E1MarmmUmrez4,
                            E1MarmmNumtp4 = E1MarmmNumtp4,
                            E1MarmmVolume4 = E1MarmmVolume4,
                            E1MarmmVoleh4 = E1MarmmVoleh4,
                            E1MarmmBrgew4 = E1MarmmBrgew4,
                            E1MarmmGewei = E1MarmmGewei
                        }
                    }
                }
            };

            _classUnderTest = new MaterialMasterMessageMap();
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

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].BISMT, E1MaramBismt);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].MATKL, E1MaramMatkl);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].PRDHA, E1MaramPrdha);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].GROES, E1MaramGroes);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].WERKS, E1MarcmWerks);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].MSGFN, E1MarcmMsgfn);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].PRCTR, E1MarcmPrctr);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].EKGRP, E1MarcmEkgrp);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].MAABC, E1MarcmMaabc);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].DISPR, E1MarcmDispr);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].BSTRF, E1MarcmBstrf);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARCM[0].DZEIT, E1MarcmDzeit);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MAKTM[0].MAKTX, E1MaktmMaktx);

            //Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].POSEX, E1CucfgPosex0); /* Not used but leave comment in */
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].CONFIG_ID, E1CucfgConfigId0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].ROOT_ID, E1CucfgRootId0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUINS[0].INST_ID, E1CuinsInstId0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUINS[0].OBJ_TYPE, E1CuinsObjType0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUINS[0].CLASS_TYPE, E1CuinsClassType0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUINS[0].OBJ_KEY, E1CuinsObjKey0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[0].INST_ID, E1CuvalInstId0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[0].CHARC, E1CuvalCharc0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[0].VALUE, E1CuvalValue0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[1].INST_ID, E1CuvalInstId1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[1].CHARC, E1CuvalCharc1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[1].VALUE, E1CuvalValue1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[2].INST_ID, E1CuvalInstId2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[2].CHARC, E1CuvalCharc2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[2].VALUE, E1CuvalValue2);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[3].INST_ID, E1CuvalInstId3);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[3].CHARC, E1CuvalCharc3);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[0].E1CUVAL[3].VALUE, E1CuvalValue3);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].POSEX, E1CucfgPosex1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].CONFIG_ID, E1CucfgConfigId1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].ROOT_ID, E1CucfgRootId1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUINS[0].INST_ID, E1CuinsInstId1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUINS[0].OBJ_TYPE, E1CuinsObjType1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUINS[0].CLASS_TYPE, E1CuinsClassType1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUINS[0].OBJ_KEY, E1CuinsObjKey1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[0].INST_ID, E1CuvalInstId4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[0].CHARC, E1CuvalCharc4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[0].VALUE, E1CuvalValue4);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[1].INST_ID, E1CuvalInstId5);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[1].CHARC, E1CuvalCharc5);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[1].VALUE, E1CuvalValue5);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[2].INST_ID, E1CuvalInstId6);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[2].CHARC, E1CuvalCharc6);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[2].VALUE, E1CuvalValue6);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[3].INST_ID, E1CuvalInstId7);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[3].CHARC, E1CuvalCharc7);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1CUCFG[1].E1CUVAL[3].VALUE, E1CuvalValue7);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[0].MSGFN, E1MvkemMsgfn0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[0].VKORG, E1MvkemVkorg0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[0].PRODH, E1MvkemProdh0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[0].PRAT1, E1MvkemPrat10);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[0].PRAT4, E1MvkemPrat40);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[1].MSGFN, E1MvkemMsgfn1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[1].VKORG, E1MvkemVkorg1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[1].PRODH, E1MvkemProdh1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[1].PRAT1, E1MvkemPrat11);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MVKEM[1].PRAT4, E1MvkemPrat41);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].MSGFN, E1MtxhmMsgfn0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].TDOBJECT, E1MtxhmTdobject0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].TDID, E1MtxhmTdid0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].TDSPRAS, E1MtxhmTdspras0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].TDTEXTTYPE, E1MtxhmTdtexttype0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].TDNAME, E1MtxhmTdname0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].E1MTXLM[0].MSGFN, E1MtxlmMsgfn0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].E1MTXLM[0].TDFORMAT, E1MtxlmTdformat0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[0].E1MTXLM[0].TDLINE, E1MtxlmTdline0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].MSGFN, E1MtxhmMsgfn1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].TDOBJECT, E1MtxhmTdobject1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].TDID, E1MtxhmTdid1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].TDSPRAS, E1MtxhmTdspras1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].TDTEXTTYPE, E1MtxhmTdtexttype1);
            //Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].TDNAME, E1MtxhmTdname1); /*not used but leave comment in*/

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].E1MTXLM[0].MSGFN, E1MtxlmMsgfn1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].E1MTXLM[0].TDFORMAT, E1MtxlmTdformat1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[1].E1MTXLM[0].TDLINE, E1MtxlmTdline1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].MSGFN, E1MtxhmMsgfn2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].TDOBJECT, E1MtxhmTdobject2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].TDID, E1MtxhmTdid2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].TDSPRAS, E1MtxhmTdspras2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].TDTEXTTYPE, E1MtxhmTdtexttype2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].TDNAME, E1MtxhmTdname2);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].E1MTXLM[0].MSGFN, E1MtxlmMsgfn2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].E1MTXLM[0].TDFORMAT, E1MtxlmTdformat2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MTXHM[2].E1MTXLM[0].TDLINE, E1MtxlmTdline2);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].MSGFN, E1MlgnmMsgfn);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].LGNUM, E1MlgnmLgnum);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].LGBKZ, E1MlgnmLgbkz);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].LHMG1, E1MlgnmLhmg1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].E1MLGTM[0].KOBER, E1MlgtmKober);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MLGNM[0].E1MLGTM[0].NSMNG, E1MlgtmNsmng);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MBEWM[0].MSGFN, E1MbewmMsgfn);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MBEWM[0].BWKEY, E1MbewmBwkey);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZSAN,  Z1MaramZzsan);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZQULN, Z1MaramZzquln);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZPCSN, Z1MaramZzpcsn);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZDM1N, Z1MaramZzdm1N);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZDM2N, Z1MaramZzdm2N);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZDM5N, Z1MaramZzdm5N);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZCOL1, Z1MaramZzcol1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZPRFN, Z1MaramZzprfn);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZSTOL, Z1MaramZzstol);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZROLL, Z1MaramZzroll);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZMTWC, Z1MaramZzmtwc);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].Z1MARAM[0].ZZPKDM, Z1MaramZzpkdm);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[0].MSGFN, E1MarmmMsgfn0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[0].MEINH, E1MarmmMeinh0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[0].UMREZ, E1MarmmUmrez0);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[0].UMREN, E1MarmmUmren0);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[1].MSGFN, E1MarmmMsgfn1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[1].MEINH, E1MarmmMeinh1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[1].UMREZ, E1MarmmUmrez1);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[1].UMREN, E1MarmmUmren1);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[2].MSGFN, E1MarmmMsgfn2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[2].MEINH, E1MarmmMeinh2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[2].UMREZ, E1MarmmUmrez2);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[2].UMREN, E1MarmmUmren2);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[3].MSGFN, E1MarmmMsgfn3);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[3].MEINH, E1MarmmMeinh3);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[3].UMREZ, E1MarmmUmrez3);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[3].UMREN, E1MarmmUmren3);

            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].MSGFN, E1MarmmMsgfn4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].MEINH, E1MarmmMeinh4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].UMREZ, E1MarmmUmrez4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].UMREN, E1MarmmUmren4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].NUMTP, E1MarmmNumtp4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].VOLUM, E1MarmmVolume4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].VOLEH, E1MarmmVoleh4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].BRGEW, E1MarmmBrgew4);
            Assert.AreEqual(_soapMessage.IDOC.E1MARAM[0].E1MARMM[4].GEWEI, E1MarmmGewei);
        }
    }
}
