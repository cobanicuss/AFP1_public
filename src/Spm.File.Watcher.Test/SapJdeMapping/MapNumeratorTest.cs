using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapNumeratorTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Numerator";
        private ResultDto _dto;

        [Test]
        public void ProperFractionShouldReturnValidDenominator()
        {
            this.Given(Scenario)
             .When(_ => ProperFractionProvidedForNumerator())
                 .And(_ => ExecutingMapping())
             .Then(_ => MappingIsGood(_dto))
                 .And(_ => CorrectNumeratorIsReturned(_dto))

             .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapNumerator(NumeratorInput);
        }
    }
}