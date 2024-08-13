using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPhysicalPackSizeTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Physical-Pack-Size";
        private ResultDto _dto;

        [Test]
        public void ValidInputsShouldProvidePhysicalPackSizeOutput()
        {
            this.Given(Scenario)
            .When(_ => ValidPlantAndHeightAndWidthIsProvided())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectPhysicalPackSizeIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void WhenPlantIsDefaultValueThenReturnEmptyPhysicalPackSizeResult()
        {
            this.Given(Scenario)
            .When(_ => DefaultPlantAsInputValue())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => EmptyStringIsReturned(_dto))

            .BDDfy();
        }


        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPhysicalPackSize(Plant, Height, Width);
        }
    }
}
