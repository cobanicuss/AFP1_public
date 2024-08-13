using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapCompCodeTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Company-Code";
        private ResultDto _dto;

        [Test]
        public void CompanyCodeMappingHasNoJdeCompanyCodeAndNoDefaultExist()
        {
            this.Given(Scenario)
            .When(_ => CompanyCodeMappingDoesNotContainJdeCompanyCodeAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForJdeCompanyCode(_dto))

            .BDDfy();
        }

        [Test]
        public void CompanyCodeMappingHasNoJdeCompanyCodeButDefaultDoesExist()
        {
            this.Given(Scenario)
            .When(_ => CompanyCodeMappingDoesNotContainJdeCompanyCodeButDefaultExists())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultCompanyCodeIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void CompanyCodeMappingDoesContainJdeCompanyCode()
        {
            this.Given(Scenario)
            .When(_ => CompanyCodeMappingContainsJdeCompanyCode())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CompanyCodeIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapCompCode(MapCompanyCodeList, CompanyCode);
        }
    }
}
