using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Moq;
using NUnit.Framework;
using TestStack.BDDfy;
using Spm.File.Watcher.Service.Downloader;
using Spm.Shared;

namespace Spm.File.Watcher.Test.FileIo
{
    [TestFixture]
    public class FileManipulaterTest
    {
        private IWorkWithFiles _classUnderTest;
        private Mock<IHelpMoveFiles> _move;
        private Mock<IFileBuffer> _buffer;

        private FileStream _fileStream;

        private const string TestFileName = "FileManipulatorTest1.txt";
        private static string _path;
        private static string _pathList;
        private static string _fullFilePath;
        private const string PathToError = @"C:\Err\";
        private string _errorFileName;
        private const string TextInFile = "Test file.";
        private static string _fileExtension;
        private const string FileNameForList = "FileManipulatorListTest_";
        private const int FileCountInList = 4;
        private static string _folderA;
        private static string _folderB;

        private bool _fileLockResult;
        private bool _isFileMissingResult;
        private IList<string> _listFileResult;

        [SetUp]
        public void Setup()
        {
            _buffer = new Mock<IFileBuffer>();
            _move = new Mock<IHelpMoveFiles>();

            _errorFileName = "ErrorFile.txt";
            _pathList = @"C:\FileManipulatorList\";
            _fileExtension = ".txt";
            _path = @"C:\";
            _fullFilePath = $@"{_path}{TestFileName}";
            _folderA = @"C:\FileManipulatorTestA\";
            _folderB = @"C:\FileManipulatorTestB\";
        }

        [Test]
        public void IfFileIsLockedThenReturnTrue()
        {
            this.Given(_ => FileIsLocked())
           .When(_ => LockingIsDetermined())
           .Then(_ => FileIsLockedMustBeReturned())

           .BDDfy();

            CleanUpAfterLockTest();
        }

        [Test]
        public void IfFileIsNotLockedTheReturnFalse()
        {
            this.Given(_ => FileIsNotLocked())
           .When(_ => LockingIsDetermined())
           .Then(_ => FileIsNotLockedMustBeReturned())

           .BDDfy();

            DeletTestFile(_fullFilePath);
        }

        [Test]
        public void FileIsMissingThrowsErrorWhenPathToFileDoesNotExist()
        {
            this.Given("input path is bad")
           .When("checking if file is missing")
           .Then(_ => IsFileMissingThrowsErrorOnBadPath())

           .BDDfy();
        }

        [Test]
        public void FileIsMissingThrowsErrorWhenPathToErrorFileDoesNotExist()
        {
            this.Given("input path-to-error is bad")
           .When("checking if file is missing")
           .Then(_ => IsFileMissingThrowsErrorOnBadPathToErrorFile())

           .BDDfy();
        }

        [Test]
        public void IsFileMissingShouldReturnTrueWhenFileIsNotAvailableAndErrorFileIsNotAvailable()
        {
            this.Given(_ => FileIsNotAvailable(_path))
                    .And(_ => ErrorFileIsNotAvailable())
                    .And(_ => ErrorNeedsToBeMovedIntoFile(PathToError))
                .When(_ => CheckingIfFileIsMissing())
                .Then(_ => FileAvailabilityWasCalled(_path))
                    .And(_ => ErrorFileAvailabilityWasCalled())
                    .And(_ => MoveIntoErrorFileWasCalled(PathToError, TestFileName))
                    .And(_ => IsFileMissingReturnsTrue())

            .BDDfy();

            DeleteErrorFolderWithFiles();
        }

        [Test]
        public void IsFileMissingShouldReturnTrueInCasesWhereTheErrorFileNameIsEmptyAndTheFileIsNotAvailable()
        {
            this.Given(_ => FileIsNotAvailable(_path))
                    .And(_ => ErrorFileNameIsEmpty())
                    .And(_ => ErrorFileIsNotAvailable())
                    .And(_ => ErrorNeedsToBeMovedIntoFile(PathToError))
                .When(_ => CheckingIfFileIsMissing())
                .Then(_ => FileAvailabilityWasCalled(_path))
                    .And(_ => MoveIntoErrorFileWasCalled(PathToError, TestFileName))
                    .And(_ => IsFileMissingReturnsTrue())

            .BDDfy();

            DeleteErrorFolderWithFiles();
        }

