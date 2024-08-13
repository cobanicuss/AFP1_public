using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = Constants.PayloadNameSpace, IsNullable = false)]
    public partial class SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE
    {

        private SYSTAT01IDOC iDOCField;

        private string[] textField;

        /// <remarks/>
        public SYSTAT01IDOC IDOC
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
    }
}