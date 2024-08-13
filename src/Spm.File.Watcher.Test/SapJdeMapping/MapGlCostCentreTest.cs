using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGlCostCentreTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of General-Ledger Cost-Centre";
        private ResultDto _dto;

        [Test]
        public void ShouldReturnDefaultSapCostCentreWhenReferenceDocumentStartsWithOv()
        {
            this.Given(Scenario)
            .When(_ => ReferenceDocumentNumberStartsWithOv())
                .And(_ => GlAccountDoesNotMatchAnyDefaults())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultSapCostCentreIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionWhenJdeGlAccountAndDefaultWasNotFound()
        {
            ResetRefDocNumAndGlAccount();

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
            ResetRefDocNumAndGlAccount();

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
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultSapType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => EmptyStringIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionWhenJdeGlAccountIsPassedAndSapTypeIsDefaultCostAndJdeDeptCodeAndDefaultDoesNotExist()
        {
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultCostSapType())
                .And(_ => CostCenterGlPostingMappingDoesNotContainSapCostCentre())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForDefaultJdeDepartmentCode(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnSapCostCentreWhenJdeGlAccountIsPassedAndJdeDeptIsDefalutAndSapTypeIsDefaultCost()
        {
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultCostSapType())
                .And(_ => CostCenterGlPostingMappingHasDefaultJdeDepartmentCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapCostCenterIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnSapCostCentreWhenJdeGlAccountIsPassedAndJdeDeptIsPassedAndSapTypeIsDefaultCost()
        {
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccountWithDefaultCostSapType())
                .And(_ => CostCenterGlPostingMappingDoesHaveSapCostCenter())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapCostCenterIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGlCostCentre(MapGlAccountsGlPostingsList, MapCostCentreGlPostingList, GlAccount, RefDocNum, CostCentre);
        }
    }
}