using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NServiceBus.Logging;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Downloader
{
    public class GeneralLedgerFileData : IGetDataForGeneralLedger
    {
        private readonly IWorkWithFiles _files;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerFileData));
        private const int ColumnCount = Constants.GeneralLedgerColumnCount; //NOT zero based. ONE based//

        public GeneralLedgerFileData(IWorkWithFiles files)
        {
            _files = files;
        }

        public IList<GeneralLedgerDto> ExtractDataFromFile(string path, string errorPath, string fileName, string errorFileName)
        {
            Logger.Info($"Extracting data from File={path}{fileName}");
            var generalLedgerDtoList = new List<GeneralLedgerDto>();

            try
            {
                var fullFilePath = $"{path}{fileName}";
                var rowList = System.IO.File.ReadAllText(fullFilePath).Split('\n').ToList();
                var firstRowIsHeader = 0;

                if (!rowList.Any() || rowList.Count == 1) return null;

                foreach (var row in rowList)
                {
                    firstRowIsHeader++;

                    if (firstRowIsHeader <= 1) continue;
                    
                    if (string.Equals(row, "\r") ||
                        string.Equals(row, "\n") ||
                        string.Equals(row, "\t") ||
                        string.IsNullOrEmpty(row) ||
                        string.IsNullOrWhiteSpace(row)) continue;

                    var commaCount = row.Count(x => x == ',');
                    var rowCopy = row;
                    
                    if(commaCount == ColumnCount) rowCopy = Regex.Replace(row, @",\r", "\r");

                    var columnArr = rowCopy.Split(',');
                    if (columnArr.Count() != ColumnCount)
                    {
                        FileIsBadlyFormatted(path, errorPath, fileName, errorFileName, firstRowIsHeader, columnArr);
                    }

                    var dataDto = MakeDto(columnArr);

                    generalLedgerDtoList.Add(dataDto);
                }
                return generalLedgerDtoList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        private static GeneralLedgerDto MakeDto(string[] columnArr)
        {
            var dataDto = new GeneralLedgerDto
            {
                HeaderTxt = columnArr[0].Trim(),
                CompCode = columnArr[1].Trim(),
                DocDate = columnArr[2].Trim(),
                PstingDate = columnArr[3].Trim(),
                RefDocNo = columnArr[4].Trim(),
                AllocNmbr = columnArr[5].Trim(),
                CostCentre = columnArr[6].Trim(),
                GlAccount = columnArr[7].Trim(),
                Currency = columnArr[8].Trim(),
                AmtDoccur = Regex.Replace(columnArr[9], @"\r|\n", string.Empty).Trim()
            };
            return dataDto;
        }

        private void FileIsBadlyFormatted(string sourcePath,
            string desitnationPathToError,
            string fileName,
            string errorFileName,
            int firstRowIsHeader,
            IEnumerable<string> columnArr)
        {
            var error = Constants.BadFileFormat(firstRowIsHeader.ToString(), fileName, ColumnCount.ToString(), columnArr.Count().ToString());

            _files.CreateErrorFileForIssue(sourcePath, desitnationPathToError, fileName, errorFileName, error);

            throw new FormatException(error);
        }
    }
}