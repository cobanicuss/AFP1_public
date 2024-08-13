using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapCreatDateTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Create Date";
        private ResultDto _dto;

        [Test]
        public void CreateDateShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CreateDateMustBeConverted(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapCreatDate(CreateDate);
        }
    }
}
