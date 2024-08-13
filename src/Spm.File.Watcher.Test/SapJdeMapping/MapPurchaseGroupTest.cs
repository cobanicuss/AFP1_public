using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPurchaseGroupTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Purchase-Group";
        private ResultDto _dto;

        [Test]
        public void PurchaseGroupMappingHasNoJdePurchaseGroupAndNoDefaultExist()
        {
            this.Given(Scenario)
            .When(_ => PurchaseGroupMappingDoesNotContainJdePurchaseGroupAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForJdePurchaseGroup(_dto))

            .BDDfy();
        }

        [Test]
        public void PurchaseGroupMappingHasNoJdePurchaseGroupButDefaultDoesExist()
        {
            this.Given(Scenario)
            .When(_ => PurchaseGroupMappingDoesNotContainJdePurchaseGroupButDefaultExists())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultPurchaseGroupIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void PurchaseGroupMappingDoesContainJdePurchaseGroup()
        {
            this.Given(Scenario)
            .When(_ => PurchaseGroupMappingContainsJdePurchaseGroup())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PurchaseGroupIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPurchaseGroup(MapPurchaseGroupList, PurchaseGroup);
        }
    }
}