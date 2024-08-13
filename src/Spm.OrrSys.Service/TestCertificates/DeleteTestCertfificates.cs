using System.IO;
using System.Linq;
using NServiceBus.Logging;

namespace Spm.OrrSys.Service.TestCertificates
{
    public class DeleteTestCertificates : IDeleteTestCertificates
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DeleteTestCertificates));

        public void DeleteBakupTestCertificateFilesIfFolderIsFull(string folder, int testCertificateBufferSize)
        {
            if (!Directory.Exists(folder)) return;

            var fileCount = Directory.GetFiles(folder).Length;

            Logger.Info($"fileCount={fileCount}, pdfBufferSize={testCertificateBufferSize}.");

            if (fileCount <= testCertificateBufferSize) return;

            Logger.Info("Buffer is full: deleting files.");

            var filesToRemain = (int)(0.75f * testCertificateBufferSize);
            Logger.Info($"filesToRemain={filesToRemain}");

            var filesToDelete = new DirectoryInfo(folder).GetFiles().OrderByDescending(x => x.LastWriteTime).Skip(filesToRemain).ToList();
            Logger.Info($"filesToDelete={filesToDelete.Count}");

            foreach (var fi in filesToDelete)
            {
                fi.Delete();
            }
        }

        public void DeletePreviousTestCertficateFiles(string folder)
        {
            var folderMissing = !Directory.Exists(folder);

            if (folderMissing) return;

            var mainfolder = new DirectoryInfo(folder);

            foreach (var dir in mainfolder.GetDirectories())
            {
                Logger.Info($"Inside {folder}.");
                Logger.Info("Deleting all previous folders.");
                dir.Delete(true);
            }

            foreach (var file in mainfolder.GetFiles())
            {
                Logger.Info($"Inside {folder}.");
                Logger.Info("Deleting all previous folders.");
                file.Delete();
            }
        }
    }
}