using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapLocationTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Location";
        private ResultDto _dto;

        [Test]
        public void LocationMappingHasNoJdeLocationAndNoDefaultExist()
        {
            this.Given(Scenario)
            .When(_ => LocationMappingDoesNotContainJdeLocationAndNoDefault())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsBad(_dto))
                .And(_ => ErrorDetailsProvidedForJdeLocation(_dto))

            .BDDfy();
        }

        [Test]
        public void LocationMappingHasNoJdeLocationButDefaultDoesExist()
        {
            this.Given(Scenario)
            .When(_ => LocationMappingDoesNotContainJdeLocationButDefaultExists())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => DefaultLocationIsReturned(_dto))

            .BDDfy();
        }

        [Test]
        public void LocationMappingDoesContainJdeLocation()
        {
            this.Given(Scenario)
            .When(_ => LocationMappingContainsJdeLocation())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => LocationIsReturned(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapLocation(MapLocationList, Location);
        }
    }
}