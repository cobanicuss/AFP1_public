using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPoItemTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Purchase-Order Item";
        private ResultDto _dto;

        [Test]
        public void PoItemShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PoItemOutputMustBeStripped(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPoItem(PoItem);
        }
    }
}