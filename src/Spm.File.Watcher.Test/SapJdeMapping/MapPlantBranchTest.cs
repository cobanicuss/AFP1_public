using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPlantBranchTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Plant from Branch";
        private ResultDto _dto;

        [Test]
        public void PlantBranchMappingHasNoJdeBranchCode()
        {
            this.Given(Scenario)
            .When(_ => PlantBranchMappingDoesNotContainJdeBranchCodeAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForMissingPlantBranchCode(_dto))

            .BDDfy();
        }
        
        [Test]
        public void PlantBranchMappingDoesContainJdeBranchCode()
        {
            this.Given(Scenario)
            .When(_ => PlantBranchMappingContainsJdeBranchCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PlantIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPlantBranch(MapPlantBranchList, Plant);
        }
    }
}