        [Test]
        public void IsFileMissingShouldReturnFalseWhenTheFileIsAvailable()
        {
            this.Given(_ => FileIsAvailable(_path))
                    .And(_ => ErrorFileIsAvailable())
                    .And(_ => ErrorNeedsToBeMovedIntoFile(PathToError))
                .When(_ => CheckingIfFileIsMissing())
                .Then(_ => FileAvailabilityWasCalled(_path))
                    .And(_ => MoveIntoErrorFileIsNotCalled())
                    .And(_ => IsMissingReturnsFalse())

            .BDDfy();

            DeleteErrorFolderWithFiles();
        }

        [Test]
        public void FileListMustBeOrderedCorrectlyWhenGettingAllFilesByTypeFromDesignatedFolder()
        {
            this.Given(_ => TimeToGetFilesFromDesignatedFolder())
                .When(_ => GettingAllFilesByType())
                .Then(_ => CorrectFilesMustBeSelected())
                    .And(_ => SelectionMustBeInCorrectOrder())

            .BDDfy();

            DeleteFolder(_pathList);
        }

        [Test]
        public void GettingAllFilesInFolderByTypeThrowsErrorWhenPathDoesNotExist()
        {
            this.Given("time to get all files from designated folder")
                    .And(_ => DesignatedFolderDoesNotExist())
                .When("getting a list of all files")
                .Then(_ => ErrorIsThrownOnFolderThatDoesNotExist())

            .BDDfy();
        }

        [Test]
        public void GettingAllFilesInFolderByTypeThrowsErrorWhenFileExtensionIsEmpty()
        {
            this.Given(_ => TimeToGetFilesFromDesignatedFolder())
                    .And(_ => FileExtensionIsEmpty())
                .When("getting a list of all files")
                .Then(_ => ErrorIsThrownOnEmptyFileExtension())

            .BDDfy();

            DeleteFolder(_pathList);
        }

        [Test]
        public void DeletingFileWillThrowAnErrorIfFolderDoesNotExist()
        {
            this.Given("file needs to be deleted")
                    .And(_ => FolderDoesNotExistForDeletion())
                .When("deleting file")
                .Then(_ => FileDeletionThrowsDirectoryNotFoundException())

            .BDDfy();
        }

        [Test]
        public void FileCanBeDeletedIfFileIsAvailable()
        {
            this.Given(_ => FileNeedsToBeDetleted())
                    .And(_ => FileIsAvailable(_path))
                .When(_ => DeletingFile())
                .Then(_ => FileAvailabilityWasCalled(_path))
                    .And(_ => FileIsGone())

            .BDDfy();
        }

        [Test]
        public void FileIsIgnoredForDeletionWhenFileIsNotAvailable()
        {
            this.Given(_ => FileIsNotAvailable(_path))
                .When(_ => DeletingFile())
                .Then(_ => FileAvailabilityWasCalled(_path))
                    .And(_ => FileIsGone())

            .BDDfy();
        }

        [Test]
        public void WhenDownloadingFileIfTheFolderDoesNotExistAnErrorIsThrown()
        {
            this.Given("file needs to be downloaded")
                    .And(_ => FolderDoesNotExist())
                .When(_ => DownloadingFileWithBadFolder())
                .Then(_ => DownloadingFileThrowsAnError())
                    .And(_ => BufferMaintenanceWasNotCalled())

            .BDDfy();
        }

        [Test]
        public void WhenDownloadingFileTheFileMustBeMoved()
        {
            this.Given(_ => FileNeedsToBeDownloaded())
                    .And(_ => FolderDoesExist())
                    .And(_ => BufferNeedsToBeMaintained())
                .When(_ => DownloadingFile())
                .Then(_ => FileHasBeenMoved())
                    .And(_ => BufferMaintenanceWasCalled())

            .BDDfy();

            DeleteFolder(_folderA);
            DeleteFolder(_folderB);
        }

