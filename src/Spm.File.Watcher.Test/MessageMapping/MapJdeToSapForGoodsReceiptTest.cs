using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.Service.Messages;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.MessageMapping
{
    [TestFixture]
    public class MapJdeToSapForGoodsReceiptTest
    {
        private IMapJdeToSapForAllGoods _classUnderTest;
        private Mock<ICreateMappingByLineItem> _lineItemMapping;
        private Mock<IMapPayloads> _mapPayloads;
        private Mock<ICastDto> _castDto;

        private const string SagaReferenceId = "SagaReferenceId";
        private const string MoveType = "MoveType";

        public const string ErrorInMapping = "ErrorInMapping";

        private List<GoodsDto> _mappingInput;
        private GoodsMappingResultSplitDto _mappingOutput;

        private readonly GoodsDto _payloadInput = new GoodsDto();
        private GoodsCommand _mapPayloadOutput;

        private GoodsPayload _goodsPayloadOutput;

        private MappingResultGoodsDto _mappingResultGoodsDto;

        [Test]
        public void JdeToSapMappingShouldBeDoneCorrectly()
        {
            _mappingInput = MappingInput();
            _mappingResultGoodsDto = SetMappingResultGoodsDto();

            this.Given(_ => MappingFromJdeToSapForGoods())
            .When(_ => SplittingMappingResult())
            .Then(_ => LineItemMappingMethodMustBeCalled())
                .And(_ => ResultingMethodMustHaveCorrectValues())

            .BDDfy();
        }

        [Test]
        public void JdeToSapMappingProblemsShouldBeListedInTheMappingResultList()
        {
            _mappingInput = MappingInput();
            _mappingResultGoodsDto = SetMappingResultGoodsDtoWithProblems();

            this.Given(_ => MappingFromJdeToSapForGoodsWithMappingError())
            .When(_ => SplittingMappingResult())
            .Then(_ => LineItemMappingMethodMustBeCalled())
                .And(_ => CastingOfProblemDtoMethodMustBeCalled())
                .And(_ => ErrorDetailsMustHaveCorrectValues())
            .BDDfy();
        }

        [Test]
        public void PayloadShouldBeMappedCorrectly()
        {
            _goodsPayloadOutput = SetGoodsPayload();

            this.Given(_ => PayloadMappingFromJdeToSapForGoods())
            .When(_ => MappingPayload())
            .Then(_ => MappingMethodMustBeCalled())
                .And(_ => ResultingMappingMustHaveCorrectValues())

            .BDDfy();
        }

        private void MappingFromJdeToSapForGoods()
        {
            _castDto = new Mock<ICastDto>();
            _castDto.Setup(x => x.AsString(It.IsAny<List<ProblemDto>>()))
                .Returns(It.IsAny<string>);

            SetupContinues();
        }

        private void MappingFromJdeToSapForGoodsWithMappingError()
        {
            _castDto = new Mock<ICastDto>();
            _castDto.Setup(x => x.AsString(It.IsAny<List<ProblemDto>>()))
                .Returns(ErrorInMapping);

            SetupContinues();
        }

        private void SetupContinues()
        {
            _mapPayloads = new Mock<IMapPayloads>();

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();
            _lineItemMapping.Setup(
                x => x.ForGoods(It.IsAny<GoodsDto>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_mappingResultGoodsDto);

            _classUnderTest = new MapJdeToSapForGoods(_lineItemMapping.Object, _mapPayloads.Object, _castDto.Object);
        }

        private void PayloadMappingFromJdeToSapForGoods()
        {
            _castDto = new Mock<ICastDto>();

            _mapPayloads = new Mock<IMapPayloads>();
            _mapPayloads.Setup(x => x.MapGoodsPayload(It.IsAny<GoodsDto>())).Returns(_goodsPayloadOutput);

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();

            _classUnderTest = new MapJdeToSapForGoods(_lineItemMapping.Object, _mapPayloads.Object, _castDto.Object);
        }

        private void SplittingMappingResult()
        {
            _mappingOutput = _classUnderTest.SplitMappingResult(_mappingInput, MoveType);
        }

        private void MappingPayload()
        {
            _mapPayloadOutput = _classUnderTest.MapPayload(_payloadInput);
        }

        private void LineItemMappingMethodMustBeCalled()
        {
            _lineItemMapping.Verify(x => x.ForGoods(It.IsAny<GoodsDto>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        private void MappingMethodMustBeCalled()
        {
            _mapPayloads.Verify(x => x.MapGoodsPayload(It.IsAny<GoodsDto>()));
        }

        private static List<GoodsDto> MappingInput()
        {
            return new List<GoodsDto> { new GoodsDto() };
        }

        private void ResultingMethodMustHaveCorrectValues()
        {
            Assert.IsNotNull(_mappingOutput);
            Assert.IsNotNull(_mappingOutput.MappedDataDtoList);
            Assert.IsNotNull(_mappingOutput.MappingResultList);

            Assert.IsTrue(_mappingOutput.MappedDataDtoList.Any());
            Assert.IsFalse(_mappingOutput.MappingResultList.Any());

            // Not testing mapping details. 
            // Only testing that object has been added to list correctly.
            // The details of mapping is in other tests.
            // So only checking one property.
            Assert.AreEqual(_mappingOutput.MappedDataDtoList[0].SagaReferenceId, SagaReferenceId);
        }

        private static MappingResultGoodsDto SetMappingResultGoodsDto()
        {
            return new MappingResultGoodsDto
            {
                Mapped = GetGoodsSagaDto(),
                MappingProblemList = new List<ProblemDto>()
            };
        }

        private static MappingResultGoodsDto SetMappingResultGoodsDtoWithProblems()
        {
            return new MappingResultGoodsDto
            {
                Mapped = GetGoodsSagaDto(),
                MappingProblemList = new List<ProblemDto>
                {
                    new ProblemDto
                    {
                       Result = "Error1",
                       RowNumber = 1
                    }
                }
            };
        }

        private static GoodsSagaDto GetGoodsSagaDto()
        {
            return new GoodsSagaDto
            {
                SagaReferenceId = SagaReferenceId
            };
        }

        private void ResultingMappingMustHaveCorrectValues()
        {
            Assert.IsNotNull(_mapPayloadOutput);
            Assert.IsNotNull(_mapPayloadOutput.Payload);
        }

        private static GoodsPayload SetGoodsPayload()
        {
            return new GoodsPayload
            {
                GoodsPayloadItem = new List<GoodsPayloadItem>()
            };
        }

        private void CastingOfProblemDtoMethodMustBeCalled()
        {
            _castDto.Verify(x => x.AsString(It.IsAny<List<ProblemDto>>()));
        }

        private void ErrorDetailsMustHaveCorrectValues()
        {
            Assert.IsNotNull(_mappingOutput);
            Assert.IsNotNull(_mappingOutput.MappedDataDtoList);
            Assert.IsNotNull(_mappingOutput.MappingResultList);

            Assert.IsFalse(_mappingOutput.MappedDataDtoList.Any());
            Assert.IsTrue(_mappingOutput.MappingResultList.Any());

            Assert.IsNotEmpty(_mappingOutput.MappingResultList[0]);
        }
    }
}