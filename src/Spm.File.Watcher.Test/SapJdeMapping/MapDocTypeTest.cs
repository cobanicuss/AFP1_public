using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapDocTypeTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Doc-Type";
        private ResultDto _dto;

        [Test]
        public void DocTypeMappingHasNoJdeDocType()
        {
            this.Given(Scenario)
            .When(_ => DocTypeMappingDoesNotContainJdeDocType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForJdeDocType(_dto))

            .BDDfy();
        }
        
        [Test]
        public void DocTypeMappingContainsJdeDocType()
        {
            this.Given(Scenario)
            .When(_ => DocTypeMappingDoesContainJdeDocType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DocTypeIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapDocType(MapDocTypesList, DocType);
        }
    }
}
