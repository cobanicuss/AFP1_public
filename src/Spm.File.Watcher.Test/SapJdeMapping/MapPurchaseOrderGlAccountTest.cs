using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPurchaseOrderGlAccountTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Purchase-Order General-Ledger account";
        private ResultDto _dto;

        [Test]
        public void MaterialGroupMappingHasNoJdeStockType()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingDoesNotContainJdeStockType())
                .And(_ => GlAccountsGlPositingMappingDoesNotContainJdeGlAccountAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForStockType(_dto))

            .BDDfy();
        }

        [Test]
        public void MaterialGroupMappingHasJdeStockTypeButNoJdeGlAccountExistsInGlAccountsGlPostingMapping()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsJdeStockTypeOnly())
                .And(_ => GlAccountsGlPositingMappingDoesNotContainJdeGlAccountAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForDefaultJdeGlAccountCode2(_dto))

            .BDDfy();
        }

        [Test]
        public void MaterialGroupMappingHasJdeStockTypeAndJdeGlAccountExistsInGlAccountsGlPostingMapping()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsSapGlAccount())
                .And(_ => GlAccountGlPositingMappingContainsJdeGlAccount())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapGlAccountIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void MaterialGroupMappingHasJdeStockTypeOnlyAndJdeGlAccountExistsInGlAccountsGlPostingMapping()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsJdeStockTypeOnly())
                .And(_ => GlAccountGlPositingMappingContainsJdeGlAccount())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapGlAccountIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPurchaseOrderGlAccount(MapMaterialGroupList, MapGlAccountsGlPostingsList,
                Plant,
                LnType,
                GlAccount,
                ExInGst);
        }
    }
}