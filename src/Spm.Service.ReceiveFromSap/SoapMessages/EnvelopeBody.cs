using System.Xml.Serialization;

namespace Spm.Service.ReceiveFromSap.SoapMessages
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {
        private SYSTAT01_PRODUCTACHIEVEMENTRESPONSE sYSTAT01_PRODUCTACHIEVEMENTRESPONSEField;
        private SYSTAT01_PURCHASEORDERCREATERESPONSE sYSTAT01_PURCHASEORDERCREATERESPONSEField;
        private SYSTAT01_PURCHASEORDERCHANGERESPONSE sYSTAT01_PURCHASEORDERCHANGERESPONSEField;
        private SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE sYSTAT01_PRODUCTIONORDERSTATUSRESPONSEField;
        private SYSTAT01_GOODSRECEIPTRESPONSE sYSTAT01_GOODSRECEIPTRESPONSEField;
        private SYSTAT01_GENERALLEDGERRESPONSE sYSTAT01_GENERALLEDGERRESPONSEField;

        private ZOBTC01 zOBTC01Field;

        private string[] textField;

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_PRODUCTACHIEVEMENTRESPONSE SYSTAT01_PRODUCTACHIEVEMENTRESPONSE
        {
            get
            {
                return this.sYSTAT01_PRODUCTACHIEVEMENTRESPONSEField;
            }
            set
            {
                this.sYSTAT01_PRODUCTACHIEVEMENTRESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_PURCHASEORDERCREATERESPONSE SYSTAT01_PURCHASEORDERCREATERESPONSE
        {
            get
            {
                return this.sYSTAT01_PURCHASEORDERCREATERESPONSEField;
            }
            set
            {
                this.sYSTAT01_PURCHASEORDERCREATERESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_PURCHASEORDERCHANGERESPONSE SYSTAT01_PURCHASEORDERCHANGERESPONSE
        {
            get
            {
                return this.sYSTAT01_PURCHASEORDERCHANGERESPONSEField;
            }
            set
            {
                this.sYSTAT01_PURCHASEORDERCHANGERESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE
        {
            get
            {
                return this.sYSTAT01_PRODUCTIONORDERSTATUSRESPONSEField;
            }
            set
            {
                this.sYSTAT01_PRODUCTIONORDERSTATUSRESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_GOODSRECEIPTRESPONSE SYSTAT01_GOODSRECEIPTRESPONSE
        {
            get
            {
                return this.sYSTAT01_GOODSRECEIPTRESPONSEField;
            }
            set
            {
                this.sYSTAT01_GOODSRECEIPTRESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public SYSTAT01_GENERALLEDGERRESPONSE SYSTAT01_GENERALLEDGERRESPONSE
        {
            get
            {
                return this.sYSTAT01_GENERALLEDGERRESPONSEField;
            }
            set
            {
                this.sYSTAT01_GENERALLEDGERRESPONSEField = value;
            }
        }

        [XmlElement(Namespace = Constants.PayloadNameSpace)]
        public ZOBTC01 ZOBTC01
        {
            get
            {
                return this.zOBTC01Field;
            }
            set
            {
                this.zOBTC01Field = value;
            }
        }

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