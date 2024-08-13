using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class GeneralLedgerFileDataMap : ClassMap<GeneralLedgerFileData>
    {
        public GeneralLedgerFileDataMap()
        {
            Table("GeneralLedgerFileData");
            Id(x => x.DataId);
            Map(x => x.DateTimeFileUploaded);
            Map(x => x.FileName);
            Map(x => x.SagaReferenceId);

            Map(x => x.HeaderTxt);
            Map(x => x.CompCode);
            Map(x => x.DocDate);
            Map(x => x.PstingDate);
            Map(x => x.RefDocNo);
            Map(x => x.AllocNmbr);
            Map(x => x.CostCentre);
            Map(x => x.GlAccount);
            Map(x => x.Currency);
            Map(x => x.AmtDoccur);
        }
    }
}