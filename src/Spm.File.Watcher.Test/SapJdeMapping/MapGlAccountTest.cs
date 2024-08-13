using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapGlAccountTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of General-Ledger account";
        private ResultDto _dto;

        [Test]
        public void ShouldReturnDefaultSapGlAccountWhenReferenceDocumentStartsWithOv()
        {
            this.Given(Scenario)
            .When(_ => ReferenceDocumentNumberStartsWithOv())
                .And(_ => GlAccountDoesNotMatchAnyDefaults())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultSapGlAccountIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnErrorConditionWhenJdeGlAccountIsNotFoundAndNoDefaultExists()
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
        public void ShouldReturnSapGlAccountWhenDefaultJdeGlAccountIsUsed()
        {
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsDefaultJdeGlAccount())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapGlAccountIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void ShouldReturnSapGlAccountWhenJdeGlAccountIsUsed()
        {
            ResetRefDocNumAndGlAccount();

            this.Given(Scenario)
            .When(_ => GlAccountGlPositingMappingContainsJdeGlAccount())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapGlAccountIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapGlAccount(MapGlAccountsGlPostingsList, GlAccount, RefDocNum);
        }
    }
}
