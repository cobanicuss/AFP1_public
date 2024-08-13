using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NServiceBus.Logging;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Downloader
{
    /// <summary>
    /// This is an abstraction away from what technology is used for data extraction.
    /// Previously this was done by FTP. 
    /// This abstraction is the Single Point of Conctact for re-writing data extraction.
    /// Do NOT remove this abstraction.
    /// The abstaction is within the context of a 'File'. 
    /// This can be any file. Physical, Memory, FTP, HTML etc.
    /// This implementation uses physical files on a mapped file-server.
    /// </summary>

    public class FileManipulator : IWorkWithFiles
    {
        private readonly IFileBuffer _buffer;
        private readonly IHelpMoveFiles _move;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(FileManipulator));

        public FileManipulator(
            IFileBuffer buffer,
            IHelpMoveFiles move)
        {
            _buffer = buffer;
            _move = move;
        }

        public bool IsBaseLocationAvailable(string path)
        {
            try
            {
                var doesFolderExist = Directory.Exists(path);
                return doesFolderExist;
            }
            catch (Exception ex)
            {
                #region IMPORTANT
                //Error conitions CAN/DOES get raised here over the network.//
                //Even when Exists(_) returns false on error conditions.//
                //Deliberately swallowing of error has been instructed by business.//
                //Infinite polling on error has been Instructed by business.//
                #endregion

                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);

                return false;
            }
        }

        public bool IsFileMissing(string path, string fileName, string pathToError, string errorFileName = "")
        {
            if (!Directory.Exists(path)) throw new DirectoryNotFoundException(path);
            if (!Directory.Exists(pathToError)) throw new DirectoryNotFoundException(pathToError);

            var fileIsMissing = !_move.IsFileAvailableForMove(path, fileName);
            var errorFileIsMissing = string.IsNullOrEmpty(errorFileName) || !_move.IsFileAvailableForMove(pathToError, errorFileName);

            #region IMPORTANT
            // The if statement is nested. Leave it as is. //
            // && is critical here and both conditions MUST apply like this.//
            #endregion
            if (fileIsMissing && errorFileIsMissing)
            {
                var errorMessage = $"{Constants.FileWentMissingDuringProcessing} fileName={fileName}.";
                _move.MoveIntoErrorFile(pathToError, fileName, errorMessage);

                Logger.Error("Unexpected manual manipulation of file!!!");
                Logger.Error($"Error-Message:{errorMessage}");

                return true;
            }

            return false;
        }

        public IList<string> GetAllFilesInFolderByType(string path, string fileType)
        {
            if(!Directory.Exists(path)) throw new DirectoryNotFoundException(path);
            if(string.IsNullOrEmpty(fileType)) throw new ArgumentOutOfRangeException(fileType);

            var searchPattern = $"*{fileType}";
            var dirInfo = new DirectoryInfo(path);
            var fileList = dirInfo.GetFiles(searchPattern).OrderBy(x => x.LastWriteTime).ToArray();
            var fileNameList = fileList.Select(x => x.Name).ToList();

            return fileNameList;
        }

        public void DeleteFile(string path, string fileName)
        {
            if(!Directory.Exists(path)) throw new DirectoryNotFoundException(path);

            var fileIsMissing = !_move.IsFileAvailableForMove(path, fileName);
            if (fileIsMissing) return;

            var fullFilepath = $"{path}{fileName}";

            Logger.Info($"Deleting file. fileName={fullFilepath}");

            System.IO.File.Delete(fullFilepath);

            Logger.Info($"File deleted: {fullFilepath}");
        }

        public void DownloadFile(string fileName, string localDestnPath, string path, int bufferFileCount)
        {
            if(!Directory.Exists(path)) throw new DirectoryNotFoundException(path);

            Logger.Info($"Downloading file:{fileName}.");
            if (!Directory.Exists(localDestnPath)) Directory.CreateDirectory(localDestnPath);

            var destination = $"{localDestnPath}{fileName}";
            var source = $"{path}{fileName}";

            _buffer.DeleteOverflow(localDestnPath, bufferFileCount);

            System.IO.File.Copy(source, destination, overwrite: true);
            Logger.Info("Done downloading file.");
        }

        public void UploadFile(string fileName, string sourcePath, string destinationPath)
        {
            Logger.Info($"Uploading file:{fileName}.");
            if (!Directory.Exists(sourcePath)) return;

            var newSubFolder = $@"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}\";
            var newDestination = $"{destinationPath}{newSubFolder}";
            var newDestinationPath = $"{newDestination}{fileName}";

            if (!Directory.Exists(newDestination)) Directory.CreateDirectory(newDestination);

            var source = $"{sourcePath}{fileName}";

            System.IO.File.Copy(source, newDestinationPath, overwrite: true);
            Logger.Info("Done uploading file.");
        }

        public void CreateErrorFileForIssue(string sourcePath, string destinationPath, string fileName, string errorFileName, string error)
        {
            var fileIsAvailable = _move.IsFileAvailableForMove(sourcePath, fileName);

            if (fileIsAvailable) _move.MoveFileBetweenFolders(sourcePath, destinationPath, fileName);

            _move.MoveIntoErrorFile(destinationPath, errorFileName, error);

            Logger.Error($"Error-Message:{error}");
        }

        public bool IsFileLocked(string path, string fileName)
        {
            var fullFilePath = $"{path}{fileName}";

            try
            {
                using (var stream = new FileStream(fullFilePath, FileMode.Open)) { stream.Flush(); }
            }
            catch { return true; } /*Explicit swallow of error is OK and correct. Retry SHALL resolve lock.*/

            return false;
        }
    }
}