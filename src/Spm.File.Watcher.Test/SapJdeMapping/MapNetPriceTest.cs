using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapNetPriceTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Net-Price";
        private ResultDto _dto;

      [Test]
        public void ShouldReturnDefaultWhenNetPriceIsTooSmall()
        {
            this.Given(Scenario)
            .When(_ => NetPriceIsToSmall())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultNetPriceIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnProperNetPrice()
        {
            this.Given(Scenario)
            .When(_ => NetPriceIsCorrect())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => NetPriceIsValid(_dto))

            .BDDfy();
        }
        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapNetPrice(NetPrice);
        }
    }
}