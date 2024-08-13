using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapPoUnitTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of PurchareOrder-Unit";
        private ResultDto _dto;

        [Test]
        public void UnitOfMeasureMappingHasNoJdeUomAndNoDefaultExist()
        {
            this.Given(Scenario)
            .When(_ => UnitOfMeasureMappingDoesNotContainJdeUnitOfMeasureAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForUnitOfMeasure(_dto))

            .BDDfy();
        }

        [Test]
        public void UnitOfMeasureMappingHasNoJdeUomButDefaultDoesExist()
        {
            this.Given(Scenario)
            .When(_ => UnitOfMeasureMappingDoesNotContainJdeUnitOfMeasureButDefaultExist())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultUnitOfMeasureIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void UnitOfMeasureMappingContainsJdeUom()
        {
            this.Given(Scenario)
            .When(_ => UnitOfMeasureMappingContainsJdeUnitOfMeasure())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => UnitOfMeasureIsReturned(_dto))

            .BDDfy();
        }


        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapPoUnit(MapUnitOfMeasureList, PoUnit);
        }
    }
}
