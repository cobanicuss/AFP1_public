using System.IO;
using System.Linq;

namespace Spm.Shared
{
    public interface IFileBuffer
    {
        void DeleteOverflow(string path, int fileCount);
    }

    public class FileBuffer : IFileBuffer
    {
        public void DeleteOverflow(string path, int fileCount)
        {
            var totFilesInFolder = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).Length;
            var fileCountToDelete = totFilesInFolder - fileCount + 1;

            if (totFilesInFolder >= fileCount)
            {
                var filesToDelete = new DirectoryInfo(path).GetFiles().OrderBy(x => x.LastWriteTime).Take(fileCountToDelete).ToList();
                foreach (var file in filesToDelete)
                {
                    File.Delete(file.FullName);
                }
            }
        }
    }
}