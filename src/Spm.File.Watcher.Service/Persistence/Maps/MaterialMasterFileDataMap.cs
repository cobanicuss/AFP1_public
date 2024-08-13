using FluentNHibernate.Mapping;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Persistence.Maps
{
    public class MaterialMasterFileDataMap : ClassMap<MaterialMasterFileData>
    {
        public MaterialMasterFileDataMap()
        {
            Table("MaterialMasterFileData");
            Id(x => x.DataId);
            Map(x => x.DateTimeFileUploaded);
            Map(x => x.FileName);
            Map(x => x.SagaReferenceId);
            Map(x => x.Itm);
            Map(x => x.Litm);
            Map(x => x.Aitm);
            Map(x => x.Dsc1);
            Map(x => x.Dsc2);
            Map(x => x.Dsc12);
            Map(x => x.LongDsc);
            Map(x => x.Mcu);
            Map(x => x.Stkt);
            Map(x => x.Draw);
            Map(x => x.Srp3Desc);
            Map(x => x.Srp4Desc);
            Map(x => x.Srp4Desc2);
            Map(x => x.Srp5Desc);
            Map(x => x.Srp6Desc);
            Map(x => x.Srp7Desc);
            Map(x => x.Srp8Desc);
            Map(x => x.Prp0);
            Map(x => x.Prp0M);
            Map(x => x.Prp1Desc);
            Map(x => x.Prp1Desc2);
            Map(x => x.Prp2Desc);
            Map(x => x.Prp3Desc);
            Map(x => x.Sec1);
            Map(x => x.Sec2);
            Map(x => x.Prp4Desc);
            Map(x => x.Prp5Desc);
            Map(x => x.Prp6Desc);
            Map(x => x.Imprp3);
            Map(x => x.Imprp3Desc);
            Map(x => x.Imprp4);
            Map(x => x.Imprp4Desc);
            Map(x => x.Imuom1);
            Map(x => x.Bq);
            Map(x => x.BwCk);
            Map(x => x.Kgs);
            Map(x => x.Mt);
            Map(x => x.GhMm);
            Map(x => x.GwMm);
            Map(x => x.Kg);
        }
    }
}