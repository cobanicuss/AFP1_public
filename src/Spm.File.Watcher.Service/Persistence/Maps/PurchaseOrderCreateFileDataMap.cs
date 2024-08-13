using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class PurchaseOrderCreateFileDataMap : ClassMap<PurchaseOrderCreateFileData>
    {
        public PurchaseOrderCreateFileDataMap()
        {
            Table("PurchaseOrderCreateFileData");
            Id(x => x.DataId);
            Map(x => x.DateTimeFileUploaded);
            Map(x => x.FileName);
            Map(x => x.SagaReferenceId);

            Map(x => x.PoNumber);
            Map(x => x.CompCode);
            Map(x => x.DocType);
            Map(x => x.CreatDate);
            Map(x => x.CreatedBy);
            Map(x => x.ItemIntvl);
            Map(x => x.Vendor);
            Map(x => x.PurchOrg);
            Map(x => x.PurGroup);
            Map(x => x.Currency);
            Map(x => x.DocDate);
            Map(x => x.PoItem);
            Map(x => x.DeleteInd);
            Map(x => x.ShortText);
            Map(x => x.Plant);
            Map(x => x.StgeLoc);
            Map(x => x.MatlGroup);
            Map(x => x.VendMat);
            Map(x => x.Quantity);
            Map(x => x.PoUnit);
            Map(x => x.OrderprUn);
            Map(x => x.NetPrice);
            Map(x => x.PriceUnit);
            Map(x => x.OverDlvTol);
            Map(x => x.NoMoreGr);
            Map(x => x.Acctasscat);
            Map(x => x.PreqName);
            Map(x => x.SerialNo);
            Map(x => x.GlAccount);
            Map(x => x.Costcenter);
            Map(x => x.DeliveryDate);
            Map(x => x.LnType);
            Map(x => x.Taxable);
        }
    }
}
