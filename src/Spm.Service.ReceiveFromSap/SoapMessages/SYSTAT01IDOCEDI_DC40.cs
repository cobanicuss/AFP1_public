using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class SYSTAT01IDOCEDI_DC40
    {

        private string tABNAMField;

        private string dIRECTField;

        private string iDOCTYPField;

        private string mESTYPField;

        private string sNDPORField;

        private string sNDPRTField;

        private string sNDPRNField;

        private string rCVPORField;

        private string rCVPRTField;

        private string rCVPRNField;

        private string sEGMENTField;

        /// <remarks/>
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

        /// <remarks/>
        public string DIRECT
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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