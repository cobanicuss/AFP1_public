using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapStorageSectionTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnCorrectStorageSectionForSalisburyAndPrp()
        {
            this.Given(_ => CorrectValueForSalisburyAndPrpIsPassedInForFindingStorageSection())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectStorageSectionIsReturnedForSalisburyAndPrp(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnCorrectStorageSectionForSalisburyAndPrpIsEmpty()
        {
            this.Given(_ => CorrectValueForSalisburyAndPrpEmptyIsPassedInForFindingStorageSection())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectStorageSectionIsReturnedForSalisburyAndPrpEmpty(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnCorrectStorageSectionForOsulivansBeach()
        {
            this.Given(_ => CorrectValueForOsulivansBeachIsPassedInForFindingStorageSection())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectStorageSectionIsReturnedForOsulivansBeach(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionIfNothingFoundForSalisburyAndOsulivansBeach()
        {
            this.Given(_ => NothingIsFoundForStorageSection())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForNontFindingStorageSection(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapStorageSection(Mcu, Prp4);
        }
    }
}