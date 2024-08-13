using System.Collections.Generic;
using System.Text;

namespace Spm.Shared
{
    public class TestCertificatePayload
    {
        public List<TestCertificatePayloadItem> TestCertificatePayloadItemList { get; set; }
    }

    public class TestCertificatePayloadItem
    {
        public string SapMaterialNumber { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SaleInvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string PackNumber { get; set; }
        public string DateUpdated { get; set; }
        public string ShipTo { get; set; }
        public string EmailAddress { get; set; }
        public string PurchaseOrder { get; set; }
    }

    public class TestCertificatePayloadAsString
    {
        public static string ToString(TestCertificatePayloadItem testCertificatePayload)
        {
            var sb = new StringBuilder();
            sb.Append("SapMaterialNumber={0},");
            sb.Append("SalesOrderNumber={1},");
            sb.Append("SaleInvoiceNumber={2},");
            sb.Append("CustomerName={3},");
            sb.Append("CustomerAccountNumber={4},");
            sb.Append("PackNumber={5},");
            sb.Append("DateUpdated={6},");
            sb.Append("ShipTo={7},");
            sb.Append("EmailAddress={8},");
            sb.Append("PurchaseOrder={9},");

            var str = string.Format(sb.ToString(),
                testCertificatePayload.SapMaterialNumber,
                testCertificatePayload.SalesOrderNumber,
                testCertificatePayload.SaleInvoiceNumber,
                testCertificatePayload.CustomerName,
                testCertificatePayload.CustomerAccountNumber,
                testCertificatePayload.PackNumber,
                testCertificatePayload.DateUpdated,
                testCertificatePayload.ShipTo,
                testCertificatePayload.EmailAddress,
                testCertificatePayload.PurchaseOrder);

            return str;
        }
    }
}