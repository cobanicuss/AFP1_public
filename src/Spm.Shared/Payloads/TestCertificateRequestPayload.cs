using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    public class TestCertificateRequestPayload
    {
        public List<TestCertificateRequestPayloadItem> TestCertificatePayloadItemList { get; set; }
    }

    public class TestCertificateRequestPayloadItem
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

    public class TestCertificateRequestPayloadAsString
    {
        public static string ToString(TestCertificateRequestPayloadItem item)
        {
            var str = $@"SapMaterialNumber={item.SapMaterialNumber},
            SalesOrderNumber={item.SalesOrderNumber},
            SaleInvoiceNumber={item.SaleInvoiceNumber},
            CustomerName={item.CustomerName},
            CustomerAccountNumber={item.CustomerAccountNumber},
            PackNumber={item.PackNumber},
            DateUpdated={item.DateUpdated},
            ShipTo={item.ShipTo},
            EmailAddress={item.EmailAddress},
            PurchaseOrder={item.PurchaseOrder}";

            return str;
        }
    }
}