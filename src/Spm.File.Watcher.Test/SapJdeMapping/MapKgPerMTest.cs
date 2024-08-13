using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapKgPerMTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void DevidingKilogramsByMetersMustReturn3DigitsRoundedUp()
        {
            this.Given(_ => PassingInValidKilogramsAndMeters())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => DivisionWithIntMustBeRoundedUp(_dto))
            .BDDfy();
        }

        [Test]
        public void DevidingByZeroReturnZeroAsString()
        {
            this.Given(_ => PassingInValidKilogramsAndZeroMeters())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => DivisionByZeroMustReturn3DigitZeroString(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapKgPerM(Kg, M);
        }
    }
}