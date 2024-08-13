using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPackWeightTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;
        
        [Test]
        public void InputPackWeightWithMoreThan3DigitsAfterCommaMustRoundUp()
        {
            this.Given(_ => PassingInValidJdePackWeightWithMoreThan3Digits())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => ThreeDigitsRoundedUp(_dto, JdePackWeight))
            .BDDfy();
        }

        [Test]
        public void InputPackWeightWith3DigitsAfterCommaMustNotRoundUp()
        {
            this.Given(_ => PassingInValidJdePackWeightWith3Digits())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => ThreeDigitsRoundedUp(_dto, JdePackWeight))
            .BDDfy();
        }

        [Test]
        public void InputPackWeightWithLessThan3DigitsAfterCommaMustNotRoundUp()
        {
            this.Given(_ => PassingInValidJdePackWeightWithLessThan3Digits())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => ThreeDigitsRoundedUp(_dto, JdePackWeight))
            .BDDfy();
        }

        [Test]
        public void InputPackWeightWithZeroInFrontOfCommaShouldRemainAsIs()
        {
            this.Given(_ => PassingInValidJdePackWeightWithZeroInFrontOfComma())
            .When(_ => ExecutingMapping())
            .Then(_ => MustHaveThreeDigitsAfterComma(_dto))
                .And(_ => ThreeDigitsRoundedUp(_dto, JdePackWeight))
                .And(_ => ZeroStillRemains(_dto))
            .BDDfy();
        }
        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPackWeight(JdePackWeight);
        }
    }
}