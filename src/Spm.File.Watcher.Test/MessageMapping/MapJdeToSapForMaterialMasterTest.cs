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
    public class MapJdeToSapForMaterialMasterTest
    {
        private IMapJdeToSapForMaterialMaster _classUnderTest;
        private Mock<ICreateMappingByLineItem> _lineItemMapping;
        private Mock<IMapPayloads> _mapPayloads;
        private Mock<ICastDto> _castDto;

        private const string SagaReferenceId = "SagaReferenceIdMaterialMaster";

        private List<MaterialMasterDto> _mappingInput;
        private MaterialMasterMappingResultSplitDto _mappingOutput;
        
        private readonly MaterialMasterSapDto _payloadInput = new MaterialMasterSapDto();
        private MaterialMasterCommand _payloadOutput;

        private MappingResultMaterialMasterDto _mappingResultMaterialMasterDto;

        private MaterialMasterPayload _materialMasterPayload;

        [Test]
        public void JdeToSapMappingShouldBeDoneCorrectly()
        {
            _mappingInput = MappingInput();
            _mappingResultMaterialMasterDto = SetMappingResultMaterialMasterDto();

            this.Given(_ => MappingFromJdeToSapForMaterialMaster())
            .When(_ => SplittingMappingResult())
            .Then(_ => LineItemMappingMethodMustBeCalled())
                .And(_ => ResultingMethodMustHaveCorrectValues())
            .BDDfy();
        }

        [Test]
        public void PayloadShouldBeMappedCorrectly()
        {
            _materialMasterPayload = SetMaterialMasterPayload();

            this.Given(_ => PayloadMappingForMaterialMaster())
            .When(_ => MappingPayload())
            .Then(_ => MappingMethodMustBeCalled())

            .BDDfy();
        }

        private MaterialMasterPayload SetMaterialMasterPayload()
        {
            return new MaterialMasterPayload
            {
                MaterialMasterPayloadItem = new List<MaterialMasterPayloadItem>()
            };
        }

        private void MappingFromJdeToSapForMaterialMaster()
        {
            _castDto = new Mock<ICastDto>();
            _mapPayloads = new Mock<IMapPayloads>();

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();
            _lineItemMapping.Setup(x => x.ForMaterialMaster(It.IsAny<MaterialMasterDto>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(_mappingResultMaterialMasterDto);

            _classUnderTest = new MapJdeToSapForMaterialMaster(_lineItemMapping.Object, _mapPayloads.Object, _castDto.Object);
        }

        private void PayloadMappingForMaterialMaster()
        {
            _castDto = new Mock<ICastDto>();

            _mapPayloads = new Mock<IMapPayloads>();
            _mapPayloads.Setup(x => x.MapMaterialMasterPayload(It.IsAny<MaterialMasterSapDto>())).Returns(_materialMasterPayload);

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();
           
            _classUnderTest = new MapJdeToSapForMaterialMaster(_lineItemMapping.Object, _mapPayloads.Object, _castDto.Object);
        }

        private void SplittingMappingResult()
        {
            _mappingOutput = _classUnderTest.SplitMappingResult(_mappingInput);
        }

        private void MappingPayload()
        {
            _payloadOutput = _classUnderTest.MapPayload(_payloadInput);
        }

        private void LineItemMappingMethodMustBeCalled()
        {
            _lineItemMapping.Verify(x => x.ForMaterialMaster(It.IsAny<MaterialMasterDto>(), It.IsAny<int>(), It.IsAny<string>()));
        }

        private void MappingMethodMustBeCalled()
        {
            _mapPayloads.Verify(x => x.MapMaterialMasterPayload(It.IsAny<MaterialMasterSapDto>()));
            Assert.IsNotNull(_payloadOutput);
        }

        private static List<MaterialMasterDto> MappingInput()
        {
            return new List<MaterialMasterDto>{new MaterialMasterDto()};
        }

        private void ResultingMethodMustHaveCorrectValues()
        {
            Assert.IsNotNull(_mappingOutput);
            Assert.IsNotNull(_mappingOutput.MappedDataDtoList);
            Assert.IsNotNull(_mappingOutput.MappingResultList);

            Assert.IsTrue(_mappingOutput.MappedDataDtoList.Any());
            Assert.IsFalse(_mappingOutput.MappingResultList.Any());

            Assert.AreEqual(_mappingOutput.MappedDataDtoList.FirstOrDefault().Key.SagaReferenceId, SagaReferenceId);
            Assert.AreEqual(_mappingOutput.MappedDataDtoList.FirstOrDefault().Value.SagaReferenceId, SagaReferenceId);
        }

        private static MappingResultMaterialMasterDto SetMappingResultMaterialMasterDto()
        {
            return new MappingResultMaterialMasterDto
            {
                Mapped = new KeyValuePair<MaterialMasterSagaDto, MaterialMasterSapDto>(new MaterialMasterSagaDto
                {
                    // Not testing mapping details. 
                    // Only testing that object has been added to list correctly.
                    // The details of mapping is in other tests.
                    // So only checking one property.
                    SagaReferenceId = SagaReferenceId 
                }, new MaterialMasterSapDto
                {
                    // Not testing mapping details. 
                    // Only testing that object has been added to list correctly.
                    // The details of mapping is in other tests.
                    // So only checking one property.
                    SagaReferenceId = SagaReferenceId 
                }),
                MappingProblemList = new List<ProblemDto>()
            };
        }
    }
}       