        [Test]
        public void IfDestinationFolderDoesNotExistItMustBeCreated()
        {
            this.Given(_ => FileNeedsToBeUploaded())
                    .And(_ => DestinatioFolderDoesNotExist())
                .When(_ => UploadingFile())
                .Then(_ => DestinationFolderWasCreated())
                    .And(_ => NewSystemFolderWasCreated())
                    .And(_ => DestinationFolderContainsUploadedFile())

            .BDDfy();

            DeleteFolder(_folderA);
            DeleteFolder(_folderB);
        }

        [Test]
        public void CreatingErrorFileForIssueMustFollowSpicificSteps()
        {
            this.Given(_ => FileIsAvailable(_folderB))
                    .And(_ => FileNeedsToBeMovedBetweenFolders())
                    .And(_ => ErrorNeedsToBeMovedIntoFile(_folderA))
                .When(_ => CreateErrorFileForIssue())
                .Then(_ => FileAvailabilityWasCalled(_folderB))
                    .And(_ => MoveFileBetweenFoldersWasCalled())
                    .And(_ => MoveIntoErrorFileWasCalled(_folderA, _errorFileName))

            .BDDfy();
        }

        [Test]
        public void CreatingErrorFileForIssueMustNotMoveFileIfFileIsNotAvailable()
        {
            this.Given(_ => FileIsNotAvailable(_folderB))
                    .And(_ => FileNeedsToBeMovedBetweenFolders())
                    .And(_ => ErrorNeedsToBeMovedIntoFile(_folderA))
                .When(_ => CreateErrorFileForIssue())
                .Then(_ => FileAvailabilityWasCalled(_folderB))
                    .And(_ => MoveFileBetweenFoldersWasNotCalled())
                    .And(_ => MoveIntoErrorFileWasCalled(_folderA, _errorFileName))

            .BDDfy();
        }

        private void CreateErrorFileForIssue()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _classUnderTest.CreateErrorFileForIssue(_folderB, _folderA, TestFileName, _errorFileName, "Some Error");
        }

