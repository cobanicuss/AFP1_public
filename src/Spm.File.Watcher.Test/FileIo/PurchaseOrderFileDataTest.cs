using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Spm.File.Watcher.Service.Downloader;
using Spm.File.Watcher.Service.Dto;
using TestStack.BDDfy;
using Constants = Spm.File.Watcher.Service.Constants;

namespace Spm.File.Watcher.Test.FileIo
{
    [TestFixture]
    public class PurchaseOrderFileDataTest : FileDataTestBase
    {
        private IGetDataForPurchaseOrder _classUnderTest;
        private IList<PurchaseOrderDto> _dto;
        private Exception _result;

        protected override int Columns => Constants.PurchaseOrderColumnCount;
        protected override string TestFileName => "PurchaseOrderFileDataTest.csv";
        protected string TestErrorFileName => "PurchaseOrderErrorTest.txt";

        [SetUp]
        public void Setup()
        {
            FileManipulator = new Mock<IWorkWithFiles>();
            FileManipulator.Setup(x => x.CreateErrorFileForIssue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            FileManipulator.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));

            _classUnderTest = new PurchaseOrderFileData(FileManipulator.Object);
            _dto = null;
        }

        [Test]
        public override void DataShouldBeExtractedFromCorrecltyFormattedFile()
        {
            this.Given(_ => CorrectlyFormatedFile())
           .When(_ => ExtractingDataFromFile())
           .Then(_ => DataMustContainDataFromFile())
                .And(_ => RemoveTestFile())
           .BDDfy();
        }

        [Test]
        public override void DataShouldBeEmptyForEmptyFile()
        {
            this.Given(_ => FileWithHeadingOnlyAndNoData())
           .When(_ => ExtractingDataFromFile())
           .Then(_ => DataMustBeEmptyButNotNull())
                .And(_ => RemoveTestFile())
           .BDDfy();
        }

        [Test]
        public override void DtoShouldBeEmptyForFileWithOnlyEmptyStringValues()
        {
            this.Given(_ => DataIsEmptyString())
           .When(_ => ExtractingDataFromFile())
           .Then(_ => DataMustContainItemWithAllEmptyString())
                .And(_ => RemoveTestFile())
           .BDDfy();
        }

        [Test]
        public override void AddedEndCommaInFileLineShouldBeIgnored()
        {
            this.Given(_ => FileWithExtraCommaAtEndOfLine())
           .When(_ => ExtractingDataFromFile())
           .Then(_ => DataMustContainDataFromFile())
                .And(_ => RemoveTestFile())
           .BDDfy();
        }

        [Test]
        public override void WrongColumnsInFileMustCreateErrorCondition()
        {
            this.Given(_ => FileWithLessColumns())
           .When(_ => ExtractingDataFromBadFile())
           .Then(_ => DataMustBeNull())
                .And(_ => ErrorMustBeThrown())
           .BDDfy();

            RemoveTestFile();
        }

        protected override void DataMustBeEmptyButNotNull()
        {
            Assert.IsFalse(_dto.Any());
        }

        protected override void DataMustBeNull()
        {
            Assert.IsNull(_dto);
        }

        protected override void ExtractingDataFromFile()
        {
            _dto = _classUnderTest.ExtractDataFromFile(Path, Path, TestFileName, TestErrorFileName);
        }

        protected void ExtractingDataFromBadFile()
        {
            _result = ExecuteActionThatThrows(MethodUnderTestWhichThrowsException);
        }

        protected override void MethodUnderTestWhichThrowsException()
        {
            _dto = _classUnderTest.ExtractDataFromFile(Path, Path, TestFileName, TestErrorFileName);
        }

        protected void ErrorMustBeThrown()
        {
            Assert.IsInstanceOf<FormatException>(_result);
        }

