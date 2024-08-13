using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Dto;
using Spm.OrrSys.Service.Map;
using Spm.OrrSys.Service.Repositories;
using Spm.OrrSys.Service.Soap.SqlReportService;
using Spm.OrrSys.Service.TestCertificates;
using Spm.Shared.Payloads;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.BusinessRules
{
    [TestFixture]
    public class TestCertificateBusinessTest
    {
        private DateTime _dateTimeNow;
        private List<OutboundTestCertificateDto> _testCertificateDtoList;
        private TestCertificateRequestCommand _command;

        private const string CertificateId = "test10";
        private const string NewPdf = "Test20";
        private const string PurchaseOrder = "Test30";
        private const int RowNumber = 123;
        private const string FirstLevelLot = "Test40";
        private const string HeatNumber = "Test50";
        private const string SapMaterialNumber = "Test60";
        private const string InboundId = "Test70";
        private const string Ivaitm = "Test80";
        private const string SalesOrderNumber = "Test90";
        private const string SaleInvoiceNumber = "Test100";
        private const string CustomerName = "Test110";
        private const string CustomerAccountNumber = "Test120";
        private const string PackNumber = "Test130";
        private const string DateUpdated = "Test140";
        private const string ShipTo = "Test150";
        private const string EmailAddress = "Test160";

        private IDoTestCertificateBusiness _classUnderTest;
        private Mock<IJdeImportRepository> _jde;
        private Mock<ITestCertRepository> _testCert;
        private Mock<IDoTestCertificateCommunication> _soap;
        private Mock<IMapTestCertificateMessage> _map;
        private Mock<IWriteTestCertificates> _write;
        private Mock<IDeleteTestCertificates> _del;
        private Mock<IUniqueNumbers> _uniqueNumber;

        private TestCertificateOutboundPayload _output;
        private IList<OutboundTestCertificateDto> _outputTestCertificateDtoList;
        private IList<int> _uniqueIds;

        [SetUp]
        public void Setup()
        {
            _uniqueNumber = new Mock<IUniqueNumbers>();
            _del = new Mock<IDeleteTestCertificates>();
            _map = new Mock<IMapTestCertificateMessage>();
            _soap = new Mock<IDoTestCertificateCommunication>();
            _write = new Mock<IWriteTestCertificates>();
            _jde = new Mock<IJdeImportRepository>();
            _testCert = new Mock<ITestCertRepository>();

            _classUnderTest = new TestCertificateBusiness(
                _jde.Object,
                _testCert.Object,
                _soap.Object,
                _map.Object,
                _write.Object,
                _del.Object,
                _uniqueNumber.Object);
        }

        [Test]
        public void TestCertificatePayloadMustBeCratedForCommandMessageWhenTriggerIsReceived()
        {
            this.Given(_ => ListOfOutboundTestCertificateDtoIsPassedIn())
           .When(_ => CreatingPaylaod())
           .Then(_ => UniqueNumberMustBeCreated())
               .And(_ => MappingToPdfDtoIsDone())
               .And(_ => WritePdfToFile())
               .And(_ => WriteToBakup())
               .And(_ => Base64EncodingWithLineBreaks())
               .And(_ => OutputTestCertificatePayloadContainsCorrectData())
           .BDDfy();
        }

        [Test]
        public void InboundTestCertificateRequestWasReceivedByServiceForSoap()
        {
            this.Given(_ => CommandMessageWasRecieved())
           .When(_ => CreatingInboundTestCertificates())
           .Then(_ => GetCrossReferenceNumberListMustBeCalled())
               .And(_ => BuilkInsertOfTestCertsIsDone())
           .BDDfy();
        }

        [Test]
        public void InboundTestCertificateRequestThrowsErrorIfPayloadHasCountOfZero()
        {
            this.Given(_ => CommandMessageWithPayloadCountOfZero())
           .When(_ => CreateDependanciesForInboundTestCertificate())
           .Then(_ => AnErrorIsThrown())
           .BDDfy();
        }

        [Test]
        public void InboundTestCertificateRequestThrowsErrorIfPayloadIsEmpty()
        {
            this.Given(_ => CommandMessageWithPayloadNull())
           .When(_ => CreateDependanciesForInboundTestCertificate())
           .Then(_ => AnErrorIsThrown())
           .BDDfy();
        }

        [Test]
        public void InboundTestCertificateMustErrorIfCrossReferenceNumbersAreZero()
        {
            this.Given(_ => CommandMessageWasRecieved())
           .When(_ => CommandMessageWithZeroCrossReferenceNumbers())
           .Then(_ => AnErrorIsThrown())
           .BDDfy();
        }


        [Test]
        public void InboundTestCertificateMustErrorIfCrossReferenceNumbersAreNull()
        {
            this.Given(_ => CommandMessageWasRecieved())
           .When(_ => CommandMessageWithNullCrossReferenceNumbers())
           .Then(_ => AnErrorIsThrown())
           .BDDfy();
        }

        [Test]
        public void DeletingPreviousTestCertificatesMustBeDoneIfRestricted()
        {
            this.Given(_ => RestictedTestCertificatesNeedsToBeDeleted())
           .When(_ => DeletePreviousTestCertificates(true))
           .Then(_ => PreviousMustBeDeleted())
            .And(_ => BakupsMustBeDeleted())
           .BDDfy();
        }

        [Test]
        public void DeletingPreviousTestCertificatesMustNotBeDoneIfRestricted()
        {
            this.Given(_ => RestictedTestCertificatesNeedsToBeDeleted())
           .When(_ => DeletePreviousTestCertificates(false))
           .Then(_ => PreviousMustBeNotDeleted())
            .And(_ => BakupsMustBeDeleted())
           .BDDfy();
        }

        [Test]
        public void GetOutboundTestCertifcateData()
        {
            this.Given("Outbound data is needed")
           .When(_ => CallingGetOutboundData())
           .Then(_ => GetOutboundTestCerticateDataMustBeCalled())
           .BDDfy();
        }

        [Test]
        public void GetUniqueDto()
        {
            this.Given("List of Dto's are passed in.")
           .When(_ => CallingGetUniqueDtoList())
           .Then(_ => UniqueNumbersExist())
           .BDDfy();
        }

        [Test]
        public void WhenCreatingOutboundResponseUpdateMustBeDone()
        {
            this.Given("Database must be updated.")
           .When(_ => CallingBulkTestCertUpdate())
           .Then(_ => ReposityBulkUpdateMustBeCalled())
           .BDDfy();
        }

        private void ReposityBulkUpdateMustBeCalled()
        {
            _testCert.Verify(x => x.BulkUpdateTestCertificate(It.IsAny<string[]>()));
        }

        private void CallingBulkTestCertUpdate()
        {
            _testCert.Setup(x => x.BulkUpdateTestCertificate(It.IsAny<string[]>()));

            _classUnderTest.CreateOutboundTestCertificateResponse(It.IsAny<string[]>());
        }

        private void UniqueNumbersExist()
        {
            Assert.IsTrue(_uniqueIds.Any());
        }

        private void CallingGetUniqueDtoList()
        {
            ListOfOutboundTestCertificateDtoIsPassedIn();

            _uniqueIds = _classUnderTest.GetByUniqueReportGroup(_testCertificateDtoList);
        }

        private void CallingGetOutboundData()
        {
            ListOfOutboundTestCertificateDtoIsPassedIn();

            _jde.Setup(x => x.GetOutboundTestCerticateData())
                .Returns(_testCertificateDtoList);

            _outputTestCertificateDtoList = _classUnderTest.GetOutboundTestCertificateData();
        }

        private void GetOutboundTestCerticateDataMustBeCalled()
        {
            Assert.IsTrue(_outputTestCertificateDtoList.Any());
        }

        private void PreviousMustBeNotDeleted()
        {
            _del.Verify(x => x.DeletePreviousTestCertficateFiles(It.IsAny<string>()),Times.Never());
        }

        private void BakupsMustBeDeleted()
        {
            _del.Verify(x => x.DeleteBakupTestCertificateFilesIfFolderIsFull(It.IsAny<string>(), It.IsAny<int>()));
        }

        private void PreviousMustBeDeleted()
        {
            _del.Verify(x => x.DeletePreviousTestCertficateFiles(It.IsAny<string>()));
        }

        private void DeletePreviousTestCertificates(bool isRestricted)
        {
            _classUnderTest.DeletePreviousTestCertificates(isRestricted, It.IsAny<string>(), It.IsAny<int>());
        }

        private void RestictedTestCertificatesNeedsToBeDeleted()
        {
            _del.Setup(x => x.DeletePreviousTestCertficateFiles(It.IsAny<string>()));
            _del.Setup(x => x.DeleteBakupTestCertificateFilesIfFolderIsFull(It.IsAny<string>(), It.IsAny<int>()));
        }

        private void CommandMessageWasRecieved()
        {
            _command = new TestCertificateRequestCommand
            {
                CertificateId = CertificateId,
                InboundId = InboundId,
                Payload = new TestCertificateRequestPayload
                {
                    TestCertificatePayloadItemList = new List<TestCertificateRequestPayloadItem>
                    {
                        new TestCertificateRequestPayloadItem
                        {
                            SapMaterialNumber = SapMaterialNumber,
                            SalesOrderNumber = SalesOrderNumber,
                            SaleInvoiceNumber = SaleInvoiceNumber,
                            CustomerName = CustomerName,
                            CustomerAccountNumber = CustomerAccountNumber,
                            PackNumber = PackNumber,
                            DateUpdated = DateUpdated,
                            ShipTo = ShipTo,
                            EmailAddress = EmailAddress,
                            PurchaseOrder = PurchaseOrder,
                        }
                    }
                }
            };
        }

        private void ListOfOutboundTestCertificateDtoIsPassedIn()
        {
            _dateTimeNow = DateTime.Now;

            _testCertificateDtoList = new List<OutboundTestCertificateDto>
            {
                new OutboundTestCertificateDto
                {
                    ReportGroup = 1,
                    PurchaseOrder = PurchaseOrder,
                    RowNumber = RowNumber,
                    FirstLevelLot = FirstLevelLot,
                    HeatNumber = HeatNumber,
                    SapMaterialNumber = SapMaterialNumber
                }
            };
        }

        private void CreatingPaylaod()
        {
            _uniqueNumber.Setup(x => x.CreateUniqueCertificateNumber()).Returns(It.IsAny<string>());
            
            _map.Setup(x => x.ToPdfDto(It.IsAny<OutboundTestCertificateDto>(), It.IsAny<string>()))
                .Returns(PdfDto());
            
            _soap.Setup(x => x.GetPdf(It.IsAny<TestCertDto>()))
                .Returns(ByteArray());
            
            _write.Setup(x => x.ToFile(It.IsAny<byte[]>(), It.IsAny<TestCertDto>(), It.IsAny<string>()));
            _write.Setup(x => x.ToBakup(It.IsAny<byte[]>(), It.IsAny<TestCertDto>(), It.IsAny<string>()));
            _write.Setup(x => x.ToBase64EncodingWithLineBreaks(It.IsAny<byte[]>())).Returns(NewPdfAsString());

            _output = _classUnderTest.CreatePayloadForOutboundTestCertificateRequest(_testCertificateDtoList);
        }

        private static string NewPdfAsString()
        {
            return NewPdf;
        }

        private static byte[] ByteArray()
        {
            return new[] {new byte(), new byte()};
        }

        private void UniqueNumberMustBeCreated()
        {
            _uniqueNumber.Verify(x => x.CreateUniqueCertificateNumber(),Times.Once());
        }

        private void MappingToPdfDtoIsDone()
        {
            _map.Verify(x => x.ToPdfDto(It.IsAny<OutboundTestCertificateDto>(), It.IsAny<string>()), Times.Once());
        }

        private void WritePdfToFile()
        {
            _write.Verify(x => x.ToFile(It.IsAny<byte[]>(), It.IsAny<TestCertDto>(), It.IsAny<string>()),Times.Once());
        }

        private void WriteToBakup()
        {
            _write.Verify(x => x.ToBakup(It.IsAny<byte[]>(), It.IsAny<TestCertDto>(), It.IsAny<string>()), Times.Once());
        }

        private void Base64EncodingWithLineBreaks()
        {
            _write.Verify(x => x.ToBase64EncodingWithLineBreaks(It.IsAny<byte[]>()), Times.Once());
        }

        private static TestCertDto PdfDto()
        {
            return new TestCertDto
            {
                CertificateId = CertificateId
            };
        }

        private void OutputTestCertificatePayloadContainsCorrectData()
        {
            Assert.AreEqual(_output.CertificateNumber, CertificateId);
            Assert.AreEqual(_output.CertificateDateTime.Date, _dateTimeNow.Date);
            Assert.AreEqual(_output.CertificateFunctionCode, Service.Constants.CertificateFunctionCode);
            Assert.AreEqual(_output.SellerPartyIdScheme, Service.Constants.SellerPartyIdScheme);
            Assert.AreEqual(_output.CustomerPartyIdentifier, Service.Constants.CustomerPartyIdentifier);
            Assert.AreEqual(_output.Attachment, NewPdf);
            Assert.AreEqual(_output.AttachmentType, Service.Constants.FileType);
            Assert.AreEqual(_output.AttachmentEncoding, Service.Constants.EncodingType);
            Assert.AreEqual(_output.CustomerOrderNumber, PurchaseOrder);
            Assert.AreEqual(_output.LineItems.Count, _testCertificateDtoList.Count);
        }

        private void CreatingInboundTestCertificates()
        {
            CreateDependanciesForInboundTestCertificate();

            _classUnderTest.CreateInboundTestCertificate(_command);
        }

        private void CreateDependanciesForInboundTestCertificate()
        {
            _jde.Setup(x => x.GetCrossReferenceNumberList(It.IsAny<List<string>>()))
                .Returns(CreateMaterialCrossRererenceDto());

            _testCert.Setup(x => x.BulkInsertTestCertificate(It.IsAny<List<DespatchedPacksByCustomerOrderDto>>()));
        }

        private static List<MaterialCrossReferenceNumberDto> CreateMaterialCrossRererenceDto()
        {
            return new List<MaterialCrossReferenceNumberDto>
            {
                new MaterialCrossReferenceNumberDto
                {
                    Ivcitm = SapMaterialNumber,
                    Ivaitm = Ivaitm
                }
            };
        }

        private static List<MaterialCrossReferenceNumberDto> CreateNullMaterialCrossRererenceDto()
        {
            return null;
        }

        private static List<MaterialCrossReferenceNumberDto> CreateZeroMaterialCrossRererenceDto()
        {
            return new List<MaterialCrossReferenceNumberDto>();
        }

        private void GetCrossReferenceNumberListMustBeCalled()
        {
            _jde.Verify(x => x.GetCrossReferenceNumberList(It.IsAny<List<string>>()), Times.Once());
        }

        private void BuilkInsertOfTestCertsIsDone()
        {
            _testCert.Verify(x => x.BulkInsertTestCertificate(It.IsAny<List<DespatchedPacksByCustomerOrderDto>>()), Times.Once());
        }

        private void CommandMessageWithPayloadCountOfZero()
        {
            _command = new TestCertificateRequestCommand
            {
                CertificateId = CertificateId,
                InboundId = InboundId,
                Payload = new TestCertificateRequestPayload
                {
                    TestCertificatePayloadItemList = new List<TestCertificateRequestPayloadItem>()
                }
            };
        }

        private void CommandMessageWithPayloadNull()
        {
            _command = new TestCertificateRequestCommand
            {
                CertificateId = CertificateId,
                InboundId = InboundId,
                Payload = null
            };
        }

        private void AnErrorIsThrown()
        {
            Assert.Throws<Exception>(() => _classUnderTest.CreateInboundTestCertificate(_command));
        }

        private void CommandMessageWithZeroCrossReferenceNumbers()
        {
            _jde.Setup(x => x.GetCrossReferenceNumberList(It.IsAny<List<string>>()))
                .Returns(CreateZeroMaterialCrossRererenceDto());

            _testCert.Setup(x => x.BulkInsertTestCertificate(It.IsAny<List<DespatchedPacksByCustomerOrderDto>>()));
        }

        private void CommandMessageWithNullCrossReferenceNumbers()
        {
            _jde.Setup(x => x.GetCrossReferenceNumberList(It.IsAny<List<string>>()))
                .Returns(CreateNullMaterialCrossRererenceDto());

            _testCert.Setup(x => x.BulkInsertTestCertificate(It.IsAny<List<DespatchedPacksByCustomerOrderDto>>()));
        }
    }
}