        private void UploadingFile()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _classUnderTest.UploadFile(TestFileName, _folderB, _folderA);
        }

        private static void DestinatioFolderDoesNotExist()
        {
            if (Directory.Exists(_folderA)) throw new Exception($"Folder must NOT exist at this point. {_folderA}");
        }

        private static void FileNeedsToBeUploaded()
        {
            CreateFolder(_folderB);
            CreateFile($"{_folderB}{TestFileName}");
        }

        private void BufferMaintenanceWasCalled()
        {
            _buffer.Verify(x => x.DeleteOverflow(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        private static void FileHasBeenMoved()
        {
            Assert.IsTrue(System.IO.File.Exists($"{_folderA}{TestFileName}"));
            Assert.IsTrue(System.IO.File.Exists($"{_folderB}{TestFileName}"));
        }

        private void BufferNeedsToBeMaintained()
        {
            _buffer.Setup(x => x.DeleteOverflow(It.IsAny<string>(), It.IsAny<int>()));
        }

        private static void FolderDoesExist()
        {
            CreateFile($"{_folderA}{TestFileName}");
        }

        private static void FileNeedsToBeDownloaded()
        {
            CreateFolder(_folderA);

            CreateFolder(_folderB);
        }

        private static void CreateFolder(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        private void DownloadingFile()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _classUnderTest.DownloadFile(TestFileName, _folderB, _folderA, 10);
        }

        private void DownloadingFileWithBadFolder()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
        }

        private void DeletingFile()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _classUnderTest.DeleteFile(_path, TestFileName);
        }

        private static void FileNeedsToBeDetleted()
        {
            CreateFile(_fullFilePath);
        }

        private static void FolderDoesNotExist()
        {
            _folderA = "BadPath";
        }

        private static void FileExtensionIsEmpty()
        {
            _fileExtension = string.Empty;
        }

        private static void DesignatedFolderDoesNotExist()
        {
            _pathList = "BadPath";
        }

        private static void FolderDoesNotExistForDeletion()
        {
            _path = "BadPath";
        }

        private void GettingAllFilesByType()
        {
            _listFileResult = _classUnderTest.GetAllFilesInFolderByType(_pathList, _fileExtension);
        }

        private void TimeToGetFilesFromDesignatedFolder()
        {
            CreateListFolder();

            CreateListOfFilesTotaling(FileCountInList);

            ChangeOrderOfFilesExplicitly();

            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
        }

        private static void CreateListFolder()
        {
            if (!Directory.Exists(_pathList)) Directory.CreateDirectory(_pathList);
        }

        private static void DeleteFolder(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }

        private static void ChangeOrderOfFilesExplicitly()
        {
            // 0 1 2 3 => Order Of Creation
            // make chagnes to 1 and 3
            // 0 2 1 3 => New Order for Testing

            var changeFile1 = $"{_pathList}{FileNameForList}{1}{_fileExtension}";
            var txtOfFile1 = System.IO.File.ReadAllText(changeFile1);
            txtOfFile1 = txtOfFile1.Replace(TextInFile, DateTime.Now.ToString("F"));
            System.IO.File.WriteAllText(changeFile1, txtOfFile1);

            var changeFile3 = $"{_pathList}{FileNameForList}{3}{_fileExtension}";
            var textOfFile3 = System.IO.File.ReadAllText(changeFile3);
            textOfFile3 = textOfFile3.Replace(TextInFile, DateTime.Now.ToString("F"));
            System.IO.File.WriteAllText(changeFile3, textOfFile3);
        }

        private static void CreateListOfFilesTotaling(int totalFiles)
        {
            for (var i = 0; i < totalFiles; i++)
            {
                var fileName = $"{_pathList}{FileNameForList}{i}{_fileExtension}";
                CreateFile(fileName);
            }
        }

        private void ErrorFileNameIsEmpty()
        {
            _errorFileName = string.Empty;
        }

        private static void DeleteErrorFolderWithFiles()
        {
            if (Directory.Exists(PathToError)) Directory.Delete(PathToError, true);
        }

        private void ErrorNeedsToBeMovedIntoFile(string pathToError)
        {
            _move.Setup(x => x.MoveIntoErrorFile(pathToError, _errorFileName, It.IsAny<string>()));
        }

        private void CheckingIfFileIsMissing()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _isFileMissingResult = _classUnderTest.IsFileMissing(_path, TestFileName, PathToError, _errorFileName);
        }

        private void ErrorFileIsNotAvailable()
        {
            if (!Directory.Exists(PathToError)) Directory.CreateDirectory(PathToError);

            _move.Setup(x => x.IsFileAvailableForMove(PathToError, _errorFileName)).Returns(false);
        }

        private void ErrorFileIsAvailable()
        {
            if (!Directory.Exists(PathToError)) Directory.CreateDirectory(PathToError);

            _move.Setup(x => x.IsFileAvailableForMove(PathToError, _errorFileName)).Returns(true);
        }

        private void FileNeedsToBeMovedBetweenFolders()
        {
            _move.Setup(x => x.MoveFileBetweenFolders(_folderB, _folderA, TestFileName));
        }

        private void FileIsNotAvailable(string path)
        {
            _move.Setup(x => x.IsFileAvailableForMove(path, TestFileName)).Returns(false);
        }

        private void FileIsAvailable(string path)
        {
            _move.Setup(x => x.IsFileAvailableForMove(path, TestFileName)).Returns(true);
        }

        private void FileIsLocked()
        {
            DeletTestFile(_fullFilePath);

            CreateFile(_fullFilePath);

            _fileStream = new FileStream(_fullFilePath, FileMode.Open);
        }

        private static void CreateFile(string fullFilePath)
        {
            if (System.IO.File.Exists(fullFilePath)) return;

            using (var fs = System.IO.File.Create(fullFilePath))
            {
                var data = new UTF8Encoding(true).GetBytes(TextInFile);
                fs.Write(data, 0, data.Length);
            }
        }

        private static void FileIsNotLocked()
        {
            DeletTestFile(_fullFilePath);

            CreateFile(_fullFilePath);
        }

        private void LockingIsDetermined()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            _fileLockResult = _classUnderTest.IsFileLocked(_path, TestFileName);
        }

        private void CleanUpAfterLockTest()
        {
            _fileStream.Flush();
            _fileStream.Close();

            DeletTestFile(_fullFilePath);
        }

        private static void DeletTestFile(string fullFilePath)
        {
            if (System.IO.File.Exists(fullFilePath)) System.IO.File.Delete(fullFilePath);
        }

        private void BufferMaintenanceWasNotCalled()
        {
            _buffer.Verify(x => x.DeleteOverflow(It.IsAny<string>(), It.IsAny<int>()), Times.Never());
        }

        private void MoveFileBetweenFoldersWasCalled()
        {
            _move.Verify(x => x.MoveFileBetweenFolders(_folderB, _folderA, TestFileName));
        }

        private void MoveFileBetweenFoldersWasNotCalled()
        {
            _move.Verify(x => x.MoveFileBetweenFolders(_folderB, _folderA, TestFileName), Times.Never());
        }

        private void MoveIntoErrorFileWasCalled(string path, string errorFile)
        {
            _move.Verify(x => x.MoveIntoErrorFile(path, errorFile, It.IsAny<string>()), Times.Once());
        }

        private void MoveIntoErrorFileIsNotCalled()
        {
            _move.Verify(x => x.MoveIntoErrorFile(PathToError, TestFileName, It.IsAny<string>()), Times.Never());
        }

        private void ErrorFileAvailabilityWasCalled()
        {
            _move.Verify(x => x.IsFileAvailableForMove(PathToError, _errorFileName), Times.Once());
        }

        private void FileAvailabilityWasCalled(string path)
        {
            _move.Verify(x => x.IsFileAvailableForMove(path, TestFileName), Times.Once());
        }

        private void CorrectFilesMustBeSelected()
        {
            Assert.IsTrue(_listFileResult.Count == FileCountInList);
        }

        private void SelectionMustBeInCorrectOrder()
        {
            Assert.IsTrue(_listFileResult[0].Contains("_0"));
            Assert.IsTrue(_listFileResult[1].Contains("_2"));
            Assert.IsTrue(_listFileResult[2].Contains("_1"));
            Assert.IsTrue(_listFileResult[3].Contains("_3"));
        }

        private static void FileIsGone()
        {
            Assert.IsTrue(!System.IO.File.Exists(_fullFilePath));
        }

        private void FileDeletionThrowsDirectoryNotFoundException()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.DeleteFile(_path, TestFileName));
        }

        private void ErrorIsThrownOnEmptyFileExtension()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            Assert.Throws<ArgumentOutOfRangeException>(() => _classUnderTest.GetAllFilesInFolderByType(_pathList, _fileExtension));
        }

        private void ErrorIsThrownOnFolderThatDoesNotExist()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.GetAllFilesInFolderByType(_pathList, _fileExtension));
        }

        private void DownloadingFileThrowsAnError()
        {
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.DownloadFile(TestFileName, _folderB, _folderA, 1));
        }

        private static void NewSystemFolderWasCreated()
        {
            Assert.IsTrue(Directory.Exists($@"{_folderA}{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}\"));
        }

        private static void DestinationFolderContainsUploadedFile()
        {
            Assert.IsTrue(System.IO.File.Exists($@"{_folderA}{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}\{ TestFileName}"));
        }

        private static void DestinationFolderWasCreated()
        {
            Assert.IsTrue(Directory.Exists(_folderA));
        }

        private void IsFileMissingThrowsErrorOnBadPathToErrorFile()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.IsFileMissing(_path, TestFileName, "BadPathToError", _errorFileName));
        }

        private void IsFileMissingThrowsErrorOnBadPath()
        {
            _classUnderTest = new FileManipulator(_buffer.Object, _move.Object);
            Assert.Throws<DirectoryNotFoundException>(() => _classUnderTest.IsFileMissing("BadPath", TestFileName, PathToError, _errorFileName));
        }

        private void FileIsLockedMustBeReturned()
        {
            Assert.IsTrue(_fileLockResult);
        }

        private void FileIsNotLockedMustBeReturned()
        {
            Assert.IsFalse(_fileLockResult);
        }

        private void IsFileMissingReturnsTrue()
        {
            Assert.IsTrue(_isFileMissingResult);
        }

        private void IsMissingReturnsFalse()
        {
            Assert.IsFalse(_isFileMissingResult);
        }
    }
}