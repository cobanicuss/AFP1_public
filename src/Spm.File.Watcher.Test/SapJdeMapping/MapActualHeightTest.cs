using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapActualHeightTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ActualHeightShouldReturnWithTheCorrectFormatting()
        {
            this.Given(_ => ProperInputForActualHeightIsPassedIn())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectRoundingWith2PlacesAfterComma(_dto))
            .BDDfy();
        }
        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapActualHeight(ActualHeightInput);
        }
    }
}