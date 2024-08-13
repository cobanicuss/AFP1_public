using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPurchOrgTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Purchase-Organization";
        private ResultDto _dto;

        [Test]
        public void PurchOrgOutputShouldBeFormatedCorrectly()
        {
            this.Given(Scenario)
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => PurchaseOrganizationOutputMustHaveLeadingZeros(_dto))

            .BDDfy();
        }
        
        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPurchOrg(PurchOrgInput);
        }
    }
}