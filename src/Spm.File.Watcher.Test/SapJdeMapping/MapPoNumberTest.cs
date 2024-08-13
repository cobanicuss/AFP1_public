using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPoNumberTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Purchase-Order Number";
        private ResultDto _dto;

        [Test]
        public void PoNumberShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PoNumberOutputMustHaveLeadingZeros(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPoNumber(PoNumber);
        }
    }
}
