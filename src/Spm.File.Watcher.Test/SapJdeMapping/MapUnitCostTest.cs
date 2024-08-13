using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public class MapUnitCostTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void WhenUnitCostIsDeterminedThenResultMustBeCorrect()
        {
            this.Given(_ => InputsAreProvidedForUnitCostCalculation("7", "22"))
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectValueIsRetunedForUnitCostCalculation(_dto), "3.17")
            .BDDfy();
        }

        [Test]
        public void WhenUnitCostIsDeterminedAndInputsIsSmallerOrEqualToZeroThenMappingFailed()
        {
            this.Given(_ => InputsAreProvidedForUnitCostCalculation("0", "0"))
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapUnitCost(Kg, UnitCost);
        }
    }
}