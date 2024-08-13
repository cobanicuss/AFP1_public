using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapStorageTypeTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void ShouldReturnStorageTypeIfBranchCodeExist()
        {
            this.Given(_ => CorrectBranchCodeIsPassedInForFindingStorageType())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectStorageTypeIsReturnedForGivenBranchCode(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionIfStorageTypeNotFound()
        {
            this.Given(_ => BranchCodeCannotBeFoundInBranchMapping())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForEmptyBranchCode(_dto))
            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionIfStorageTypeIsEmpty()
        {
            this.Given(_ => BranchCodeCanBeFoundButSapProfitCenreIsEmpty())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForEmptyBranchCode(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapStorageType(MapPlantBranchList, BranchCode);
        }
    }
}