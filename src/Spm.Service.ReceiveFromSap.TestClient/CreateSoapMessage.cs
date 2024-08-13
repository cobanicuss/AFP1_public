using System;
using Spm.Service.ReceiveFromSap.TestClient.SpmWebServiceProxy;

namespace Spm.Service.ReceiveFromSap.TestClient
{
    public class CreateSoapMessage
    {
        public static SYSTAT01_PRODUCTACHIEVEMENTRESPONSE ProductAchievement(string number)
        {
            var systat01 = new SYSTAT01_PRODUCTACHIEVEMENTRESPONSE
            {
                IDOC = new SYSTAT01_PRODUCTACHIEVEMENTRESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_PRODUCTACHIEVEMENTRESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_PRODUCTACHIEVEMENTRESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_TESTCERTIFICATERESPONSE TestCertificate(string number)
        {
            var systat01 = new SYSTAT01_TESTCERTIFICATERESPONSE
            {
                IDOC = new SYSTAT01_TESTCERTIFICATERESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_TESTCERTIFICATERESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_TESTCERTIFICATERESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_PURCHASEORDERCREATERESPONSE PurchaseOrderCreate(string number)
        {
            var systat01 = new SYSTAT01_PURCHASEORDERCREATERESPONSE
            {
                IDOC = new SYSTAT01_PURCHASEORDERCREATERESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_PURCHASEORDERCREATERESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_PURCHASEORDERCREATERESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_PURCHASEORDERCHANGERESPONSE PurchaseOrderChange(string number)
        {
            var systat01 = new SYSTAT01_PURCHASEORDERCHANGERESPONSE
            {
                IDOC = new SYSTAT01_PURCHASEORDERCHANGERESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_PURCHASEORDERCHANGERESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_PURCHASEORDERCHANGERESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE ProductionOrderStatus(string sagaReferenceId)
        {
            var systat01 = new SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE
            {
                IDOC = new SYSTAT01_PRODUCTIONORDERSTATUSRESPONSEIDOC()
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_PRODUCTIONORDERSTATUSRESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = sagaReferenceId,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_PRODUCTIONORDERSTATUSRESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_GOODSRECEIPTRESPONSE GoodsReceipt(string number)
        {
            var systat01 = new SYSTAT01_GOODSRECEIPTRESPONSE
            {
                IDOC = new SYSTAT01_GOODSRECEIPTRESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_GOODSRECEIPTRESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I",


                    },
                    EDI_DC40 = new SYSTAT01_GOODSRECEIPTRESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_GENERALLEDGERRESPONSE GeneralLedger(string number)
        {
            var systat01 = new SYSTAT01_GENERALLEDGERRESPONSE
            {
                IDOC = new SYSTAT01_GENERALLEDGERRESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_GENERALLEDGERRESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_GENERALLEDGERRESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static SYSTAT01_MATERIALMASTERRESPONSE MaterialMaster(string number)
        {
            var systat01 = new SYSTAT01_MATERIALMASTERRESPONSE
            {
                IDOC = new SYSTAT01_MATERIALMASTERRESPONSEIDOC
                {
                    BEGIN = "1",
                    E1STATS = new SYSTAT01_MATERIALMASTERRESPONSEIDOCE1STATS
                    {
                        SEGMENT = "1",
                        DOCNUM = number,
                        LOGDAT = "20150724",
                        LOGTIM = "135318",
                        STATUS = "41",
                        UNAME = "SAP",
                        REPID = "SAP",
                        STACOD = "00",
                        STATXT = "DocumentreceivedbyBSLDistribution",
                        STATYP = "I"
                    },
                    EDI_DC40 = new SYSTAT01_MATERIALMASTERRESPONSEIDOCEDI_DC40
                    {
                        SEGMENT = "1",
                        TABNAM = "EDI_DC40",
                        DIRECT = "2",
                        IDOCTYP = "SYSTAT01",
                        MESTYP = "STATUS",
                        SNDPOR = "BSD_SAP",
                        SNDPRT = "LS",
                        SNDPRN = "BSLDistribution",
                        RCVPOR = "ORRSYSD",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD"
                    }
                }
            };
            return systat01;
        }

        public static ZOBTC01 TestCertificate()
        {
            var zobtc01 = new ZOBTC01
            {
                IDOC = new ZOBTC01IDOC
                {
                    EDI_DC40 = new ZOBTC01IDOCEDI_DC40
                    {
                        DOCNUM = Guid.NewGuid().ToString()
                    },
                    Z1TCLIN = new[]
                    {   new ZOBTC01IDOCZ1TCLIN 
                        {
                            MATNR = "000000000000222831",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567891",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                new ZOBTC01IDOCZ1TCLINZ1TCEML
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        }, 
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000223756",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567892",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        },
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000222831",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567893",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        },
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000223756",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567894",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        },
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000222832",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567895",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        },
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000222834",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567896",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        },
                        new ZOBTC01IDOCZ1TCLIN
                        {
                            MATNR = "000000000000223555",
                            VBELV = "VBELV",
                            VBELN = "VBELN",
                            NAME1 = "NAME1",
                            KUNNR = "KUNNR",
                            HEAT = "1234567897",
                            WADAT_IST = "WADAT",
                            SHIPTO = "SHIPTO",
                            PURCHASEORDER = "PURCHASEORDER",
                            Z1TCEML = new[]
                            {
                                 new ZOBTC01IDOCZ1TCLINZ1TCEML  
                                {
                                    EMAIL = "me.me@me.com"
                                } 
                            }
                        }
                    }
                }
            };

            return zobtc01;
        }

        public static ZMATMAS5 MaterialMasterUpdate()
        {
            var zmatmas5 = new ZMATMAS5
            {
                IDOC = new ZMATMAS5IDOC
                {
                    EDI_DC40 = new ZMATMAS5IDOCEDI_DC40
                    {
                        TABNAM = "EDI_DC40",
                        MANDT = "101",
                        DOCNUM = "0000000001692899",
                        DOCREL = "701",
                        STATUS = "30",
                        DIRECT = ZMATMAS5IDOCEDI_DC40DIRECT.Item1,
                        OUTMOD = "2",
                        IDOCTYP = "MATMAS05",
                        CIMTYP = "ZMATMAS5",
                        MESTYP = "MATMAS",
                        SNDPOR = "SAPECD",
                        SNDPRT = "LS",
                        SNDPRN = "ECD101",
                        RCVPOR = "SAPXID",
                        RCVPRT = "LS",
                        RCVPRN = "ORRSYSD",
                        CREDAT = "20160511",
                        CRETIM = "143956",
                        SERIAL = "20160511143955"
                    },
                    E1MARAM = new []
                    {
                        new ZMATMAS5IDOCE1MARAM
                        {
                            MATNR = "000000000000214455",
                            BISMT = "MS00117",
                            EAN11 = "9350345056479",
                            Z1MARAM = new []
                            {
                                new ZMATMAS5IDOCE1MARAMZ1MARAM
                                {
                                    ZZREJT = "000000000000214370",
                                    ZZREJTEAN = "9350345056004"
                                }
                            },
                            E1MAKTM = new []
                            {
                                new ZMATMAS5IDOCE1MARAME1MAKTM
                                {

                                    SPRAS = "E",
                                    MAKTX = "Test for MI 6th May",
                                    SPRAS_ISO = "EN"
                                },
                                new ZMATMAS5IDOCE1MARAME1MAKTM
                                {
                                    SPRAS = "S",
                                    MAKTX = "RHS-050-050-02.5-C350-P-08000",
                                    SPRAS_ISO = "Z2"
                                }
                            }
                        }
                    }
                }
            };

            return zmatmas5;
        }
    }
}