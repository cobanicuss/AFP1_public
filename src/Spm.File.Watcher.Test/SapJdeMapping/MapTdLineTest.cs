using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapTdLineTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnConcatinatedInputWithSpaceBetweenThem()
        {
            this.Given(_ => ProperStingValuesArePassedInForTdLine())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectStringWithTwoConcatenatedSpacesIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapTdLine(Prp2Desc2, Dsc1, Dsc2);
        }
    }
}