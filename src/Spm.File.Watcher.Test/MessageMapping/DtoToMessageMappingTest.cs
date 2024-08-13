using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;
using Spm.File.Watcher.Service;

namespace Spm.File.Watcher.Test.MessageMapping
{
    [TestFixture]
    public class DtoToMessageMappingTest
    {
        private int _generalLedgerDataColumnCount;
        private int _goodsDataColumnCount;
        private int _materialMasterDataColumnCount;
        private int _purchaseOrderDataColumnCount;

        private int _generalLedgerDtoColumnCount;
        private int _goodsDtoColumnCount;
        private int _materialMasterDtoColumnCount;
        private int _purchaseOrderDtoColumnCount;

        [Test]
        public void DtoColumnCountMustMatchFileDataColumnCountDuringDataExtraction()
        {
            this.Given("Dto column count must match file data column count for mapping to work.")
            .When(_ => MappingDtoToIncommingFileData())
            .Then(_ => ColumnCountsMustMatch())

            .BDDfy();
        }

        private void MappingDtoToIncommingFileData()
        {
            _generalLedgerDataColumnCount = Constants.GeneralLedgerColumnCount;
            _goodsDataColumnCount = Constants.GoodsColumnCount;
            _materialMasterDataColumnCount = Constants.MaterialMasterColumnCount;
            _purchaseOrderDataColumnCount = Constants.PurchaseOrderColumnCount;

            _generalLedgerDtoColumnCount = typeof(GeneralLedgerDto).GetProperties().Length;
            _goodsDtoColumnCount = typeof(GoodsDto).GetProperties().Length;
            _materialMasterDtoColumnCount = typeof(MaterialMasterDto).GetProperties().Length;
            _purchaseOrderDtoColumnCount = typeof(PurchaseOrderDto).GetProperties().Length;
        }

        private void ColumnCountsMustMatch()
        {
            Assert.AreEqual(_generalLedgerDataColumnCount, _generalLedgerDtoColumnCount);
            Assert.AreEqual(_goodsDataColumnCount, _goodsDtoColumnCount);
            Assert.AreEqual(_materialMasterDataColumnCount, _materialMasterDtoColumnCount);
            Assert.AreEqual(_purchaseOrderDataColumnCount, _purchaseOrderDtoColumnCount);
        }
    }
}