using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = Constants.PayloadNameSpace, IsNullable = false)]
    public partial class ZOBTC01
    {
        private ZOBTCZOBTC01 iDOCField;

        public ZOBTCZOBTC01 IDOC
        {
            get
            {
                return this.iDOCField;
            }
            set
            {
                this.iDOCField = value;
            }
        }
    }

    [XmlType(AnonymousType = true)]
    public partial class ZOBTCZOBTC01
    {
        private EDI_DC40ZOBTCZOBTC01 eDI_DC40Field;
        private ZOBTC01Z1TCLIN[] z1TCLINField;
        private string bEGINField;
        public ZOBTCZOBTC01()
        {
            this.bEGINField = "1";
        }

        public EDI_DC40ZOBTCZOBTC01 EDI_DC40
        {
            get
            {
                return this.eDI_DC40Field;
            }
            set
            {
                this.eDI_DC40Field = value;
            }
        }

        [XmlElement("Z1TCLIN")]
        public ZOBTC01Z1TCLIN[] Z1TCLIN
        {
            get
            {
                return this.z1TCLINField;
            }
            set
            {
                this.z1TCLINField = value;
            }
        }

        [XmlAttribute()]
        public string BEGIN
        {
            get
            {
                return this.bEGINField;
            }
            set
            {
                this.bEGINField = value;
            }
        }
    }

    [XmlType(AnonymousType = true)]
    public partial class ZOBTC01Z1TCEML
    {
        private string eMAILField;
        private string sEGMENTField;
        public ZOBTC01Z1TCEML()
        {
            this.sEGMENTField = "1";
        }

        public string EMAIL
        {
            get
            {
                return this.eMAILField;
            }
            set
            {
                this.eMAILField = value;
            }
        }

        [XmlAttribute()]
        public string SEGMENT
        {
            get
            {
                return this.sEGMENTField;
            }
            set
            {
                this.sEGMENTField = value;
            }
        }
    }

    [XmlType(AnonymousType = true)]
    public partial class EDI_DC40ZOBTCZOBTC01
    {
        private string tABNAMField;
        private string mANDTField;
        private string dOCNUMField;
        private string dOCRELField;
        private string sTATUSField;
        private EDI_DC40ZOBTCZOBTC01DIRECT dIRECTField;
        private string oUTMODField;
        private string eXPRSSField;
        private string tESTField;
        private string iDOCTYPField;
        private string cIMTYPField;
        private string mESTYPField;
        private string mESCODField;
        private string mESFCTField;
        private string sTDField;
        private string sTDVRSField;
        private string sTDMESField;
        private string sNDPORField;
        private string sNDPRTField;
        private string sNDPFCField;
        private string sNDPRNField;
        private string sNDSADField;
        private string sNDLADField;
        private string rCVPORField;
        private string rCVPRTField;
        private string rCVPFCField;
        private string rCVPRNField;
        private string rCVSADField;
        private string rCVLADField;
        private string cREDATField;
        private string cRETIMField;
        private string rEFINTField;
        private string rEFGRPField;
        private string rEFMESField;
        private string aRCKEYField;
        private string sERIALField;
        private string sEGMENTField;
        public EDI_DC40ZOBTCZOBTC01()
        {
            this.tABNAMField = "EDI_DC40";
            this.iDOCTYPField = "ZOBTC01";
            this.mESTYPField = "ZOBTC";
            this.sEGMENTField = "1";
        }

        
        public string TABNAM
        {
            get
            {
                return this.tABNAMField;
            }
            set
            {
                this.tABNAMField = value;
            }
        }

        
        public string MANDT
        {
            get
            {
                return this.mANDTField;
            }
            set
            {
                this.mANDTField = value;
            }
        }

        
        public string DOCNUM
        {
            get
            {
                return this.dOCNUMField;
            }
            set
            {
                this.dOCNUMField = value;
            }
        }

        
        public string DOCREL
        {
            get
            {
                return this.dOCRELField;
            }
            set
            {
                this.dOCRELField = value;
            }
        }

        
        public string STATUS
        {
            get
            {
                return this.sTATUSField;
            }
            set
            {
                this.sTATUSField = value;
            }
        }

        
        public EDI_DC40ZOBTCZOBTC01DIRECT DIRECT
        {
            get
            {
                return this.dIRECTField;
            }
            set
            {
                this.dIRECTField = value;
            }
        }

        
        public string OUTMOD
        {
            get
            {
                return this.oUTMODField;
            }
            set
            {
                this.oUTMODField = value;
            }
        }

        
        public string EXPRSS
        {
            get
            {
                return this.eXPRSSField;
            }
            set
            {
                this.eXPRSSField = value;
            }
        }

        
        public string TEST
        {
            get
            {
                return this.tESTField;
            }
            set
            {
                this.tESTField = value;
            }
        }

        
        public string IDOCTYP
        {
            get
            {
                return this.iDOCTYPField;
            }
            set
            {
                this.iDOCTYPField = value;
            }
        }

        
        public string CIMTYP
        {
            get
            {
                return this.cIMTYPField;
            }
            set
            {
                this.cIMTYPField = value;
            }
        }

        
        public string MESTYP
        {
            get
            {
                return this.mESTYPField;
            }
            set
            {
                this.mESTYPField = value;
            }
        }

        
        public string MESCOD
        {
            get
            {
                return this.mESCODField;
            }
            set
            {
                this.mESCODField = value;
            }
        }

        
        public string MESFCT
        {
            get
            {
                return this.mESFCTField;
            }
            set
            {
                this.mESFCTField = value;
            }
        }

        
        public string STD
        {
            get
            {
                return this.sTDField;
            }
            set
            {
                this.sTDField = value;
            }
        }

        
        public string STDVRS
        {
            get
            {
                return this.sTDVRSField;
            }
            set
            {
                this.sTDVRSField = value;
            }
        }

        
        public string STDMES
        {
            get
            {
                return this.sTDMESField;
            }
            set
            {
                this.sTDMESField = value;
            }
        }

        
        public string SNDPOR
        {
            get
            {
                return this.sNDPORField;
            }
            set
            {
                this.sNDPORField = value;
            }
        }

        
        public string SNDPRT
        {
            get
            {
                return this.sNDPRTField;
            }
            set
            {
                this.sNDPRTField = value;
            }
        }

        
        public string SNDPFC
        {
            get
            {
                return this.sNDPFCField;
            }
            set
            {
                this.sNDPFCField = value;
            }
        }

        
        public string SNDPRN
        {
            get
            {
                return this.sNDPRNField;
            }
            set
            {
                this.sNDPRNField = value;
            }
        }

        
        public string SNDSAD
        {
            get
            {
                return this.sNDSADField;
            }
            set
            {
                this.sNDSADField = value;
            }
        }

        
        public string SNDLAD
        {
            get
            {
                return this.sNDLADField;
            }
            set
            {
                this.sNDLADField = value;
            }
        }

        
        public string RCVPOR
        {
            get
            {
                return this.rCVPORField;
            }
            set
            {
                this.rCVPORField = value;
            }
        }

        
        public string RCVPRT
        {
            get
            {
                return this.rCVPRTField;
            }
            set
            {
                this.rCVPRTField = value;
            }
        }

        
        public string RCVPFC
        {
            get
            {
                return this.rCVPFCField;
            }
            set
            {
                this.rCVPFCField = value;
            }
        }

        
        public string RCVPRN
        {
            get
            {
                return this.rCVPRNField;
            }
            set
            {
                this.rCVPRNField = value;
            }
        }

        
        public string RCVSAD
        {
            get
            {
                return this.rCVSADField;
            }
            set
            {
                this.rCVSADField = value;
            }
        }

        
        public string RCVLAD
        {
            get
            {
                return this.rCVLADField;
            }
            set
            {
                this.rCVLADField = value;
            }
        }

        
        public string CREDAT
        {
            get
            {
                return this.cREDATField;
            }
            set
            {
                this.cREDATField = value;
            }
        }

        
        public string CRETIM
        {
            get
            {
                return this.cRETIMField;
            }
            set
            {
                this.cRETIMField = value;
            }
        }

        
        public string REFINT
        {
            get
            {
                return this.rEFINTField;
            }
            set
            {
                this.rEFINTField = value;
            }
        }

        
        public string REFGRP
        {
            get
            {
                return this.rEFGRPField;
            }
            set
            {
                this.rEFGRPField = value;
            }
        }

        
        public string REFMES
        {
            get
            {
                return this.rEFMESField;
            }
            set
            {
                this.rEFMESField = value;
            }
        }

        
        public string ARCKEY
        {
            get
            {
                return this.aRCKEYField;
            }
            set
            {
                this.aRCKEYField = value;
            }
        }

        
        public string SERIAL
        {
            get
            {
                return this.sERIALField;
            }
            set
            {
                this.sERIALField = value;
            }
        }

        [XmlAttribute()]
        public string SEGMENT
        {
            get
            {
                return this.sEGMENTField;
            }
            set
            {
                this.sEGMENTField = value;
            }
        }
    }

    [XmlType(AnonymousType = true)]
    public enum EDI_DC40ZOBTCZOBTC01DIRECT
    {

        [XmlEnum("1")]
        Item1,

        [XmlEnum("2")]
        Item2,
    }
    
    [XmlType(AnonymousType = true)]
    public partial class ZOBTC01Z1TCLIN
    {
        private string vBELVField;
        private string vBELNField;
        private string kUNNRField;
        private string nAME1Field;
        private string hEATField;
        private string mATNRField;
        private string wADAT_ISTField;
        private string sHIPTOField;
        private string pURCHASEORDERField;
        private ZOBTC01Z1TCEML[] z1TCEMLField;
        private string sEGMENTField;
        public ZOBTC01Z1TCLIN()
        {
            this.sEGMENTField = "1";
        }

        
        public string VBELV
        {
            get
            {
                return this.vBELVField;
            }
            set
            {
                this.vBELVField = value;
            }
        }

        
        public string VBELN
        {
            get
            {
                return this.vBELNField;
            }
            set
            {
                this.vBELNField = value;
            }
        }

        
        public string KUNNR
        {
            get
            {
                return this.kUNNRField;
            }
            set
            {
                this.kUNNRField = value;
            }
        }

        
        public string NAME1
        {
            get
            {
                return this.nAME1Field;
            }
            set
            {
                this.nAME1Field = value;
            }
        }

        
        public string HEAT
        {
            get
            {
                return this.hEATField;
            }
            set
            {
                this.hEATField = value;
            }
        }

        
        public string MATNR
        {
            get
            {
                return this.mATNRField;
            }
            set
            {
                this.mATNRField = value;
            }
        }

        
        public string WADAT_IST
        {
            get
            {
                return this.wADAT_ISTField;
            }
            set
            {
                this.wADAT_ISTField = value;
            }
        }

        
        public string SHIPTO
        {
            get
            {
                return this.sHIPTOField;
            }
            set
            {
                this.sHIPTOField = value;
            }
        }

        
        public string PURCHASEORDER
        {
            get
            {
                return this.pURCHASEORDERField;
            }
            set
            {
                this.pURCHASEORDERField = value;
            }
        }

        [XmlElement(IsNullable = false)]
        public ZOBTC01Z1TCEML[] Z1TCEML
        {
            get
            {
                return this.z1TCEMLField;
            }
            set
            {
                this.z1TCEMLField = value;
            }
        }

        [XmlAttribute()]
        public string SEGMENT
        {
            get
            {
                return this.sEGMENTField;
            }
            set
            {
                this.sEGMENTField = value;
            }
        }
    }
}