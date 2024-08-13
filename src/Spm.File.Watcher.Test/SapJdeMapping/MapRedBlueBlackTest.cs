using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapRedBlueBlackTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnUpperCaseLiteralRedForRedString()
        {
            this.Given(_ => ProperRedTextIsPassedIn())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectColorFormatingAndTextIsReturned(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnUpperCaseLiteralBlueForBlueString()
        {
            this.Given(_ => ProperBlueTextIsPassedIn())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectColorFormatingAndTextIsReturned(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnUpperCaseLiteralBlackForBlackString()
        {
            this.Given(_ => ProperBlackTextIsPassedIn())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectColorFormatingAndTextIsReturned(_dto))
            .BDDfy();
        }
        
        [Test]
        public void AnyOtherInputShouldReturnEmptyString()
        {
            this.Given(_ => BadInputColorStringIsPassedIn())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto)) /*BEWARE: Mapping result Is-Ok for bad input color string*/
                .And(_ => EmptyStringIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapRedBlueBlack(ColorTextInput);
        }
    }
}