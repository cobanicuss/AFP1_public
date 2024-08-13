//using NUnit.Framework;
//using Spm.Service.ForSoap.Config;
//using Spm.Service.ForSoap.Mapper;
//using Spm.Service.ForSoap.Messages;
//using Spm.Shared.Payloads;
//using TestStack.BDDfy;

//namespace Spm.Service.ForSoap.Test.SoapMessageMap
//{
//    [TestFixture]
//    public class InventoryMovementMessageMapTest
//    {
//        private IInventoryMovementMessageMap _classUnderTest;
//        private ProductAchievementSapCommand _nserviceBusMessage;
//        private InventoryMovementDetails _soapMessage;

//        private const string SagaReferenceId = "a";
//        private const string LotNumber = "z";
//        private const string MovementNumber = "b";
//        private const string MovementDateTime = "c";
//        private const string MovementDateTimeFormatText = "d";
//        private const string StockControllerPartyText = "e";
//        private const string MovementHeaderNoteText = "f";
//        private const string InventoryNatureCodeIn = "prime";
//        private const InventoryNatureList InventoryNatureCodeOut = InventoryNatureList.Prime;
//        private const string InventoryMovementLineNumber = "h";
//        private const string IdentifierText = "i";
//        private const string CustomerArticleNumber = "j";
//        private const string SupplierArticleNumber = "k";
//        private const string LocationIdentifierText = "l";
//        private static readonly string QuantityUnitCodeIn = MeasureUnitList.ANN.ToString();
//        private const MeasureUnitList QuantityUnitCodeOut = MeasureUnitList.ANN;
//        private const string QuantityTextIn = "1.1";
//        private const decimal QuantityTextOut = 1.1m;
//        private const string WarehouseSerialNumber = "o";
//        private const string PackageWarehouseLocationText = "p";
//        private const string PartPackIn = "q";
//        private readonly PackageTypeCode _partPackOut = new PackageTypeCode { Value = PackageTypeList.Other };

//        private const string Sndprn = DefaultSapVariables.OrrSysDevAndTest;

//        [Test]
//        public void MappingNsbToSoapMustBeCorrectly()
//        {
//            this.Given(_ => NServiceBusMessageToSoapIsRequired())
//           .When(_ => MappingMessages())
//           .Then(_ => SoapMessageMustBeMappedCorrectly())
//           .BDDfy();
//        }

//        private void NServiceBusMessageToSoapIsRequired()
//        {
//            ProfileConfigVariables.SndPrn = Sndprn;

//            _nserviceBusMessage = new ProductAchievementSapCommand
//            {
//                SagaReferenceId = SagaReferenceId,
//                LotNumber = LotNumber,
//                Payload = new InventoryMovementPayload
//                {
//                    MovementNumber = MovementNumber,
//                    MovementDateTime = MovementDateTime,
//                    MovementDateTimeFormatText = MovementDateTimeFormatText,
//                    StockControllerPartyText = StockControllerPartyText,
//                    MovementHeaderNoteText = MovementHeaderNoteText,
//                    InventoryNatureCode = InventoryNatureCodeIn,
//                    InventoryMovementLineNumber = InventoryMovementLineNumber,
//                    IdentifierText = IdentifierText,
//                    CustomerArticleNumber = CustomerArticleNumber,
//                    SupplierArticleNumber = SupplierArticleNumber,
//                    LocationIdentifierText = LocationIdentifierText,
//                    QuantityUnitCode = QuantityUnitCodeIn,
//                    QuantityText = QuantityTextIn,
//                    WarehouseSerialNumber = WarehouseSerialNumber,
//                    PackageWarehouseLocationText = PackageWarehouseLocationText,
//                    PartPack = PartPackIn
//                }
//            };

//            _classUnderTest = new InventoryMovementMessageMap();
//        }

//        private void MappingMessages()
//        {
//            _soapMessage = _classUnderTest.MakeSoapMessage(_nserviceBusMessage);
//        }

//        private void SoapMessageMustBeMappedCorrectly()
//        {
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.MessageID, SagaReferenceId);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.InventoryMovementNumber, MovementNumber);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.InventoryMovementDateTime.dateTimeFormatText, MovementDateTimeFormatText);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.InventoryMovementDateTime.Value, MovementDateTime);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.StockControllerParty.PartyIdentification.PartyIdentifier.Value, DefaultSapVariables.OrrSysDevAndTest);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.InventoryMovementHeaderNote.Value, MovementHeaderNoteText);
//            Assert.AreEqual(_soapMessage.InventoryMovementHeader.InventoryNatureCode.Value, InventoryNatureCodeOut);

//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementLineNumber, InventoryMovementLineNumber);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].CustomerParty.PartyIdentification.PartyIdentifier.Value, IdentifierText);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].ItemSpecification.ItemIdentification.CustomerArticleNumber.Value, CustomerArticleNumber);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].ItemSpecification.ItemIdentification.SupplierArticleNumber.Value, SupplierArticleNumber);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].TransferNumber, QuantityUnitCodeIn);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].MoveToLocation.PlaceOfDestination.LocationIdentifier.Value, LocationIdentifierText);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].MovementQuantity[0].quantityUnitCode, QuantityUnitCodeOut);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].MovementQuantity[0].quantityUnitCodeListIdentifier, string.Empty);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].MovementQuantity[0].quantityUnitCodeListAgencyIdentifier, string.Empty);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].MovementQuantity[0].Value, QuantityTextOut);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].PackageLine[0].PackageInformation.WarehouseSerialNumber, LotNumber);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].PackageLine[0].PackageInformation.PackageWarehouseLocation.Value, PackageWarehouseLocationText);
//            Assert.AreEqual(_soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].PackageLine[0].PackageInformation.PackageTypeCode.Value, _partPackOut.Value);
//        }
//    }
//}
