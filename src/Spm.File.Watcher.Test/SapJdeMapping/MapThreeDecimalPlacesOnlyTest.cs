using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapThreeDecimalPlacesOnlyTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnThreeDecimalPlacesAfterCommaWithoutRounding()
        {
            this.Given(_ => ProperIntegerIsPassedInForThreeDecimalFormatting())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectThreeDecimalResultIsReturned(_dto))
            .BDDfy();
        }

        [Test]
        public void BadInputsShouldReturnZeroWithThreeDecimalPlaces()
        {
            this.Given(_ => BadIntegerIsPassedInForThreeDecimalFormatting())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => StringWithZeroAndThreeDecimalPalcesIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapThreeDecimalPlacesOnly(ThreeDecimalPlacesInput);
        }
    }
}
