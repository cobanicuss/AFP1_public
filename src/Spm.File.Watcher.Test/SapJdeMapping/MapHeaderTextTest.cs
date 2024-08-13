using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapHeaderTextTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Header-Text";
        private ResultDto _dto;

        [Test]
        public void ShouldReturnFormatedHeaderText()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => HeaderTextIsProperlyFormated(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapHeaderText(Id);
        }
    }
}