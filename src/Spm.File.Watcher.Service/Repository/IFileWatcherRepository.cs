using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Repository
{
    public interface IFileWatcherRepository
    {
        void InsertPurchaseOrderCreateData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId);
        void InsertPurchaseOrderChangeData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId);

        void InsertMaterialMasterData(IList<MaterialMasterSagaDto> dtoList, string fileName);

        void InsertGoodsReceiptData(IList<GoodsSagaDto> dtoList, string filename, string type);
        void InsertGoodsReversalData(IList<GoodsSagaDto> dtoList, string filename, string type);

        void InsertGeneralLedgerData(IList<GeneralLedgerItemDto> dtoList, string fileName, string sagaReferenceId);
    }
}