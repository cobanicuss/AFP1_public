using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapDenominatorTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Denominator";
        private ResultDto _dto;

        [Test]
        public void ProperFractionShouldReturnValidDenominator()
        {
            this.Given(Scenario)
             .When(_ => ProperFractionProvidedForDenominator())
                 .And(_ => ExecutingMapping())
             .Then(_ => MappingIsGood(_dto))
                 .And(_ => CorrectDenominatorIsReturned(_dto))

             .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapDenominator(DenominatorInput);
        }
    }
}