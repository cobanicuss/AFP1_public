using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Spm.File.Watcher.Service.Downloader;
using TestStack.BDDfy;

namespace Spm.File.Watcher.Test.FileIo
{
    public class HelpMoveFileTest
    {
        private IHelpMoveFiles _classUnderTest;

        private const string TestFileName = "FileMoveTest1.txt";
        private const string Path = @"C:\";
        private string _sourcePath;
        private string _destinationPath;
        private readonly string _fullFilePath = $@"{Path}{TestFileName}";

        private bool _doesFileExist;

        private const string Err0 = "SomeError1";
        private const string Err1 = "SomeError2";

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HelpMoveFile();
        }

        [Test]
        public void CorrectFilePathConcatinationForFileAvailableReturnsTrue()
        {
            this.Given(_ => CheckingIfFileExist())
            .When(_ => CheckingFileThatIsAvailable())
            .Then(_ => FileAvailabilityReturnsTrue())
            .BDDfy();

            DeletTestFile();
        }

        [Test]
        public void BrokenFilePathConcatinationForFileAvailableReturnsFalse()
        {
            this.Given("checking if file exists")
            .When(_ => CheckingFileIsAvailableWithBadDirectoryPaths())
            .Then(_ => FileAvailabilityReturnFalse())

            .BDDfy();
        }

        [Test]
        public void EmptyFileNameReturnsFalseWhenCheckingIfFileIsAvailable()
        {
            this.Given("checking if file is available")
            .When(_ => CheckingThatFileIsAvailableWithEmptyFileName())
            .Then(_ => FileAvailabilityReturnFalse())
            .BDDfy();
        }

        private void CheckingThatFileIsAvailableWithEmptyFileName()
        {
            _doesFileExist = _classUnderTest.IsFileAvailableForMove(Path, string.Empty);
        }

        [Test]
        public void FilePathConcatinationForMoveBetweenFoldersIsCorrect()
        {
            this.Given(_ => FileNeedsToBeMovedFromSourceToDetinationFolder())
            .When(_ => MovingFilesBetweenFolders())
            .Then(_ => MovementMustBeSuccessfull())

            .BDDfy();

            DeletFoldersAndContainingFiles();

            DeletTestFile();
        }

        [Test]
        public void BrokenFilePathConcatinationInDestinationForMoveBetweenFoldersThrows()
        {
            this.Given(_ => FileNeedsToBeMovedFromBadDetinationFolder())
            .When("moving files between folders")
            .Then(_ => ErrorMustBeThrown())

            .BDDfy();
        }

        [Test]
        public void BrokenFilePathConcatinationInSourceForMoveBetweenFoldersThrows()
        {
            this.Given(_ => FileNeedsToBeMovedFromBadSourceFolder())
            .When("moving files between folders")
            .Then(_ => ErrorMustBeThrown())

            .BDDfy();
        }


        [Test]
        public void FilePathConcatinationForMovingErrorIntoErrorFileIsCorrect()
        {
            this.Given(_ => ErrorFileNeedsToBeCreated())
            .When(_ => CreatingErrorFile())
            .Then(_ => ErrorFileMustExists())
                .And(_ => ErrorTextMustBeInFile())

            .BDDfy();

            DeletTestFile();
        }

        [Test]
        public void BrokenFilePathConcatinationForMoveIntoErrorThrows()
        {
            this.Given(_ => ErrorFileWithBadDetails())
            .When("moving error into file with bad folder")
            .Then(_ => ErrorMustBeThrownForBadErrorFileDetails())

            .BDDfy();
        }

        private void CreatingErrorFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Err0);
            sb.AppendLine(Err1);

            _classUnderTest.MoveIntoErrorFile(_sourcePath, TestFileName, sb.ToString());
        }

        private void ErrorFileNeedsToBeCreated()
        {
            _sourcePath = @"C:\";

            _classUnderTest = new HelpMoveFile();
        }

        private void ErrorFileWithBadDetails()
        {
            _sourcePath = "BadFolder";

            _classUnderTest = new HelpMoveFile();
        }

        private void CheckingIfFileExist()
        {
            DeletTestFile();

            CreateFileInDirectory(Path);
        }

        private void FileNeedsToBeMovedFromBadDetinationFolder()
        {
            _sourcePath = @"C:\";
            _destinationPath = "BadFolderFolderPath";
        }

        private void FileNeedsToBeMovedFromBadSourceFolder()
        {
            _sourcePath = "BadFolderFolderPath";
            _destinationPath = @"C:\";
        }

        private void FileNeedsToBeMovedFromSourceToDetinationFolder()
        {
            _sourcePath = @"C:\A\";
            _destinationPath = @"C:\B\";

            if (!Directory.Exists(_sourcePath)) Directory.CreateDirectory(_sourcePath);
            if (!Directory.Exists(_destinationPath)) Directory.CreateDirectory(_destinationPath);

            CreateFileInDirectory(_sourcePath);
        }

        private void DeletFoldersAndContainingFiles()
        {
            if (Directory.Exists(_sourcePath)) Directory.Delete(_sourcePath, true);
            if (Directory.Exists(_destinationPath)) Directory.Delete(_destinationPath, true);
        }

        private static void CreateFileInDirectory(string source)
        {
            var fullFilePath = $"{source}{TestFileName}";

            using (var fs = System.IO.File.Create(fullFilePath))
            {
                var data = new UTF8Encoding(true).GetBytes("Test file.");
                fs.Write(data, 0, data.Length);
            }
        }

        private void CheckingFileIsAvailableWithBadDirectoryPaths()
        {
            _doesFileExist = _classUnderTest.IsFileAvailableForMove("BadFolderPath", "BadFileName");
        }

        private void DeletTestFile()
        {
            if (System.IO.File.Exists(_fullFilePath)) System.IO.File.Delete(_fullFilePath);
        }

        private void ErrorMustBeThrown()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.MoveFileBetweenFolders(_sourcePath, _destinationPath, TestFileName));
        }

        private void ErrorMustBeThrownForBadErrorFileDetails()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.MoveIntoErrorFile(_sourcePath, TestFileName, Err1));
        }

        private void ErrorFileMustExists()
        {
            Assert.IsTrue(System.IO.File.Exists(_fullFilePath));
        }

        private void ErrorTextMustBeInFile()
        {
            var fullFilePath = $"{_sourcePath}{TestFileName}";

            var rowList = System.IO.File.ReadAllText(fullFilePath).Split('\n').ToList();

            Assert.IsTrue(rowList[0].Contains(Err0));
            Assert.IsTrue(rowList[1].Contains(Err1));
        }

        private void MovementMustBeSuccessfull()
        {
            Assert.IsTrue(System.IO.File.Exists($"{_destinationPath}{TestFileName}"));
        }

        private void MovingFilesBetweenFolders()
        {
            _classUnderTest.MoveFileBetweenFolders(_sourcePath, _destinationPath, TestFileName);
        }

        private void FileAvailabilityReturnsTrue()
        {
            Assert.IsTrue(_doesFileExist);
        }

        private void CheckingFileThatIsAvailable()
        {
            _doesFileExist = _classUnderTest.IsFileAvailableForMove(Path, TestFileName);
        }

        private void FileAvailabilityReturnFalse()
        {
            Assert.IsFalse(_doesFileExist);
        }
    }
}