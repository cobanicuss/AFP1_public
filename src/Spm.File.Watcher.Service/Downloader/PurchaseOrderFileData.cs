using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NServiceBus.Logging;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Downloader
{
    public class PurchaseOrderFileData : IGetDataForPurchaseOrder
    {
        private readonly IWorkWithFiles _files;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PurchaseOrderFileData));
        private const int ColumnCount = Constants.PurchaseOrderColumnCount; //NOT zero based. ONE based//

        public PurchaseOrderFileData(IWorkWithFiles files)
        {
            _files = files;
        }

        public IList<PurchaseOrderDto> ExtractDataFromFile(string path, string errorPath, string fileName, string errorFileName)
        {
            var purchaseOrderDtoList = new List<PurchaseOrderDto>();

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

                    purchaseOrderDtoList.Add(dataDto);
                }

                return purchaseOrderDtoList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }
        }

        private static PurchaseOrderDto MakeDto(IList<string> columnArr)
        {
            var dataDto = new PurchaseOrderDto
            {
                PoNumber = columnArr[0].Trim(),
                CompCode = columnArr[1].Trim(),
                DocType = columnArr[2].Trim(),
                CreateDate = columnArr[3].Trim(),
                CreatedBy = columnArr[4].Trim(),
                ItemIntvl = columnArr[5].Trim(),
                Vendor = columnArr[6].Trim(),
                PurchOrg = columnArr[7].Trim(),
                PurGroup = columnArr[8].Trim(),
                Currency = columnArr[9].Trim(),
                DocDate = columnArr[10].Trim(),
                PoItem = columnArr[11].Trim(),
                DeleteInd = columnArr[12].Trim(),
                ShortText = columnArr[13].Trim(),
                Plant = columnArr[14].Trim(),
                StgeLoc = columnArr[15].Trim(),
                MatlGroup = columnArr[16].Trim(),
                VendMat = columnArr[17].Trim(),
                Quantity = columnArr[18].Trim(),
                PoUnit = columnArr[19].Trim(),
                OrderPrUn = columnArr[20].Trim(),
                NetPrice = columnArr[21].Trim(),
                PriceUnit = columnArr[22].Trim(),
                OverDlvTol = columnArr[23].Trim(),
                NoMoreGr = columnArr[24].Trim(),
                Acctasscat = columnArr[25].Trim(),
                PreqName = columnArr[26].Trim(),
                SerialNo = columnArr[27].Trim(),
                GlAccount = columnArr[28].Trim(),
                CostCenter = columnArr[29].Trim(),
                DeliveryDate = columnArr[30].Trim(),
                LnType = columnArr[31].Trim(),
                Taxable = Regex.Replace(columnArr[32], @"\r|\n", string.Empty).Trim()
            };
            return dataDto;
        }

        private void FileIsBadlyFormatted(string sourcePath, 
            string destinationPathToError, 
            string fileName, 
            string errorFileName, 
            int firstRowIsHeader, 
            IEnumerable<string> columnArr)
        {
            var error = Constants.BadFileFormat(firstRowIsHeader.ToString(), fileName, ColumnCount.ToString(), columnArr.Count().ToString());

            _files.CreateErrorFileForIssue(sourcePath, destinationPathToError, fileName, errorFileName, error);

            throw new FormatException(error);
        }
    }
}