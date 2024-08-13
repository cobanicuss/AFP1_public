using System;
using System.IO;
using System.Text;
using NServiceBus.Logging;
using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.TestCertificates
{
    public class WriteTestCertificates : IWriteTestCertificates
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WriteTestCertificates));

        public void ToBakup(byte[] pdf, TestCertBakupDto testCertDto, string bakupFolder)
        {
            var file = $@"{Constants.FileIdentifier}_{testCertDto.CertificateId}_{testCertDto.PurchaseOrder}_{Guid.NewGuid()}.{Constants.FileType}";
            var fullPath = $"{bakupFolder}{file}";

            var folderIsMissing = !Directory.Exists(bakupFolder);

            if (folderIsMissing) Directory.CreateDirectory(bakupFolder);

            LogDetailOfTestCertificate(bakupFolder, file);

            WriteToFile(pdf, fullPath);
        }

        public void ToFile(byte[] pdf, TestCertFileDto testCertDto, string testCertificatePath)
        {
            var folderPerDay = $@"{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}\";
            var folder = $@"{testCertificatePath}{folderPerDay}";
            var file = $@"{Constants.FileIdentifier}_{testCertDto.CertificateId}-{testCertDto.SalesOrderNumber}-{testCertDto.SalesInvoiceNumber}-{testCertDto.CustomerAccountNumber}-{testCertDto.CustomerName}_{testCertDto.PurchaseOrder}.{Constants.FileType}";
            var fullPath = $"{folder}{file}";

            var folderIsMissing = !Directory.Exists(folder);

            if (folderIsMissing) Directory.CreateDirectory(folder);

            LogDetailOfTestCertificate(folder, file);

            WriteToFile(pdf, fullPath);
        }

        public string ToBase64EncodingWithLineBreaks(byte[] pdf)
        {
            var arrayLength = (long)(4.0d / 3.0d * pdf.Length);

            if (arrayLength % 4 != 0)
            {
                arrayLength += 4 - arrayLength % 4;
            }

            var charArray = new char[arrayLength];

            Convert.ToBase64CharArray(pdf, 0, pdf.Length, charArray, 0);

            var line = string.Empty;
            var lineIndex = 0;
            var sb = new StringBuilder();

            foreach (var ch in charArray)
            {
                lineIndex ++;

                line = line + ch;

                if (lineIndex == 64)
                {
                    sb.AppendLine(line);
                    line = string.Empty;
                    lineIndex = 0;
                }
            }
            if(!string.IsNullOrEmpty(line)) sb.AppendLine(line);

            return sb.ToString();
        }

        private static void WriteToFile(byte[] pdf, string fullPath)
        {
            if(System.IO.File.Exists(fullPath)) System.IO.File.Delete(fullPath);

            using (var stream = System.IO.File.OpenWrite(fullPath))
            {
                stream.Write(pdf, 0, pdf.Length);
                stream.Close();
            }
        }

        private static void LogDetailOfTestCertificate(string folder, string file)
        {
            Logger.Info($"Working with folder:{folder}.");
            Logger.Info($"Pdf file-name:{file}.");
            Logger.Info("Write pdf to file:");
        }
    }
}