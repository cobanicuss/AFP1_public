using System.Collections.Generic;
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
    public class MapJdeToSapForGeneralLedgerTest
    {
        private IMapJdeToSapForGeneralLedger _classUnderTest;
        private Mock<ICreateMappingByLineItem> _lineItemMapping;
        private Mock<IMapPayloads> _mapPayloads;

        private const string HeaderTxt = "HeaderTxt";

        private MappingResultGeneralLedgerDto _mappingResultGeneralLedgerDto;
        private List<GeneralLedgerSapDto> _generalLedgerSapDtoList;

        private readonly List<GeneralLedgerSapDto> _payloadInput = new List<GeneralLedgerSapDto>();
        private GeneralLedgerCommand _payloadOutput = new GeneralLedgerCommand();

        private GeneralLedgerSapDto _generalLedgerSapDto;

        private readonly GeneralLedgerPayload _generalLedgerPayload = new GeneralLedgerPayload();

        [Test]
        public void JdeToSapMappingShouldBeDoneCorrectly()
        {
            _generalLedgerSapDtoList = SetGeneralLedgerSapDtoList();
            _generalLedgerSapDto = SetGeneralLedgerSapDto();

            this.Given(_ => MappingFromJdeToSapGeneralLedger())
            .When(_ => CreatingMapping())
            .Then(_ => LineItemMappingMethodMustBeCalled())
                .And(_ => ResultingMappingMustHaveCorrectValues())

            .BDDfy();
        }

        [Test]
        public void PayloadShouldBeMappedCorrectly()
        {
            this.Given(_ => PayloadMappingForGeneralLedger())
            .When(_ => MappingPayload())
            .Then(_ => MappingMethodMustBeCalled())
                .And(_ => ResultingPayloadMustBeValid())

            .BDDfy();
        }

        private void MappingFromJdeToSapGeneralLedger()
        {
            _mapPayloads = new Mock<IMapPayloads>();

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();
            _lineItemMapping.Setup(x => x.ForGeneralLedger(It.IsAny<GeneralLedgerSapDto>(), It.IsAny<List<ProblemDto>>(), 1))
                .Returns(_generalLedgerSapDto);

            _classUnderTest = new MapJdeToSapForGeneralLedger(_lineItemMapping.Object, _mapPayloads.Object);
        }

        private void PayloadMappingForGeneralLedger()
        {
            _mapPayloads = new Mock<IMapPayloads>();
            _mapPayloads.Setup(x => x.MapGeneralLedgerPayload(It.IsAny<List<GeneralLedgerSapDto>>())).Returns(_generalLedgerPayload);

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();

            _classUnderTest = new MapJdeToSapForGeneralLedger(_lineItemMapping.Object, _mapPayloads.Object);
        }

        private void CreatingMapping()
        {
            _mappingResultGeneralLedgerDto = _classUnderTest.CreateMapping(_generalLedgerSapDtoList);
        }

        private void MappingPayload()
        {
            _payloadOutput = _classUnderTest.MapPayload(_payloadInput);
        }

        private void LineItemMappingMethodMustBeCalled()
        {
            _lineItemMapping.Verify(x => x.ForGeneralLedger(It.IsAny<GeneralLedgerSapDto>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
        }

        private void MappingMethodMustBeCalled()
        {
            _mapPayloads.Verify(x => x.MapGeneralLedgerPayload(It.IsAny<List<GeneralLedgerSapDto>>()));
        }

        private static List<GeneralLedgerSapDto> SetGeneralLedgerSapDtoList()
        {
            return new List<GeneralLedgerSapDto> { new GeneralLedgerSapDto() };
        }

        private void ResultingMappingMustHaveCorrectValues()
        {
            Assert.IsNotNull(_mappingResultGeneralLedgerDto);
            Assert.IsNotNull(_mappingResultGeneralLedgerDto.MappedList);
            Assert.IsNotNull(_mappingResultGeneralLedgerDto.MappingProblemList);

            // Not testing mapping details. 
            // Only testing that object has been added to list correctly.
            // The details of mapping is in other tests.
            // So only checking one property.
            Assert.AreEqual(_mappingResultGeneralLedgerDto.MappedList[0].HeaderTxt, HeaderTxt);
        }

        private static GeneralLedgerSapDto SetGeneralLedgerSapDto()
        {
            return new GeneralLedgerSapDto
            {
                HeaderTxt = HeaderTxt
            };
        }

        private void ResultingPayloadMustBeValid()
        {
            Assert.IsNotNull(_payloadOutput);
            Assert.IsNotNull(_payloadOutput.Payload);
        }
    }
}