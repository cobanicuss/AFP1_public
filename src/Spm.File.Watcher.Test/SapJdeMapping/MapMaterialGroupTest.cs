using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapMaterialGroupTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Materail-Group";
        private ResultDto _dto;
        
        [Test]
        public void MaterialGroupHasNoJdeStockType()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingDoesNotContainJdeStockType())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForStockType(_dto))

            .BDDfy();
        }

        [Test]
        public void SapMaterialGroupDoesExistForJdeStockType()
        {
            this.Given(Scenario)
            .When(_ => MaterialGroupMappingContainsSapMaterialGroup())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => SapMaterialGroupIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapMaterialGroup(MapMaterialGroupList,
                Plant,
                LnType,
                ExInGst);
        }
    }
}
