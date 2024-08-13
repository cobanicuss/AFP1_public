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
    public class MaterialMasterFileDataTest : FileDataTestBase
    {
        private IGetDataForMaterialMaster _classUnderTest;
        private IList<MaterialMasterDto> _dto;
        private Exception _result;

        protected override int Columns => Constants.MaterialMasterColumnCount;
        protected override string TestFileName => "MaterialMasterFileDataTest.csv";
        protected string TestErrorFileName => "MaterialMasterErrorTest.txt";

        [SetUp]
        public void Setup()
        {
            FileManipulator = new Mock<IWorkWithFiles>();
            FileManipulator.Setup(x => x.CreateErrorFileForIssue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            FileManipulator.Setup(x => x.DeleteFile(It.IsAny<string>(), It.IsAny<string>()));

            _classUnderTest = new MaterialMasterFileData(FileManipulator.Object);
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
            Assert.IsNotNull(_dto);
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
            Assert.AreEqual(_dto[0].Itm, Data[0]);
            Assert.AreEqual(_dto[0].Litm, Data[1]);
            Assert.AreEqual(_dto[0].Aitm, Data[2]);
            Assert.AreEqual(_dto[0].Dsc1, Data[3]);
            Assert.AreEqual(_dto[0].Dsc2, Data[4]);
            Assert.AreEqual(_dto[0].Dsc12, Data[5]);
            Assert.AreEqual(_dto[0].LongDsc, Data[6]);
            Assert.AreEqual(_dto[0].Mcu, Data[7]);
            Assert.AreEqual(_dto[0].Stkt, Data[8]);
            Assert.AreEqual(_dto[0].Draw, Data[9]);
            Assert.AreEqual(_dto[0].Srp3Desc, Data[10]);
            Assert.AreEqual(_dto[0].Srp4Desc, Data[11]);
            Assert.AreEqual(_dto[0].Srp4Desc2, Data[12]);
            Assert.AreEqual(_dto[0].Srp5Desc, Data[13]);
            Assert.AreEqual(_dto[0].Srp6Desc, Data[14]);
            Assert.AreEqual(_dto[0].Srp7Desc, Data[15]);
            Assert.AreEqual(_dto[0].Srp8Desc, Data[16]);
            Assert.AreEqual(_dto[0].Prp0, Data[17]);
            Assert.AreEqual(_dto[0].Prp0M, Data[18]);
            Assert.AreEqual(_dto[0].Prp1Desc, Data[19]);
            Assert.AreEqual(_dto[0].Prp1Desc2, Data[20]);
            Assert.AreEqual(_dto[0].Prp2Desc, Data[21]);
            Assert.AreEqual(_dto[0].Prp3Desc, Data[22]);
            Assert.AreEqual(_dto[0].Sec1, Data[23]);
            Assert.AreEqual(_dto[0].Sec2, Data[24]);
            Assert.AreEqual(_dto[0].Prp4Desc, Data[25]);
            Assert.AreEqual(_dto[0].Prp5Desc, Data[26]);
            Assert.AreEqual(_dto[0].Prp6Desc, Data[27]);
            Assert.AreEqual(_dto[0].Imprp3, Data[28]);
            Assert.AreEqual(_dto[0].Imprp3Desc, Data[29]);
            Assert.AreEqual(_dto[0].Imprp4, Data[30]);
            Assert.AreEqual(_dto[0].Imprp4Desc, Data[31]);
            Assert.AreEqual(_dto[0].Imuom1, Data[32]);
            Assert.AreEqual(_dto[0].Bq, Data[33]);
            Assert.AreEqual(_dto[0].BwCk, Data[34]);
            Assert.AreEqual(_dto[0].Kgs, Data[35]);
            Assert.AreEqual(_dto[0].Mt, Data[36]);
            Assert.AreEqual(_dto[0].GhMm, Data[37]);
            Assert.AreEqual(_dto[0].GwMm, Data[38]);
            Assert.AreEqual(_dto[0].Kg, Data[39]);
            Assert.AreEqual(_dto[0].Prp4, Data[40]);
            Assert.AreEqual(_dto[0].Srp1, Data[41]);
            Assert.AreEqual(_dto[0].Srp1Desc, Data[42]);
            Assert.AreEqual(_dto[0].Prp3Desc2, Data[43]);
            Assert.AreEqual(_dto[0].Prp2Desc2, Data[44]);
            Assert.AreEqual(_dto[0].UnitCost, Data[45]);
        }

        protected override void DataMustContainItemWithAllEmptyString()
        {
            Assert.IsTrue(_dto.Any());

            Assert.AreEqual(_dto[0].Itm, string.Empty);
            Assert.AreEqual(_dto[0].Litm, string.Empty);
            Assert.AreEqual(_dto[0].Aitm, string.Empty);
            Assert.AreEqual(_dto[0].Dsc1, string.Empty);
            Assert.AreEqual(_dto[0].Dsc2, string.Empty);
            Assert.AreEqual(_dto[0].Dsc12, string.Empty);
            Assert.AreEqual(_dto[0].LongDsc, string.Empty);
            Assert.AreEqual(_dto[0].Mcu, string.Empty);
            Assert.AreEqual(_dto[0].Stkt, string.Empty);
            Assert.AreEqual(_dto[0].Draw, string.Empty);
            Assert.AreEqual(_dto[0].Srp3Desc, string.Empty);
            Assert.AreEqual(_dto[0].Srp4Desc, string.Empty);
            Assert.AreEqual(_dto[0].Srp4Desc2, string.Empty);
            Assert.AreEqual(_dto[0].Srp5Desc, string.Empty);
            Assert.AreEqual(_dto[0].Srp6Desc, string.Empty);
            Assert.AreEqual(_dto[0].Srp7Desc, string.Empty);
            Assert.AreEqual(_dto[0].Srp8Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp0, string.Empty);
            Assert.AreEqual(_dto[0].Prp0M, string.Empty);
            Assert.AreEqual(_dto[0].Prp1Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp1Desc2, string.Empty);
            Assert.AreEqual(_dto[0].Prp2Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp3Desc, string.Empty);
            Assert.AreEqual(_dto[0].Sec1, string.Empty);
            Assert.AreEqual(_dto[0].Sec2, string.Empty);
            Assert.AreEqual(_dto[0].Prp4Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp5Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp6Desc, string.Empty);
            Assert.AreEqual(_dto[0].Imprp3, string.Empty);
            Assert.AreEqual(_dto[0].Imprp3Desc, string.Empty);
            Assert.AreEqual(_dto[0].Imprp4, string.Empty);
            Assert.AreEqual(_dto[0].Imprp4Desc, string.Empty);
            Assert.AreEqual(_dto[0].Imuom1, string.Empty);
            Assert.AreEqual(_dto[0].Bq, string.Empty);
            Assert.AreEqual(_dto[0].BwCk, string.Empty);
            Assert.AreEqual(_dto[0].Kgs, string.Empty);
            Assert.AreEqual(_dto[0].Mt, string.Empty);
            Assert.AreEqual(_dto[0].GhMm, string.Empty);
            Assert.AreEqual(_dto[0].GwMm, string.Empty);
            Assert.AreEqual(_dto[0].Kg, string.Empty);
            Assert.AreEqual(_dto[0].Prp4, string.Empty);
            Assert.AreEqual(_dto[0].Srp1, string.Empty);
            Assert.AreEqual(_dto[0].Srp1Desc, string.Empty);
            Assert.AreEqual(_dto[0].Prp3Desc2, string.Empty);
            Assert.AreEqual(_dto[0].Prp2Desc2, string.Empty);
            Assert.AreEqual(_dto[0].UnitCost, string.Empty);
        }
    }
}
