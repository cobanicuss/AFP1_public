using System;

namespace Spm.OrrSys.Service.Dto
{
    public class DespatchedPacksByCustomerOrderDto
    {
        public string SalesOrderNumber { get; set; }
        public string SaleInvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string PackNumber { get; set; }
        public string Item { get; set; }
        public string DateUpdated { get; set; }
        public string TimeUpdated { get; set; }
        public string State { get; set; }
        public string ShipTo { get; set; }
        public string EmailAddress { get; set; }
        public string Processed { get; set; }
        public string PurchaseOrder { get; set; }
        public string SapMatNum { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }
}