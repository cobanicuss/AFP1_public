using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.SapJdeMapping
{
    [TestFixture]
    public class MapProductAttributeTest : MappingBusinessRulesTestBase
    {
        private ResultDto _dto;

        [Test]
        public void SalisburyShouldReturnTheCorrectResult()
        {
            this.Given(_ => SalisburyIsPassedInForProductAttribute())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => CorrectProductAttributeIsReturned(_dto))
            .BDDfy();
        }

        [Test]
        public void AnythingOtherThanSalisburyShouldReturnEmptyString()
        {
            this.Given(_ => OtherThanSalisburyIsPassedInForProductAttribute())
            .When(_ => ExecutingMapping())
            .Then(_ => MappingIsGood(_dto))
                .And(_ => EmptyStringIsReturned(_dto))
            .BDDfy();
        }

        protected override void ExecutingMapping()
        {
            _dto = ClassUnderTest.MapProductAttribute(ProductAttributeInput);
        }
    }
}