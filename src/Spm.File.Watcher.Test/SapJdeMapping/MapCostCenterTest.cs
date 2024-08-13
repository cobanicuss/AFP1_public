using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapCostCenterTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Cost-Centre";
        private ResultDto _dto;
        
        [Test]
        public void MaterialGroupHasNoJdeStockType()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingDoesNotContainJdeStockType())
                .And(_ => CostCenterGlPostingMappingDoesNotContainSapCostCentre())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForStockType(_dto))

            .BDDfy();
        }

        [Test]
        public void CostCenterGlPostingHasNoDefaultJdeDepartmentCode()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsJdeStockTypeOnly())
                .And(_ => CostCenterGlPostingMappingDoesNotContainSapCostCentre())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForDefaultJdeDepartmentCode(_dto))

            .BDDfy();
        }

        [Test]
        public void SapCostCenterDoesExistInMaterailGroupMapping()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsSapCostCenter())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapCostCenterIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void SapCostCenterDoesNotExistInMaterialGroupMappingButJdeDepartmentCodeDoesExist()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsJdeStockTypeOnly())
                .And(_ => CostCenterGlPostingMappingDoesHaveSapCostCenter())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapCostCenterIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapCostCenter(MapMaterialGroupList, MapCostCentreGlPostingList,
                Plant,
                LnType,
                CostCentre,
                ExInGst);
        }
    }
}