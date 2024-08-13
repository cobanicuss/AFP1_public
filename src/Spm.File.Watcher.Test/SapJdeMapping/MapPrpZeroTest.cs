using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPrpZeroTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnCorrectPrpForSalisbury()
        {
            this.Given(_ => SalisburyIsPassedInForFormattingPrp())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PrpShouldBeFormattedCorrectlyForSalisbury(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnCorrectPrpIfNotSalisbury()
        {
            this.Given(_ => SalisburyIsNotPassedInForFormattingPrp())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PrpShouldBeFormattedCorrectlyForNonSalisburyInput(_dto))
            .BDDfy();
        }
        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPrpZero(Mcu, Prp0);
        }
    }
}