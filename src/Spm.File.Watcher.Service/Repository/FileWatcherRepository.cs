using System.Collections.Generic;
using NHibernate;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Repository
{
    public class FileWatcherRepository : IFileWatcherRepository
    {
        private readonly IDoBulkInsert _bulkInsert;
        private readonly ICastDto _castDto;
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public FileWatcherRepository(IDoBulkInsert bulkInsert,  ICastDto castDto)
        {
            _bulkInsert = bulkInsert;
            _castDto = castDto;
        }

        public void InsertPurchaseOrderCreateData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId)
        {
            var purchaseOrderCreateList = _castDto.AsPurchaseOrderCreateFileData(dtoList, fileName, sagaReferenceId);

            const string tableName = Constants.PurchaseOrderCreateTableName;

            var dt = DataTableSetupForBulkInsert.CreatePurchaseOrderCreateFileData(tableName);
            DataTableSetupForBulkInsert.PopulatePurchaseOrderCreate(purchaseOrderCreateList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }

        public void InsertPurchaseOrderChangeData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId)
        {
            var purchaseOrderChangeList = _castDto.AsPurchaseOrderChangeFileData(dtoList, fileName, sagaReferenceId);

            const string tableName = Constants.PurchaseOrderChangeTableName;

            var dt = DataTableSetupForBulkInsert.CreatePurchaseOrderChangeFileData(tableName);
            DataTableSetupForBulkInsert.PopulatePurchaseOrderChange(purchaseOrderChangeList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }

        public void InsertMaterialMasterData(IList<MaterialMasterSagaDto> dtoList, string fileName)
        {
            var materialMasterDataList = _castDto.AsMaterialMasterFileData(dtoList, fileName);

            const string tableName = Constants.MaterialMasterTableName;

            var dt = DataTableSetupForBulkInsert.CreateMaterialMasterFileData(tableName);
            DataTableSetupForBulkInsert.PopulateMaterialMaster(materialMasterDataList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }

        public void InsertGoodsReceiptData(IList<GoodsSagaDto> dtoList, string filename, string type)
        {
            var goodsDataList = _castDto.AsGoodsFileData(dtoList, filename, type);

            const string tableName = Constants.GoodsReceiptTableName;

            var dt = DataTableSetupForBulkInsert.CreateGoodsReceiptFileData(tableName);
            DataTableSetupForBulkInsert.PopulateGoodsReceipt(goodsDataList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }

        public void InsertGoodsReversalData(IList<GoodsSagaDto> dtoList, string filename, string type)
        {
            var goodsDataList = _castDto.AsGoodsFileData(dtoList, filename, type);

            const string tableName = Constants.GoodsReversalTableName;

            var dt = DataTableSetupForBulkInsert.CreateGoodsReversalFileData(tableName);
            DataTableSetupForBulkInsert.PopulateGoodsReversal(goodsDataList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }

        public void InsertGeneralLedgerData(IList<GeneralLedgerItemDto> dtoList, string fileName, string sagaReferenceId)
        {
            var generalLedgerList = _castDto.AsGeneralLederFileData(dtoList, fileName, sagaReferenceId);

            const string tableName = Constants.GeneralLedgerTableName;

            var dt = DataTableSetupForBulkInsert.CreateGeneralLedgerFileData(tableName);
            DataTableSetupForBulkInsert.PopulateGeneralLedger(generalLedgerList, dt);

            _bulkInsert.BulkInsert(dt, tableName);
        }
    }
}