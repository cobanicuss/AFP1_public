using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class SYSTAT01IDOCE1STATS
    {

        private string dOCNUMField;

        private string lOGDATField;

        private string lOGTIMField;

        private string sTATUSField;

        private string uNAMEField;

        private string rEPIDField;

        private string sTACODField;

        private string sTATXTField;

        private string sTATYPField;

        private string sEGMENTField;

        /// <remarks/>
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

        /// <remarks/>
        public string LOGDAT
        {
            get
            {
                return this.lOGDATField;
            }
            set
            {
                this.lOGDATField = value;
            }
        }

        /// <remarks/>
        public string LOGTIM
        {
            get
            {
                return this.lOGTIMField;
            }
            set
            {
                this.lOGTIMField = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public string UNAME
        {
            get
            {
                return this.uNAMEField;
            }
            set
            {
                this.uNAMEField = value;
            }
        }

        /// <remarks/>
        public string REPID
        {
            get
            {
                return this.rEPIDField;
            }
            set
            {
                this.rEPIDField = value;
            }
        }

        /// <remarks/>
        public string STACOD
        {
            get
            {
                return this.sTACODField;
            }
            set
            {
                this.sTACODField = value;
            }
        }

        /// <remarks/>
        public string STATXT
        {
            get
            {
                return this.sTATXTField;
            }
            set
            {
                this.sTATXTField = value;
            }
        }

        /// <remarks/>
        public string STATYP
        {
            get
            {
                return this.sTATYPField;
            }
            set
            {
                this.sTATYPField = value;
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