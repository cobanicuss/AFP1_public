using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGlDocDateTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of General-Ledger Doc-Date";
        private ResultDto _dto;


        [Test]
        public void GlDocDateShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => GlDocDateIsValid())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => GlDocDateMustBeConverted(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGlDocDate(GlDocDate);
        }
    }
}
