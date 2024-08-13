using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Spm.File.Watcher.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Handlers;
using Spm.OrrSys.Service.Repositories;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class CacheMapUpdateRequestCommandHandlerTest
    {
        private Mock<IOrrSysMappingRepository> _repository;
        private NServiceBus.Testing.Handler<CacheMapUpdateRequestCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IOrrSysMappingRepository>();
            _repository.Setup(x => x.GetBranchMapping()).Returns(GetMapBranchList());
            _repository.Setup(x => x.GetCompanyCodeMapping()).Returns(GetMapCompanyCodeList());
            _repository.Setup(x => x.GetCostCentreGlPostingMapping()).Returns(GetMapCostCentreGlPostingList());
            _repository.Setup(x => x.GetDocTypeMapping()).Returns(GetMapDocTypesList());
            _repository.Setup(x => x.GetGlAccountsGlPostingMapping()).Returns(GetMapGlAccountsGlPostingList());
            _repository.Setup(x => x.GetLocationMapping()).Returns(GetMapLocationList());
            _repository.Setup(x => x.GetMaterialGroupMapping()).Returns(GetMapMaterialGroupList());
            _repository.Setup(x => x.GetPlantMapping()).Returns(GetMapPlantList());
            _repository.Setup(x => x.GetProfitCentreGlPostingMapping()).Returns(GetMapProfitCentreGlPostingList());
            _repository.Setup(x => x.GetPurchaseGroupMapping()).Returns(GetMapPurchaseGroupList());
            _repository.Setup(x => x.GetUnitOfMeasureMapping()).Returns(GetMapUnitOfMeasureList());

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new CacheMapUpdateRequestCommandHandler(bus, _repository.Object));
        }

        [Test]
        public void TheFileWatcherServiceHasSendCacheUpdateCommand()
        {
            this.Given("Cache map request from file-watcher-service")
                .When("Handler is called")
                .Then(_ => CacheMapUpdateRequestCommandMustBeSend())
                    .And(_ => AllTheDataExtractionMethodsMustBeCalled())

                .BDDfy();
        }

        private void AllTheDataExtractionMethodsMustBeCalled()
        {
            _repository.Verify(x => x.GetBranchMapping(), Times.Once());
            _repository.Verify(x => x.GetCompanyCodeMapping(), Times.Once());
            _repository.Verify(x => x.GetCostCentreGlPostingMapping(), Times.Once());
            _repository.Verify(x => x.GetDocTypeMapping(), Times.Once());
            _repository.Verify(x => x.GetGlAccountsGlPostingMapping(), Times.Once());
            _repository.Verify(x => x.GetLocationMapping(), Times.Once());
            _repository.Verify(x => x.GetMaterialGroupMapping(), Times.Once());
            _repository.Verify(x => x.GetPlantMapping(), Times.Once());
            _repository.Verify(x => x.GetProfitCentreGlPostingMapping(), Times.Once());
            _repository.Verify(x => x.GetPurchaseGroupMapping(), Times.Once());
            _repository.Verify(x => x.GetUnitOfMeasureMapping(), Times.Once());
        }

        private void CacheMapUpdateRequestCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<CacheMapResponseCommand>(command =>
                    command.FileWatcherSagaId == FileWatcherSagaInitConst.FileWatcherSagaId
                    && command.MapBranchList.Count == 1
                    && command.MapCompanyCodeList.Count == 1
                    && command.MapCostCentreGlPostingList.Count == 1
                    && command.MapDocTypesList.Count == 1
                    && command.MapGlAccountsGlPostingList.Count == 1
                    && command.MapLocationList.Count == 1
                    && command.MapMaterialGroupList.Count == 1
                    && command.MapPlantList.Count == 1
                    && command.MapProfitCentreGlPostingList.Count == 1
                    && command.MapPurchaseGroupList.Count == 1
                    && command.MapUnitOfMeasureList.Count == 1
                )
                .OnMessage(new CacheMapUpdateRequestCommand { FileWatcherSagaId = FileWatcherSagaInitConst.FileWatcherSagaId });
        }

        private static List<MapBranch> GetMapBranchList()
        {
            return new List<MapBranch> { new MapBranch() };
        }

        private static List<MapCompanyCode> GetMapCompanyCodeList()
        {
            return new List<MapCompanyCode> { new MapCompanyCode() };
        }

        private static List<MapCostCentreGlPosting> GetMapCostCentreGlPostingList()
        {
            return new List<MapCostCentreGlPosting> { new MapCostCentreGlPosting() };
        }

        private static List<MapDocTypes> GetMapDocTypesList()
        {
            return new List<MapDocTypes> { new MapDocTypes() };
        }

        private static List<MapGlAccountsGlPosting> GetMapGlAccountsGlPostingList()
        {
            return new List<MapGlAccountsGlPosting> { new MapGlAccountsGlPosting() };
        }

        private static List<MapLocation> GetMapLocationList()
        {
            return new List<MapLocation> { new MapLocation() };
        }

        private static List<MapMaterialGroup> GetMapMaterialGroupList()
        {
            return new List<MapMaterialGroup> { new MapMaterialGroup() };
        }

        private static List<MapPlant> GetMapPlantList()
        {
            return new List<MapPlant> { new MapPlant() };
        }

        private static List<MapProfitCentreGlPosting> GetMapProfitCentreGlPostingList()
        {
            return new List<MapProfitCentreGlPosting> { new MapProfitCentreGlPosting() };
        }

        private static List<MapPurchaseGroup> GetMapPurchaseGroupList()
        {
            return new List<MapPurchaseGroup> { new MapPurchaseGroup() };
        }

        private static List<MapUnitOfMeasure> GetMapUnitOfMeasureList()
        {
            return new List<MapUnitOfMeasure> { new MapUnitOfMeasure() };
        }
    }
}