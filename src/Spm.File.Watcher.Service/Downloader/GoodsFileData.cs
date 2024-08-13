using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NServiceBus.Logging;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Downloader
{
    public class GoodsFileData : IGetDataForGoods
    {
        private readonly IWorkWithFiles _files;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsFileData));
        private const int ColumnCount = Constants.GoodsColumnCount; //NOT zero based. ONE based//

        public GoodsFileData(IWorkWithFiles files)
        {
            _files = files;
        }

        public IList<GoodsDto> ExtractDataFromFile(string path, string errorPath, string fileName, string errorFileName)
        {
            Logger.Info($"Extracting data from File={path}{fileName}");
            var goodsDtoList = new List<GoodsDto>();

            try
            {
                var fullFilePath = $"{path}{fileName}";
                var rowList = System.IO.File.ReadAllText(fullFilePath).Split('\n').ToList();
                var firstRowIsHeader = 0;

                if (!rowList.Any() || rowList.Count < 2) return null;

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

                    if (commaCount == ColumnCount) rowCopy = Regex.Replace(row, @",\r", "\r");

                    var columnArr = rowCopy.Split(',');
                    if (columnArr.Count() != ColumnCount)
                    {
                        FileIsBadlyFormatted(path, errorPath, fileName, errorFileName, firstRowIsHeader, columnArr);
                    }

                    var dataDto = MakeDto(columnArr);

                    goodsDtoList.Add(dataDto);
                }

                return goodsDtoList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        private static GoodsDto MakeDto(IList<string> columnArr)
        {
            var dataDto = new GoodsDto
            {
                PstngDate = columnArr[0].Trim(),
                DocDate = columnArr[1].Trim(),
                RefDocNo = columnArr[2].Trim(),
                HeaderTxt = columnArr[3].Trim(),
                GmCode = columnArr[4].Trim(),
                Plant = columnArr[5].Trim(),
                StgeLoc = columnArr[6].Trim(),
                MoveType = columnArr[7].Trim(),
                EntryQnt = columnArr[8].Trim(),
                EntryUom = columnArr[9].Trim(),
                PoNumber = columnArr[10].Trim(),
                PoItem = columnArr[11].Trim(),
                MvtInd = columnArr[12].Trim(),
                NoMoreGr = columnArr[13].Trim(),
                TranTypeInd = columnArr[14].Trim(),
                Id = columnArr[15].Trim(),
                OrderType = columnArr[16].Trim(),
                DocType = columnArr[17].Trim(),
                ReceiptDoc = Regex.Replace(columnArr[18], @"\r|\n", string.Empty).Trim()
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