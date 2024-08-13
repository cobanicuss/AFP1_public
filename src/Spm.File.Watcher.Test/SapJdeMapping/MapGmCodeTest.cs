using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGmCodeTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Gm-Code";
        private ResultDto _dto;

        [Test]
        public void GmCodeShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => GmCodeOutputMustHaveLeadingZeros(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGmCode(GmCodeInput);
        }
    }
}
