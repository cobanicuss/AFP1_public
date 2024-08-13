using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGoodsDocDateTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Goods Receipt/Renewal Doc-Date";
        private ResultDto _dto;

        [Test]
        public void ShouldReturnEmptyStringWhenGoodsDocDateIsInvalid()
        {
            this.Given(Scenario)
            .When(_ => GoodsDocDateIsInvalid())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto)) /*Yes BEWARE: Mapping IS good for empty string*/
                .And(_ => EmptyStringIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void GoodsDocDateShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => GoodsDocDateIsValid())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => GoodsDocDateMustBeConverted(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGoodsDocDate(GoodsDocDate);
        }
    }
}