using System;
using Spm.Shared;

namespace Spm.OrrSys.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class DespatchedPacksByCustomerOrder : IMarkAsDomain
    {
        public virtual string SalesOrderNumber { get; set; }
        public virtual string SaleInvoiceNumber { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerAccountNumber { get; set; }
        public virtual string PackNumber { get; set; }
        public virtual string Item { get; set; }
        public virtual string DateUpdated { get; set; }
        public virtual string TimeUpdated { get; set; }
        public virtual string State { get; set; }
        public virtual string ShipTo { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Processed { get; set; }
        public virtual string PurchaseOrder { get; set; }
        public virtual string SapMatNum { get; set; }
        public virtual DateTime DateTimeCreated { get; set; }
    }
}