using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapProductHierarchyTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void SalisburyShouldReturnTheCorrectResult()
        {
            this.Given(_ => SalisburyIsPassedInForProductHierarchy())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectProductHierarchyIsReturnedForSalisbury(_dto))
            .BDDfy();
        }

        [Test]
        public void OsulivansBeachShouldReturnTheCorrectResult()
        {
            this.Given(_ => OsulivansBeachIsPassedInForProductHierarchy())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectProductHierarchyIsReturnedForOsulivansBeach(_dto))
            .BDDfy();
        }

        [Test]
        public void ProductHierarchyShouldReturnErrorConditionForBadInput()
        {
            this.Given(_ => BadInputForProductHierarchy())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorConditionForProductHierarchyIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapProductHierarchy(ProductHierarcyPlant);
        }

    }
}