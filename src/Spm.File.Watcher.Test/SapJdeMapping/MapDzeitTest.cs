using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public class MapDzeitTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void AllLeadingZerorsMustBeStrippedOffForDzeit()
        {
            this.Given(_ => ProperStingValuesArePassedInWihtTenLeadingZeros())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => AllLeadingZerosAreGone(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapDzeit(Spr1);
        }
    }
}