        protected override void DataMustContainDataFromFile()
        {
            Assert.AreEqual(_dto[0].PoNumber, Data[0]);
            Assert.AreEqual(_dto[0].CompCode, Data[1]);
            Assert.AreEqual(_dto[0].DocType, Data[2]);
            Assert.AreEqual(_dto[0].CreateDate, Data[3]);
            Assert.AreEqual(_dto[0].CreatedBy, Data[4]);
            Assert.AreEqual(_dto[0].ItemIntvl, Data[5]);
            Assert.AreEqual(_dto[0].Vendor, Data[6]);
            Assert.AreEqual(_dto[0].PurchOrg, Data[7]);
            Assert.AreEqual(_dto[0].PurGroup, Data[8]);
            Assert.AreEqual(_dto[0].Currency, Data[9]);
            Assert.AreEqual(_dto[0].DocDate, Data[10]);
            Assert.AreEqual(_dto[0].PoItem, Data[11]);
            Assert.AreEqual(_dto[0].DeleteInd, Data[12]);
            Assert.AreEqual(_dto[0].ShortText, Data[13]);
            Assert.AreEqual(_dto[0].Plant, Data[14]);
            Assert.AreEqual(_dto[0].StgeLoc, Data[15]);
            Assert.AreEqual(_dto[0].MatlGroup, Data[16]);
            Assert.AreEqual(_dto[0].VendMat, Data[17]);
            Assert.AreEqual(_dto[0].Quantity, Data[18]);
            Assert.AreEqual(_dto[0].PoUnit, Data[19]);
            Assert.AreEqual(_dto[0].OrderPrUn, Data[20]);
            Assert.AreEqual(_dto[0].NetPrice, Data[21]);
            Assert.AreEqual(_dto[0].PriceUnit, Data[22]);
            Assert.AreEqual(_dto[0].OverDlvTol, Data[23]);
            Assert.AreEqual(_dto[0].NoMoreGr, Data[24]);
            Assert.AreEqual(_dto[0].Acctasscat, Data[25]);
            Assert.AreEqual(_dto[0].PreqName, Data[26]);
            Assert.AreEqual(_dto[0].SerialNo, Data[27]);
            Assert.AreEqual(_dto[0].GlAccount, Data[28]);
            Assert.AreEqual(_dto[0].CostCenter, Data[29]);
            Assert.AreEqual(_dto[0].DeliveryDate, Data[30]);
            Assert.AreEqual(_dto[0].LnType, Data[31]);
            Assert.AreEqual(_dto[0].Taxable, Data[32]);
        }

        protected override void DataMustContainItemWithAllEmptyString()
        {
            Assert.IsTrue(_dto.Any());
            Assert.AreEqual(_dto[0].PoNumber, string.Empty);
            Assert.AreEqual(_dto[0].CompCode, string.Empty);
            Assert.AreEqual(_dto[0].DocType, string.Empty);
            Assert.AreEqual(_dto[0].CreateDate, string.Empty);
            Assert.AreEqual(_dto[0].CreatedBy, string.Empty);
            Assert.AreEqual(_dto[0].ItemIntvl, string.Empty);
            Assert.AreEqual(_dto[0].Vendor, string.Empty);
            Assert.AreEqual(_dto[0].PurchOrg, string.Empty);
            Assert.AreEqual(_dto[0].PurGroup, string.Empty);
            Assert.AreEqual(_dto[0].Currency, string.Empty);
            Assert.AreEqual(_dto[0].DocDate, string.Empty);
            Assert.AreEqual(_dto[0].PoItem, string.Empty);
            Assert.AreEqual(_dto[0].DeleteInd, string.Empty);
            Assert.AreEqual(_dto[0].ShortText, string.Empty);
            Assert.AreEqual(_dto[0].Plant, string.Empty);
            Assert.AreEqual(_dto[0].StgeLoc, string.Empty);
            Assert.AreEqual(_dto[0].MatlGroup, string.Empty);
            Assert.AreEqual(_dto[0].VendMat, string.Empty);
            Assert.AreEqual(_dto[0].Quantity, string.Empty);
            Assert.AreEqual(_dto[0].PoUnit, string.Empty);
            Assert.AreEqual(_dto[0].OrderPrUn, string.Empty);
            Assert.AreEqual(_dto[0].NetPrice, string.Empty);
            Assert.AreEqual(_dto[0].PriceUnit, string.Empty);
            Assert.AreEqual(_dto[0].OverDlvTol, string.Empty);
            Assert.AreEqual(_dto[0].NoMoreGr, string.Empty);
            Assert.AreEqual(_dto[0].Acctasscat, string.Empty);
            Assert.AreEqual(_dto[0].PreqName, string.Empty);
            Assert.AreEqual(_dto[0].SerialNo, string.Empty);
            Assert.AreEqual(_dto[0].GlAccount, string.Empty);
            Assert.AreEqual(_dto[0].CostCenter, string.Empty);
            Assert.AreEqual(_dto[0].DeliveryDate, string.Empty);
            Assert.AreEqual(_dto[0].LnType, string.Empty);
            Assert.AreEqual(_dto[0].Taxable, string.Empty);
        }
    }
}