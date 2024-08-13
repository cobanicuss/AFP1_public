using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapDeliveryDateTest : MappingBusinessRulesTestBase
    {
        private const string Scenario = "Mapping of Delivery Date";
        private ResultDto _dto;
        
        [Test]
        public void DeliveryDateInThePastShouldBeFormatedToToday()
        {
            this.Given(Scenario)
            .When(_ => DeliveryDateIsInThePast())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ConvertedDeliveryDateMustBeToday(_dto))

            .BDDfy();
        }

        [Test]
        public void DeliveryDateInTheFutureShouldBeFormatedToTheFuture()
        {
            this.Given(Scenario)
            .When(_ => DeliveryDateIsInTheFuture())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ConvertedDeliveryDateMustBeInTheFuture(_dto))

            .BDDfy();
        }

        [Test]
        public void DeliveryDateForTodayShouldBeFormatedToToday()
        {
            this.Given(Scenario)
            .When(_ => DeliveryDateIsToday())
                .And(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => ConvertedDeliveryDateMustBeToday(_dto))

            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapDeliveryDate(DeliveryDate);
        }
    }
}
