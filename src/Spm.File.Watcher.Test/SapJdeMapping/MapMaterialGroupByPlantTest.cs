using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapMaterialGroupByPlantTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void SalisburyShouldReturnTheCorrectResult()
        {
            this.Given(_ => SalisburyIsPassedInForMaterialGroup())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectMaterialGroupIsReturnedForSalisbury(_dto))
            .BDDfy();
        }

        [Test]
        public void OsulivansBeachShouldReturnTheCorrectResult()
        {
            this.Given(_ => OsulivansBeachIsPassedInForMaterialGroup())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectMaterialGroupIsReturnedForOsulivansBeach(_dto))
            .BDDfy();
        }

        [Test]
        public void MaterailGroupByPlantShouldReturnErrorConditionForBadInput()
        {
            this.Given(_ => BadInputForMaterialGroupByPlant())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorConditionForMaterialGroupByPlantIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapMaterialGroupByPlant(MaterailGroupPlant);
        }

    }
}
