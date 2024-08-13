using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = Constants.PayloadNameSpace, IsNullable = false)]
    public partial class ZMATMAS5
    {
        private MATMASMATMAS05ZMATMAS5 iDOCField;

        public MATMASMATMAS05ZMATMAS5 IDOC
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
    public partial class MATMASMATMAS05ZMATMAS5
    {
        private EDI_DC40MATMASMATMAS05ZMATMAS5 eDI_DC40Field;
        private ZMATMAS5E1MARAM[] e1MARAMField;
        private ZMATMAS5E1UPSLINK e1UPSLINKField;
        private string bEGINField;
        public MATMASMATMAS05ZMATMAS5()
        {
            this.bEGINField = "1";
        }

        public EDI_DC40MATMASMATMAS05ZMATMAS5 EDI_DC40
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
        [XmlElement("E1MARAM")]
        public ZMATMAS5E1MARAM[] E1MARAM
        {
            get
            {
                return this.e1MARAMField;
            }
            set
            {
                this.e1MARAMField = value;
            }
        }

        public ZMATMAS5E1UPSLINK E1UPSLINK
        {
            get
            {
                return this.e1UPSLINKField;
            }
            set
            {
                this.e1UPSLINKField = value;
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
    public partial class ZMATMAS5E1MVEGM
    {
        private string mSGFNField;
        private string eRTAGField;
        private string vBWRTField;
        private string kOVBWField;
        private string kZEXIField;
        private string aNTEIField;
        private string sEGMENTField;
        public ZMATMAS5E1MVEGM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string ERTAG
        {
            get
            {
                return this.eRTAGField;
            }
            set
            {
                this.eRTAGField = value;
            }
        }
        
        public string VBWRT
        {
            get
            {
                return this.vBWRTField;
            }
            set
            {
                this.vBWRTField = value;
            }
        }
        
        public string KOVBW
        {
            get
            {
                return this.kOVBWField;
            }
            set
            {
                this.kOVBWField = value;
            }
        }
        
        public string KZEXI
        {
            get
            {
                return this.kZEXIField;
            }
            set
            {
                this.kZEXIField = value;
            }
        }
        
        public string ANTEI
        {
            get
            {
                return this.aNTEIField;
            }
            set
            {
                this.aNTEIField = value;
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
    public partial class ZMATMAS5E1MARC1
    {
        private string nFMAT_EXTERNALField;
        private string nFMAT_VERSIONField;
        private string nFMAT_GUIDField;
        private string sTDPD_EXTERNALField;
        private string sTDPD_VERSIONField;
        private string sTDPD_GUIDField;
        private string vRBMT_EXTERNALField;
        private string vRBMT_VERSIONField;
        private string vRBMT_GUIDField;
        private string sEGMENTField;
        public ZMATMAS5E1MARC1()
        {
            this.sEGMENTField = "1";
        }
        
        public string NFMAT_EXTERNAL
        {
            get
            {
                return this.nFMAT_EXTERNALField;
            }
            set
            {
                this.nFMAT_EXTERNALField = value;
            }
        }
        
        public string NFMAT_VERSION
        {
            get
            {
                return this.nFMAT_VERSIONField;
            }
            set
            {
                this.nFMAT_VERSIONField = value;
            }
        }
        
        public string NFMAT_GUID
        {
            get
            {
                return this.nFMAT_GUIDField;
            }
            set
            {
                this.nFMAT_GUIDField = value;
            }
        }
        
        public string STDPD_EXTERNAL
        {
            get
            {
                return this.sTDPD_EXTERNALField;
            }
            set
            {
                this.sTDPD_EXTERNALField = value;
            }
        }
        
        public string STDPD_VERSION
        {
            get
            {
                return this.sTDPD_VERSIONField;
            }
            set
            {
                this.sTDPD_VERSIONField = value;
            }
        }
        
        public string STDPD_GUID
        {
            get
            {
                return this.sTDPD_GUIDField;
            }
            set
            {
                this.sTDPD_GUIDField = value;
            }
        }
        
        public string VRBMT_EXTERNAL
        {
            get
            {
                return this.vRBMT_EXTERNALField;
            }
            set
            {
                this.vRBMT_EXTERNALField = value;
            }
        }
        
        public string VRBMT_VERSION
        {
            get
            {
                return this.vRBMT_VERSIONField;
            }
            set
            {
                this.vRBMT_VERSIONField = value;
            }
        }
        
        public string VRBMT_GUID
        {
            get
            {
                return this.vRBMT_GUIDField;
            }
            set
            {
                this.vRBMT_GUIDField = value;
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
    public partial class ZMATMAS5E1MPRWM
    {
        private string mSGFNField;
        private string eRTAGField;
        private string pRWRTField;
        private string kOPRWField;
        private string sAIINField;
        private string fIXKZField;
        private string eXPRWField;
        private string aNTEIField;
        private string sEGMENTField;
        public ZMATMAS5E1MPRWM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string ERTAG
        {
            get
            {
                return this.eRTAGField;
            }
            set
            {
                this.eRTAGField = value;
            }
        }
        
        public string PRWRT
        {
            get
            {
                return this.pRWRTField;
            }
            set
            {
                this.pRWRTField = value;
            }
        }
        
        public string KOPRW
        {
            get
            {
                return this.kOPRWField;
            }
            set
            {
                this.kOPRWField = value;
            }
        }
        
        public string SAIIN
        {
            get
            {
                return this.sAIINField;
            }
            set
            {
                this.sAIINField = value;
            }
        }
        
        public string FIXKZ
        {
            get
            {
                return this.fIXKZField;
            }
            set
            {
                this.fIXKZField = value;
            }
        }
        
        public string EXPRW
        {
            get
            {
                return this.eXPRWField;
            }
            set
            {
                this.eXPRWField = value;
            }
        }
        
        public string ANTEI
        {
            get
            {
                return this.aNTEIField;
            }
            set
            {
                this.aNTEIField = value;
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
    public partial class ZMATMAS5E1MARA1
    {
        private string mATNR_EXTERNALField;
        private string mATNR_VERSIONField;
        private string mATNR_GUIDField;
        private string bMATN_EXTERNALField;
        private string bMATN_VERSIONField;
        private string bMATN_GUIDField;
        private string sTOFF_EXTERNALField;
        private string sTOFF_VERSIONField;
        private string sTOFF_GUIDField;
        private string hUTYP_DFLTField;
        private string pILFERABLEField;
        private string wHSTCField;
        private string wHMATGRField;
        private string hNDLCODEField;
        private string hAZMATField;
        private string hUTYPField;
        private string tARE_VARField;
        private string mAXCField;
        private string mAXC_TOLField;
        private string mAXLField;
        private string mAXBField;
        private string mAXHField;
        private string mAXDIM_UOMField;
        private string hERKLField;
        private string mFRGRField;
        private string qQTIMEField;
        private string qQTIMEUOMField;
        private string qGRPField;
        private string sERIALField;
        private string pS_SMARTFORMField;
        private string cWQPROCField;
        private string cWQTOLGRField;
        private string aDPROFField;
        private string sEGMENTField;
        public ZMATMAS5E1MARA1()
        {
            this.sEGMENTField = "1";
        }
        
        public string MATNR_EXTERNAL
        {
            get
            {
                return this.mATNR_EXTERNALField;
            }
            set
            {
                this.mATNR_EXTERNALField = value;
            }
        }
        
        public string MATNR_VERSION
        {
            get
            {
                return this.mATNR_VERSIONField;
            }
            set
            {
                this.mATNR_VERSIONField = value;
            }
        }
        
        public string MATNR_GUID
        {
            get
            {
                return this.mATNR_GUIDField;
            }
            set
            {
                this.mATNR_GUIDField = value;
            }
        }
        
        public string BMATN_EXTERNAL
        {
            get
            {
                return this.bMATN_EXTERNALField;
            }
            set
            {
                this.bMATN_EXTERNALField = value;
            }
        }
        
        public string BMATN_VERSION
        {
            get
            {
                return this.bMATN_VERSIONField;
            }
            set
            {
                this.bMATN_VERSIONField = value;
            }
        }
        
        public string BMATN_GUID
        {
            get
            {
                return this.bMATN_GUIDField;
            }
            set
            {
                this.bMATN_GUIDField = value;
            }
        }
        
        public string STOFF_EXTERNAL
        {
            get
            {
                return this.sTOFF_EXTERNALField;
            }
            set
            {
                this.sTOFF_EXTERNALField = value;
            }
        }
        
        public string STOFF_VERSION
        {
            get
            {
                return this.sTOFF_VERSIONField;
            }
            set
            {
                this.sTOFF_VERSIONField = value;
            }
        }
        
        public string STOFF_GUID
        {
            get
            {
                return this.sTOFF_GUIDField;
            }
            set
            {
                this.sTOFF_GUIDField = value;
            }
        }
        
        public string HUTYP_DFLT
        {
            get
            {
                return this.hUTYP_DFLTField;
            }
            set
            {
                this.hUTYP_DFLTField = value;
            }
        }
        
        public string PILFERABLE
        {
            get
            {
                return this.pILFERABLEField;
            }
            set
            {
                this.pILFERABLEField = value;
            }
        }
        
        public string WHSTC
        {
            get
            {
                return this.wHSTCField;
            }
            set
            {
                this.wHSTCField = value;
            }
        }
        
        public string WHMATGR
        {
            get
            {
                return this.wHMATGRField;
            }
            set
            {
                this.wHMATGRField = value;
            }
        }
        
        public string HNDLCODE
        {
            get
            {
                return this.hNDLCODEField;
            }
            set
            {
                this.hNDLCODEField = value;
            }
        }
        
        public string HAZMAT
        {
            get
            {
                return this.hAZMATField;
            }
            set
            {
                this.hAZMATField = value;
            }
        }
        
        public string HUTYP
        {
            get
            {
                return this.hUTYPField;
            }
            set
            {
                this.hUTYPField = value;
            }
        }
        
        public string TARE_VAR
        {
            get
            {
                return this.tARE_VARField;
            }
            set
            {
                this.tARE_VARField = value;
            }
        }
        
        public string MAXC
        {
            get
            {
                return this.mAXCField;
            }
            set
            {
                this.mAXCField = value;
            }
        }
        
        public string MAXC_TOL
        {
            get
            {
                return this.mAXC_TOLField;
            }
            set
            {
                this.mAXC_TOLField = value;
            }
        }
        
        public string MAXL
        {
            get
            {
                return this.mAXLField;
            }
            set
            {
                this.mAXLField = value;
            }
        }
        
        public string MAXB
        {
            get
            {
                return this.mAXBField;
            }
            set
            {
                this.mAXBField = value;
            }
        }
        
        public string MAXH
        {
            get
            {
                return this.mAXHField;
            }
            set
            {
                this.mAXHField = value;
            }
        }
        
        public string MAXDIM_UOM
        {
            get
            {
                return this.mAXDIM_UOMField;
            }
            set
            {
                this.mAXDIM_UOMField = value;
            }
        }
        
        public string HERKL
        {
            get
            {
                return this.hERKLField;
            }
            set
            {
                this.hERKLField = value;
            }
        }
        
        public string MFRGR
        {
            get
            {
                return this.mFRGRField;
            }
            set
            {
                this.mFRGRField = value;
            }
        }
        
        public string QQTIME
        {
            get
            {
                return this.qQTIMEField;
            }
            set
            {
                this.qQTIMEField = value;
            }
        }
        
        public string QQTIMEUOM
        {
            get
            {
                return this.qQTIMEUOMField;
            }
            set
            {
                this.qQTIMEUOMField = value;
            }
        }
        
        public string QGRP
        {
            get
            {
                return this.qGRPField;
            }
            set
            {
                this.qGRPField = value;
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
        
        public string PS_SMARTFORM
        {
            get
            {
                return this.pS_SMARTFORMField;
            }
            set
            {
                this.pS_SMARTFORMField = value;
            }
        }
        
        public string CWQPROC
        {
            get
            {
                return this.cWQPROCField;
            }
            set
            {
                this.cWQPROCField = value;
            }
        }
        
        public string CWQTOLGR
        {
            get
            {
                return this.cWQTOLGRField;
            }
            set
            {
                this.cWQTOLGRField = value;
            }
        }
        
        public string ADPROF
        {
            get
            {
                return this.aDPROFField;
            }
            set
            {
                this.aDPROFField = value;
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
    public partial class ZMATMAS5E1MLGNM
    {
        private string mSGFNField;
        private string lGNUMField;
        private string lVORMField;
        private string lGBKZField;
        private string lTKZEField;
        private string lTKZAField;
        private string lHMG1Field;
        private string lHMG2Field;
        private string lHMG3Field;
        private string lHME1Field;
        private string lHME2Field;
        private string lHME3Field;
        private string lETY1Field;
        private string lETY2Field;
        private string lETY3Field;
        private string lVSMEField;
        private string kZZULField;
        private string bLOCKField;
        private string kZMBFField;
        private string bSSKZField;
        private string mKAPVField;
        private string bEZMEField;
        private string pLKPTField;
        private string vOMEMField;
        private string l2SKRField;
        private ZMATMAS5E1MLGTM[] e1MLGTMField;
        private string sEGMENTField;
        public ZMATMAS5E1MLGNM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string LGNUM
        {
            get
            {
                return this.lGNUMField;
            }
            set
            {
                this.lGNUMField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string LGBKZ
        {
            get
            {
                return this.lGBKZField;
            }
            set
            {
                this.lGBKZField = value;
            }
        }
        
        public string LTKZE
        {
            get
            {
                return this.lTKZEField;
            }
            set
            {
                this.lTKZEField = value;
            }
        }
        
        public string LTKZA
        {
            get
            {
                return this.lTKZAField;
            }
            set
            {
                this.lTKZAField = value;
            }
        }
        
        public string LHMG1
        {
            get
            {
                return this.lHMG1Field;
            }
            set
            {
                this.lHMG1Field = value;
            }
        }
        
        public string LHMG2
        {
            get
            {
                return this.lHMG2Field;
            }
            set
            {
                this.lHMG2Field = value;
            }
        }
        
        public string LHMG3
        {
            get
            {
                return this.lHMG3Field;
            }
            set
            {
                this.lHMG3Field = value;
            }
        }
        
        public string LHME1
        {
            get
            {
                return this.lHME1Field;
            }
            set
            {
                this.lHME1Field = value;
            }
        }
        
        public string LHME2
        {
            get
            {
                return this.lHME2Field;
            }
            set
            {
                this.lHME2Field = value;
            }
        }
        
        public string LHME3
        {
            get
            {
                return this.lHME3Field;
            }
            set
            {
                this.lHME3Field = value;
            }
        }
        
        public string LETY1
        {
            get
            {
                return this.lETY1Field;
            }
            set
            {
                this.lETY1Field = value;
            }
        }
        
        public string LETY2
        {
            get
            {
                return this.lETY2Field;
            }
            set
            {
                this.lETY2Field = value;
            }
        }
        
        public string LETY3
        {
            get
            {
                return this.lETY3Field;
            }
            set
            {
                this.lETY3Field = value;
            }
        }
        
        public string LVSME
        {
            get
            {
                return this.lVSMEField;
            }
            set
            {
                this.lVSMEField = value;
            }
        }
        
        public string KZZUL
        {
            get
            {
                return this.kZZULField;
            }
            set
            {
                this.kZZULField = value;
            }
        }
        
        public string BLOCK
        {
            get
            {
                return this.bLOCKField;
            }
            set
            {
                this.bLOCKField = value;
            }
        }
        
        public string KZMBF
        {
            get
            {
                return this.kZMBFField;
            }
            set
            {
                this.kZMBFField = value;
            }
        }
        
        public string BSSKZ
        {
            get
            {
                return this.bSSKZField;
            }
            set
            {
                this.bSSKZField = value;
            }
        }
        
        public string MKAPV
        {
            get
            {
                return this.mKAPVField;
            }
            set
            {
                this.mKAPVField = value;
            }
        }
        
        public string BEZME
        {
            get
            {
                return this.bEZMEField;
            }
            set
            {
                this.bEZMEField = value;
            }
        }
        
        public string PLKPT
        {
            get
            {
                return this.pLKPTField;
            }
            set
            {
                this.pLKPTField = value;
            }
        }
        
        public string VOMEM
        {
            get
            {
                return this.vOMEMField;
            }
            set
            {
                this.vOMEMField = value;
            }
        }
        
        public string L2SKR
        {
            get
            {
                return this.l2SKRField;
            }
            set
            {
                this.l2SKRField = value;
            }
        }
        [XmlElement("E1MLGTM")]
        public ZMATMAS5E1MLGTM[] E1MLGTM
        {
            get
            {
                return this.e1MLGTMField;
            }
            set
            {
                this.e1MLGTMField = value;
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
    public partial class ZMATMAS5E1MLGTM
    {
        private string mSGFNField;
        private string lGTYPField;
        private string lVORMField;
        private string lGPLAField;
        private string lPMAXField;
        private string lPMINField;
        private string mAMNGField;
        private string nSMNGField;
        private string kOBERField;
        private string rDMNGField;
        private string sEGMENTField;
        public ZMATMAS5E1MLGTM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string LGTYP
        {
            get
            {
                return this.lGTYPField;
            }
            set
            {
                this.lGTYPField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string LGPLA
        {
            get
            {
                return this.lGPLAField;
            }
            set
            {
                this.lGPLAField = value;
            }
        }
        
        public string LPMAX
        {
            get
            {
                return this.lPMAXField;
            }
            set
            {
                this.lPMAXField = value;
            }
        }
        
        public string LPMIN
        {
            get
            {
                return this.lPMINField;
            }
            set
            {
                this.lPMINField = value;
            }
        }
        
        public string MAMNG
        {
            get
            {
                return this.mAMNGField;
            }
            set
            {
                this.mAMNGField = value;
            }
        }
        
        public string NSMNG
        {
            get
            {
                return this.nSMNGField;
            }
            set
            {
                this.nSMNGField = value;
            }
        }
        
        public string KOBER
        {
            get
            {
                return this.kOBERField;
            }
            set
            {
                this.kOBERField = value;
            }
        }
        
        public string RDMNG
        {
            get
            {
                return this.rDMNGField;
            }
            set
            {
                this.rDMNGField = value;
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
    public partial class ZMATMAS5E1MPOPM
    {
        private string mSGFNField;
        private string vERSPField;
        private string pROPRField;
        private string mODAWField;
        private string mODAVField;
        private string kZPARField;
        private string oPGRAField;
        private string kZINIField;
        private string pRMODField;
        private string aLPHAField;
        private string bETA1Field;
        private string gAMMAField;
        private string dELTAField;
        private string ePSILField;
        private string sIGGRField;
        private string pERKZField;
        private string pRDATField;
        private string pERANField;
        private string pERINField;
        private string pERIOField;
        private string pEREXField;
        private string aNZPRField;
        private string fIMONField;
        private string gWERTField;
        private string gWER1Field;
        private string gWER2Field;
        private string vMGWEField;
        private string vMGW1Field;
        private string vMGW2Field;
        private string tWERTField;
        private string vMTWEField;
        private string pRMADField;
        private string vMMADField;
        private string fSUMMField;
        private string vMFSUField;
        private string gEWGRField;
        private string tHKOFField;
        private string aUSNAField;
        private string pROABField;
        private string sEGMENTField;
        public ZMATMAS5E1MPOPM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string VERSP
        {
            get
            {
                return this.vERSPField;
            }
            set
            {
                this.vERSPField = value;
            }
        }
        
        public string PROPR
        {
            get
            {
                return this.pROPRField;
            }
            set
            {
                this.pROPRField = value;
            }
        }
        
        public string MODAW
        {
            get
            {
                return this.mODAWField;
            }
            set
            {
                this.mODAWField = value;
            }
        }
        
        public string MODAV
        {
            get
            {
                return this.mODAVField;
            }
            set
            {
                this.mODAVField = value;
            }
        }
        
        public string KZPAR
        {
            get
            {
                return this.kZPARField;
            }
            set
            {
                this.kZPARField = value;
            }
        }
        
        public string OPGRA
        {
            get
            {
                return this.oPGRAField;
            }
            set
            {
                this.oPGRAField = value;
            }
        }
        
        public string KZINI
        {
            get
            {
                return this.kZINIField;
            }
            set
            {
                this.kZINIField = value;
            }
        }
        
        public string PRMOD
        {
            get
            {
                return this.pRMODField;
            }
            set
            {
                this.pRMODField = value;
            }
        }
        
        public string ALPHA
        {
            get
            {
                return this.aLPHAField;
            }
            set
            {
                this.aLPHAField = value;
            }
        }
        
        public string BETA1
        {
            get
            {
                return this.bETA1Field;
            }
            set
            {
                this.bETA1Field = value;
            }
        }
        
        public string GAMMA
        {
            get
            {
                return this.gAMMAField;
            }
            set
            {
                this.gAMMAField = value;
            }
        }
        
        public string DELTA
        {
            get
            {
                return this.dELTAField;
            }
            set
            {
                this.dELTAField = value;
            }
        }
        
        public string EPSIL
        {
            get
            {
                return this.ePSILField;
            }
            set
            {
                this.ePSILField = value;
            }
        }
        
        public string SIGGR
        {
            get
            {
                return this.sIGGRField;
            }
            set
            {
                this.sIGGRField = value;
            }
        }
        
        public string PERKZ
        {
            get
            {
                return this.pERKZField;
            }
            set
            {
                this.pERKZField = value;
            }
        }
        
        public string PRDAT
        {
            get
            {
                return this.pRDATField;
            }
            set
            {
                this.pRDATField = value;
            }
        }
        
        public string PERAN
        {
            get
            {
                return this.pERANField;
            }
            set
            {
                this.pERANField = value;
            }
        }
        
        public string PERIN
        {
            get
            {
                return this.pERINField;
            }
            set
            {
                this.pERINField = value;
            }
        }
        
        public string PERIO
        {
            get
            {
                return this.pERIOField;
            }
            set
            {
                this.pERIOField = value;
            }
        }
        
        public string PEREX
        {
            get
            {
                return this.pEREXField;
            }
            set
            {
                this.pEREXField = value;
            }
        }
        
        public string ANZPR
        {
            get
            {
                return this.aNZPRField;
            }
            set
            {
                this.aNZPRField = value;
            }
        }
        
        public string FIMON
        {
            get
            {
                return this.fIMONField;
            }
            set
            {
                this.fIMONField = value;
            }
        }
        
        public string GWERT
        {
            get
            {
                return this.gWERTField;
            }
            set
            {
                this.gWERTField = value;
            }
        }
        
        public string GWER1
        {
            get
            {
                return this.gWER1Field;
            }
            set
            {
                this.gWER1Field = value;
            }
        }
        
        public string GWER2
        {
            get
            {
                return this.gWER2Field;
            }
            set
            {
                this.gWER2Field = value;
            }
        }
        
        public string VMGWE
        {
            get
            {
                return this.vMGWEField;
            }
            set
            {
                this.vMGWEField = value;
            }
        }
        
        public string VMGW1
        {
            get
            {
                return this.vMGW1Field;
            }
            set
            {
                this.vMGW1Field = value;
            }
        }
        
        public string VMGW2
        {
            get
            {
                return this.vMGW2Field;
            }
            set
            {
                this.vMGW2Field = value;
            }
        }
        
        public string TWERT
        {
            get
            {
                return this.tWERTField;
            }
            set
            {
                this.tWERTField = value;
            }
        }
        
        public string VMTWE
        {
            get
            {
                return this.vMTWEField;
            }
            set
            {
                this.vMTWEField = value;
            }
        }
        
        public string PRMAD
        {
            get
            {
                return this.pRMADField;
            }
            set
            {
                this.pRMADField = value;
            }
        }
        
        public string VMMAD
        {
            get
            {
                return this.vMMADField;
            }
            set
            {
                this.vMMADField = value;
            }
        }
        
        public string FSUMM
        {
            get
            {
                return this.fSUMMField;
            }
            set
            {
                this.fSUMMField = value;
            }
        }
        
        public string VMFSU
        {
            get
            {
                return this.vMFSUField;
            }
            set
            {
                this.vMFSUField = value;
            }
        }
        
        public string GEWGR
        {
            get
            {
                return this.gEWGRField;
            }
            set
            {
                this.gEWGRField = value;
            }
        }
        
        public string THKOF
        {
            get
            {
                return this.tHKOFField;
            }
            set
            {
                this.tHKOFField = value;
            }
        }
        
        public string AUSNA
        {
            get
            {
                return this.aUSNAField;
            }
            set
            {
                this.aUSNAField = value;
            }
        }
        
        public string PROAB
        {
            get
            {
                return this.pROABField;
            }
            set
            {
                this.pROABField = value;
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
    public partial class ZMATMAS5E1MVEUM
    {
        private string mSGFNField;
        private string eRTAGField;
        private string vBWRTField;
        private string kOVBWField;
        private string kZEXIField;
        private string aNTEIField;
        private string sEGMENTField;
        public ZMATMAS5E1MVEUM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string ERTAG
        {
            get
            {
                return this.eRTAGField;
            }
            set
            {
                this.eRTAGField = value;
            }
        }
        
        public string VBWRT
        {
            get
            {
                return this.vBWRTField;
            }
            set
            {
                this.vBWRTField = value;
            }
        }
        
        public string KOVBW
        {
            get
            {
                return this.kOVBWField;
            }
            set
            {
                this.kOVBWField = value;
            }
        }
        
        public string KZEXI
        {
            get
            {
                return this.kZEXIField;
            }
            set
            {
                this.kZEXIField = value;
            }
        }
        
        public string ANTEI
        {
            get
            {
                return this.aNTEIField;
            }
            set
            {
                this.aNTEIField = value;
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
    public partial class ZMATMAS5E1MLANM
    {
        private string mSGFNField;
        private string aLANDField;
        private string tATY1Field;
        private string tAXM1Field;
        private string tATY2Field;
        private string tAXM2Field;
        private string tATY3Field;
        private string tAXM3Field;
        private string tATY4Field;
        private string tAXM4Field;
        private string tATY5Field;
        private string tAXM5Field;
        private string tATY6Field;
        private string tAXM6Field;
        private string tATY7Field;
        private string tAXM7Field;
        private string tATY8Field;
        private string tAXM8Field;
        private string tATY9Field;
        private string tAXM9Field;
        private string tAXIMField;
        private string sEGMENTField;
        public ZMATMAS5E1MLANM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string ALAND
        {
            get
            {
                return this.aLANDField;
            }
            set
            {
                this.aLANDField = value;
            }
        }
        
        public string TATY1
        {
            get
            {
                return this.tATY1Field;
            }
            set
            {
                this.tATY1Field = value;
            }
        }
        
        public string TAXM1
        {
            get
            {
                return this.tAXM1Field;
            }
            set
            {
                this.tAXM1Field = value;
            }
        }
        
        public string TATY2
        {
            get
            {
                return this.tATY2Field;
            }
            set
            {
                this.tATY2Field = value;
            }
        }
        
        public string TAXM2
        {
            get
            {
                return this.tAXM2Field;
            }
            set
            {
                this.tAXM2Field = value;
            }
        }
        
        public string TATY3
        {
            get
            {
                return this.tATY3Field;
            }
            set
            {
                this.tATY3Field = value;
            }
        }
        
        public string TAXM3
        {
            get
            {
                return this.tAXM3Field;
            }
            set
            {
                this.tAXM3Field = value;
            }
        }
        
        public string TATY4
        {
            get
            {
                return this.tATY4Field;
            }
            set
            {
                this.tATY4Field = value;
            }
        }
        
        public string TAXM4
        {
            get
            {
                return this.tAXM4Field;
            }
            set
            {
                this.tAXM4Field = value;
            }
        }
        
        public string TATY5
        {
            get
            {
                return this.tATY5Field;
            }
            set
            {
                this.tATY5Field = value;
            }
        }
        
        public string TAXM5
        {
            get
            {
                return this.tAXM5Field;
            }
            set
            {
                this.tAXM5Field = value;
            }
        }
        
        public string TATY6
        {
            get
            {
                return this.tATY6Field;
            }
            set
            {
                this.tATY6Field = value;
            }
        }
        
        public string TAXM6
        {
            get
            {
                return this.tAXM6Field;
            }
            set
            {
                this.tAXM6Field = value;
            }
        }
        
        public string TATY7
        {
            get
            {
                return this.tATY7Field;
            }
            set
            {
                this.tATY7Field = value;
            }
        }
        
        public string TAXM7
        {
            get
            {
                return this.tAXM7Field;
            }
            set
            {
                this.tAXM7Field = value;
            }
        }
        
        public string TATY8
        {
            get
            {
                return this.tATY8Field;
            }
            set
            {
                this.tATY8Field = value;
            }
        }
        
        public string TAXM8
        {
            get
            {
                return this.tAXM8Field;
            }
            set
            {
                this.tAXM8Field = value;
            }
        }
        
        public string TATY9
        {
            get
            {
                return this.tATY9Field;
            }
            set
            {
                this.tATY9Field = value;
            }
        }
        
        public string TAXM9
        {
            get
            {
                return this.tAXM9Field;
            }
            set
            {
                this.tAXM9Field = value;
            }
        }
        
        public string TAXIM
        {
            get
            {
                return this.tAXIMField;
            }
            set
            {
                this.tAXIMField = value;
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
    public partial class ZMATMAS5E1UPSLINK
    {
        private string uPSNAMField;
        private string oRGNAMField;
        private string mESTYPField;
        private string oBJIDField;
        private string oBJVALField;
        private string sEGMENTField;
        public ZMATMAS5E1UPSLINK()
        {
            this.sEGMENTField = "1";
        }
        
        public string UPSNAM
        {
            get
            {
                return this.uPSNAMField;
            }
            set
            {
                this.uPSNAMField = value;
            }
        }
        
        public string ORGNAM
        {
            get
            {
                return this.oRGNAMField;
            }
            set
            {
                this.oRGNAMField = value;
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
        
        public string OBJID
        {
            get
            {
                return this.oBJIDField;
            }
            set
            {
                this.oBJIDField = value;
            }
        }
        
        public string OBJVAL
        {
            get
            {
                return this.oBJVALField;
            }
            set
            {
                this.oBJVALField = value;
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
    public partial class ZMATMAS5E1MTXHM
    {
        private string mSGFNField;
        private string tDOBJECTField;
        private string tDNAMEField;
        private string tDIDField;
        private string tDSPRASField;
        private string tDTEXTTYPEField;
        private string sPRAS_ISOField;
        private ZMATMAS5E1MTXLM[] e1MTXLMField;
        private string sEGMENTField;
        public ZMATMAS5E1MTXHM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string TDOBJECT
        {
            get
            {
                return this.tDOBJECTField;
            }
            set
            {
                this.tDOBJECTField = value;
            }
        }
        
        public string TDNAME
        {
            get
            {
                return this.tDNAMEField;
            }
            set
            {
                this.tDNAMEField = value;
            }
        }
        
        public string TDID
        {
            get
            {
                return this.tDIDField;
            }
            set
            {
                this.tDIDField = value;
            }
        }
        
        public string TDSPRAS
        {
            get
            {
                return this.tDSPRASField;
            }
            set
            {
                this.tDSPRASField = value;
            }
        }
        
        public string TDTEXTTYPE
        {
            get
            {
                return this.tDTEXTTYPEField;
            }
            set
            {
                this.tDTEXTTYPEField = value;
            }
        }
        
        public string SPRAS_ISO
        {
            get
            {
                return this.sPRAS_ISOField;
            }
            set
            {
                this.sPRAS_ISOField = value;
            }
        }
        [XmlElement("E1MTXLM")]
        public ZMATMAS5E1MTXLM[] E1MTXLM
        {
            get
            {
                return this.e1MTXLMField;
            }
            set
            {
                this.e1MTXLMField = value;
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
    public partial class ZMATMAS5E1MTXLM
    {
        private string mSGFNField;
        private string tDFORMATField;
        private string tDLINEField;
        private string sEGMENTField;
        public ZMATMAS5E1MTXLM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string TDFORMAT
        {
            get
            {
                return this.tDFORMATField;
            }
            set
            {
                this.tDFORMATField = value;
            }
        }
        
        public string TDLINE
        {
            get
            {
                return this.tDLINEField;
            }
            set
            {
                this.tDLINEField = value;
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
    public partial class ZMATMAS5E1MEANM
    {
        private string mSGFNField;
        private string mEINHField;
        private string lFNUMField;
        private string eAN11Field;
        private string eANTPField;
        private string hPEANField;
        private string sEGMENTField;
        public ZMATMAS5E1MEANM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string MEINH
        {
            get
            {
                return this.mEINHField;
            }
            set
            {
                this.mEINHField = value;
            }
        }
        
        public string LFNUM
        {
            get
            {
                return this.lFNUMField;
            }
            set
            {
                this.lFNUMField = value;
            }
        }
        
        public string EAN11
        {
            get
            {
                return this.eAN11Field;
            }
            set
            {
                this.eAN11Field = value;
            }
        }
        
        public string EANTP
        {
            get
            {
                return this.eANTPField;
            }
            set
            {
                this.eANTPField = value;
            }
        }
        
        public string HPEAN
        {
            get
            {
                return this.hPEANField;
            }
            set
            {
                this.hPEANField = value;
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
    public partial class ZMATMAS5E1MARAM
    {
        private string mSGFNField;
        private string mATNRField;
        private string eRSDAField;
        private string eRNAMField;
        private string lAEDAField;
        private string aENAMField;
        private string pSTATField;
        private string lVORMField;
        private string mTARTField;
        private string mBRSHField;
        private string mATKLField;
        private string bISMTField;
        private string mEINSField;
        private string bSTMEField;
        private string zEINRField;
        private string zEIARField;
        private string zEIVRField;
        private string zEIFOField;
        private string aESZNField;
        private string bLATTField;
        private string bLANZField;
        private string fERTHField;
        private string fORMTField;
        private string gROESField;
        private string wRKSTField;
        private string nORMTField;
        private string lABORField;
        private string eKWSLField;
        private string bRGEWField;
        private string nTGEWField;
        private string gEWEIField;
        private string vOLUMField;
        private string vOLEHField;
        private string bEHVOField;
        private string rAUBEField;
        private string tEMPBField;
        private string tRAGRField;
        private string sTOFFField;
        private string sPARTField;
        private string kUNNRField;
        private string wESCHField;
        private string bWVORField;
        private string bWSCLField;
        private string sAISOField;
        private string eTIARField;
        private string eTIFOField;
        private string eAN11Field;
        private string nUMTPField;
        private string lAENGField;
        private string bREITField;
        private string hOEHEField;
        private string mEABMField;
        private string pRDHAField;
        private string cADKZField;
        private string eRGEWField;
        private string eRGEIField;
        private string eRVOLField;
        private string eRVOEField;
        private string gEWTOField;
        private string vOLTOField;
        private string vABMEField;
        private string kZKFGField;
        private string xCHPFField;
        private string vHARTField;
        private string fUELGField;
        private string sTFAKField;
        private string mAGRVField;
        private string bEGRUField;
        private string qMPURField;
        private string rBNRMField;
        private string mHDRZField;
        private string mHDHBField;
        private string mHDLPField;
        private string vPSTAField;
        private string eXTWGField;
        private string mSTAEField;
        private string mSTAVField;
        private string mSTDEField;
        private string mSTDVField;
        private string kZUMWField;
        private string kOSCHField;
        private string nRFHGField;
        private string mFRPNField;
        private string mFRNRField;
        private string bMATNField;
        private string mPROFField;
        private string pROFLField;
        private string iHIVIField;
        private string iLOOSField;
        private string kZGVHField;
        private string xGCHPField;
        private string cOMPLField;
        private string kZEFFField;
        private string rDMHDField;
        private string iPRKZField;
        private string pRZUSField;
        private string mTPOS_MARAField;
        private string gEWTO_NEWField;
        private string vOLTO_NEWField;
        private string wRKST_NEWField;
        private string aENNRField;
        private string mATFIField;
        private string cMRELField;
        private string sATNRField;
        private string sLED_BBDField;
        private string gTIN_VARIANTField;
        private string gENNRField;
        private string sERLVField;
        private string rMATPField;
        private string gDS_RELEVANTField;
        private string mCONDField;
        private string rETDELCField;
        private string lOGLEV_RETOField;
        private string nSNIDField;
        private string wEORAField;
        private string _CWM_TOLGRField;
        private string _CWM_TARAField;
        private string _CWM_TARUMField;
        private string pACKCODEField;
        private string dG_PACK_STATUSField;
        private ZMATMAS5Z1MARAM[] z1MARAMField;
        private ZMATMAS5E1MARA1 e1MARA1Field;
        private ZMATMAS5E1MAKTM[] e1MAKTMField;
        private ZMATMAS5E1MARCM[] e1MARCMField;
        private ZMATMAS5E1MARMM[] e1MARMMField;
        private ZMATMAS5E1MBEWM[] e1MBEWMField;
        private ZMATMAS5E1MLGNM[] e1MLGNMField;
        private ZMATMAS5E1MVKEM[] e1MVKEMField;
        private ZMATMAS5E1MLANM[] e1MLANMField;
        private ZMATMAS5E1MTXHM[] e1MTXHMField;
        private ZMATMAS5E1CUCFG[] e1CUCFGField;
        private string sEGMENTField;
        public ZMATMAS5E1MARAM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
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
        
        public string ERSDA
        {
            get
            {
                return this.eRSDAField;
            }
            set
            {
                this.eRSDAField = value;
            }
        }
        
        public string ERNAM
        {
            get
            {
                return this.eRNAMField;
            }
            set
            {
                this.eRNAMField = value;
            }
        }
        
        public string LAEDA
        {
            get
            {
                return this.lAEDAField;
            }
            set
            {
                this.lAEDAField = value;
            }
        }
        
        public string AENAM
        {
            get
            {
                return this.aENAMField;
            }
            set
            {
                this.aENAMField = value;
            }
        }
        
        public string PSTAT
        {
            get
            {
                return this.pSTATField;
            }
            set
            {
                this.pSTATField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string MTART
        {
            get
            {
                return this.mTARTField;
            }
            set
            {
                this.mTARTField = value;
            }
        }
        
        public string MBRSH
        {
            get
            {
                return this.mBRSHField;
            }
            set
            {
                this.mBRSHField = value;
            }
        }
        
        public string MATKL
        {
            get
            {
                return this.mATKLField;
            }
            set
            {
                this.mATKLField = value;
            }
        }
        
        public string BISMT
        {
            get
            {
                return this.bISMTField;
            }
            set
            {
                this.bISMTField = value;
            }
        }
        
        public string MEINS
        {
            get
            {
                return this.mEINSField;
            }
            set
            {
                this.mEINSField = value;
            }
        }
        
        public string BSTME
        {
            get
            {
                return this.bSTMEField;
            }
            set
            {
                this.bSTMEField = value;
            }
        }
        
        public string ZEINR
        {
            get
            {
                return this.zEINRField;
            }
            set
            {
                this.zEINRField = value;
            }
        }
        
        public string ZEIAR
        {
            get
            {
                return this.zEIARField;
            }
            set
            {
                this.zEIARField = value;
            }
        }
        
        public string ZEIVR
        {
            get
            {
                return this.zEIVRField;
            }
            set
            {
                this.zEIVRField = value;
            }
        }
        
        public string ZEIFO
        {
            get
            {
                return this.zEIFOField;
            }
            set
            {
                this.zEIFOField = value;
            }
        }
        
        public string AESZN
        {
            get
            {
                return this.aESZNField;
            }
            set
            {
                this.aESZNField = value;
            }
        }
        
        public string BLATT
        {
            get
            {
                return this.bLATTField;
            }
            set
            {
                this.bLATTField = value;
            }
        }
        
        public string BLANZ
        {
            get
            {
                return this.bLANZField;
            }
            set
            {
                this.bLANZField = value;
            }
        }
        
        public string FERTH
        {
            get
            {
                return this.fERTHField;
            }
            set
            {
                this.fERTHField = value;
            }
        }
        
        public string FORMT
        {
            get
            {
                return this.fORMTField;
            }
            set
            {
                this.fORMTField = value;
            }
        }
        
        public string GROES
        {
            get
            {
                return this.gROESField;
            }
            set
            {
                this.gROESField = value;
            }
        }
        
        public string WRKST
        {
            get
            {
                return this.wRKSTField;
            }
            set
            {
                this.wRKSTField = value;
            }
        }
        
        public string NORMT
        {
            get
            {
                return this.nORMTField;
            }
            set
            {
                this.nORMTField = value;
            }
        }
        
        public string LABOR
        {
            get
            {
                return this.lABORField;
            }
            set
            {
                this.lABORField = value;
            }
        }
        
        public string EKWSL
        {
            get
            {
                return this.eKWSLField;
            }
            set
            {
                this.eKWSLField = value;
            }
        }
        
        public string BRGEW
        {
            get
            {
                return this.bRGEWField;
            }
            set
            {
                this.bRGEWField = value;
            }
        }
        
        public string NTGEW
        {
            get
            {
                return this.nTGEWField;
            }
            set
            {
                this.nTGEWField = value;
            }
        }
        
        public string GEWEI
        {
            get
            {
                return this.gEWEIField;
            }
            set
            {
                this.gEWEIField = value;
            }
        }
        
        public string VOLUM
        {
            get
            {
                return this.vOLUMField;
            }
            set
            {
                this.vOLUMField = value;
            }
        }
        
        public string VOLEH
        {
            get
            {
                return this.vOLEHField;
            }
            set
            {
                this.vOLEHField = value;
            }
        }
        
        public string BEHVO
        {
            get
            {
                return this.bEHVOField;
            }
            set
            {
                this.bEHVOField = value;
            }
        }
        
        public string RAUBE
        {
            get
            {
                return this.rAUBEField;
            }
            set
            {
                this.rAUBEField = value;
            }
        }
        
        public string TEMPB
        {
            get
            {
                return this.tEMPBField;
            }
            set
            {
                this.tEMPBField = value;
            }
        }
        
        public string TRAGR
        {
            get
            {
                return this.tRAGRField;
            }
            set
            {
                this.tRAGRField = value;
            }
        }
        
        public string STOFF
        {
            get
            {
                return this.sTOFFField;
            }
            set
            {
                this.sTOFFField = value;
            }
        }
        
        public string SPART
        {
            get
            {
                return this.sPARTField;
            }
            set
            {
                this.sPARTField = value;
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
        
        public string WESCH
        {
            get
            {
                return this.wESCHField;
            }
            set
            {
                this.wESCHField = value;
            }
        }
        
        public string BWVOR
        {
            get
            {
                return this.bWVORField;
            }
            set
            {
                this.bWVORField = value;
            }
        }
        
        public string BWSCL
        {
            get
            {
                return this.bWSCLField;
            }
            set
            {
                this.bWSCLField = value;
            }
        }
        
        public string SAISO
        {
            get
            {
                return this.sAISOField;
            }
            set
            {
                this.sAISOField = value;
            }
        }
        
        public string ETIAR
        {
            get
            {
                return this.eTIARField;
            }
            set
            {
                this.eTIARField = value;
            }
        }
        
        public string ETIFO
        {
            get
            {
                return this.eTIFOField;
            }
            set
            {
                this.eTIFOField = value;
            }
        }
        
        public string EAN11
        {
            get
            {
                return this.eAN11Field;
            }
            set
            {
                this.eAN11Field = value;
            }
        }
        
        public string NUMTP
        {
            get
            {
                return this.nUMTPField;
            }
            set
            {
                this.nUMTPField = value;
            }
        }
        
        public string LAENG
        {
            get
            {
                return this.lAENGField;
            }
            set
            {
                this.lAENGField = value;
            }
        }
        
        public string BREIT
        {
            get
            {
                return this.bREITField;
            }
            set
            {
                this.bREITField = value;
            }
        }
        
        public string HOEHE
        {
            get
            {
                return this.hOEHEField;
            }
            set
            {
                this.hOEHEField = value;
            }
        }
        
        public string MEABM
        {
            get
            {
                return this.mEABMField;
            }
            set
            {
                this.mEABMField = value;
            }
        }
        
        public string PRDHA
        {
            get
            {
                return this.pRDHAField;
            }
            set
            {
                this.pRDHAField = value;
            }
        }
        
        public string CADKZ
        {
            get
            {
                return this.cADKZField;
            }
            set
            {
                this.cADKZField = value;
            }
        }
        
        public string ERGEW
        {
            get
            {
                return this.eRGEWField;
            }
            set
            {
                this.eRGEWField = value;
            }
        }
        
        public string ERGEI
        {
            get
            {
                return this.eRGEIField;
            }
            set
            {
                this.eRGEIField = value;
            }
        }
        
        public string ERVOL
        {
            get
            {
                return this.eRVOLField;
            }
            set
            {
                this.eRVOLField = value;
            }
        }
        
        public string ERVOE
        {
            get
            {
                return this.eRVOEField;
            }
            set
            {
                this.eRVOEField = value;
            }
        }
        
        public string GEWTO
        {
            get
            {
                return this.gEWTOField;
            }
            set
            {
                this.gEWTOField = value;
            }
        }
        
        public string VOLTO
        {
            get
            {
                return this.vOLTOField;
            }
            set
            {
                this.vOLTOField = value;
            }
        }
        
        public string VABME
        {
            get
            {
                return this.vABMEField;
            }
            set
            {
                this.vABMEField = value;
            }
        }
        
        public string KZKFG
        {
            get
            {
                return this.kZKFGField;
            }
            set
            {
                this.kZKFGField = value;
            }
        }
        
        public string XCHPF
        {
            get
            {
                return this.xCHPFField;
            }
            set
            {
                this.xCHPFField = value;
            }
        }
        
        public string VHART
        {
            get
            {
                return this.vHARTField;
            }
            set
            {
                this.vHARTField = value;
            }
        }
        
        public string FUELG
        {
            get
            {
                return this.fUELGField;
            }
            set
            {
                this.fUELGField = value;
            }
        }
        
        public string STFAK
        {
            get
            {
                return this.sTFAKField;
            }
            set
            {
                this.sTFAKField = value;
            }
        }
        
        public string MAGRV
        {
            get
            {
                return this.mAGRVField;
            }
            set
            {
                this.mAGRVField = value;
            }
        }
        
        public string BEGRU
        {
            get
            {
                return this.bEGRUField;
            }
            set
            {
                this.bEGRUField = value;
            }
        }
        
        public string QMPUR
        {
            get
            {
                return this.qMPURField;
            }
            set
            {
                this.qMPURField = value;
            }
        }
        
        public string RBNRM
        {
            get
            {
                return this.rBNRMField;
            }
            set
            {
                this.rBNRMField = value;
            }
        }
        
        public string MHDRZ
        {
            get
            {
                return this.mHDRZField;
            }
            set
            {
                this.mHDRZField = value;
            }
        }
        
        public string MHDHB
        {
            get
            {
                return this.mHDHBField;
            }
            set
            {
                this.mHDHBField = value;
            }
        }
        
        public string MHDLP
        {
            get
            {
                return this.mHDLPField;
            }
            set
            {
                this.mHDLPField = value;
            }
        }
        
        public string VPSTA
        {
            get
            {
                return this.vPSTAField;
            }
            set
            {
                this.vPSTAField = value;
            }
        }
        
        public string EXTWG
        {
            get
            {
                return this.eXTWGField;
            }
            set
            {
                this.eXTWGField = value;
            }
        }
        
        public string MSTAE
        {
            get
            {
                return this.mSTAEField;
            }
            set
            {
                this.mSTAEField = value;
            }
        }
        
        public string MSTAV
        {
            get
            {
                return this.mSTAVField;
            }
            set
            {
                this.mSTAVField = value;
            }
        }
        
        public string MSTDE
        {
            get
            {
                return this.mSTDEField;
            }
            set
            {
                this.mSTDEField = value;
            }
        }
        
        public string MSTDV
        {
            get
            {
                return this.mSTDVField;
            }
            set
            {
                this.mSTDVField = value;
            }
        }
        
        public string KZUMW
        {
            get
            {
                return this.kZUMWField;
            }
            set
            {
                this.kZUMWField = value;
            }
        }
        
        public string KOSCH
        {
            get
            {
                return this.kOSCHField;
            }
            set
            {
                this.kOSCHField = value;
            }
        }
        
        public string NRFHG
        {
            get
            {
                return this.nRFHGField;
            }
            set
            {
                this.nRFHGField = value;
            }
        }
        
        public string MFRPN
        {
            get
            {
                return this.mFRPNField;
            }
            set
            {
                this.mFRPNField = value;
            }
        }
        
        public string MFRNR
        {
            get
            {
                return this.mFRNRField;
            }
            set
            {
                this.mFRNRField = value;
            }
        }
        
        public string BMATN
        {
            get
            {
                return this.bMATNField;
            }
            set
            {
                this.bMATNField = value;
            }
        }
        
        public string MPROF
        {
            get
            {
                return this.mPROFField;
            }
            set
            {
                this.mPROFField = value;
            }
        }
        
        public string PROFL
        {
            get
            {
                return this.pROFLField;
            }
            set
            {
                this.pROFLField = value;
            }
        }
        
        public string IHIVI
        {
            get
            {
                return this.iHIVIField;
            }
            set
            {
                this.iHIVIField = value;
            }
        }
        
        public string ILOOS
        {
            get
            {
                return this.iLOOSField;
            }
            set
            {
                this.iLOOSField = value;
            }
        }
        
        public string KZGVH
        {
            get
            {
                return this.kZGVHField;
            }
            set
            {
                this.kZGVHField = value;
            }
        }
        
        public string XGCHP
        {
            get
            {
                return this.xGCHPField;
            }
            set
            {
                this.xGCHPField = value;
            }
        }
        
        public string COMPL
        {
            get
            {
                return this.cOMPLField;
            }
            set
            {
                this.cOMPLField = value;
            }
        }
        
        public string KZEFF
        {
            get
            {
                return this.kZEFFField;
            }
            set
            {
                this.kZEFFField = value;
            }
        }
        
        public string RDMHD
        {
            get
            {
                return this.rDMHDField;
            }
            set
            {
                this.rDMHDField = value;
            }
        }
        
        public string IPRKZ
        {
            get
            {
                return this.iPRKZField;
            }
            set
            {
                this.iPRKZField = value;
            }
        }
        
        public string PRZUS
        {
            get
            {
                return this.pRZUSField;
            }
            set
            {
                this.pRZUSField = value;
            }
        }
        
        public string MTPOS_MARA
        {
            get
            {
                return this.mTPOS_MARAField;
            }
            set
            {
                this.mTPOS_MARAField = value;
            }
        }
        
        public string GEWTO_NEW
        {
            get
            {
                return this.gEWTO_NEWField;
            }
            set
            {
                this.gEWTO_NEWField = value;
            }
        }
        
        public string VOLTO_NEW
        {
            get
            {
                return this.vOLTO_NEWField;
            }
            set
            {
                this.vOLTO_NEWField = value;
            }
        }
        
        public string WRKST_NEW
        {
            get
            {
                return this.wRKST_NEWField;
            }
            set
            {
                this.wRKST_NEWField = value;
            }
        }
        
        public string AENNR
        {
            get
            {
                return this.aENNRField;
            }
            set
            {
                this.aENNRField = value;
            }
        }
        
        public string MATFI
        {
            get
            {
                return this.mATFIField;
            }
            set
            {
                this.mATFIField = value;
            }
        }
        
        public string CMREL
        {
            get
            {
                return this.cMRELField;
            }
            set
            {
                this.cMRELField = value;
            }
        }
        
        public string SATNR
        {
            get
            {
                return this.sATNRField;
            }
            set
            {
                this.sATNRField = value;
            }
        }
        
        public string SLED_BBD
        {
            get
            {
                return this.sLED_BBDField;
            }
            set
            {
                this.sLED_BBDField = value;
            }
        }
        
        public string GTIN_VARIANT
        {
            get
            {
                return this.gTIN_VARIANTField;
            }
            set
            {
                this.gTIN_VARIANTField = value;
            }
        }
        
        public string GENNR
        {
            get
            {
                return this.gENNRField;
            }
            set
            {
                this.gENNRField = value;
            }
        }
        
        public string SERLV
        {
            get
            {
                return this.sERLVField;
            }
            set
            {
                this.sERLVField = value;
            }
        }
        
        public string RMATP
        {
            get
            {
                return this.rMATPField;
            }
            set
            {
                this.rMATPField = value;
            }
        }
        
        public string GDS_RELEVANT
        {
            get
            {
                return this.gDS_RELEVANTField;
            }
            set
            {
                this.gDS_RELEVANTField = value;
            }
        }
        
        public string MCOND
        {
            get
            {
                return this.mCONDField;
            }
            set
            {
                this.mCONDField = value;
            }
        }
        
        public string RETDELC
        {
            get
            {
                return this.rETDELCField;
            }
            set
            {
                this.rETDELCField = value;
            }
        }
        
        public string LOGLEV_RETO
        {
            get
            {
                return this.lOGLEV_RETOField;
            }
            set
            {
                this.lOGLEV_RETOField = value;
            }
        }
        
        public string NSNID
        {
            get
            {
                return this.nSNIDField;
            }
            set
            {
                this.nSNIDField = value;
            }
        }
        
        public string WEORA
        {
            get
            {
                return this.wEORAField;
            }
            set
            {
                this.wEORAField = value;
            }
        }
        [XmlElement("_-CWM_-TOLGR")]
        public string _CWM_TOLGR
        {
            get
            {
                return this._CWM_TOLGRField;
            }
            set
            {
                this._CWM_TOLGRField = value;
            }
        }
        [XmlElement("_-CWM_-TARA")]
        public string _CWM_TARA
        {
            get
            {
                return this._CWM_TARAField;
            }
            set
            {
                this._CWM_TARAField = value;
            }
        }
        [XmlElement("_-CWM_-TARUM")]
        public string _CWM_TARUM
        {
            get
            {
                return this._CWM_TARUMField;
            }
            set
            {
                this._CWM_TARUMField = value;
            }
        }
        
        public string PACKCODE
        {
            get
            {
                return this.pACKCODEField;
            }
            set
            {
                this.pACKCODEField = value;
            }
        }
        
        public string DG_PACK_STATUS
        {
            get
            {
                return this.dG_PACK_STATUSField;
            }
            set
            {
                this.dG_PACK_STATUSField = value;
            }
        }
        [XmlElement("Z1MARAM")]
        public ZMATMAS5Z1MARAM[] Z1MARAM
        {
            get
            {
                return this.z1MARAMField;
            }
            set
            {
                this.z1MARAMField = value;
            }
        }
        
        public ZMATMAS5E1MARA1 E1MARA1
        {
            get
            {
                return this.e1MARA1Field;
            }
            set
            {
                this.e1MARA1Field = value;
            }
        }
        [XmlElement("E1MAKTM")]
        public ZMATMAS5E1MAKTM[] E1MAKTM
        {
            get
            {
                return this.e1MAKTMField;
            }
            set
            {
                this.e1MAKTMField = value;
            }
        }
        [XmlElement("E1MARCM")]
        public ZMATMAS5E1MARCM[] E1MARCM
        {
            get
            {
                return this.e1MARCMField;
            }
            set
            {
                this.e1MARCMField = value;
            }
        }
        [XmlElement("E1MARMM")]
        public ZMATMAS5E1MARMM[] E1MARMM
        {
            get
            {
                return this.e1MARMMField;
            }
            set
            {
                this.e1MARMMField = value;
            }
        }
        [XmlElement("E1MBEWM")]
        public ZMATMAS5E1MBEWM[] E1MBEWM
        {
            get
            {
                return this.e1MBEWMField;
            }
            set
            {
                this.e1MBEWMField = value;
            }
        }
        [XmlElement("E1MLGNM")]
        public ZMATMAS5E1MLGNM[] E1MLGNM
        {
            get
            {
                return this.e1MLGNMField;
            }
            set
            {
                this.e1MLGNMField = value;
            }
        }
        [XmlElement("E1MVKEM")]
        public ZMATMAS5E1MVKEM[] E1MVKEM
        {
            get
            {
                return this.e1MVKEMField;
            }
            set
            {
                this.e1MVKEMField = value;
            }
        }
        [XmlElement("E1MLANM")]
        public ZMATMAS5E1MLANM[] E1MLANM
        {
            get
            {
                return this.e1MLANMField;
            }
            set
            {
                this.e1MLANMField = value;
            }
        }
        [XmlElement("E1MTXHM")]
        public ZMATMAS5E1MTXHM[] E1MTXHM
        {
            get
            {
                return this.e1MTXHMField;
            }
            set
            {
                this.e1MTXHMField = value;
            }
        }
        [XmlElement("E1CUCFG")]
        public ZMATMAS5E1CUCFG[] E1CUCFG
        {
            get
            {
                return this.e1CUCFGField;
            }
            set
            {
                this.e1CUCFGField = value;
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
    public partial class ZMATMAS5Z1MARAM
    {
        private string zZSANField;
        private string zZSECTField;
        private string zZQULNField;
        private string zZPCSNField;
        private string zZPRTNField;
        private string zZDM1NField;
        private string zZDM2NField;
        private string zZDM3NField;
        private string zZDM4NField;
        private string zZDM5NField;
        private string zZEXCNField;
        private string zZCOL1Field;
        private string zZCOL2Field;
        private string zZPCOAField;
        private string zZDIPAField;
        private string zZBRANField;
        private string zZWTHIField;
        private string zZENCNField;
        private string zZLCLNField;
        private string zZHDNNField;
        private string zZHTCNField;
        private string zZPDNNField;
        private string zZFTYPField;
        private string zZPRFNField;
        private string zZCONNField;
        private string zZBOSEField;
        private string zZGLOSField;
        private string zZGSPEField;
        private string zZDESCField;
        private string zZMCATField;
        private string zZTEM1Field;
        private string zZTEM2Field;
        private string zZTEM3Field;
        private string zZTEM4Field;
        private string zZTEM5Field;
        private string zZREJTField;
        private string zZALTMField;
        private string zZCGAGField;
        private string zZTYP2Field;
        private string zZDM6N3Field;
        private string zZSTOLField;
        private string zZROLLField;
        private string zZMTWCField;
        private string zZBASEField;
        private string zZVN1RField;
        private string zZVN2RField;
        private string zZPKDMField;
        private string zZMINPField;
        private string zZREJTEANField;
        private string sEGMENTField;
        public ZMATMAS5Z1MARAM()
        {
            this.sEGMENTField = "1";
        }
        
        public string ZZSAN
        {
            get
            {
                return this.zZSANField;
            }
            set
            {
                this.zZSANField = value;
            }
        }
        
        public string ZZSECT
        {
            get
            {
                return this.zZSECTField;
            }
            set
            {
                this.zZSECTField = value;
            }
        }
        
        public string ZZQULN
        {
            get
            {
                return this.zZQULNField;
            }
            set
            {
                this.zZQULNField = value;
            }
        }
        
        public string ZZPCSN
        {
            get
            {
                return this.zZPCSNField;
            }
            set
            {
                this.zZPCSNField = value;
            }
        }
        
        public string ZZPRTN
        {
            get
            {
                return this.zZPRTNField;
            }
            set
            {
                this.zZPRTNField = value;
            }
        }
        
        public string ZZDM1N
        {
            get
            {
                return this.zZDM1NField;
            }
            set
            {
                this.zZDM1NField = value;
            }
        }
        
        public string ZZDM2N
        {
            get
            {
                return this.zZDM2NField;
            }
            set
            {
                this.zZDM2NField = value;
            }
        }
        
        public string ZZDM3N
        {
            get
            {
                return this.zZDM3NField;
            }
            set
            {
                this.zZDM3NField = value;
            }
        }
        
        public string ZZDM4N
        {
            get
            {
                return this.zZDM4NField;
            }
            set
            {
                this.zZDM4NField = value;
            }
        }
        
        public string ZZDM5N
        {
            get
            {
                return this.zZDM5NField;
            }
            set
            {
                this.zZDM5NField = value;
            }
        }
        
        public string ZZEXCN
        {
            get
            {
                return this.zZEXCNField;
            }
            set
            {
                this.zZEXCNField = value;
            }
        }
        
        public string ZZCOL1
        {
            get
            {
                return this.zZCOL1Field;
            }
            set
            {
                this.zZCOL1Field = value;
            }
        }
        
        public string ZZCOL2
        {
            get
            {
                return this.zZCOL2Field;
            }
            set
            {
                this.zZCOL2Field = value;
            }
        }
        
        public string ZZPCOA
        {
            get
            {
                return this.zZPCOAField;
            }
            set
            {
                this.zZPCOAField = value;
            }
        }
        
        public string ZZDIPA
        {
            get
            {
                return this.zZDIPAField;
            }
            set
            {
                this.zZDIPAField = value;
            }
        }
        
        public string ZZBRAN
        {
            get
            {
                return this.zZBRANField;
            }
            set
            {
                this.zZBRANField = value;
            }
        }
        
        public string ZZWTHI
        {
            get
            {
                return this.zZWTHIField;
            }
            set
            {
                this.zZWTHIField = value;
            }
        }
        
        public string ZZENCN
        {
            get
            {
                return this.zZENCNField;
            }
            set
            {
                this.zZENCNField = value;
            }
        }
        
        public string ZZLCLN
        {
            get
            {
                return this.zZLCLNField;
            }
            set
            {
                this.zZLCLNField = value;
            }
        }
        
        public string ZZHDNN
        {
            get
            {
                return this.zZHDNNField;
            }
            set
            {
                this.zZHDNNField = value;
            }
        }
        
        public string ZZHTCN
        {
            get
            {
                return this.zZHTCNField;
            }
            set
            {
                this.zZHTCNField = value;
            }
        }
        
        public string ZZPDNN
        {
            get
            {
                return this.zZPDNNField;
            }
            set
            {
                this.zZPDNNField = value;
            }
        }
        
        public string ZZFTYP
        {
            get
            {
                return this.zZFTYPField;
            }
            set
            {
                this.zZFTYPField = value;
            }
        }
        
        public string ZZPRFN
        {
            get
            {
                return this.zZPRFNField;
            }
            set
            {
                this.zZPRFNField = value;
            }
        }
        
        public string ZZCONN
        {
            get
            {
                return this.zZCONNField;
            }
            set
            {
                this.zZCONNField = value;
            }
        }
        
        public string ZZBOSE
        {
            get
            {
                return this.zZBOSEField;
            }
            set
            {
                this.zZBOSEField = value;
            }
        }
        
        public string ZZGLOS
        {
            get
            {
                return this.zZGLOSField;
            }
            set
            {
                this.zZGLOSField = value;
            }
        }
        
        public string ZZGSPE
        {
            get
            {
                return this.zZGSPEField;
            }
            set
            {
                this.zZGSPEField = value;
            }
        }
        
        public string ZZDESC
        {
            get
            {
                return this.zZDESCField;
            }
            set
            {
                this.zZDESCField = value;
            }
        }
        
        public string ZZMCAT
        {
            get
            {
                return this.zZMCATField;
            }
            set
            {
                this.zZMCATField = value;
            }
        }
        
        public string ZZTEM1
        {
            get
            {
                return this.zZTEM1Field;
            }
            set
            {
                this.zZTEM1Field = value;
            }
        }
        
        public string ZZTEM2
        {
            get
            {
                return this.zZTEM2Field;
            }
            set
            {
                this.zZTEM2Field = value;
            }
        }
        
        public string ZZTEM3
        {
            get
            {
                return this.zZTEM3Field;
            }
            set
            {
                this.zZTEM3Field = value;
            }
        }
        
        public string ZZTEM4
        {
            get
            {
                return this.zZTEM4Field;
            }
            set
            {
                this.zZTEM4Field = value;
            }
        }
        
        public string ZZTEM5
        {
            get
            {
                return this.zZTEM5Field;
            }
            set
            {
                this.zZTEM5Field = value;
            }
        }
        
        public string ZZREJT
        {
            get
            {
                return this.zZREJTField;
            }
            set
            {
                this.zZREJTField = value;
            }
        }
        
        public string ZZALTM
        {
            get
            {
                return this.zZALTMField;
            }
            set
            {
                this.zZALTMField = value;
            }
        }
        
        public string ZZCGAG
        {
            get
            {
                return this.zZCGAGField;
            }
            set
            {
                this.zZCGAGField = value;
            }
        }
        
        public string ZZTYP2
        {
            get
            {
                return this.zZTYP2Field;
            }
            set
            {
                this.zZTYP2Field = value;
            }
        }
        
        public string ZZDM6N3
        {
            get
            {
                return this.zZDM6N3Field;
            }
            set
            {
                this.zZDM6N3Field = value;
            }
        }
        
        public string ZZSTOL
        {
            get
            {
                return this.zZSTOLField;
            }
            set
            {
                this.zZSTOLField = value;
            }
        }
        
        public string ZZROLL
        {
            get
            {
                return this.zZROLLField;
            }
            set
            {
                this.zZROLLField = value;
            }
        }
        
        public string ZZMTWC
        {
            get
            {
                return this.zZMTWCField;
            }
            set
            {
                this.zZMTWCField = value;
            }
        }
        
        public string ZZBASE
        {
            get
            {
                return this.zZBASEField;
            }
            set
            {
                this.zZBASEField = value;
            }
        }
        
        public string ZZVN1R
        {
            get
            {
                return this.zZVN1RField;
            }
            set
            {
                this.zZVN1RField = value;
            }
        }
        
        public string ZZVN2R
        {
            get
            {
                return this.zZVN2RField;
            }
            set
            {
                this.zZVN2RField = value;
            }
        }
        
        public string ZZPKDM
        {
            get
            {
                return this.zZPKDMField;
            }
            set
            {
                this.zZPKDMField = value;
            }
        }
        
        public string ZZMINP
        {
            get
            {
                return this.zZMINPField;
            }
            set
            {
                this.zZMINPField = value;
            }
        }
        
        public string ZZREJTEAN
        {
            get
            {
                return this.zZREJTEANField;
            }
            set
            {
                this.zZREJTEANField = value;
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
    public partial class ZMATMAS5E1MAKTM
    {
        private string mSGFNField;
        private string sPRASField;
        private string mAKTXField;
        private string sPRAS_ISOField;
        private string sEGMENTField;
        public ZMATMAS5E1MAKTM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string SPRAS
        {
            get
            {
                return this.sPRASField;
            }
            set
            {
                this.sPRASField = value;
            }
        }
        
        public string MAKTX
        {
            get
            {
                return this.mAKTXField;
            }
            set
            {
                this.mAKTXField = value;
            }
        }
        
        public string SPRAS_ISO
        {
            get
            {
                return this.sPRAS_ISOField;
            }
            set
            {
                this.sPRAS_ISOField = value;
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
    public partial class ZMATMAS5E1MARCM
    {
        private string mSGFNField;
        private string wERKSField;
        private string pSTATField;
        private string lVORMField;
        private string bWTTYField;
        private string mAABCField;
        private string kZKRIField;
        private string eKGRPField;
        private string aUSMEField;
        private string dISPRField;
        private string dISMMField;
        private string dISPOField;
        private string pLIFZField;
        private string wEBAZField;
        private string pERKZField;
        private string aUSSSField;
        private string dISLSField;
        private string bESKZField;
        private string sOBSLField;
        private string mINBEField;
        private string eISBEField;
        private string bSTMIField;
        private string bSTMAField;
        private string bSTFEField;
        private string bSTRFField;
        private string mABSTField;
        private string lOSFXField;
        private string sBDKZField;
        private string lAGPRField;
        private string aLTSLField;
        private string kZAUSField;
        private string aUSDTField;
        private string nFMATField;
        private string kZBEDField;
        private string mISKZField;
        private string fHORIField;
        private string pFREIField;
        private string fFREIField;
        private string rGEKZField;
        private string fEVORField;
        private string bEARZField;
        private string rUEZTField;
        private string tRANZField;
        private string bASMGField;
        private string dZEITField;
        private string mAXLZField;
        private string lZEIHField;
        private string kZPROField;
        private string gPMKZField;
        private string uEETOField;
        private string uEETKField;
        private string uNETOField;
        private string wZEITField;
        private string aTPKZField;
        private string vZUSLField;
        private string hERBLField;
        private string iNSMKField;
        private string sSQSSField;
        private string kZDKZField;
        private string uMLMCField;
        private string lADGRField;
        private string xCHPFField;
        private string uSEQUField;
        private string lGRADField;
        private string aUFTLField;
        private string pLVARField;
        private string oTYPEField;
        private string oBJIDField;
        private string mTVFPField;
        private string pERIVField;
        private string kZKFKField;
        private string vRVEZField;
        private string vBAMGField;
        private string vBEAZField;
        private string lIZYKField;
        private string bWSCLField;
        private string kAUTBField;
        private string kORDBField;
        private string sTAWNField;
        private string hERKLField;
        private string hERKRField;
        private string eXPMEField;
        private string mTVERField;
        private string pRCTRField;
        private string tRAMEField;
        private string mRPPPField;
        private string sAUFTField;
        private string fXHORField;
        private string vRMODField;
        private string vINT1Field;
        private string vINT2Field;
        private string sTLALField;
        private string sTLANField;
        private string pLNNRField;
        private string aPLALField;
        private string lOSGRField;
        private string sOBSKField;
        private string fRTMEField;
        private string lGPROField;
        private string dISGRField;
        private string kAUSFField;
        private string qZGTPField;
        private string tAKZTField;
        private string rWPROField;
        private string cOPAMField;
        private string aBCINField;
        private string aWSLSField;
        private string sERNPField;
        private string sTDPDField;
        private string sFEPRField;
        private string xMCNGField;
        private string qSSYSField;
        private string lFRHYField;
        private string rDPRFField;
        private string vRBMTField;
        private string vRBWKField;
        private string vRBDTField;
        private string vRBFKField;
        private string aUTRUField;
        private string pREFEField;
        private string pRENCField;
        private string pRENOField;
        private string pRENDField;
        private string pRENEField;
        private string pRENGField;
        private string iTARKField;
        private string pRFRQField;
        private string kZKUPField;
        private string sTRGRField;
        private string lGFSBField;
        private string sCHGTField;
        private string cCFIXField;
        private string ePRIOField;
        private string qMATAField;
        private string pLNTYField;
        private string mMSTAField;
        private string sFCPFField;
        private string sHFLGField;
        private string sHZETField;
        private string mDACHField;
        private string kZECHField;
        private string mMSTDField;
        private string mFRGRField;
        private string fVIDKField;
        private string iNDUSField;
        private string mOWNRField;
        private string mOGRUField;
        private string cASNRField;
        private string gPNUMField;
        private string sTEUCField;
        private string fABKZField;
        private string mATGRField;
        private string lOGGRField;
        private string vSPVBField;
        private string dPLFSField;
        private string dPLPUField;
        private string dPLHOField;
        private string mINLSField;
        private string mAXLSField;
        private string fIXLSField;
        private string lTINCField;
        private string cOMPLField;
        private string cONVTField;
        private string fPRFMField;
        private string sHPROField;
        private string fXPRUField;
        private string kZPSPField;
        private string oCMPFField;
        private string aPOKZField;
        private string aHDISField;
        private string eISLOField;
        private string nCOSTField;
        private string mEGRUField;
        private string rOTATION_DATEField;
        private string uCHKZField;
        private string uCMATField;
        private string iUID_RELEVANTField;
        private string iUID_TYPEField;
        private string uID_IEAField;
        private ZMATMAS5Z1MARCM[] z1MARCMField;
        private ZMATMAS5E1MARC1 e1MARC1Field;
        private ZMATMAS5E1MARDM[] e1MARDMField;
        private ZMATMAS5E1MFHMM e1MFHMMField;
        private ZMATMAS5E1MPGDM e1MPGDMField;
        private ZMATMAS5E1MPOPM e1MPOPMField;
        private ZMATMAS5E1MPRWM[] e1MPRWMField;
        private ZMATMAS5E1MVEGM[] e1MVEGMField;
        private ZMATMAS5E1MVEUM[] e1MVEUMField;
        private ZMATMAS5E1MKALM[] e1MKALMField;
        private string sEGMENTField;
        public ZMATMAS5E1MARCM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string WERKS
        {
            get
            {
                return this.wERKSField;
            }
            set
            {
                this.wERKSField = value;
            }
        }
        
        public string PSTAT
        {
            get
            {
                return this.pSTATField;
            }
            set
            {
                this.pSTATField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string BWTTY
        {
            get
            {
                return this.bWTTYField;
            }
            set
            {
                this.bWTTYField = value;
            }
        }
        
        public string MAABC
        {
            get
            {
                return this.mAABCField;
            }
            set
            {
                this.mAABCField = value;
            }
        }
        
        public string KZKRI
        {
            get
            {
                return this.kZKRIField;
            }
            set
            {
                this.kZKRIField = value;
            }
        }
        
        public string EKGRP
        {
            get
            {
                return this.eKGRPField;
            }
            set
            {
                this.eKGRPField = value;
            }
        }
        
        public string AUSME
        {
            get
            {
                return this.aUSMEField;
            }
            set
            {
                this.aUSMEField = value;
            }
        }
        
        public string DISPR
        {
            get
            {
                return this.dISPRField;
            }
            set
            {
                this.dISPRField = value;
            }
        }
        
        public string DISMM
        {
            get
            {
                return this.dISMMField;
            }
            set
            {
                this.dISMMField = value;
            }
        }
        
        public string DISPO
        {
            get
            {
                return this.dISPOField;
            }
            set
            {
                this.dISPOField = value;
            }
        }
        
        public string PLIFZ
        {
            get
            {
                return this.pLIFZField;
            }
            set
            {
                this.pLIFZField = value;
            }
        }
        
        public string WEBAZ
        {
            get
            {
                return this.wEBAZField;
            }
            set
            {
                this.wEBAZField = value;
            }
        }
        
        public string PERKZ
        {
            get
            {
                return this.pERKZField;
            }
            set
            {
                this.pERKZField = value;
            }
        }
        
        public string AUSSS
        {
            get
            {
                return this.aUSSSField;
            }
            set
            {
                this.aUSSSField = value;
            }
        }
        
        public string DISLS
        {
            get
            {
                return this.dISLSField;
            }
            set
            {
                this.dISLSField = value;
            }
        }
        
        public string BESKZ
        {
            get
            {
                return this.bESKZField;
            }
            set
            {
                this.bESKZField = value;
            }
        }
        
        public string SOBSL
        {
            get
            {
                return this.sOBSLField;
            }
            set
            {
                this.sOBSLField = value;
            }
        }
        
        public string MINBE
        {
            get
            {
                return this.mINBEField;
            }
            set
            {
                this.mINBEField = value;
            }
        }
        
        public string EISBE
        {
            get
            {
                return this.eISBEField;
            }
            set
            {
                this.eISBEField = value;
            }
        }
        
        public string BSTMI
        {
            get
            {
                return this.bSTMIField;
            }
            set
            {
                this.bSTMIField = value;
            }
        }
        
        public string BSTMA
        {
            get
            {
                return this.bSTMAField;
            }
            set
            {
                this.bSTMAField = value;
            }
        }
        
        public string BSTFE
        {
            get
            {
                return this.bSTFEField;
            }
            set
            {
                this.bSTFEField = value;
            }
        }
        
        public string BSTRF
        {
            get
            {
                return this.bSTRFField;
            }
            set
            {
                this.bSTRFField = value;
            }
        }
        
        public string MABST
        {
            get
            {
                return this.mABSTField;
            }
            set
            {
                this.mABSTField = value;
            }
        }
        
        public string LOSFX
        {
            get
            {
                return this.lOSFXField;
            }
            set
            {
                this.lOSFXField = value;
            }
        }
        
        public string SBDKZ
        {
            get
            {
                return this.sBDKZField;
            }
            set
            {
                this.sBDKZField = value;
            }
        }
        
        public string LAGPR
        {
            get
            {
                return this.lAGPRField;
            }
            set
            {
                this.lAGPRField = value;
            }
        }
        
        public string ALTSL
        {
            get
            {
                return this.aLTSLField;
            }
            set
            {
                this.aLTSLField = value;
            }
        }
        
        public string KZAUS
        {
            get
            {
                return this.kZAUSField;
            }
            set
            {
                this.kZAUSField = value;
            }
        }
        
        public string AUSDT
        {
            get
            {
                return this.aUSDTField;
            }
            set
            {
                this.aUSDTField = value;
            }
        }
        
        public string NFMAT
        {
            get
            {
                return this.nFMATField;
            }
            set
            {
                this.nFMATField = value;
            }
        }
        
        public string KZBED
        {
            get
            {
                return this.kZBEDField;
            }
            set
            {
                this.kZBEDField = value;
            }
        }
        
        public string MISKZ
        {
            get
            {
                return this.mISKZField;
            }
            set
            {
                this.mISKZField = value;
            }
        }
        
        public string FHORI
        {
            get
            {
                return this.fHORIField;
            }
            set
            {
                this.fHORIField = value;
            }
        }
        
        public string PFREI
        {
            get
            {
                return this.pFREIField;
            }
            set
            {
                this.pFREIField = value;
            }
        }
        
        public string FFREI
        {
            get
            {
                return this.fFREIField;
            }
            set
            {
                this.fFREIField = value;
            }
        }
        
        public string RGEKZ
        {
            get
            {
                return this.rGEKZField;
            }
            set
            {
                this.rGEKZField = value;
            }
        }
        
        public string FEVOR
        {
            get
            {
                return this.fEVORField;
            }
            set
            {
                this.fEVORField = value;
            }
        }
        
        public string BEARZ
        {
            get
            {
                return this.bEARZField;
            }
            set
            {
                this.bEARZField = value;
            }
        }
        
        public string RUEZT
        {
            get
            {
                return this.rUEZTField;
            }
            set
            {
                this.rUEZTField = value;
            }
        }
        
        public string TRANZ
        {
            get
            {
                return this.tRANZField;
            }
            set
            {
                this.tRANZField = value;
            }
        }
        
        public string BASMG
        {
            get
            {
                return this.bASMGField;
            }
            set
            {
                this.bASMGField = value;
            }
        }
        
        public string DZEIT
        {
            get
            {
                return this.dZEITField;
            }
            set
            {
                this.dZEITField = value;
            }
        }
        
        public string MAXLZ
        {
            get
            {
                return this.mAXLZField;
            }
            set
            {
                this.mAXLZField = value;
            }
        }
        
        public string LZEIH
        {
            get
            {
                return this.lZEIHField;
            }
            set
            {
                this.lZEIHField = value;
            }
        }
        
        public string KZPRO
        {
            get
            {
                return this.kZPROField;
            }
            set
            {
                this.kZPROField = value;
            }
        }
        
        public string GPMKZ
        {
            get
            {
                return this.gPMKZField;
            }
            set
            {
                this.gPMKZField = value;
            }
        }
        
        public string UEETO
        {
            get
            {
                return this.uEETOField;
            }
            set
            {
                this.uEETOField = value;
            }
        }
        
        public string UEETK
        {
            get
            {
                return this.uEETKField;
            }
            set
            {
                this.uEETKField = value;
            }
        }
        
        public string UNETO
        {
            get
            {
                return this.uNETOField;
            }
            set
            {
                this.uNETOField = value;
            }
        }
        
        public string WZEIT
        {
            get
            {
                return this.wZEITField;
            }
            set
            {
                this.wZEITField = value;
            }
        }
        
        public string ATPKZ
        {
            get
            {
                return this.aTPKZField;
            }
            set
            {
                this.aTPKZField = value;
            }
        }
        
        public string VZUSL
        {
            get
            {
                return this.vZUSLField;
            }
            set
            {
                this.vZUSLField = value;
            }
        }
        
        public string HERBL
        {
            get
            {
                return this.hERBLField;
            }
            set
            {
                this.hERBLField = value;
            }
        }
        
        public string INSMK
        {
            get
            {
                return this.iNSMKField;
            }
            set
            {
                this.iNSMKField = value;
            }
        }
        
        public string SSQSS
        {
            get
            {
                return this.sSQSSField;
            }
            set
            {
                this.sSQSSField = value;
            }
        }
        
        public string KZDKZ
        {
            get
            {
                return this.kZDKZField;
            }
            set
            {
                this.kZDKZField = value;
            }
        }
        
        public string UMLMC
        {
            get
            {
                return this.uMLMCField;
            }
            set
            {
                this.uMLMCField = value;
            }
        }
        
        public string LADGR
        {
            get
            {
                return this.lADGRField;
            }
            set
            {
                this.lADGRField = value;
            }
        }
        
        public string XCHPF
        {
            get
            {
                return this.xCHPFField;
            }
            set
            {
                this.xCHPFField = value;
            }
        }
        
        public string USEQU
        {
            get
            {
                return this.uSEQUField;
            }
            set
            {
                this.uSEQUField = value;
            }
        }
        
        public string LGRAD
        {
            get
            {
                return this.lGRADField;
            }
            set
            {
                this.lGRADField = value;
            }
        }
        
        public string AUFTL
        {
            get
            {
                return this.aUFTLField;
            }
            set
            {
                this.aUFTLField = value;
            }
        }
        
        public string PLVAR
        {
            get
            {
                return this.pLVARField;
            }
            set
            {
                this.pLVARField = value;
            }
        }
        
        public string OTYPE
        {
            get
            {
                return this.oTYPEField;
            }
            set
            {
                this.oTYPEField = value;
            }
        }
        
        public string OBJID
        {
            get
            {
                return this.oBJIDField;
            }
            set
            {
                this.oBJIDField = value;
            }
        }
        
        public string MTVFP
        {
            get
            {
                return this.mTVFPField;
            }
            set
            {
                this.mTVFPField = value;
            }
        }
        
        public string PERIV
        {
            get
            {
                return this.pERIVField;
            }
            set
            {
                this.pERIVField = value;
            }
        }
        
        public string KZKFK
        {
            get
            {
                return this.kZKFKField;
            }
            set
            {
                this.kZKFKField = value;
            }
        }
        
        public string VRVEZ
        {
            get
            {
                return this.vRVEZField;
            }
            set
            {
                this.vRVEZField = value;
            }
        }
        
        public string VBAMG
        {
            get
            {
                return this.vBAMGField;
            }
            set
            {
                this.vBAMGField = value;
            }
        }
        
        public string VBEAZ
        {
            get
            {
                return this.vBEAZField;
            }
            set
            {
                this.vBEAZField = value;
            }
        }
        
        public string LIZYK
        {
            get
            {
                return this.lIZYKField;
            }
            set
            {
                this.lIZYKField = value;
            }
        }
        
        public string BWSCL
        {
            get
            {
                return this.bWSCLField;
            }
            set
            {
                this.bWSCLField = value;
            }
        }
        
        public string KAUTB
        {
            get
            {
                return this.kAUTBField;
            }
            set
            {
                this.kAUTBField = value;
            }
        }
        
        public string KORDB
        {
            get
            {
                return this.kORDBField;
            }
            set
            {
                this.kORDBField = value;
            }
        }
        
        public string STAWN
        {
            get
            {
                return this.sTAWNField;
            }
            set
            {
                this.sTAWNField = value;
            }
        }
        
        public string HERKL
        {
            get
            {
                return this.hERKLField;
            }
            set
            {
                this.hERKLField = value;
            }
        }
        
        public string HERKR
        {
            get
            {
                return this.hERKRField;
            }
            set
            {
                this.hERKRField = value;
            }
        }
        
        public string EXPME
        {
            get
            {
                return this.eXPMEField;
            }
            set
            {
                this.eXPMEField = value;
            }
        }
        
        public string MTVER
        {
            get
            {
                return this.mTVERField;
            }
            set
            {
                this.mTVERField = value;
            }
        }
        
        public string PRCTR
        {
            get
            {
                return this.pRCTRField;
            }
            set
            {
                this.pRCTRField = value;
            }
        }
        
        public string TRAME
        {
            get
            {
                return this.tRAMEField;
            }
            set
            {
                this.tRAMEField = value;
            }
        }
        
        public string MRPPP
        {
            get
            {
                return this.mRPPPField;
            }
            set
            {
                this.mRPPPField = value;
            }
        }
        
        public string SAUFT
        {
            get
            {
                return this.sAUFTField;
            }
            set
            {
                this.sAUFTField = value;
            }
        }
        
        public string FXHOR
        {
            get
            {
                return this.fXHORField;
            }
            set
            {
                this.fXHORField = value;
            }
        }
        
        public string VRMOD
        {
            get
            {
                return this.vRMODField;
            }
            set
            {
                this.vRMODField = value;
            }
        }
        
        public string VINT1
        {
            get
            {
                return this.vINT1Field;
            }
            set
            {
                this.vINT1Field = value;
            }
        }
        
        public string VINT2
        {
            get
            {
                return this.vINT2Field;
            }
            set
            {
                this.vINT2Field = value;
            }
        }
        
        public string STLAL
        {
            get
            {
                return this.sTLALField;
            }
            set
            {
                this.sTLALField = value;
            }
        }
        
        public string STLAN
        {
            get
            {
                return this.sTLANField;
            }
            set
            {
                this.sTLANField = value;
            }
        }
        
        public string PLNNR
        {
            get
            {
                return this.pLNNRField;
            }
            set
            {
                this.pLNNRField = value;
            }
        }
        
        public string APLAL
        {
            get
            {
                return this.aPLALField;
            }
            set
            {
                this.aPLALField = value;
            }
        }
        
        public string LOSGR
        {
            get
            {
                return this.lOSGRField;
            }
            set
            {
                this.lOSGRField = value;
            }
        }
        
        public string SOBSK
        {
            get
            {
                return this.sOBSKField;
            }
            set
            {
                this.sOBSKField = value;
            }
        }
        
        public string FRTME
        {
            get
            {
                return this.fRTMEField;
            }
            set
            {
                this.fRTMEField = value;
            }
        }
        
        public string LGPRO
        {
            get
            {
                return this.lGPROField;
            }
            set
            {
                this.lGPROField = value;
            }
        }
        
        public string DISGR
        {
            get
            {
                return this.dISGRField;
            }
            set
            {
                this.dISGRField = value;
            }
        }
        
        public string KAUSF
        {
            get
            {
                return this.kAUSFField;
            }
            set
            {
                this.kAUSFField = value;
            }
        }
        
        public string QZGTP
        {
            get
            {
                return this.qZGTPField;
            }
            set
            {
                this.qZGTPField = value;
            }
        }
        
        public string TAKZT
        {
            get
            {
                return this.tAKZTField;
            }
            set
            {
                this.tAKZTField = value;
            }
        }
        
        public string RWPRO
        {
            get
            {
                return this.rWPROField;
            }
            set
            {
                this.rWPROField = value;
            }
        }
        
        public string COPAM
        {
            get
            {
                return this.cOPAMField;
            }
            set
            {
                this.cOPAMField = value;
            }
        }
        
        public string ABCIN
        {
            get
            {
                return this.aBCINField;
            }
            set
            {
                this.aBCINField = value;
            }
        }
        
        public string AWSLS
        {
            get
            {
                return this.aWSLSField;
            }
            set
            {
                this.aWSLSField = value;
            }
        }
        
        public string SERNP
        {
            get
            {
                return this.sERNPField;
            }
            set
            {
                this.sERNPField = value;
            }
        }
        
        public string STDPD
        {
            get
            {
                return this.sTDPDField;
            }
            set
            {
                this.sTDPDField = value;
            }
        }
        
        public string SFEPR
        {
            get
            {
                return this.sFEPRField;
            }
            set
            {
                this.sFEPRField = value;
            }
        }
        
        public string XMCNG
        {
            get
            {
                return this.xMCNGField;
            }
            set
            {
                this.xMCNGField = value;
            }
        }
        
        public string QSSYS
        {
            get
            {
                return this.qSSYSField;
            }
            set
            {
                this.qSSYSField = value;
            }
        }
        
        public string LFRHY
        {
            get
            {
                return this.lFRHYField;
            }
            set
            {
                this.lFRHYField = value;
            }
        }
        
        public string RDPRF
        {
            get
            {
                return this.rDPRFField;
            }
            set
            {
                this.rDPRFField = value;
            }
        }
        
        public string VRBMT
        {
            get
            {
                return this.vRBMTField;
            }
            set
            {
                this.vRBMTField = value;
            }
        }
        
        public string VRBWK
        {
            get
            {
                return this.vRBWKField;
            }
            set
            {
                this.vRBWKField = value;
            }
        }
        
        public string VRBDT
        {
            get
            {
                return this.vRBDTField;
            }
            set
            {
                this.vRBDTField = value;
            }
        }
        
        public string VRBFK
        {
            get
            {
                return this.vRBFKField;
            }
            set
            {
                this.vRBFKField = value;
            }
        }
        
        public string AUTRU
        {
            get
            {
                return this.aUTRUField;
            }
            set
            {
                this.aUTRUField = value;
            }
        }
        
        public string PREFE
        {
            get
            {
                return this.pREFEField;
            }
            set
            {
                this.pREFEField = value;
            }
        }
        
        public string PRENC
        {
            get
            {
                return this.pRENCField;
            }
            set
            {
                this.pRENCField = value;
            }
        }
        
        public string PRENO
        {
            get
            {
                return this.pRENOField;
            }
            set
            {
                this.pRENOField = value;
            }
        }
        
        public string PREND
        {
            get
            {
                return this.pRENDField;
            }
            set
            {
                this.pRENDField = value;
            }
        }
        
        public string PRENE
        {
            get
            {
                return this.pRENEField;
            }
            set
            {
                this.pRENEField = value;
            }
        }
        
        public string PRENG
        {
            get
            {
                return this.pRENGField;
            }
            set
            {
                this.pRENGField = value;
            }
        }
        
        public string ITARK
        {
            get
            {
                return this.iTARKField;
            }
            set
            {
                this.iTARKField = value;
            }
        }
        
        public string PRFRQ
        {
            get
            {
                return this.pRFRQField;
            }
            set
            {
                this.pRFRQField = value;
            }
        }
        
        public string KZKUP
        {
            get
            {
                return this.kZKUPField;
            }
            set
            {
                this.kZKUPField = value;
            }
        }
        
        public string STRGR
        {
            get
            {
                return this.sTRGRField;
            }
            set
            {
                this.sTRGRField = value;
            }
        }
        
        public string LGFSB
        {
            get
            {
                return this.lGFSBField;
            }
            set
            {
                this.lGFSBField = value;
            }
        }
        
        public string SCHGT
        {
            get
            {
                return this.sCHGTField;
            }
            set
            {
                this.sCHGTField = value;
            }
        }
        
        public string CCFIX
        {
            get
            {
                return this.cCFIXField;
            }
            set
            {
                this.cCFIXField = value;
            }
        }
        
        public string EPRIO
        {
            get
            {
                return this.ePRIOField;
            }
            set
            {
                this.ePRIOField = value;
            }
        }
        
        public string QMATA
        {
            get
            {
                return this.qMATAField;
            }
            set
            {
                this.qMATAField = value;
            }
        }
        
        public string PLNTY
        {
            get
            {
                return this.pLNTYField;
            }
            set
            {
                this.pLNTYField = value;
            }
        }
        
        public string MMSTA
        {
            get
            {
                return this.mMSTAField;
            }
            set
            {
                this.mMSTAField = value;
            }
        }
        
        public string SFCPF
        {
            get
            {
                return this.sFCPFField;
            }
            set
            {
                this.sFCPFField = value;
            }
        }
        
        public string SHFLG
        {
            get
            {
                return this.sHFLGField;
            }
            set
            {
                this.sHFLGField = value;
            }
        }
        
        public string SHZET
        {
            get
            {
                return this.sHZETField;
            }
            set
            {
                this.sHZETField = value;
            }
        }
        
        public string MDACH
        {
            get
            {
                return this.mDACHField;
            }
            set
            {
                this.mDACHField = value;
            }
        }
        
        public string KZECH
        {
            get
            {
                return this.kZECHField;
            }
            set
            {
                this.kZECHField = value;
            }
        }
        
        public string MMSTD
        {
            get
            {
                return this.mMSTDField;
            }
            set
            {
                this.mMSTDField = value;
            }
        }
        
        public string MFRGR
        {
            get
            {
                return this.mFRGRField;
            }
            set
            {
                this.mFRGRField = value;
            }
        }
        
        public string FVIDK
        {
            get
            {
                return this.fVIDKField;
            }
            set
            {
                this.fVIDKField = value;
            }
        }
        
        public string INDUS
        {
            get
            {
                return this.iNDUSField;
            }
            set
            {
                this.iNDUSField = value;
            }
        }
        
        public string MOWNR
        {
            get
            {
                return this.mOWNRField;
            }
            set
            {
                this.mOWNRField = value;
            }
        }
        
        public string MOGRU
        {
            get
            {
                return this.mOGRUField;
            }
            set
            {
                this.mOGRUField = value;
            }
        }
        
        public string CASNR
        {
            get
            {
                return this.cASNRField;
            }
            set
            {
                this.cASNRField = value;
            }
        }
        
        public string GPNUM
        {
            get
            {
                return this.gPNUMField;
            }
            set
            {
                this.gPNUMField = value;
            }
        }
        
        public string STEUC
        {
            get
            {
                return this.sTEUCField;
            }
            set
            {
                this.sTEUCField = value;
            }
        }
        
        public string FABKZ
        {
            get
            {
                return this.fABKZField;
            }
            set
            {
                this.fABKZField = value;
            }
        }
        
        public string MATGR
        {
            get
            {
                return this.mATGRField;
            }
            set
            {
                this.mATGRField = value;
            }
        }
        
        public string LOGGR
        {
            get
            {
                return this.lOGGRField;
            }
            set
            {
                this.lOGGRField = value;
            }
        }
        
        public string VSPVB
        {
            get
            {
                return this.vSPVBField;
            }
            set
            {
                this.vSPVBField = value;
            }
        }
        
        public string DPLFS
        {
            get
            {
                return this.dPLFSField;
            }
            set
            {
                this.dPLFSField = value;
            }
        }
        
        public string DPLPU
        {
            get
            {
                return this.dPLPUField;
            }
            set
            {
                this.dPLPUField = value;
            }
        }
        
        public string DPLHO
        {
            get
            {
                return this.dPLHOField;
            }
            set
            {
                this.dPLHOField = value;
            }
        }
        
        public string MINLS
        {
            get
            {
                return this.mINLSField;
            }
            set
            {
                this.mINLSField = value;
            }
        }
        
        public string MAXLS
        {
            get
            {
                return this.mAXLSField;
            }
            set
            {
                this.mAXLSField = value;
            }
        }
        
        public string FIXLS
        {
            get
            {
                return this.fIXLSField;
            }
            set
            {
                this.fIXLSField = value;
            }
        }
        
        public string LTINC
        {
            get
            {
                return this.lTINCField;
            }
            set
            {
                this.lTINCField = value;
            }
        }
        
        public string COMPL
        {
            get
            {
                return this.cOMPLField;
            }
            set
            {
                this.cOMPLField = value;
            }
        }
        
        public string CONVT
        {
            get
            {
                return this.cONVTField;
            }
            set
            {
                this.cONVTField = value;
            }
        }
        
        public string FPRFM
        {
            get
            {
                return this.fPRFMField;
            }
            set
            {
                this.fPRFMField = value;
            }
        }
        
        public string SHPRO
        {
            get
            {
                return this.sHPROField;
            }
            set
            {
                this.sHPROField = value;
            }
        }
        
        public string FXPRU
        {
            get
            {
                return this.fXPRUField;
            }
            set
            {
                this.fXPRUField = value;
            }
        }
        
        public string KZPSP
        {
            get
            {
                return this.kZPSPField;
            }
            set
            {
                this.kZPSPField = value;
            }
        }
        
        public string OCMPF
        {
            get
            {
                return this.oCMPFField;
            }
            set
            {
                this.oCMPFField = value;
            }
        }
        
        public string APOKZ
        {
            get
            {
                return this.aPOKZField;
            }
            set
            {
                this.aPOKZField = value;
            }
        }
        
        public string AHDIS
        {
            get
            {
                return this.aHDISField;
            }
            set
            {
                this.aHDISField = value;
            }
        }
        
        public string EISLO
        {
            get
            {
                return this.eISLOField;
            }
            set
            {
                this.eISLOField = value;
            }
        }
        
        public string NCOST
        {
            get
            {
                return this.nCOSTField;
            }
            set
            {
                this.nCOSTField = value;
            }
        }
        
        public string MEGRU
        {
            get
            {
                return this.mEGRUField;
            }
            set
            {
                this.mEGRUField = value;
            }
        }
        
        public string ROTATION_DATE
        {
            get
            {
                return this.rOTATION_DATEField;
            }
            set
            {
                this.rOTATION_DATEField = value;
            }
        }
        
        public string UCHKZ
        {
            get
            {
                return this.uCHKZField;
            }
            set
            {
                this.uCHKZField = value;
            }
        }
        
        public string UCMAT
        {
            get
            {
                return this.uCMATField;
            }
            set
            {
                this.uCMATField = value;
            }
        }
        
        public string IUID_RELEVANT
        {
            get
            {
                return this.iUID_RELEVANTField;
            }
            set
            {
                this.iUID_RELEVANTField = value;
            }
        }
        
        public string IUID_TYPE
        {
            get
            {
                return this.iUID_TYPEField;
            }
            set
            {
                this.iUID_TYPEField = value;
            }
        }
        
        public string UID_IEA
        {
            get
            {
                return this.uID_IEAField;
            }
            set
            {
                this.uID_IEAField = value;
            }
        }
        [XmlElement("Z1MARCM")]
        public ZMATMAS5Z1MARCM[] Z1MARCM
        {
            get
            {
                return this.z1MARCMField;
            }
            set
            {
                this.z1MARCMField = value;
            }
        }
        
        public ZMATMAS5E1MARC1 E1MARC1
        {
            get
            {
                return this.e1MARC1Field;
            }
            set
            {
                this.e1MARC1Field = value;
            }
        }
        [XmlElement("E1MARDM")]
        public ZMATMAS5E1MARDM[] E1MARDM
        {
            get
            {
                return this.e1MARDMField;
            }
            set
            {
                this.e1MARDMField = value;
            }
        }
        
        public ZMATMAS5E1MFHMM E1MFHMM
        {
            get
            {
                return this.e1MFHMMField;
            }
            set
            {
                this.e1MFHMMField = value;
            }
        }
        
        public ZMATMAS5E1MPGDM E1MPGDM
        {
            get
            {
                return this.e1MPGDMField;
            }
            set
            {
                this.e1MPGDMField = value;
            }
        }
        
        public ZMATMAS5E1MPOPM E1MPOPM
        {
            get
            {
                return this.e1MPOPMField;
            }
            set
            {
                this.e1MPOPMField = value;
            }
        }
        [XmlElement("E1MPRWM")]
        public ZMATMAS5E1MPRWM[] E1MPRWM
        {
            get
            {
                return this.e1MPRWMField;
            }
            set
            {
                this.e1MPRWMField = value;
            }
        }
        [XmlElement("E1MVEGM")]
        public ZMATMAS5E1MVEGM[] E1MVEGM
        {
            get
            {
                return this.e1MVEGMField;
            }
            set
            {
                this.e1MVEGMField = value;
            }
        }
        [XmlElement("E1MVEUM")]
        public ZMATMAS5E1MVEUM[] E1MVEUM
        {
            get
            {
                return this.e1MVEUMField;
            }
            set
            {
                this.e1MVEUMField = value;
            }
        }
        [XmlElement("E1MKALM")]
        public ZMATMAS5E1MKALM[] E1MKALM
        {
            get
            {
                return this.e1MKALMField;
            }
            set
            {
                this.e1MKALMField = value;
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




    [XmlType(TypeName = "ZMATMAS5.Z1MARCM")]
    public partial class ZMATMAS5Z1MARCM
    {
        private string zZPOLICYField;
        private string sEGMENTField;
        public ZMATMAS5Z1MARCM()
        {
            this.sEGMENTField = "1";
        }
        
        public string ZZPOLICY
        {
            get
            {
                return this.zZPOLICYField;
            }
            set
            {
                this.zZPOLICYField = value;
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
    public partial class ZMATMAS5E1MARDM
    {
        private string mSGFNField;
        private string lGORTField;
        private string pSTATField;
        private string lVORMField;
        private string dISKZField;
        private string lSOBSField;
        private string lMINBField;
        private string lBSTFField;
        private string hERKLField;
        private string eXPPGField;
        private string eXVERField;
        private string lGPBEField;
        private string pRCTLField;
        private string lWMKBField;
        private string bSKRFField;
        private string sEGMENTField;
        public ZMATMAS5E1MARDM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string LGORT
        {
            get
            {
                return this.lGORTField;
            }
            set
            {
                this.lGORTField = value;
            }
        }
        
        public string PSTAT
        {
            get
            {
                return this.pSTATField;
            }
            set
            {
                this.pSTATField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string DISKZ
        {
            get
            {
                return this.dISKZField;
            }
            set
            {
                this.dISKZField = value;
            }
        }
        
        public string LSOBS
        {
            get
            {
                return this.lSOBSField;
            }
            set
            {
                this.lSOBSField = value;
            }
        }
        
        public string LMINB
        {
            get
            {
                return this.lMINBField;
            }
            set
            {
                this.lMINBField = value;
            }
        }
        
        public string LBSTF
        {
            get
            {
                return this.lBSTFField;
            }
            set
            {
                this.lBSTFField = value;
            }
        }
        
        public string HERKL
        {
            get
            {
                return this.hERKLField;
            }
            set
            {
                this.hERKLField = value;
            }
        }
        
        public string EXPPG
        {
            get
            {
                return this.eXPPGField;
            }
            set
            {
                this.eXPPGField = value;
            }
        }
        
        public string EXVER
        {
            get
            {
                return this.eXVERField;
            }
            set
            {
                this.eXVERField = value;
            }
        }
        
        public string LGPBE
        {
            get
            {
                return this.lGPBEField;
            }
            set
            {
                this.lGPBEField = value;
            }
        }
        
        public string PRCTL
        {
            get
            {
                return this.pRCTLField;
            }
            set
            {
                this.pRCTLField = value;
            }
        }
        
        public string LWMKB
        {
            get
            {
                return this.lWMKBField;
            }
            set
            {
                this.lWMKBField = value;
            }
        }
        
        public string BSKRF
        {
            get
            {
                return this.bSKRFField;
            }
            set
            {
                this.bSKRFField = value;
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
    public partial class ZMATMAS5E1MFHMM
    {
        private string mSGFNField;
        private string oBJTYField;
        private string oBJIDField;
        private string zAEHLField;
        private string oBJTY_VField;
        private string oBJID_VField;
        private string kZKBLField;
        private string sTEUFField;
        private string sTEUF_REFField;
        private string fGRU1Field;
        private string fGRU2Field;
        private string pLANVField;
        private string kTSCHField;
        private string kTSCH_REFField;
        private string bZOFFBField;
        private string bZOFFB_REFField;
        private string oFFSTBField;
        private string eHOFFBField;
        private string oFFSTB_REFField;
        private string bZOFFEField;
        private string bZOFFE_REFField;
        private string oFFSTEField;
        private string eHOFFEField;
        private string oFFSTE_REFField;
        private string mGFORMField;
        private string mGFORM_REFField;
        private string eWFORMField;
        private string eWFORM_REFField;
        private string pAR01Field;
        private string pAR02Field;
        private string pAR03Field;
        private string pAR04Field;
        private string pAR05Field;
        private string pAR06Field;
        private string pARU1Field;
        private string pARU2Field;
        private string pARU3Field;
        private string pARU4Field;
        private string pARU5Field;
        private string pARU6Field;
        private string pARV1Field;
        private string pARV2Field;
        private string pARV3Field;
        private string pARV4Field;
        private string pARV5Field;
        private string pARV6Field;
        private string sEGMENTField;
        public ZMATMAS5E1MFHMM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string OBJTY
        {
            get
            {
                return this.oBJTYField;
            }
            set
            {
                this.oBJTYField = value;
            }
        }
        
        public string OBJID
        {
            get
            {
                return this.oBJIDField;
            }
            set
            {
                this.oBJIDField = value;
            }
        }
        
        public string ZAEHL
        {
            get
            {
                return this.zAEHLField;
            }
            set
            {
                this.zAEHLField = value;
            }
        }
        
        public string OBJTY_V
        {
            get
            {
                return this.oBJTY_VField;
            }
            set
            {
                this.oBJTY_VField = value;
            }
        }
        
        public string OBJID_V
        {
            get
            {
                return this.oBJID_VField;
            }
            set
            {
                this.oBJID_VField = value;
            }
        }
        
        public string KZKBL
        {
            get
            {
                return this.kZKBLField;
            }
            set
            {
                this.kZKBLField = value;
            }
        }
        
        public string STEUF
        {
            get
            {
                return this.sTEUFField;
            }
            set
            {
                this.sTEUFField = value;
            }
        }
        
        public string STEUF_REF
        {
            get
            {
                return this.sTEUF_REFField;
            }
            set
            {
                this.sTEUF_REFField = value;
            }
        }
        
        public string FGRU1
        {
            get
            {
                return this.fGRU1Field;
            }
            set
            {
                this.fGRU1Field = value;
            }
        }
        
        public string FGRU2
        {
            get
            {
                return this.fGRU2Field;
            }
            set
            {
                this.fGRU2Field = value;
            }
        }
        
        public string PLANV
        {
            get
            {
                return this.pLANVField;
            }
            set
            {
                this.pLANVField = value;
            }
        }
        
        public string KTSCH
        {
            get
            {
                return this.kTSCHField;
            }
            set
            {
                this.kTSCHField = value;
            }
        }
        
        public string KTSCH_REF
        {
            get
            {
                return this.kTSCH_REFField;
            }
            set
            {
                this.kTSCH_REFField = value;
            }
        }
        
        public string BZOFFB
        {
            get
            {
                return this.bZOFFBField;
            }
            set
            {
                this.bZOFFBField = value;
            }
        }
        
        public string BZOFFB_REF
        {
            get
            {
                return this.bZOFFB_REFField;
            }
            set
            {
                this.bZOFFB_REFField = value;
            }
        }
        
        public string OFFSTB
        {
            get
            {
                return this.oFFSTBField;
            }
            set
            {
                this.oFFSTBField = value;
            }
        }
        
        public string EHOFFB
        {
            get
            {
                return this.eHOFFBField;
            }
            set
            {
                this.eHOFFBField = value;
            }
        }
        
        public string OFFSTB_REF
        {
            get
            {
                return this.oFFSTB_REFField;
            }
            set
            {
                this.oFFSTB_REFField = value;
            }
        }
        
        public string BZOFFE
        {
            get
            {
                return this.bZOFFEField;
            }
            set
            {
                this.bZOFFEField = value;
            }
        }
        
        public string BZOFFE_REF
        {
            get
            {
                return this.bZOFFE_REFField;
            }
            set
            {
                this.bZOFFE_REFField = value;
            }
        }
        
        public string OFFSTE
        {
            get
            {
                return this.oFFSTEField;
            }
            set
            {
                this.oFFSTEField = value;
            }
        }
        
        public string EHOFFE
        {
            get
            {
                return this.eHOFFEField;
            }
            set
            {
                this.eHOFFEField = value;
            }
        }
        
        public string OFFSTE_REF
        {
            get
            {
                return this.oFFSTE_REFField;
            }
            set
            {
                this.oFFSTE_REFField = value;
            }
        }
        
        public string MGFORM
        {
            get
            {
                return this.mGFORMField;
            }
            set
            {
                this.mGFORMField = value;
            }
        }
        
        public string MGFORM_REF
        {
            get
            {
                return this.mGFORM_REFField;
            }
            set
            {
                this.mGFORM_REFField = value;
            }
        }
        
        public string EWFORM
        {
            get
            {
                return this.eWFORMField;
            }
            set
            {
                this.eWFORMField = value;
            }
        }
        
        public string EWFORM_REF
        {
            get
            {
                return this.eWFORM_REFField;
            }
            set
            {
                this.eWFORM_REFField = value;
            }
        }
        
        public string PAR01
        {
            get
            {
                return this.pAR01Field;
            }
            set
            {
                this.pAR01Field = value;
            }
        }
        
        public string PAR02
        {
            get
            {
                return this.pAR02Field;
            }
            set
            {
                this.pAR02Field = value;
            }
        }
        
        public string PAR03
        {
            get
            {
                return this.pAR03Field;
            }
            set
            {
                this.pAR03Field = value;
            }
        }
        
        public string PAR04
        {
            get
            {
                return this.pAR04Field;
            }
            set
            {
                this.pAR04Field = value;
            }
        }
        
        public string PAR05
        {
            get
            {
                return this.pAR05Field;
            }
            set
            {
                this.pAR05Field = value;
            }
        }
        
        public string PAR06
        {
            get
            {
                return this.pAR06Field;
            }
            set
            {
                this.pAR06Field = value;
            }
        }
        
        public string PARU1
        {
            get
            {
                return this.pARU1Field;
            }
            set
            {
                this.pARU1Field = value;
            }
        }
        
        public string PARU2
        {
            get
            {
                return this.pARU2Field;
            }
            set
            {
                this.pARU2Field = value;
            }
        }
        
        public string PARU3
        {
            get
            {
                return this.pARU3Field;
            }
            set
            {
                this.pARU3Field = value;
            }
        }
        
        public string PARU4
        {
            get
            {
                return this.pARU4Field;
            }
            set
            {
                this.pARU4Field = value;
            }
        }
        
        public string PARU5
        {
            get
            {
                return this.pARU5Field;
            }
            set
            {
                this.pARU5Field = value;
            }
        }
        
        public string PARU6
        {
            get
            {
                return this.pARU6Field;
            }
            set
            {
                this.pARU6Field = value;
            }
        }
        
        public string PARV1
        {
            get
            {
                return this.pARV1Field;
            }
            set
            {
                this.pARV1Field = value;
            }
        }
        
        public string PARV2
        {
            get
            {
                return this.pARV2Field;
            }
            set
            {
                this.pARV2Field = value;
            }
        }
        
        public string PARV3
        {
            get
            {
                return this.pARV3Field;
            }
            set
            {
                this.pARV3Field = value;
            }
        }
        
        public string PARV4
        {
            get
            {
                return this.pARV4Field;
            }
            set
            {
                this.pARV4Field = value;
            }
        }
        
        public string PARV5
        {
            get
            {
                return this.pARV5Field;
            }
            set
            {
                this.pARV5Field = value;
            }
        }
        
        public string PARV6
        {
            get
            {
                return this.pARV6Field;
            }
            set
            {
                this.pARV6Field = value;
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
    public partial class ZMATMAS5E1MPGDM
    {
        private string mSGFNField;
        private string pRGRPField;
        private string pRWRKField;
        private string uMREFField;
        private string pRGRP_EXTERNALField;
        private string pRGRP_VERSIONField;
        private string pRGRP_GUIDField;
        private string sEGMENTField;
        public ZMATMAS5E1MPGDM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string PRGRP
        {
            get
            {
                return this.pRGRPField;
            }
            set
            {
                this.pRGRPField = value;
            }
        }
        
        public string PRWRK
        {
            get
            {
                return this.pRWRKField;
            }
            set
            {
                this.pRWRKField = value;
            }
        }
        
        public string UMREF
        {
            get
            {
                return this.uMREFField;
            }
            set
            {
                this.uMREFField = value;
            }
        }
        
        public string PRGRP_EXTERNAL
        {
            get
            {
                return this.pRGRP_EXTERNALField;
            }
            set
            {
                this.pRGRP_EXTERNALField = value;
            }
        }
        
        public string PRGRP_VERSION
        {
            get
            {
                return this.pRGRP_VERSIONField;
            }
            set
            {
                this.pRGRP_VERSIONField = value;
            }
        }
        
        public string PRGRP_GUID
        {
            get
            {
                return this.pRGRP_GUIDField;
            }
            set
            {
                this.pRGRP_GUIDField = value;
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
    public partial class ZMATMAS5E1MKALM
    {
        private string mSGFNField;
        private string vERIDField;
        private string bDATUField;
        private string aDATUField;
        private string sTLALField;
        private string sTLANField;
        private string pLNTYField;
        private string pLNNRField;
        private string aLNALField;
        private string bESKZField;
        private string sOBSLField;
        private string lOSGRField;
        private string mDV01Field;
        private string mDV02Field;
        private string tEXT1Field;
        private string eWAHRField;
        private string vERTOField;
        private string sERKZField;
        private string bSTMIField;
        private string bSTMAField;
        private string rGEKZField;
        private string aLORTField;
        private string pLTYGField;
        private string pLNNGField;
        private string aLNAGField;
        private string pLTYMField;
        private string pLNNMField;
        private string aLNAMField;
        private string cSPLTField;
        private string mATKOField;
        private string eLPROField;
        private string pRVBEField;
        private string mATKO_EXTERNALField;
        private string mATKO_VERSIONField;
        private string mATKO_GUIDField;
        private string sEGMENTField;
        public ZMATMAS5E1MKALM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string VERID
        {
            get
            {
                return this.vERIDField;
            }
            set
            {
                this.vERIDField = value;
            }
        }
        
        public string BDATU
        {
            get
            {
                return this.bDATUField;
            }
            set
            {
                this.bDATUField = value;
            }
        }
        
        public string ADATU
        {
            get
            {
                return this.aDATUField;
            }
            set
            {
                this.aDATUField = value;
            }
        }
        
        public string STLAL
        {
            get
            {
                return this.sTLALField;
            }
            set
            {
                this.sTLALField = value;
            }
        }
        
        public string STLAN
        {
            get
            {
                return this.sTLANField;
            }
            set
            {
                this.sTLANField = value;
            }
        }
        
        public string PLNTY
        {
            get
            {
                return this.pLNTYField;
            }
            set
            {
                this.pLNTYField = value;
            }
        }
        
        public string PLNNR
        {
            get
            {
                return this.pLNNRField;
            }
            set
            {
                this.pLNNRField = value;
            }
        }
        
        public string ALNAL
        {
            get
            {
                return this.aLNALField;
            }
            set
            {
                this.aLNALField = value;
            }
        }
        
        public string BESKZ
        {
            get
            {
                return this.bESKZField;
            }
            set
            {
                this.bESKZField = value;
            }
        }
        
        public string SOBSL
        {
            get
            {
                return this.sOBSLField;
            }
            set
            {
                this.sOBSLField = value;
            }
        }
        
        public string LOSGR
        {
            get
            {
                return this.lOSGRField;
            }
            set
            {
                this.lOSGRField = value;
            }
        }
        
        public string MDV01
        {
            get
            {
                return this.mDV01Field;
            }
            set
            {
                this.mDV01Field = value;
            }
        }
        
        public string MDV02
        {
            get
            {
                return this.mDV02Field;
            }
            set
            {
                this.mDV02Field = value;
            }
        }
        
        public string TEXT1
        {
            get
            {
                return this.tEXT1Field;
            }
            set
            {
                this.tEXT1Field = value;
            }
        }
        
        public string EWAHR
        {
            get
            {
                return this.eWAHRField;
            }
            set
            {
                this.eWAHRField = value;
            }
        }
        
        public string VERTO
        {
            get
            {
                return this.vERTOField;
            }
            set
            {
                this.vERTOField = value;
            }
        }
        
        public string SERKZ
        {
            get
            {
                return this.sERKZField;
            }
            set
            {
                this.sERKZField = value;
            }
        }
        
        public string BSTMI
        {
            get
            {
                return this.bSTMIField;
            }
            set
            {
                this.bSTMIField = value;
            }
        }
        
        public string BSTMA
        {
            get
            {
                return this.bSTMAField;
            }
            set
            {
                this.bSTMAField = value;
            }
        }
        
        public string RGEKZ
        {
            get
            {
                return this.rGEKZField;
            }
            set
            {
                this.rGEKZField = value;
            }
        }
        
        public string ALORT
        {
            get
            {
                return this.aLORTField;
            }
            set
            {
                this.aLORTField = value;
            }
        }
        
        public string PLTYG
        {
            get
            {
                return this.pLTYGField;
            }
            set
            {
                this.pLTYGField = value;
            }
        }
        
        public string PLNNG
        {
            get
            {
                return this.pLNNGField;
            }
            set
            {
                this.pLNNGField = value;
            }
        }
        
        public string ALNAG
        {
            get
            {
                return this.aLNAGField;
            }
            set
            {
                this.aLNAGField = value;
            }
        }
        
        public string PLTYM
        {
            get
            {
                return this.pLTYMField;
            }
            set
            {
                this.pLTYMField = value;
            }
        }
        
        public string PLNNM
        {
            get
            {
                return this.pLNNMField;
            }
            set
            {
                this.pLNNMField = value;
            }
        }
        
        public string ALNAM
        {
            get
            {
                return this.aLNAMField;
            }
            set
            {
                this.aLNAMField = value;
            }
        }
        
        public string CSPLT
        {
            get
            {
                return this.cSPLTField;
            }
            set
            {
                this.cSPLTField = value;
            }
        }
        
        public string MATKO
        {
            get
            {
                return this.mATKOField;
            }
            set
            {
                this.mATKOField = value;
            }
        }
        
        public string ELPRO
        {
            get
            {
                return this.eLPROField;
            }
            set
            {
                this.eLPROField = value;
            }
        }
        
        public string PRVBE
        {
            get
            {
                return this.pRVBEField;
            }
            set
            {
                this.pRVBEField = value;
            }
        }
        
        public string MATKO_EXTERNAL
        {
            get
            {
                return this.mATKO_EXTERNALField;
            }
            set
            {
                this.mATKO_EXTERNALField = value;
            }
        }
        
        public string MATKO_VERSION
        {
            get
            {
                return this.mATKO_VERSIONField;
            }
            set
            {
                this.mATKO_VERSIONField = value;
            }
        }
        
        public string MATKO_GUID
        {
            get
            {
                return this.mATKO_GUIDField;
            }
            set
            {
                this.mATKO_GUIDField = value;
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
    public partial class ZMATMAS5E1MARMM
    {
        private string mSGFNField;
        private string mEINHField;
        private string uMREZField;
        private string uMRENField;
        private string eAN11Field;
        private string nUMTPField;
        private string lAENGField;
        private string bREITField;
        private string hOEHEField;
        private string mEABMField;
        private string vOLUMField;
        private string vOLEHField;
        private string bRGEWField;
        private string gEWEIField;
        private string mESUBField;
        private string gTIN_VARIANTField;
        private string _CWM_TY2TQField;
        private string nEST_FTRField;
        private string mAX_STACKField;
        private string cAPAUSEField;
        private ZMATMAS5E1MEANM[] e1MEANMField;
        private string sEGMENTField;
        public ZMATMAS5E1MARMM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string MEINH
        {
            get
            {
                return this.mEINHField;
            }
            set
            {
                this.mEINHField = value;
            }
        }
        
        public string UMREZ
        {
            get
            {
                return this.uMREZField;
            }
            set
            {
                this.uMREZField = value;
            }
        }
        
        public string UMREN
        {
            get
            {
                return this.uMRENField;
            }
            set
            {
                this.uMRENField = value;
            }
        }
        
        public string EAN11
        {
            get
            {
                return this.eAN11Field;
            }
            set
            {
                this.eAN11Field = value;
            }
        }
        
        public string NUMTP
        {
            get
            {
                return this.nUMTPField;
            }
            set
            {
                this.nUMTPField = value;
            }
        }
        
        public string LAENG
        {
            get
            {
                return this.lAENGField;
            }
            set
            {
                this.lAENGField = value;
            }
        }
        
        public string BREIT
        {
            get
            {
                return this.bREITField;
            }
            set
            {
                this.bREITField = value;
            }
        }
        
        public string HOEHE
        {
            get
            {
                return this.hOEHEField;
            }
            set
            {
                this.hOEHEField = value;
            }
        }
        
        public string MEABM
        {
            get
            {
                return this.mEABMField;
            }
            set
            {
                this.mEABMField = value;
            }
        }
        
        public string VOLUM
        {
            get
            {
                return this.vOLUMField;
            }
            set
            {
                this.vOLUMField = value;
            }
        }
        
        public string VOLEH
        {
            get
            {
                return this.vOLEHField;
            }
            set
            {
                this.vOLEHField = value;
            }
        }
        
        public string BRGEW
        {
            get
            {
                return this.bRGEWField;
            }
            set
            {
                this.bRGEWField = value;
            }
        }
        
        public string GEWEI
        {
            get
            {
                return this.gEWEIField;
            }
            set
            {
                this.gEWEIField = value;
            }
        }
        
        public string MESUB
        {
            get
            {
                return this.mESUBField;
            }
            set
            {
                this.mESUBField = value;
            }
        }
        
        public string GTIN_VARIANT
        {
            get
            {
                return this.gTIN_VARIANTField;
            }
            set
            {
                this.gTIN_VARIANTField = value;
            }
        }
        [XmlElement("_-CWM_-TY2TQ")]
        public string _CWM_TY2TQ
        {
            get
            {
                return this._CWM_TY2TQField;
            }
            set
            {
                this._CWM_TY2TQField = value;
            }
        }
        
        public string NEST_FTR
        {
            get
            {
                return this.nEST_FTRField;
            }
            set
            {
                this.nEST_FTRField = value;
            }
        }
        
        public string MAX_STACK
        {
            get
            {
                return this.mAX_STACKField;
            }
            set
            {
                this.mAX_STACKField = value;
            }
        }
        
        public string CAPAUSE
        {
            get
            {
                return this.cAPAUSEField;
            }
            set
            {
                this.cAPAUSEField = value;
            }
        }
        [XmlElement("E1MEANM")]
        public ZMATMAS5E1MEANM[] E1MEANM
        {
            get
            {
                return this.e1MEANMField;
            }
            set
            {
                this.e1MEANMField = value;
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
    public partial class ZMATMAS5E1MBEWM
    {
        private string mSGFNField;
        private string bWKEYField;
        private string bWTARField;
        private string lVORMField;
        private string vPRSVField;
        private string vERPRField;
        private string sTPRSField;
        private string pEINHField;
        private string bKLASField;
        private string vMVPRField;
        private string vMVERField;
        private string vMSTPField;
        private string vMPEIField;
        private string vMBKLField;
        private string vJVPRField;
        private string vJVERField;
        private string vJSTPField;
        private string lFGJAField;
        private string lFMONField;
        private string bWTTYField;
        private string zKPRSField;
        private string zKDATField;
        private string bWPRSField;
        private string bWPRHField;
        private string vJBWSField;
        private string vJBWHField;
        private string vVJLBField;
        private string vVMLBField;
        private string vVSALField;
        private string zPLPRField;
        private string zPLP1Field;
        private string zPLP2Field;
        private string zPLP3Field;
        private string zPLD1Field;
        private string zPLD2Field;
        private string zPLD3Field;
        private string kALKZField;
        private string kALKLField;
        private string xLIFOField;
        private string mYPOLField;
        private string bWPH1Field;
        private string bWPS1Field;
        private string aBWKZField;
        private string pSTATField;
        private string kALN1Field;
        private string kALNRField;
        private string bWVA1Field;
        private string bWVA2Field;
        private string bWVA3Field;
        private string vERS1Field;
        private string vERS2Field;
        private string vERS3Field;
        private string hRKFTField;
        private string kOSGRField;
        private string pPRDZField;
        private string pPRDLField;
        private string pPRDVField;
        private string pDATZField;
        private string pDATLField;
        private string pDATVField;
        private string eKALRField;
        private string vPLPRField;
        private string mLMAAField;
        private string mLASTField;
        private string vJBKLField;
        private string vJPEIField;
        private string hKMATField;
        private string eKLASField;
        private string qKLASField;
        private string mTUSEField;
        private string mTORGField;
        private string oWNPRField;
        private string bWPEIField;
        private string cON_FIN_VALLEVELField;
        private string cON_FIN_VALPROCField;
        private string sEGMENTField;
        public ZMATMAS5E1MBEWM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string BWKEY
        {
            get
            {
                return this.bWKEYField;
            }
            set
            {
                this.bWKEYField = value;
            }
        }
        
        public string BWTAR
        {
            get
            {
                return this.bWTARField;
            }
            set
            {
                this.bWTARField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string VPRSV
        {
            get
            {
                return this.vPRSVField;
            }
            set
            {
                this.vPRSVField = value;
            }
        }
        
        public string VERPR
        {
            get
            {
                return this.vERPRField;
            }
            set
            {
                this.vERPRField = value;
            }
        }
        
        public string STPRS
        {
            get
            {
                return this.sTPRSField;
            }
            set
            {
                this.sTPRSField = value;
            }
        }
        
        public string PEINH
        {
            get
            {
                return this.pEINHField;
            }
            set
            {
                this.pEINHField = value;
            }
        }
        
        public string BKLAS
        {
            get
            {
                return this.bKLASField;
            }
            set
            {
                this.bKLASField = value;
            }
        }
        
        public string VMVPR
        {
            get
            {
                return this.vMVPRField;
            }
            set
            {
                this.vMVPRField = value;
            }
        }
        
        public string VMVER
        {
            get
            {
                return this.vMVERField;
            }
            set
            {
                this.vMVERField = value;
            }
        }
        
        public string VMSTP
        {
            get
            {
                return this.vMSTPField;
            }
            set
            {
                this.vMSTPField = value;
            }
        }
        
        public string VMPEI
        {
            get
            {
                return this.vMPEIField;
            }
            set
            {
                this.vMPEIField = value;
            }
        }
        
        public string VMBKL
        {
            get
            {
                return this.vMBKLField;
            }
            set
            {
                this.vMBKLField = value;
            }
        }
        
        public string VJVPR
        {
            get
            {
                return this.vJVPRField;
            }
            set
            {
                this.vJVPRField = value;
            }
        }
        
        public string VJVER
        {
            get
            {
                return this.vJVERField;
            }
            set
            {
                this.vJVERField = value;
            }
        }
        
        public string VJSTP
        {
            get
            {
                return this.vJSTPField;
            }
            set
            {
                this.vJSTPField = value;
            }
        }
        
        public string LFGJA
        {
            get
            {
                return this.lFGJAField;
            }
            set
            {
                this.lFGJAField = value;
            }
        }
        
        public string LFMON
        {
            get
            {
                return this.lFMONField;
            }
            set
            {
                this.lFMONField = value;
            }
        }
        
        public string BWTTY
        {
            get
            {
                return this.bWTTYField;
            }
            set
            {
                this.bWTTYField = value;
            }
        }
        
        public string ZKPRS
        {
            get
            {
                return this.zKPRSField;
            }
            set
            {
                this.zKPRSField = value;
            }
        }
        
        public string ZKDAT
        {
            get
            {
                return this.zKDATField;
            }
            set
            {
                this.zKDATField = value;
            }
        }
        
        public string BWPRS
        {
            get
            {
                return this.bWPRSField;
            }
            set
            {
                this.bWPRSField = value;
            }
        }
        
        public string BWPRH
        {
            get
            {
                return this.bWPRHField;
            }
            set
            {
                this.bWPRHField = value;
            }
        }
        
        public string VJBWS
        {
            get
            {
                return this.vJBWSField;
            }
            set
            {
                this.vJBWSField = value;
            }
        }
        
        public string VJBWH
        {
            get
            {
                return this.vJBWHField;
            }
            set
            {
                this.vJBWHField = value;
            }
        }
        
        public string VVJLB
        {
            get
            {
                return this.vVJLBField;
            }
            set
            {
                this.vVJLBField = value;
            }
        }
        
        public string VVMLB
        {
            get
            {
                return this.vVMLBField;
            }
            set
            {
                this.vVMLBField = value;
            }
        }
        
        public string VVSAL
        {
            get
            {
                return this.vVSALField;
            }
            set
            {
                this.vVSALField = value;
            }
        }
        
        public string ZPLPR
        {
            get
            {
                return this.zPLPRField;
            }
            set
            {
                this.zPLPRField = value;
            }
        }
        
        public string ZPLP1
        {
            get
            {
                return this.zPLP1Field;
            }
            set
            {
                this.zPLP1Field = value;
            }
        }
        
        public string ZPLP2
        {
            get
            {
                return this.zPLP2Field;
            }
            set
            {
                this.zPLP2Field = value;
            }
        }
        
        public string ZPLP3
        {
            get
            {
                return this.zPLP3Field;
            }
            set
            {
                this.zPLP3Field = value;
            }
        }
        
        public string ZPLD1
        {
            get
            {
                return this.zPLD1Field;
            }
            set
            {
                this.zPLD1Field = value;
            }
        }
        
        public string ZPLD2
        {
            get
            {
                return this.zPLD2Field;
            }
            set
            {
                this.zPLD2Field = value;
            }
        }
        
        public string ZPLD3
        {
            get
            {
                return this.zPLD3Field;
            }
            set
            {
                this.zPLD3Field = value;
            }
        }
        
        public string KALKZ
        {
            get
            {
                return this.kALKZField;
            }
            set
            {
                this.kALKZField = value;
            }
        }
        
        public string KALKL
        {
            get
            {
                return this.kALKLField;
            }
            set
            {
                this.kALKLField = value;
            }
        }
        
        public string XLIFO
        {
            get
            {
                return this.xLIFOField;
            }
            set
            {
                this.xLIFOField = value;
            }
        }
        
        public string MYPOL
        {
            get
            {
                return this.mYPOLField;
            }
            set
            {
                this.mYPOLField = value;
            }
        }
        
        public string BWPH1
        {
            get
            {
                return this.bWPH1Field;
            }
            set
            {
                this.bWPH1Field = value;
            }
        }
        
        public string BWPS1
        {
            get
            {
                return this.bWPS1Field;
            }
            set
            {
                this.bWPS1Field = value;
            }
        }
        
        public string ABWKZ
        {
            get
            {
                return this.aBWKZField;
            }
            set
            {
                this.aBWKZField = value;
            }
        }
        
        public string PSTAT
        {
            get
            {
                return this.pSTATField;
            }
            set
            {
                this.pSTATField = value;
            }
        }
        
        public string KALN1
        {
            get
            {
                return this.kALN1Field;
            }
            set
            {
                this.kALN1Field = value;
            }
        }
        
        public string KALNR
        {
            get
            {
                return this.kALNRField;
            }
            set
            {
                this.kALNRField = value;
            }
        }
        
        public string BWVA1
        {
            get
            {
                return this.bWVA1Field;
            }
            set
            {
                this.bWVA1Field = value;
            }
        }
        
        public string BWVA2
        {
            get
            {
                return this.bWVA2Field;
            }
            set
            {
                this.bWVA2Field = value;
            }
        }
        
        public string BWVA3
        {
            get
            {
                return this.bWVA3Field;
            }
            set
            {
                this.bWVA3Field = value;
            }
        }
        
        public string VERS1
        {
            get
            {
                return this.vERS1Field;
            }
            set
            {
                this.vERS1Field = value;
            }
        }
        
        public string VERS2
        {
            get
            {
                return this.vERS2Field;
            }
            set
            {
                this.vERS2Field = value;
            }
        }
        
        public string VERS3
        {
            get
            {
                return this.vERS3Field;
            }
            set
            {
                this.vERS3Field = value;
            }
        }
        
        public string HRKFT
        {
            get
            {
                return this.hRKFTField;
            }
            set
            {
                this.hRKFTField = value;
            }
        }
        
        public string KOSGR
        {
            get
            {
                return this.kOSGRField;
            }
            set
            {
                this.kOSGRField = value;
            }
        }
        
        public string PPRDZ
        {
            get
            {
                return this.pPRDZField;
            }
            set
            {
                this.pPRDZField = value;
            }
        }
        
        public string PPRDL
        {
            get
            {
                return this.pPRDLField;
            }
            set
            {
                this.pPRDLField = value;
            }
        }
        
        public string PPRDV
        {
            get
            {
                return this.pPRDVField;
            }
            set
            {
                this.pPRDVField = value;
            }
        }
        
        public string PDATZ
        {
            get
            {
                return this.pDATZField;
            }
            set
            {
                this.pDATZField = value;
            }
        }
        
        public string PDATL
        {
            get
            {
                return this.pDATLField;
            }
            set
            {
                this.pDATLField = value;
            }
        }
        
        public string PDATV
        {
            get
            {
                return this.pDATVField;
            }
            set
            {
                this.pDATVField = value;
            }
        }
        
        public string EKALR
        {
            get
            {
                return this.eKALRField;
            }
            set
            {
                this.eKALRField = value;
            }
        }
        
        public string VPLPR
        {
            get
            {
                return this.vPLPRField;
            }
            set
            {
                this.vPLPRField = value;
            }
        }
        
        public string MLMAA
        {
            get
            {
                return this.mLMAAField;
            }
            set
            {
                this.mLMAAField = value;
            }
        }
        
        public string MLAST
        {
            get
            {
                return this.mLASTField;
            }
            set
            {
                this.mLASTField = value;
            }
        }
        
        public string VJBKL
        {
            get
            {
                return this.vJBKLField;
            }
            set
            {
                this.vJBKLField = value;
            }
        }
        
        public string VJPEI
        {
            get
            {
                return this.vJPEIField;
            }
            set
            {
                this.vJPEIField = value;
            }
        }
        
        public string HKMAT
        {
            get
            {
                return this.hKMATField;
            }
            set
            {
                this.hKMATField = value;
            }
        }
        
        public string EKLAS
        {
            get
            {
                return this.eKLASField;
            }
            set
            {
                this.eKLASField = value;
            }
        }
        
        public string QKLAS
        {
            get
            {
                return this.qKLASField;
            }
            set
            {
                this.qKLASField = value;
            }
        }
        
        public string MTUSE
        {
            get
            {
                return this.mTUSEField;
            }
            set
            {
                this.mTUSEField = value;
            }
        }
        
        public string MTORG
        {
            get
            {
                return this.mTORGField;
            }
            set
            {
                this.mTORGField = value;
            }
        }
        
        public string OWNPR
        {
            get
            {
                return this.oWNPRField;
            }
            set
            {
                this.oWNPRField = value;
            }
        }
        
        public string BWPEI
        {
            get
            {
                return this.bWPEIField;
            }
            set
            {
                this.bWPEIField = value;
            }
        }
        
        public string CON_FIN_VALLEVEL
        {
            get
            {
                return this.cON_FIN_VALLEVELField;
            }
            set
            {
                this.cON_FIN_VALLEVELField = value;
            }
        }
        
        public string CON_FIN_VALPROC
        {
            get
            {
                return this.cON_FIN_VALPROCField;
            }
            set
            {
                this.cON_FIN_VALPROCField = value;
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
    public partial class ZMATMAS5E1MVKEM
    {
        private string mSGFNField;
        private string vKORGField;
        private string vTWEGField;
        private string lVORMField;
        private string vERSGField;
        private string bONUSField;
        private string pROVGField;
        private string sKTOFField;
        private string vMSTAField;
        private string vMSTDField;
        private string aUMNGField;
        private string lFMNGField;
        private string eFMNGField;
        private string sCMNGField;
        private string sCHMEField;
        private string vRKMEField;
        private string mTPOSField;
        private string dWERKField;
        private string pRODHField;
        private string pMATNField;
        private string kONDMField;
        private string kTGRMField;
        private string mVGR1Field;
        private string mVGR2Field;
        private string mVGR3Field;
        private string mVGR4Field;
        private string mVGR5Field;
        private string sSTUFField;
        private string pFLKSField;
        private string lSTFLField;
        private string lSTVZField;
        private string lSTAKField;
        private string pRAT1Field;
        private string pRAT2Field;
        private string pRAT3Field;
        private string pRAT4Field;
        private string pRAT5Field;
        private string pRAT6Field;
        private string pRAT7Field;
        private string pRAT8Field;
        private string pRAT9Field;
        private string pRATAField;
        private string vAVMEField;
        private string rDPRFField;
        private string mEGRUField;
        private string pMATN_EXTERNALField;
        private string pMATN_VERSIONField;
        private string pMATN_GUIDField;
        private string sEGMENTField;
        public ZMATMAS5E1MVKEM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string VKORG
        {
            get
            {
                return this.vKORGField;
            }
            set
            {
                this.vKORGField = value;
            }
        }
        
        public string VTWEG
        {
            get
            {
                return this.vTWEGField;
            }
            set
            {
                this.vTWEGField = value;
            }
        }
        
        public string LVORM
        {
            get
            {
                return this.lVORMField;
            }
            set
            {
                this.lVORMField = value;
            }
        }
        
        public string VERSG
        {
            get
            {
                return this.vERSGField;
            }
            set
            {
                this.vERSGField = value;
            }
        }
        
        public string BONUS
        {
            get
            {
                return this.bONUSField;
            }
            set
            {
                this.bONUSField = value;
            }
        }
        
        public string PROVG
        {
            get
            {
                return this.pROVGField;
            }
            set
            {
                this.pROVGField = value;
            }
        }
        
        public string SKTOF
        {
            get
            {
                return this.sKTOFField;
            }
            set
            {
                this.sKTOFField = value;
            }
        }
        
        public string VMSTA
        {
            get
            {
                return this.vMSTAField;
            }
            set
            {
                this.vMSTAField = value;
            }
        }
        
        public string VMSTD
        {
            get
            {
                return this.vMSTDField;
            }
            set
            {
                this.vMSTDField = value;
            }
        }
        
        public string AUMNG
        {
            get
            {
                return this.aUMNGField;
            }
            set
            {
                this.aUMNGField = value;
            }
        }
        
        public string LFMNG
        {
            get
            {
                return this.lFMNGField;
            }
            set
            {
                this.lFMNGField = value;
            }
        }
        
        public string EFMNG
        {
            get
            {
                return this.eFMNGField;
            }
            set
            {
                this.eFMNGField = value;
            }
        }
        
        public string SCMNG
        {
            get
            {
                return this.sCMNGField;
            }
            set
            {
                this.sCMNGField = value;
            }
        }
        
        public string SCHME
        {
            get
            {
                return this.sCHMEField;
            }
            set
            {
                this.sCHMEField = value;
            }
        }
        
        public string VRKME
        {
            get
            {
                return this.vRKMEField;
            }
            set
            {
                this.vRKMEField = value;
            }
        }
        
        public string MTPOS
        {
            get
            {
                return this.mTPOSField;
            }
            set
            {
                this.mTPOSField = value;
            }
        }
        
        public string DWERK
        {
            get
            {
                return this.dWERKField;
            }
            set
            {
                this.dWERKField = value;
            }
        }
        
        public string PRODH
        {
            get
            {
                return this.pRODHField;
            }
            set
            {
                this.pRODHField = value;
            }
        }
        
        public string PMATN
        {
            get
            {
                return this.pMATNField;
            }
            set
            {
                this.pMATNField = value;
            }
        }
        
        public string KONDM
        {
            get
            {
                return this.kONDMField;
            }
            set
            {
                this.kONDMField = value;
            }
        }
        
        public string KTGRM
        {
            get
            {
                return this.kTGRMField;
            }
            set
            {
                this.kTGRMField = value;
            }
        }
        
        public string MVGR1
        {
            get
            {
                return this.mVGR1Field;
            }
            set
            {
                this.mVGR1Field = value;
            }
        }
        
        public string MVGR2
        {
            get
            {
                return this.mVGR2Field;
            }
            set
            {
                this.mVGR2Field = value;
            }
        }
        
        public string MVGR3
        {
            get
            {
                return this.mVGR3Field;
            }
            set
            {
                this.mVGR3Field = value;
            }
        }
        
        public string MVGR4
        {
            get
            {
                return this.mVGR4Field;
            }
            set
            {
                this.mVGR4Field = value;
            }
        }
        
        public string MVGR5
        {
            get
            {
                return this.mVGR5Field;
            }
            set
            {
                this.mVGR5Field = value;
            }
        }
        
        public string SSTUF
        {
            get
            {
                return this.sSTUFField;
            }
            set
            {
                this.sSTUFField = value;
            }
        }
        
        public string PFLKS
        {
            get
            {
                return this.pFLKSField;
            }
            set
            {
                this.pFLKSField = value;
            }
        }
        
        public string LSTFL
        {
            get
            {
                return this.lSTFLField;
            }
            set
            {
                this.lSTFLField = value;
            }
        }
        
        public string LSTVZ
        {
            get
            {
                return this.lSTVZField;
            }
            set
            {
                this.lSTVZField = value;
            }
        }
        
        public string LSTAK
        {
            get
            {
                return this.lSTAKField;
            }
            set
            {
                this.lSTAKField = value;
            }
        }
        
        public string PRAT1
        {
            get
            {
                return this.pRAT1Field;
            }
            set
            {
                this.pRAT1Field = value;
            }
        }
        
        public string PRAT2
        {
            get
            {
                return this.pRAT2Field;
            }
            set
            {
                this.pRAT2Field = value;
            }
        }
        
        public string PRAT3
        {
            get
            {
                return this.pRAT3Field;
            }
            set
            {
                this.pRAT3Field = value;
            }
        }
        
        public string PRAT4
        {
            get
            {
                return this.pRAT4Field;
            }
            set
            {
                this.pRAT4Field = value;
            }
        }
        
        public string PRAT5
        {
            get
            {
                return this.pRAT5Field;
            }
            set
            {
                this.pRAT5Field = value;
            }
        }
        
        public string PRAT6
        {
            get
            {
                return this.pRAT6Field;
            }
            set
            {
                this.pRAT6Field = value;
            }
        }
        
        public string PRAT7
        {
            get
            {
                return this.pRAT7Field;
            }
            set
            {
                this.pRAT7Field = value;
            }
        }
        
        public string PRAT8
        {
            get
            {
                return this.pRAT8Field;
            }
            set
            {
                this.pRAT8Field = value;
            }
        }
        
        public string PRAT9
        {
            get
            {
                return this.pRAT9Field;
            }
            set
            {
                this.pRAT9Field = value;
            }
        }
        
        public string PRATA
        {
            get
            {
                return this.pRATAField;
            }
            set
            {
                this.pRATAField = value;
            }
        }
        
        public string VAVME
        {
            get
            {
                return this.vAVMEField;
            }
            set
            {
                this.vAVMEField = value;
            }
        }
        
        public string RDPRF
        {
            get
            {
                return this.rDPRFField;
            }
            set
            {
                this.rDPRFField = value;
            }
        }
        
        public string MEGRU
        {
            get
            {
                return this.mEGRUField;
            }
            set
            {
                this.mEGRUField = value;
            }
        }
        
        public string PMATN_EXTERNAL
        {
            get
            {
                return this.pMATN_EXTERNALField;
            }
            set
            {
                this.pMATN_EXTERNALField = value;
            }
        }
        
        public string PMATN_VERSION
        {
            get
            {
                return this.pMATN_VERSIONField;
            }
            set
            {
                this.pMATN_VERSIONField = value;
            }
        }
        
        public string PMATN_GUID
        {
            get
            {
                return this.pMATN_GUIDField;
            }
            set
            {
                this.pMATN_GUIDField = value;
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
    public partial class ZMATMAS5E1CUCFG
    {
        private string pOSEXField;
        private string cONFIG_IDField;
        private string rOOT_IDField;
        private string sCEField;
        private string kBNAMEField;
        private string kBVERSIONField;
        private string cOMPLETEField;
        private string cONSISTENTField;
        private string cFGINFOField;
        private string kBPROFILEField;
        private string kBLANGUAGEField;
        private string cBASE_IDField;
        private string cBASE_ID_TYPEField;
        private ZMATMAS5E1CUINS[] e1CUINSField;
        private ZMATMAS5E1CUVAL[] e1CUVALField;
        private ZMATMAS5E1CUCOM[] e1CUCOMField;
        private string sEGMENTField;
        public ZMATMAS5E1CUCFG()
        {
            this.sEGMENTField = "1";
        }
        
        public string POSEX
        {
            get
            {
                return this.pOSEXField;
            }
            set
            {
                this.pOSEXField = value;
            }
        }
        
        public string CONFIG_ID
        {
            get
            {
                return this.cONFIG_IDField;
            }
            set
            {
                this.cONFIG_IDField = value;
            }
        }
        
        public string ROOT_ID
        {
            get
            {
                return this.rOOT_IDField;
            }
            set
            {
                this.rOOT_IDField = value;
            }
        }
        
        public string SCE
        {
            get
            {
                return this.sCEField;
            }
            set
            {
                this.sCEField = value;
            }
        }
        
        public string KBNAME
        {
            get
            {
                return this.kBNAMEField;
            }
            set
            {
                this.kBNAMEField = value;
            }
        }
        
        public string KBVERSION
        {
            get
            {
                return this.kBVERSIONField;
            }
            set
            {
                this.kBVERSIONField = value;
            }
        }
        
        public string COMPLETE
        {
            get
            {
                return this.cOMPLETEField;
            }
            set
            {
                this.cOMPLETEField = value;
            }
        }
        
        public string CONSISTENT
        {
            get
            {
                return this.cONSISTENTField;
            }
            set
            {
                this.cONSISTENTField = value;
            }
        }
        
        public string CFGINFO
        {
            get
            {
                return this.cFGINFOField;
            }
            set
            {
                this.cFGINFOField = value;
            }
        }
        
        public string KBPROFILE
        {
            get
            {
                return this.kBPROFILEField;
            }
            set
            {
                this.kBPROFILEField = value;
            }
        }
        
        public string KBLANGUAGE
        {
            get
            {
                return this.kBLANGUAGEField;
            }
            set
            {
                this.kBLANGUAGEField = value;
            }
        }
        
        public string CBASE_ID
        {
            get
            {
                return this.cBASE_IDField;
            }
            set
            {
                this.cBASE_IDField = value;
            }
        }
        
        public string CBASE_ID_TYPE
        {
            get
            {
                return this.cBASE_ID_TYPEField;
            }
            set
            {
                this.cBASE_ID_TYPEField = value;
            }
        }
        [XmlElement("E1CUINS")]
        public ZMATMAS5E1CUINS[] E1CUINS
        {
            get
            {
                return this.e1CUINSField;
            }
            set
            {
                this.e1CUINSField = value;
            }
        }
        [XmlElement("E1CUVAL")]
        public ZMATMAS5E1CUVAL[] E1CUVAL
        {
            get
            {
                return this.e1CUVALField;
            }
            set
            {
                this.e1CUVALField = value;
            }
        }
        [XmlElement("E1CUCOM")]
        public ZMATMAS5E1CUCOM[] E1CUCOM
        {
            get
            {
                return this.e1CUCOMField;
            }
            set
            {
                this.e1CUCOMField = value;
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
    public partial class ZMATMAS5E1CUINS
    {
        private string iNST_IDField;
        private string oBJ_TYPEField;
        private string cLASS_TYPEField;
        private string oBJ_KEYField;
        private string oBJ_TXTField;
        private string qUANTITYField;
        private string aUTHORField;
        private string qUANTITY_UNITField;
        private string cOMPLETEField;
        private string cONSISTENTField;
        private string oBJECT_GUIDField;
        private string pERSIST_IDField;
        private string pERSIST_ID_TYPEField;
        private string sEGMENTField;
        public ZMATMAS5E1CUINS()
        {
            this.sEGMENTField = "1";
        }
        
        public string INST_ID
        {
            get
            {
                return this.iNST_IDField;
            }
            set
            {
                this.iNST_IDField = value;
            }
        }
        
        public string OBJ_TYPE
        {
            get
            {
                return this.oBJ_TYPEField;
            }
            set
            {
                this.oBJ_TYPEField = value;
            }
        }
        
        public string CLASS_TYPE
        {
            get
            {
                return this.cLASS_TYPEField;
            }
            set
            {
                this.cLASS_TYPEField = value;
            }
        }
        
        public string OBJ_KEY
        {
            get
            {
                return this.oBJ_KEYField;
            }
            set
            {
                this.oBJ_KEYField = value;
            }
        }
        
        public string OBJ_TXT
        {
            get
            {
                return this.oBJ_TXTField;
            }
            set
            {
                this.oBJ_TXTField = value;
            }
        }
        
        public string QUANTITY
        {
            get
            {
                return this.qUANTITYField;
            }
            set
            {
                this.qUANTITYField = value;
            }
        }
        
        public string AUTHOR
        {
            get
            {
                return this.aUTHORField;
            }
            set
            {
                this.aUTHORField = value;
            }
        }
        
        public string QUANTITY_UNIT
        {
            get
            {
                return this.qUANTITY_UNITField;
            }
            set
            {
                this.qUANTITY_UNITField = value;
            }
        }
        
        public string COMPLETE
        {
            get
            {
                return this.cOMPLETEField;
            }
            set
            {
                this.cOMPLETEField = value;
            }
        }
        
        public string CONSISTENT
        {
            get
            {
                return this.cONSISTENTField;
            }
            set
            {
                this.cONSISTENTField = value;
            }
        }
        
        public string OBJECT_GUID
        {
            get
            {
                return this.oBJECT_GUIDField;
            }
            set
            {
                this.oBJECT_GUIDField = value;
            }
        }
        
        public string PERSIST_ID
        {
            get
            {
                return this.pERSIST_IDField;
            }
            set
            {
                this.pERSIST_IDField = value;
            }
        }
        
        public string PERSIST_ID_TYPE
        {
            get
            {
                return this.pERSIST_ID_TYPEField;
            }
            set
            {
                this.pERSIST_ID_TYPEField = value;
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
    public partial class ZMATMAS5E1CUVAL
    {
        private string iNST_IDField;
        private string cHARCField;
        private string cHARC_TXTField;
        private string vALUEField;
        private string vALUE_TXTField;
        private string aUTHORField;
        private string vALUE_TOField;
        private string vALCODEField;
        private string sEGMENTField;
        public ZMATMAS5E1CUVAL()
        {
            this.sEGMENTField = "1";
        }
        
        public string INST_ID
        {
            get
            {
                return this.iNST_IDField;
            }
            set
            {
                this.iNST_IDField = value;
            }
        }
        
        public string CHARC
        {
            get
            {
                return this.cHARCField;
            }
            set
            {
                this.cHARCField = value;
            }
        }
        
        public string CHARC_TXT
        {
            get
            {
                return this.cHARC_TXTField;
            }
            set
            {
                this.cHARC_TXTField = value;
            }
        }
        
        public string VALUE
        {
            get
            {
                return this.vALUEField;
            }
            set
            {
                this.vALUEField = value;
            }
        }
        
        public string VALUE_TXT
        {
            get
            {
                return this.vALUE_TXTField;
            }
            set
            {
                this.vALUE_TXTField = value;
            }
        }
        
        public string AUTHOR
        {
            get
            {
                return this.aUTHORField;
            }
            set
            {
                this.aUTHORField = value;
            }
        }
        
        public string VALUE_TO
        {
            get
            {
                return this.vALUE_TOField;
            }
            set
            {
                this.vALUE_TOField = value;
            }
        }
        
        public string VALCODE
        {
            get
            {
                return this.vALCODEField;
            }
            set
            {
                this.vALCODEField = value;
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
    public partial class ZMATMAS5E1CUCOM
    {
        private string mSGFNField;
        private string c_PROFILEField;
        private string cLASSTYPEField;
        private string oRGAREASField;
        private string sTATUSField;
        private string bOMAPPLField;
        private string fLAVAILCHField;
        private string bOMEXPLField;
        private string tASKLEXPLField;
        private string iNITSCREENField;
        private string fLASSEMBLYField;
        private string fLRESULTField;
        private string fLMDATAField;
        private string fLCASONLYField;
        private string fLMANCHANGField;
        private string fLHOLDBOMField;
        private string fLDELETEField;
        private string dESIGNField;
        private string nEUTRField;
        private string cHAR_VALUField;
        private string a_LAISOField;
        private string sCOPE_CHARField;
        private string sCOPE_VALUField;
        private string fL_EXCLUDEField;
        private string dISPLAYField;
        private string pRICINGField;
        private string cONFIGURField;
        private string dEFVALU_DEField;
        private string fL_MARKField;
        private string dEFVALU_CCField;
        private string tYPM_SELField;
        private string tYPM_STRAField;
        private string fL_SC_CHARField;
        private string fL_SC_DEPField;
        private string fL_SC_KNField;
        private string fL_SC_CMPFField;
        private string mULTIL_STRUField;
        private string fL_DPSEUField;
        private string oB_FIXField;
        private string oB_INSTField;
        private string fL_EOASLField;
        private string fL_SAASLField;
        private string fL_OBJ_MATField;
        private string fL_OBJ_DOCField;
        private string fL_OBJ_CLSField;
        private string fL_OBJ_TXTField;
        private string fL_SDRELField;
        private string fL_KORELField;
        private string fL_FERELField;
        private string fL_INRELField;
        private string fL_KARELField;
        private string pOSTYPENField;
        private string sORTF1Field;
        private string sORTF2Field;
        private string sORTF3Field;
        private string sORTF4Field;
        private string sORTF5Field;
        private string cLASSF1Field;
        private string cLASSF2Field;
        private string cLASSF3Field;
        private string cLASSF4Field;
        private string cLASSF5Field;
        private string pRIOField;
        private string pRSTLField;
        private string uMBEWField;
        private string fLBROWSERField;
        private string fL_PROF_OBOMField;
        private string sEGMENTField;
        public ZMATMAS5E1CUCOM()
        {
            this.sEGMENTField = "1";
        }
        
        public string MSGFN
        {
            get
            {
                return this.mSGFNField;
            }
            set
            {
                this.mSGFNField = value;
            }
        }
        
        public string C_PROFILE
        {
            get
            {
                return this.c_PROFILEField;
            }
            set
            {
                this.c_PROFILEField = value;
            }
        }
        
        public string CLASSTYPE
        {
            get
            {
                return this.cLASSTYPEField;
            }
            set
            {
                this.cLASSTYPEField = value;
            }
        }
        
        public string ORGAREAS
        {
            get
            {
                return this.oRGAREASField;
            }
            set
            {
                this.oRGAREASField = value;
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
        
        public string BOMAPPL
        {
            get
            {
                return this.bOMAPPLField;
            }
            set
            {
                this.bOMAPPLField = value;
            }
        }
        
        public string FLAVAILCH
        {
            get
            {
                return this.fLAVAILCHField;
            }
            set
            {
                this.fLAVAILCHField = value;
            }
        }
        
        public string BOMEXPL
        {
            get
            {
                return this.bOMEXPLField;
            }
            set
            {
                this.bOMEXPLField = value;
            }
        }
        
        public string TASKLEXPL
        {
            get
            {
                return this.tASKLEXPLField;
            }
            set
            {
                this.tASKLEXPLField = value;
            }
        }
        
        public string INITSCREEN
        {
            get
            {
                return this.iNITSCREENField;
            }
            set
            {
                this.iNITSCREENField = value;
            }
        }
        
        public string FLASSEMBLY
        {
            get
            {
                return this.fLASSEMBLYField;
            }
            set
            {
                this.fLASSEMBLYField = value;
            }
        }
        
        public string FLRESULT
        {
            get
            {
                return this.fLRESULTField;
            }
            set
            {
                this.fLRESULTField = value;
            }
        }
        
        public string FLMDATA
        {
            get
            {
                return this.fLMDATAField;
            }
            set
            {
                this.fLMDATAField = value;
            }
        }
        
        public string FLCASONLY
        {
            get
            {
                return this.fLCASONLYField;
            }
            set
            {
                this.fLCASONLYField = value;
            }
        }
        
        public string FLMANCHANG
        {
            get
            {
                return this.fLMANCHANGField;
            }
            set
            {
                this.fLMANCHANGField = value;
            }
        }
        
        public string FLHOLDBOM
        {
            get
            {
                return this.fLHOLDBOMField;
            }
            set
            {
                this.fLHOLDBOMField = value;
            }
        }
        
        public string FLDELETE
        {
            get
            {
                return this.fLDELETEField;
            }
            set
            {
                this.fLDELETEField = value;
            }
        }
        
        public string DESIGN
        {
            get
            {
                return this.dESIGNField;
            }
            set
            {
                this.dESIGNField = value;
            }
        }
        
        public string NEUTR
        {
            get
            {
                return this.nEUTRField;
            }
            set
            {
                this.nEUTRField = value;
            }
        }
        
        public string CHAR_VALU
        {
            get
            {
                return this.cHAR_VALUField;
            }
            set
            {
                this.cHAR_VALUField = value;
            }
        }
        
        public string A_LAISO
        {
            get
            {
                return this.a_LAISOField;
            }
            set
            {
                this.a_LAISOField = value;
            }
        }
        
        public string SCOPE_CHAR
        {
            get
            {
                return this.sCOPE_CHARField;
            }
            set
            {
                this.sCOPE_CHARField = value;
            }
        }
        
        public string SCOPE_VALU
        {
            get
            {
                return this.sCOPE_VALUField;
            }
            set
            {
                this.sCOPE_VALUField = value;
            }
        }
        
        public string FL_EXCLUDE
        {
            get
            {
                return this.fL_EXCLUDEField;
            }
            set
            {
                this.fL_EXCLUDEField = value;
            }
        }
        
        public string DISPLAY
        {
            get
            {
                return this.dISPLAYField;
            }
            set
            {
                this.dISPLAYField = value;
            }
        }
        
        public string PRICING
        {
            get
            {
                return this.pRICINGField;
            }
            set
            {
                this.pRICINGField = value;
            }
        }
        
        public string CONFIGUR
        {
            get
            {
                return this.cONFIGURField;
            }
            set
            {
                this.cONFIGURField = value;
            }
        }
        
        public string DEFVALU_DE
        {
            get
            {
                return this.dEFVALU_DEField;
            }
            set
            {
                this.dEFVALU_DEField = value;
            }
        }
        
        public string FL_MARK
        {
            get
            {
                return this.fL_MARKField;
            }
            set
            {
                this.fL_MARKField = value;
            }
        }
        
        public string DEFVALU_CC
        {
            get
            {
                return this.dEFVALU_CCField;
            }
            set
            {
                this.dEFVALU_CCField = value;
            }
        }
        
        public string TYPM_SEL
        {
            get
            {
                return this.tYPM_SELField;
            }
            set
            {
                this.tYPM_SELField = value;
            }
        }
        
        public string TYPM_STRA
        {
            get
            {
                return this.tYPM_STRAField;
            }
            set
            {
                this.tYPM_STRAField = value;
            }
        }
        
        public string FL_SC_CHAR
        {
            get
            {
                return this.fL_SC_CHARField;
            }
            set
            {
                this.fL_SC_CHARField = value;
            }
        }
        
        public string FL_SC_DEP
        {
            get
            {
                return this.fL_SC_DEPField;
            }
            set
            {
                this.fL_SC_DEPField = value;
            }
        }
        
        public string FL_SC_KN
        {
            get
            {
                return this.fL_SC_KNField;
            }
            set
            {
                this.fL_SC_KNField = value;
            }
        }
        
        public string FL_SC_CMPF
        {
            get
            {
                return this.fL_SC_CMPFField;
            }
            set
            {
                this.fL_SC_CMPFField = value;
            }
        }
        
        public string MULTIL_STRU
        {
            get
            {
                return this.mULTIL_STRUField;
            }
            set
            {
                this.mULTIL_STRUField = value;
            }
        }
        
        public string FL_DPSEU
        {
            get
            {
                return this.fL_DPSEUField;
            }
            set
            {
                this.fL_DPSEUField = value;
            }
        }
        
        public string OB_FIX
        {
            get
            {
                return this.oB_FIXField;
            }
            set
            {
                this.oB_FIXField = value;
            }
        }
        
        public string OB_INST
        {
            get
            {
                return this.oB_INSTField;
            }
            set
            {
                this.oB_INSTField = value;
            }
        }
        
        public string FL_EOASL
        {
            get
            {
                return this.fL_EOASLField;
            }
            set
            {
                this.fL_EOASLField = value;
            }
        }
        
        public string FL_SAASL
        {
            get
            {
                return this.fL_SAASLField;
            }
            set
            {
                this.fL_SAASLField = value;
            }
        }
        
        public string FL_OBJ_MAT
        {
            get
            {
                return this.fL_OBJ_MATField;
            }
            set
            {
                this.fL_OBJ_MATField = value;
            }
        }
        
        public string FL_OBJ_DOC
        {
            get
            {
                return this.fL_OBJ_DOCField;
            }
            set
            {
                this.fL_OBJ_DOCField = value;
            }
        }
        
        public string FL_OBJ_CLS
        {
            get
            {
                return this.fL_OBJ_CLSField;
            }
            set
            {
                this.fL_OBJ_CLSField = value;
            }
        }
        
        public string FL_OBJ_TXT
        {
            get
            {
                return this.fL_OBJ_TXTField;
            }
            set
            {
                this.fL_OBJ_TXTField = value;
            }
        }
        
        public string FL_SDREL
        {
            get
            {
                return this.fL_SDRELField;
            }
            set
            {
                this.fL_SDRELField = value;
            }
        }
        
        public string FL_KOREL
        {
            get
            {
                return this.fL_KORELField;
            }
            set
            {
                this.fL_KORELField = value;
            }
        }
        
        public string FL_FEREL
        {
            get
            {
                return this.fL_FERELField;
            }
            set
            {
                this.fL_FERELField = value;
            }
        }
        
        public string FL_INREL
        {
            get
            {
                return this.fL_INRELField;
            }
            set
            {
                this.fL_INRELField = value;
            }
        }
        
        public string FL_KAREL
        {
            get
            {
                return this.fL_KARELField;
            }
            set
            {
                this.fL_KARELField = value;
            }
        }
        
        public string POSTYPEN
        {
            get
            {
                return this.pOSTYPENField;
            }
            set
            {
                this.pOSTYPENField = value;
            }
        }
        
        public string SORTF1
        {
            get
            {
                return this.sORTF1Field;
            }
            set
            {
                this.sORTF1Field = value;
            }
        }
        
        public string SORTF2
        {
            get
            {
                return this.sORTF2Field;
            }
            set
            {
                this.sORTF2Field = value;
            }
        }
        
        public string SORTF3
        {
            get
            {
                return this.sORTF3Field;
            }
            set
            {
                this.sORTF3Field = value;
            }
        }
        
        public string SORTF4
        {
            get
            {
                return this.sORTF4Field;
            }
            set
            {
                this.sORTF4Field = value;
            }
        }
        
        public string SORTF5
        {
            get
            {
                return this.sORTF5Field;
            }
            set
            {
                this.sORTF5Field = value;
            }
        }
        
        public string CLASSF1
        {
            get
            {
                return this.cLASSF1Field;
            }
            set
            {
                this.cLASSF1Field = value;
            }
        }
        
        public string CLASSF2
        {
            get
            {
                return this.cLASSF2Field;
            }
            set
            {
                this.cLASSF2Field = value;
            }
        }
        
        public string CLASSF3
        {
            get
            {
                return this.cLASSF3Field;
            }
            set
            {
                this.cLASSF3Field = value;
            }
        }
        
        public string CLASSF4
        {
            get
            {
                return this.cLASSF4Field;
            }
            set
            {
                this.cLASSF4Field = value;
            }
        }
        
        public string CLASSF5
        {
            get
            {
                return this.cLASSF5Field;
            }
            set
            {
                this.cLASSF5Field = value;
            }
        }
        
        public string PRIO
        {
            get
            {
                return this.pRIOField;
            }
            set
            {
                this.pRIOField = value;
            }
        }
        
        public string PRSTL
        {
            get
            {
                return this.pRSTLField;
            }
            set
            {
                this.pRSTLField = value;
            }
        }
        
        public string UMBEW
        {
            get
            {
                return this.uMBEWField;
            }
            set
            {
                this.uMBEWField = value;
            }
        }
        
        public string FLBROWSER
        {
            get
            {
                return this.fLBROWSERField;
            }
            set
            {
                this.fLBROWSERField = value;
            }
        }
        
        public string FL_PROF_OBOM
        {
            get
            {
                return this.fL_PROF_OBOMField;
            }
            set
            {
                this.fL_PROF_OBOMField = value;
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
    public partial class EDI_DC40MATMASMATMAS05ZMATMAS5
    {
        private string tABNAMField;
        private string mANDTField;
        private string dOCNUMField;
        private string dOCRELField;
        private string sTATUSField;
        private EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT dIRECTField;
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
        public EDI_DC40MATMASMATMAS05ZMATMAS5()
        {
            this.tABNAMField = "EDI_DC40";
            this.iDOCTYPField = "MATMAS05";
            this.cIMTYPField = "ZMATMAS5";
            this.mESTYPField = "MATMAS";
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
        
        public EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT DIRECT
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
    public enum EDI_DC40MATMASMATMAS05ZMATMAS5DIRECT
    {
        [XmlEnum("1")]
        Item1,
        [XmlEnum("2")]
        Item2,
    }
    
}