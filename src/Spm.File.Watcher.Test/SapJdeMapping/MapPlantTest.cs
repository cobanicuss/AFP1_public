using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPlantTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of SalesPlant";
        private ResultDto _dto;

        [Test]
        public void PlantMappingHasNoJdeBranchCodeAndNoDefaultExist()
        {
            this.Given(Scenario)
            .When(_ => PlantMappingDoesNotContainJdeBranchCodeAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForJdeBranchCode(_dto))

            .BDDfy();
        }

        [Test]
        public void PlantMappingHasNoJdeBranchCodeButDefaultDoesExist()
        {
            this.Given(Scenario)
            .When(_ => PlantMappingDoesNotContainJdeBranchCodeButDefaultExist())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultPlantIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void PlantMappingDoesContainJdeBranchCode()
        {
            this.Given(Scenario)
            .When(_ => PlantMappingContainsJdeBranchCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PlantIsReturned(_dto))

            .BDDfy();
        }


        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPlant(MapPlantList, Plant);
        }
    }
}
