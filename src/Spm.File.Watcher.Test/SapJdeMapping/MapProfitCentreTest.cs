using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapProfitCentreTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnSapProfitCentreIfBranchCodeExist()
        {
            this.Given(_ => CorrectBranchCodeIsPassedInForFindingSapProfitCenre())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectSapProfitCenreIsReturnedForGivenBranchCode(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionIfBranchCodeNotFound()
        {
            this.Given(_ => BranchCodeCannotBeFoundInBranchMapping())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForEmptyBranchCode(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionIfSapProfitCentreIsEmpty()
        {
            this.Given(_ => BranchCodeCanBeFoundButSapProfitCenreIsEmpty())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForEmptyBranchCode(_dto))
            .BDDfy();
        }

        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapProfitCentre(MapPlantBranchList, BranchCode);
        }
    }
}
