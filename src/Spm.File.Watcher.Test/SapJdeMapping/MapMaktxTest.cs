using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    public class MapMaktxTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnFirstFortyCharactersOfInputWhenBiggerThanForty()
        {
            this.Given(_ => ProperInputStingIsBiggerThanForty())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ReturnUpToAndIncludingFirstFortyCharacters(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnUpToAndIncludingTheFirstFortyCharactersOfInputWhenInputIsSmallerThanOrEqualToForty()
        {
            this.Given(_ => ProperInputStingIsForty())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ReturnUpToAndIncludingFirstFortyCharacters(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnUpToAndIncludingNCharactersOfInputWhenInputIsSmallerThanForty()
        {
            this.Given(_ => ProperInputStingIsTen())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ReturnUpToAndIncludingFirstNCharacters(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapMaktx(Dsc12);
        }
    }
}