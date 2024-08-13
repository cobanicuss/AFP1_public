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
    public class GeneralLedgerFileDataTest : FileDataTestBase
    {
        private IGetDataForGeneralLedger _classUnderTest;
        private IList<GeneralLedgerDto> _dto;
        private Exception _result;

        protected override int Columns => Constants.GeneralLedgerColumnCount;
        protected override string TestFileName => "GeneralLedgerFileDataTest.csv";
        protected string TestErrorFileName => "GeneralLedgerErrorTest.txt";

        [SetUp]
        public void Setup()
        {
            FileManipulator = new Mock<IWorkWithFiles>();
            FileManipulator.Setup(x => x.CreateErrorFileForIssue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            FileManipulator.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));

            _classUnderTest = new GeneralLedgerFileData(FileManipulator.Object);
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
            Assert.AreEqual(_dto[0].HeaderTxt, Data[0]);
            Assert.AreEqual(_dto[0].CompCode, Data[1]);
            Assert.AreEqual(_dto[0].DocDate, Data[2]);
            Assert.AreEqual(_dto[0].PstingDate, Data[3]);
            Assert.AreEqual(_dto[0].RefDocNo, Data[4]);
            Assert.AreEqual(_dto[0].AllocNmbr, Data[5]);
            Assert.AreEqual(_dto[0].CostCentre, Data[6]);
            Assert.AreEqual(_dto[0].GlAccount, Data[7]);
            Assert.AreEqual(_dto[0].Currency, Data[8]);
            Assert.AreEqual(_dto[0].AmtDoccur, Data[9]);
        }

        protected override void DataMustContainItemWithAllEmptyString()
        {
            Assert.IsTrue(_dto.Any());
            Assert.AreEqual(_dto[0].HeaderTxt, string.Empty);
            Assert.AreEqual(_dto[0].CompCode, string.Empty);
            Assert.AreEqual(_dto[0].DocDate, string.Empty);
            Assert.AreEqual(_dto[0].PstingDate, string.Empty);
            Assert.AreEqual(_dto[0].RefDocNo, string.Empty);
            Assert.AreEqual(_dto[0].AllocNmbr, string.Empty);
            Assert.AreEqual(_dto[0].CostCentre, string.Empty);
            Assert.AreEqual(_dto[0].GlAccount, string.Empty);
            Assert.AreEqual(_dto[0].Currency, string.Empty);
            Assert.AreEqual(_dto[0].AmtDoccur, string.Empty);
        }
    }
}