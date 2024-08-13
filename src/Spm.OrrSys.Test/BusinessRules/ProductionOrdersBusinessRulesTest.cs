using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;
using Spm.OrrSys.Service.Repositories;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.BusinessRules
{
    [TestFixture]
    public class ProductionOrdersBusinessRulesTest
    {
        private IDoProductionOrderBusiness _classUnderTest;
        private Mock<ISapRepository> _sapRepository;
        private Mock<IProvideSapInboundSpecificBusinessRules> _inboundBusinessRules;
        private Mock<IProductionOrderRepository> _orrSysRepository;

        [Test]
        public void DuringCreationOfProductionOrdersBusinessRulesNeedsToApply()
        {
            this.Given(_ => ProductionOrderBusinessRulesExecutedSuccessfully())
           .When(_ => CreatingProductionOrders())
           .Then(_ => GetInboundProductionOrdersFromSapDatabase())
               .And(_ => AddProductionOrders())
               .And(_ => DeleteHistoryNDaysBack())
               .And(_ => InsertNewHistory())
               .And(_ => DeleteCurrentProductionOrders())
               .And(_ => BulkInsertNewProductionOrdersInPlaceOfDeletedPlannedOrders())
               .And(_ => TriggerBulkInsertProductionOrdersIntoOracle())
           .BDDfy();
        }

        private void ProductionOrderBusinessRulesExecutedSuccessfully()
        {
            BaseSetup();
        }

        private void CreatingProductionOrders()
        {
            InitializingProductionOrderCreation();
            _classUnderTest.CreateProductionOrders();
        }

        private void InitializingProductionOrderCreation()
        {
            _classUnderTest = new ProductionOrderBusinessRules(_orrSysRepository.Object, _sapRepository.Object, _inboundBusinessRules.Object);
        }

        protected void GetInboundProductionOrdersFromSapDatabase()
        {
            _sapRepository.Verify(x => x.GetInboundProductionOrderList());
        }

        private void AddProductionOrders()
        {
            _inboundBusinessRules.Verify(x => x.CreateJdeProductionOrders(
                                                        It.IsAny<List<DemandSapJdeWo>>(),
                                                        It.IsAny<ProductionOrderDto>()
                                                        ));
        }

        private void DeleteHistoryNDaysBack()
        {
            _orrSysRepository.Verify(x => x.DeleteOldHistory());
        }

        private void InsertNewHistory()
        {
            _orrSysRepository.Verify(x => x.InsertNewHistory());
        }

        private void DeleteCurrentProductionOrders()
        {
            _orrSysRepository.Verify(x => x.DeleteAllByTable());
        }

        private void BulkInsertNewProductionOrdersInPlaceOfDeletedPlannedOrders()
        {
            _orrSysRepository.Verify(x => x.BulkInsertByTypeList(It.IsAny<List<OrrSysF554801Z>>()));
        }

        private void TriggerBulkInsertProductionOrdersIntoOracle()
        {
            _sapRepository.Verify(x => x.TriggerBulkInsertProductionOrdersIntoOracle());
        }

        private void BaseSetup()
        {
            _sapRepository = new Mock<ISapRepository>();
            _sapRepository.Setup(x => x.GetInboundProductionOrderList()).Returns(new List<DemandSapJdeWo>());
            _sapRepository.Setup(x => x.TriggerBulkInsertProductionOrdersIntoOracle());

            _inboundBusinessRules = new Mock<IProvideSapInboundSpecificBusinessRules>();
            _inboundBusinessRules.Setup(x => x.CreateJdeProductionOrders(
                                                        It.IsAny<List<DemandSapJdeWo>>(),
                                                        It.IsAny<ProductionOrderDto>()))
                                                        .Returns(new List<OrrSysF554801Z>());

            _orrSysRepository = new Mock<IProductionOrderRepository>();
            _orrSysRepository.Setup(x => x.DeleteOldHistory());
            _orrSysRepository.Setup(x => x.InsertNewHistory());
            _orrSysRepository.Setup(x => x.DeleteAllByTable());
            _orrSysRepository.Setup(x => x.BulkInsertByTypeList(It.IsAny<List<OrrSysF554801Z>>()));
        }
    }
}