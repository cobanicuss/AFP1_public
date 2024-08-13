using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapZzdm2NTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void IfSec2InputsIsEmptyThenReturnSec1()
        {
            this.Given(_ => Sec2InputIsNullOrEmptyForZzdm2N())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Sec1IsReturnedForZzdm2N(_dto))
            .BDDfy();
        }

        [Test]
        public void IfSec2InputContainsValueThenReturnSec2()
        {
            this.Given(_ => Sec2InputCoantainsProperValue())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => Sec2IsReturnedForZzdm2N(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapZzdm2N(Sec1, Sec2);
        }
    }
}