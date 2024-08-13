using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGlProfitCentreTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of General-Ledger Profit-Centre";
        private ResultDto _dto;

        [Test]
        public void ShouldReturnErrorConditionWhenJdeGlAccountAndDefaultWasNotFound()
        {
            this.Given(Scenario)
            .When(_ => GlAccountsGlPositingMappingDoesNotContainJdeGlAccountAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForDefaultJdeGlAccountCode2(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnEmptySapTypeWhenJdeGlAccountIsPassedAndSapTypeIsNotDefaultCost()
        {
            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithSapType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => EmptyStringIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnEmptySapTypeWhenJdeGlAccountIsPassedAndSapTypeIsDefault()
        {
            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultSapType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => EmptyStringIsReturned(_dto))

            .BDDfy();
        }
        
        [Test]
        public void ShouldReturnErrorConditionWhenJdeGlAccountIsPassedAndSapTypeIsDefaultProfitAndJdeDeptCodeAndDefaultDoesNotExist()
        {
            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultProfitSapType())
                .And(_ => ProfitCenterGlPostingMappingDoesNotContainJdeDepartmentCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForDefaultJdeGlAccountCode1(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnSapCostCentreWhenJdeGlAccountIsPassedAndJdeDeptIsDefaultAndSapTypeIsDefaultProfit()
        {
            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultProfitSapType())
                .And(_ => ProfitCenterGlPostingMappingDoesContainDefaultJdeDepartmentCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapProfitCenterIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnSapCostCentreWhenJdeGlAccountIsPassedAndJdeDeptIsPassedAndSapTypeIsDefaultCost()
        {
            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultProfitSapType())
                .And(_ => ProfitCenterGlPostingMappingDoesContainJdeDepartmentCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapProfitCenterIsReturned(_dto))

            .BDDfy();
        }


        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGlProfitCentre(MapGlAccountsGlPostingsList, MapProfitCentreGlPostingList, GlAccount, CostCentre);
        }
    }
}
