using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Downloader
{
    public interface IGetDataFromFile<T> where T : IMarkAsDto
    {
        IList<T> ExtractDataFromFile(string path, string errorPath, string fileName, string errorFileName);
    }

    public interface IGetDataForGeneralLedger : IGetDataFromFile<GeneralLedgerDto> { }
    public interface IGetDataForGoods : IGetDataFromFile<GoodsDto> { }
    public interface IGetDataForPurchaseOrder : IGetDataFromFile<PurchaseOrderDto> { }
    public interface IGetDataForMaterialMaster : IGetDataFromFile<MaterialMasterDto> { }
}