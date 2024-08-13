namespace Spm.OrrSys.Service.Dto
{
    public class TestCertBakupDto
    {
        public string CertificateId { get; set; }
        public string PurchaseOrder { get; set; }
    }

    public class TestCertFileDto : TestCertBakupDto
    {
        public string SalesOrderNumber { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccountNumber { get; set; }
    }

    public class TestCertDto : TestCertFileDto
    {
        public int ReportGroup { get; set; }
        public string PdfFormat { get; set; }
    }
}