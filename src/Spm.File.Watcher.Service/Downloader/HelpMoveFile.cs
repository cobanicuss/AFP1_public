using System.IO;

namespace Spm.File.Watcher.Service.Downloader
{
    public class HelpMoveFile : IHelpMoveFiles
    {
        public bool IsFileAvailableForMove(string path, string fileName)
        {
            var fullFilePath = $"{path}{fileName}";

            var doesFileExist = System.IO.File.Exists(fullFilePath);

            return doesFileExist;
        }

        public void MoveFileBetweenFolders(string sourcePath, string destinationPath, string fileName)
        {
            var destination = $"{destinationPath}{fileName}";
            var source = $"{sourcePath}{fileName}";

            if (!Directory.Exists(sourcePath)) throw new DirectoryNotFoundException(sourcePath);
            if (!Directory.Exists(destinationPath)) throw new DirectoryNotFoundException(destinationPath);

            if (System.IO.File.Exists(destination)) System.IO.File.Delete(destination);

            System.IO.File.Move(source, destination);
        }

        public void MoveIntoErrorFile(string errorPath, string errorFileName, string error)
        {
            if (!Directory.Exists(errorPath)) throw new DirectoryNotFoundException(errorPath);

            var fullErrorPath = $"{errorPath}{errorFileName}";

            var line = new[] { error };

            System.IO.File.WriteAllLines(fullErrorPath, line);
        }
    }
}