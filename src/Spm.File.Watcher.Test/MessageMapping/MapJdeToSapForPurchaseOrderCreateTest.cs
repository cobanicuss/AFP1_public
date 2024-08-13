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
    public class MapJdeToSapForPurchaseOrderCreateTest
    {
        private IMapJdeToSapPurchaseOrderCreate _classUnderTest;
        private Mock<ICreateMappingByLineItem> _lineItemMapping;
        private Mock<IMapPayloads> _mapPayloads;

        private const string PoNumber = "a";
        private const string Currency = "jde1";
        private const string DeleteInd = "jde2";
        private const string ShortText = "jde3";
        private const string VendMat = "jde4";
        private const string Quantity = "jde5";
        private const string PriceUnit = "jde6";
        private const string OverDlvTol = "jde7";
        private const string Acctasscat = "jde8";
        private const string PreqName = "jde9";
        private const string SerialNo = "jde10";

        private readonly List<PurchaseOrderSapDto> _mappingInput = MappingInput();
        private MappingResultPurchaseOrderDto _mappingOutput;

        private readonly List<PurchaseOrderSapDto> _payloadInput = new List<PurchaseOrderSapDto>();
        private PurchaseOrderCreateCommand _payloadOutput;

        [Test]
        public void JdeToSapMappingShouldBeDoneCorrectlyForPurchaseOrderCreate()
        {
            this.Given(_ => MappingFromJdeToSap())
            .When(_ => MappingIsDone())
            .Then(_ => ReturnedMappingShouldBeCorrect())
                .And(_ => MappingIsCorrect())

            .BDDfy();
        }

        [Test]
        public void PayloadShouldBeMappedCorrectlyForGeneralLedger()
        {
            this.Given(_ => PayloadMappingFromJdeToSap())
            .When(_ => MappingPayloadIsDone())
            .Then(_ => ReturnedPayloadShouldBeCorrect())

            .BDDfy();
        }

        private void MappingFromJdeToSap()
        {
            _mapPayloads = new Mock<IMapPayloads>();
            _lineItemMapping = new Mock<ICreateMappingByLineItem>();

            _classUnderTest = new MapJdeToSapPurchaseOrderCreate(_lineItemMapping.Object, _mapPayloads.Object);
        }

        private void PayloadMappingFromJdeToSap()
        {
            _mapPayloads = new Mock<IMapPayloads>();
            _mapPayloads.Setup(x => x.MapPurchaseOrderPayload(It.IsAny<List<PurchaseOrderSapDto>>())).Returns(It.IsAny<PurchaseOrderPayload>());

            _lineItemMapping = new Mock<ICreateMappingByLineItem>();
           
            _classUnderTest = new MapJdeToSapPurchaseOrderCreate(_lineItemMapping.Object, _mapPayloads.Object);
        }

        private void MappingIsDone()
        {
            _mappingOutput = _classUnderTest.CreateMapping(_mappingInput);
        }

        private void MappingPayloadIsDone()
        {
            _payloadOutput = _classUnderTest.MapPayload(_payloadInput);
        }

        private void ReturnedMappingShouldBeCorrect()
        {
            _lineItemMapping.Verify(x => x.ForPurchaseOrderCreate(It.IsAny<PurchaseOrderDto>(), It.IsAny<List<ProblemDto>>(), It.IsAny<int>()));
        }

        private void ReturnedPayloadShouldBeCorrect()
        {
            _mapPayloads.Verify(x => x.MapPurchaseOrderPayload(It.IsAny<List<PurchaseOrderSapDto>>()));
            Assert.IsNotNull(_payloadOutput);
        }

        private static List<PurchaseOrderSapDto> MappingInput()
        {
            return new List<PurchaseOrderSapDto>{new PurchaseOrderSapDto
            {
                PoNumber = PoNumber,
                Currency = Currency,
                DeleteInd = DeleteInd,
                ShortText = ShortText,
                VendMat = VendMat,
                Quantity = Quantity,
                PriceUnit = PriceUnit,
                OverDlvTol = OverDlvTol,
                Acctasscat = Acctasscat,
                PreqName = PreqName,
                SerialNo = SerialNo
            }};
        }

        private void MappingIsCorrect()
        {
            Assert.IsNotNull(_mappingOutput);
        }
    }
}