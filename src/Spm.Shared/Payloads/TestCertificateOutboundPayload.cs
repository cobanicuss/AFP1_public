using System;
using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    [Serializable]
    public class TestCertificateOutboundPayload
    {
        public string CertificateNumber { get; set; }
        public DateTime CertificateDateTime { get; set; }
        public int CertificateFunctionCode { get; set; }
        public string SellerPartyIdScheme { get; set; }
        public string CustomerPartyIdentifier { get; set; }
        public string Attachment { get; set; }
        public string AttachmentType { get; set; }
        public string AttachmentEncoding { get; set; }
        public string CustomerOrderNumber { get; set; }
        public List<TestCertificateOutboundPayloadItem> LineItems { get; set; }
    }

    [Serializable]
    public class TestCertificateOutboundPayloadItem
    {
        public string MaterialIdentifier { get; set; }
        public string MaterialReferenceNumber { get; set; }
        public string MaterialReferenceTypeCode { get; set; }
        public string HeatNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string CustomerArticleNumber { get; set; }
    }
}