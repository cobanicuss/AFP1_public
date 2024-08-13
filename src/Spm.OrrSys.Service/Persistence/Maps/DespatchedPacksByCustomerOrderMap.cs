using FluentNHibernate.Mapping;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Persistence.Maps
{
    public class DespatchedPacksByCustomerOrderMap : ClassMap<DespatchedPacksByCustomerOrder>
    {
        public DespatchedPacksByCustomerOrderMap()
        {
            Table("DespatchedPacksByCustomerOrder");
            Map(x => x.SalesOrderNumber);
            Map(x => x.SaleInvoiceNumber);
            Map(x => x.CustomerName);
            Map(x => x.CustomerAccountNumber);
            Id(x => x.PackNumber);
            Map(x => x.Item);
            Map(x => x.DateUpdated);
            Map(x => x.TimeUpdated);
            Map(x => x.State);
            Map(x => x.ShipTo);
            Map(x => x.EmailAddress);
            Map(x => x.Processed);
            Map(x => x.PurchaseOrder);
            Map(x => x.SapMatNum);
            Map(x => x.DateTimeCreated);
        }
    }
}