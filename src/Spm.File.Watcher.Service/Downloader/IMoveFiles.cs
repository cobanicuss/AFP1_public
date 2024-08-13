namespace Spm.File.Watcher.Service.Downloader
{
    public interface IHelpMoveFiles
    {
        bool IsFileAvailableForMove(string path, string fileName);

        void MoveFileBetweenFolders(string sourcePath, string destinationPath, string fileName);

        void MoveIntoErrorFile(string errorPath, string errorFileName, string error);
    }
}