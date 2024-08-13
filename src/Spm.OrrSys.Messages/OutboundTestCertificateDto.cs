namespace Spm.OrrSys.Messages
{
    public class OutboundTestCertificateDto
    {
        public string Certificateid { get; set; }
        public int ReportGroup { get; set; }
        public string PurchaseOrder { get; set; }
        public int RowNumber { get; set; }
        public string FirstLevelLot { get; set; }
        public string HeatNumber { get; set; }
        public string SapMaterialNumber { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SaleInvoiceNumber { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerName { get; set; }
    }
}