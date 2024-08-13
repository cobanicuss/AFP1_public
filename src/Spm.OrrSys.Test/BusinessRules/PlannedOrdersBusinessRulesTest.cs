using System;
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
    public class PlannedOrdersBusinessRulesTest
    {
        private IDoPlannedOrderBusiness _classUnderTest;
        private Mock<IPlannedOrderRepository> _orrSysRepository;
        private Mock<IJdeImportRepository> _jdeImportRepository;
        private Mock<ISapRepository> _sapRepository;
        private Mock<IProvideSapInboundSpecificBusinessRules> _inboundBusinessRules;

        [Test]
        public void CreatingPlannedOrderShouldFollowBusinessRulesCorrectly()
        {
            this.Given(_ => PlannedOrderBusinessRulesExecutedSuccessfully())
            .When(_ => CreatingPlannedOrders())
            .Then(_ => GetPlannedOrderInboundRulesDto())
                .And(_ => GetOpenPlannedOrdersFromJdeImportDatabase())
                .And(_ => GetInboundPlannedOrdersFromSapDatabase())
                .And(_ => GetDistinctListFromInboundPlannedOrders())
                .And(_ => GetJdeItemDetailsFromDistinctShortItemList())
                .And(_ => AmalgamateInboundPlannedOrdersAndOpenPlannedOrders())
                .And(_ => DeleteHistoryNDaysBackAndInsertCurrentPlannedOrdersIntoHistory())
                .And(_ => DeleteCurrentPlannedOrders())
                .And(_ => BulkInsertNewPlannedOrdersInPlaceOfDeletedPlannedOrders())
                .And(_ => TriggerBulkInsertPlannedOrdersIntoOracle())
            .BDDfy();
        }

        [Test]
        public void ErrorShouldBeThrownWhenJdeItemDetailsReturnNull()
        {
            this.Given(_ => JdeItemDetailsReturnsNull())
            .When(_ => InitializingPlannedOrderCreation())
            .Then(_ => ErrorConditionIsThrownBecauseNoJdeItemDetailsWhereFound())
            .BDDfy();
        }

        [Test]
        public void ErrorShouldBeThrownWhenJdeItemDetailsReturnZero()
        {
            this.Given(_ => JdeItemDetailsReturnsZero())
            .When(_ => InitializingPlannedOrderCreation())
            .Then(_ => ErrorConditionIsThrownBecauseNoJdeItemDetailsWhereFound())
            .BDDfy();
        }

        protected void CreatingPlannedOrders()
        {
            InitializingPlannedOrderCreation();
            _classUnderTest.CreatePlannedOrders();
        }

        private void InitializingPlannedOrderCreation()
        {
            _classUnderTest = new PlannedOrderBusinessRules(_orrSysRepository.Object, _jdeImportRepository.Object, _sapRepository.Object, _inboundBusinessRules.Object);
        }

        private void GetJdeItemDetailsFromDistinctShortItemList()
        {
            _jdeImportRepository.Verify(x => x.GetJdeItemDetails(It.IsAny<List<int>>()));
        }

        private void GetDistinctListFromInboundPlannedOrders()
        {
            _inboundBusinessRules.Verify(x => x.GetDestinctJdeShortItemNumber(It.IsAny<List<DemandSapJde>>()));
        }

        protected void GetOpenPlannedOrdersFromJdeImportDatabase()
        {
            _jdeImportRepository.Verify(x => x.GetOpenPlannedOrderList(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>()));
        }

        protected void GetInboundPlannedOrdersFromSapDatabase()
        {
            _sapRepository.Verify(x => x.GetInboundPlannedOrderList());
        }

        private void DeleteHistoryNDaysBackAndInsertCurrentPlannedOrdersIntoHistory()
        {
            _orrSysRepository.Verify(x => x.CreateHistoryTraking());
        }

        private void DeleteCurrentPlannedOrders()
        {
            _orrSysRepository.Verify(x => x.DeleteAllByTable());
        }

        private void BulkInsertNewPlannedOrdersInPlaceOfDeletedPlannedOrders()
        {
            _orrSysRepository.Verify(x => x.BulkInsertByTypeList(It.IsAny<List<OrrSysF3460Z1>>()));
        }

        private void TriggerBulkInsertPlannedOrdersIntoOracle()
        {
            _sapRepository.Verify(x => x.TriggerBulkInsertPlannedOrdersIntoOracle());
        }

        private void AmalgamateInboundPlannedOrdersAndOpenPlannedOrders()
        {
            _inboundBusinessRules.Verify(x => x.CreateJdePlannedOrders(
                                                        It.IsAny<List<DemandSapJde>>(),
                                                        It.IsAny<List<JdeItemMasterDetailDto>>(),
                                                        It.IsAny<int>(),
                                                        It.IsAny<PlannedOrderDto>(),
                                                        It.IsAny<List<OrrSysF3460Z1>>()));
        }

        private void ErrorConditionIsThrownBecauseNoJdeItemDetailsWhereFound()
        {
            Assert.Throws<Exception>(() => _classUnderTest.CreatePlannedOrders());
        }

        private void GetPlannedOrderInboundRulesDto()
        {
            _inboundBusinessRules.Verify(x => x.CreatePlannedOrderDto());
        }

        private void JdeItemDetailsReturnsNull()
        {
            BaseSetup();
            SetUpJdeItemDetailsAsNull();
        }

        private void JdeItemDetailsReturnsZero()
        {
            BaseSetup();
            SetUpJdeItemDetailsAsZero();
        }

        private void PlannedOrderBusinessRulesExecutedSuccessfully()
        {
            BaseSetup();
            SetupJdeItemDetailsExist();
        }

        private void SetupJdeItemDetailsExist()
        {
            _jdeImportRepository = new Mock<IJdeImportRepository>();
            _jdeImportRepository.Setup(x => x.GetOpenPlannedOrderList(
                                                        It.IsAny<string>(),
                                                        It.IsAny<string>(),
                                                        It.IsAny<decimal>()))
                                                        .Returns(new List<OrrSysF3460Z1>());
            _jdeImportRepository.Setup(x => x.GetJdeItemDetails(
                                                        It.IsAny<List<int>>()))
                                                        .Returns(new List<JdeItemMasterDetailDto> { new JdeItemMasterDetailDto() });
        }

        private void SetUpJdeItemDetailsAsZero()
        {
            _jdeImportRepository = new Mock<IJdeImportRepository>();
            _jdeImportRepository.Setup(x => x.GetOpenPlannedOrderList(
                                                        It.IsAny<string>(),
                                                        It.IsAny<string>(),
                                                        It.IsAny<decimal>()))
                                                        .Returns(new List<OrrSysF3460Z1>());
            _jdeImportRepository.Setup(x => x.GetJdeItemDetails(
                                                        It.IsAny<List<int>>()))
                                                        .Returns(new List<JdeItemMasterDetailDto>());
        }

        private void SetUpJdeItemDetailsAsNull()
        {
            _jdeImportRepository = new Mock<IJdeImportRepository>();
            _jdeImportRepository.Setup(x => x.GetOpenPlannedOrderList(
                                                        It.IsAny<string>(),
                                                        It.IsAny<string>(),
                                                        It.IsAny<decimal>()))
                                                        .Returns(new List<OrrSysF3460Z1>());
            _jdeImportRepository.Setup(x => x.GetJdeItemDetails(
                                                        It.IsAny<List<int>>()))
                                                        .Returns((List<JdeItemMasterDetailDto>)null);
        }

        private void BaseSetup()
        {
            _inboundBusinessRules = new Mock<IProvideSapInboundSpecificBusinessRules>();
            _inboundBusinessRules.Setup(x => x.CreateJdePlannedOrders(
                                                        It.IsAny<List<DemandSapJde>>(),
                                                        It.IsAny<List<JdeItemMasterDetailDto>>(),
                                                        It.IsAny<int>(),
                                                        It.IsAny<PlannedOrderDto>(),
                                                        It.IsAny<List<OrrSysF3460Z1>>()))
                                                        .Returns(new List<OrrSysF3460Z1> { new OrrSysF3460Z1() });
            _inboundBusinessRules.Setup(x => x.CreatePlannedOrderDto()).Returns(new PlannedOrderDto());
            _inboundBusinessRules.Setup(x => x.GetDestinctJdeShortItemNumber(
                                                        It.IsAny<List<DemandSapJde>>()))
                                                        .Returns(new List<int>());

            _orrSysRepository = new Mock<IPlannedOrderRepository>();
            _orrSysRepository.Setup(x => x.BulkInsertByTypeList(It.IsAny<List<OrrSysF3460Z1>>()));
            _orrSysRepository.Setup(x => x.CreateHistoryTraking());
            _orrSysRepository.Setup(x => x.DeleteAllByTable());
            _orrSysRepository.Setup(x => x.BulkInsertByTypeList(It.IsAny<List<OrrSysF3460Z1>>()));

            _sapRepository = new Mock<ISapRepository>();
            _sapRepository.Setup(x => x.GetInboundPlannedOrderList()).Returns(new List<DemandSapJde>());
            _sapRepository.Setup(x => x.TriggerBulkInsertPlannedOrdersIntoOracle());
        }
    }
}