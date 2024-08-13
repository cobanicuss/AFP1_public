using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NServiceBus.Logging;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Downloader
{
    public class MaterialMasterFileData : IGetDataForMaterialMaster
    {
        private readonly IWorkWithFiles _files;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterFileData));
        private const int ColumnCount = Constants.MaterialMasterColumnCount; //NOT zero based. ONE based//

        public MaterialMasterFileData(IWorkWithFiles files)
        {
            _files = files;
        }

        public IList<MaterialMasterDto> ExtractDataFromFile(string path, string errorPath, string fileName, string errorFileName)
        {
            var materialMasterDtoList = new List<MaterialMasterDto>();

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

                    materialMasterDtoList.Add(dataDto);
                }
                return materialMasterDtoList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        private static MaterialMasterDto MakeDto(IList<string> columnArr)
        {
            var dataDto = new MaterialMasterDto
            {
                Itm = columnArr[0].Trim(),
                Litm = columnArr[1].Trim(),
                Aitm = columnArr[2].Trim(),
                Dsc1 = columnArr[3].Trim(),
                Dsc2 = columnArr[4].Trim(),
                Dsc12 = columnArr[5].Trim(),
                LongDsc = columnArr[6].Trim(),
                Mcu = columnArr[7].Trim(),
                Stkt = columnArr[8].Trim(),
                Draw = columnArr[9].Trim(),
                Srp3Desc = columnArr[10].Trim(),
                Srp4Desc = columnArr[11].Trim(),
                Srp4Desc2 = columnArr[12].Trim(),
                Srp5Desc = columnArr[13].Trim(),
                Srp6Desc = columnArr[14].Trim(),
                Srp7Desc = columnArr[15].Trim(),
                Srp8Desc = columnArr[16].Trim(),
                Prp0 = columnArr[17].Trim(),
                Prp0M = columnArr[18].Trim(),
                Prp1Desc = columnArr[19].Trim(),
                Prp1Desc2 = columnArr[20].Trim(),
                Prp2Desc = columnArr[21].Trim(),
                Prp3Desc = columnArr[22].Trim(),
                Sec1 = columnArr[23].Trim(),
                Sec2 = columnArr[24].Trim(),
                Prp4Desc = columnArr[25].Trim(),
                Prp5Desc = columnArr[26].Trim(),
                Prp6Desc = columnArr[27].Trim(),
                Imprp3 = columnArr[28].Trim(),
                Imprp3Desc = columnArr[29].Trim(),
                Imprp4 = columnArr[30].Trim(),
                Imprp4Desc = columnArr[31].Trim(),
                Imuom1 = columnArr[32].Trim(),
                Bq = columnArr[33].Trim(),
                BwCk = columnArr[34].Trim(),
                Kgs = columnArr[35].Trim(),
                Mt = columnArr[36].Trim(),
                GhMm = columnArr[37].Trim(),
                GwMm = columnArr[38].Trim(),
                Kg = columnArr[39].Trim(),
                Prp4 = columnArr[40].Trim(),
                Srp1 = columnArr[41].Trim(),
                Srp1Desc = columnArr[42].Trim(),
                Prp3Desc2 = columnArr[43].Trim(),
                Prp2Desc2 = columnArr[44].Trim(),
                UnitCost = Regex.Replace(columnArr[45], @"\r|\n", string.Empty).Trim()
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