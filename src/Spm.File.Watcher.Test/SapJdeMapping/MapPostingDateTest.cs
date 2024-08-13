using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPostingDateTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Posting Date";
        private ResultDto _dto;
        
        [Test]
        public void PostingDateShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => PostingDateIsValid())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PostingDateMustBeConverted(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPostingDate(PostingDate);
        }
    }
}