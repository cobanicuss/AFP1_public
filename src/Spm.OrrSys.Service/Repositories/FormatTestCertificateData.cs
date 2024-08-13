using System.Collections.Generic;
using System.Linq;
using Spm.OrrSys.Messages;

namespace Spm.OrrSys.Service.Repositories
{
    public class FormatTestCertificateData : IFormatTestCertificateData
    {
        public IList<OutboundTestCertificateDto> FormatOutboundTestCertificate(IEnumerable<object[]> resultList)
        {
            return resultList.Select(item => new OutboundTestCertificateDto
            {
                ReportGroup = int.Parse(item[0].ToString().Trim()),
                PurchaseOrder = item[1].ToString(),
                RowNumber = int.Parse(item[2].ToString().Trim()),
                FirstLevelLot = item[3].ToString(),
                HeatNumber = item[4].ToString(),
                SapMaterialNumber = item[5].ToString(),
                SalesOrderNumber = item[6].ToString(),
                SaleInvoiceNumber = item[7].ToString(),
                CustomerAccountNumber = item[8].ToString(),
                CustomerName = item[9].ToString()
            }).ToList();
        }
    }
}