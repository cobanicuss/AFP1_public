using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class SYSTAT01IDOC
    {

        private SYSTAT01IDOCEDI_DC40 eDI_DC40Field;

        private SYSTAT01IDOCE1STATS e1STATSField;

        private string[] textField;

        private string bEGINField;

        /// <remarks/>
        public SYSTAT01IDOCEDI_DC40 EDI_DC40
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

        /// <remarks/>
        public SYSTAT01IDOCE1STATS E1STATS
        {
            get
            {
                return this.e1STATSField;
            }
            set
            {
                this.e1STATSField = value;
            }
        }

        /// <remarks/>
        [XmlText()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <remarks/>
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
}