using System.Collections.Generic;

namespace Spm.File.Watcher.Service.Downloader
{
    public interface IWorkWithFiles
    {
        bool IsBaseLocationAvailable(string path);

        bool IsFileMissing(string path, string fileName, string pathToError, string errorFileName = "");

        IList<string> GetAllFilesInFolderByType(string path, string fileType);

        void DeleteFile(string path, string fileName);

        void DownloadFile(string fileName, string localDestnPath, string path, int bufferFileCount);

        void UploadFile(string fileName, string sourcePath, string destinationPath);

        void CreateErrorFileForIssue(string sourcePath, string destinationPath, string fileName, string errorFileName, string error);

        bool IsFileLocked(string path, string fileName);
    }
}