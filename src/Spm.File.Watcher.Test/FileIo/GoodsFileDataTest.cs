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
    public class GoodsFileDataTest : FileDataTestBase
    {
        private IGetDataForGoods _classUnderTest;
        private IList<GoodsDto> _dto;
        private Exception _result;

        protected override int Columns => Constants.GoodsColumnCount;
        protected override string TestFileName => "GoodsFileDataTest.csv";
        protected string TestErrorFileName => "GoodsFileDataErrorTest.txt";

        [SetUp]
        public void Setup()
        {
            FileManipulator = new Mock<IWorkWithFiles>();
            FileManipulator.Setup(x => x.CreateErrorFileForIssue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            FileManipulator.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));

            _classUnderTest = new GoodsFileData(FileManipulator.Object);
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

        protected override void MethodUnderTestWhichThrowsException()
        {
            _dto = _classUnderTest.ExtractDataFromFile(Path, Path, TestFileName, TestErrorFileName);
        }

        protected void ExtractingDataFromBadFile()
        {
            _result = ExecuteActionThatThrows(MethodUnderTestWhichThrowsException);
        }

        protected void ErrorMustBeThrown()
        {
            Assert.IsInstanceOf<FormatException>(_result);
        }

        protected override void DataMustContainDataFromFile()
        {
            Assert.AreEqual(_dto[0].PstngDate, Data[0]);
            Assert.AreEqual(_dto[0].DocDate, Data[1]);
            Assert.AreEqual(_dto[0].RefDocNo, Data[2]);
            Assert.AreEqual(_dto[0].HeaderTxt, Data[3]);
            Assert.AreEqual(_dto[0].GmCode, Data[4]);
            Assert.AreEqual(_dto[0].Plant, Data[5]);
            Assert.AreEqual(_dto[0].StgeLoc, Data[6]);
            Assert.AreEqual(_dto[0].MoveType, Data[7]);
            Assert.AreEqual(_dto[0].EntryQnt, Data[8]);
            Assert.AreEqual(_dto[0].EntryUom, Data[9]);
            Assert.AreEqual(_dto[0].PoNumber, Data[10]);
            Assert.AreEqual(_dto[0].PoItem, Data[11]);
            Assert.AreEqual(_dto[0].MvtInd, Data[12]);
            Assert.AreEqual(_dto[0].NoMoreGr, Data[13]);
            Assert.AreEqual(_dto[0].TranTypeInd, Data[14]);
            Assert.AreEqual(_dto[0].Id, Data[15]);
            Assert.AreEqual(_dto[0].OrderType, Data[16]);
            Assert.AreEqual(_dto[0].DocType, Data[17]);
            Assert.AreEqual(_dto[0].ReceiptDoc, Data[18]);
        }

        protected override void DataMustContainItemWithAllEmptyString()
        {
            Assert.IsTrue(_dto.Any());
            Assert.AreEqual(_dto[0].PstngDate, string.Empty);
            Assert.AreEqual(_dto[0].DocDate, string.Empty);
            Assert.AreEqual(_dto[0].RefDocNo, string.Empty);
            Assert.AreEqual(_dto[0].HeaderTxt, string.Empty);
            Assert.AreEqual(_dto[0].GmCode, string.Empty);
            Assert.AreEqual(_dto[0].Plant, string.Empty);
            Assert.AreEqual(_dto[0].StgeLoc, string.Empty);
            Assert.AreEqual(_dto[0].MoveType, string.Empty);
            Assert.AreEqual(_dto[0].EntryQnt, string.Empty);
            Assert.AreEqual(_dto[0].EntryUom, string.Empty);
            Assert.AreEqual(_dto[0].PoNumber, string.Empty);
            Assert.AreEqual(_dto[0].PoItem, string.Empty);
            Assert.AreEqual(_dto[0].MvtInd, string.Empty);
            Assert.AreEqual(_dto[0].NoMoreGr, string.Empty);
            Assert.AreEqual(_dto[0].TranTypeInd, string.Empty);
            Assert.AreEqual(_dto[0].Id, string.Empty);
            Assert.AreEqual(_dto[0].OrderType, string.Empty);
            Assert.AreEqual(_dto[0].DocType, string.Empty);
            Assert.AreEqual(_dto[0].ReceiptDoc, string.Empty);
        }
    }
}
