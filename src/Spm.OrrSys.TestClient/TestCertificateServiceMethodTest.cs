using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Spm.OrrSys.Service.Soap.SqlReportService;

namespace Spm.OrrSys.TestClient
{
    public class TestCertificateServiceMethodTest
    {
        private const string SqlConnectionString = @"Data Source=OSSQL01;Initial Catalog=JDE_Import;Integrated Security=True";
        private ReportExecutionServiceSoapClient _client;

        public byte[] GetPdf(IList<OutboundTestCertificateDto> dtoList)
        {
            var uniqueReportGroupIdList = dtoList.Select(x => x.ReportGroup).Distinct().ToList();
            
            var pdfData = new byte[] {};

            foreach (var id in uniqueReportGroupIdList)
            {
                var certificateId = CreateUniqueCertificateNumber();
                var otcDto = dtoList.First(x => x.ReportGroup == id);
                var pdfDto = ToPdfDto(otcDto, certificateId);
                pdfData = HitService(pdfDto);
                WritePdfToFile(pdfData, pdfDto);
            }

            return pdfData;
        }

        protected void WritePdfToFile(byte[] pdf, PdfDto pdfDto)
        {
            const string mainPath = @"C:\Temp\";
            var directoryByDay = $@"{DateTime.Today.Year}{DateTime.Today.Month}{DateTime.Today.Day}\";

            var path = $@"{mainPath}{directoryByDay}";
            var file = $@"TC_{pdfDto.CertificateId}-{pdfDto.SalesOrderNumber}-{pdfDto.SalesInvoiceNumber}-{pdfDto.CustomerAccountNumber}-{pdfDto.CustomerName}_{pdfDto.PurchaseOrder}.PDF";

            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            using (var stream = System.IO.File.OpenWrite($"{path}{file}"))
            {
                stream.Write(pdf, 0, pdf.Length);
                stream.Close();
            }
        }

        private byte[] HitService(PdfDto pdfDto)
        {
            const string testCertManufacturingReportName = @"/Quality/ManufacturingTestCertificate/Test Cert Manufacturing";
            const string language = "en-us";
            const string userName = @"STEEL\orrconintegration";

            _client = new ReportExecutionServiceSoapClient("ReportExecutionServiceSoap");

            if (_client.ClientCredentials == null) throw new Exception("ClientCredentials must not be null at this point. Unexepected error. Cannot proceed.");

            _client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            _client.ClientCredentials.UserName.UserName = userName;

            ExecutionInfo execInfo;
            var trusteduserHeader = new TrustedUserHeader();
            ServerInfoHeader serviceInfo;

            _client.LoadReport(trusteduserHeader, testCertManufacturingReportName, null, out serviceInfo, out execInfo);

            var execHeader = new ExecutionHeader { ExecutionID = execInfo.ExecutionID };
            var paramval = new ParameterValue[2];

            paramval[0] = new ParameterValue
            {
                Name = "PackNumber",
                Value = pdfDto.ReportGroup.ToString()
            };

            paramval[1] = new ParameterValue
            {
                Name = "TestCertNumber",
                Value = pdfDto.CertificateId
            };

            _client.SetExecutionParameters(execHeader, trusteduserHeader, paramval, language, out execInfo);

            byte[] result;
            string extension;
            string mimeType;
            string encoding;
            Warning[] warnings;
            string[] streamIDs;

            _client.Render(execHeader,
                trusteduserHeader,
                pdfDto.PdfFormat,
                string.Empty,
                out result,
                out extension,
                out mimeType,
                out encoding,
                out warnings,
                out streamIDs);

            return result;
        }

        public IList<OutboundTestCertificateDto> GetData()
        {
            using (var sqlConnection = new SqlConnection(SqlConnectionString))
            {
                sqlConnection.Open();
                var sqlCommand = sqlConnection.CreateCommand();
                var sqlQuery = GetOutboundTestCertificate();

                sqlCommand.CommandText = sqlQuery;

                var reader = sqlCommand.ExecuteReader();

                var returnVal = FormatOutboundTestCertificate(reader);

                sqlConnection.Close();

                return returnVal;
            }
        }

        public static string GetOutboundTestCertificate()
        {
            var sb = new StringBuilder();

            sb.Append("Select reportgroup ReportGroup ");
            sb.Append(",PurchaseOrder ");
            sb.Append(",rownumber RowNumber ");
            sb.Append(",Firstlevellot FirstLevelLot ");
            sb.Append(",HeatNumber ");
            sb.Append(",SapMaterialNumber ");
            sb.Append(",CASE WHEN SalesOrderNumber IS NULL THEN '-' ELSE SalesOrderNumber END AS SalesOrderNumber ");
            sb.Append(",SaleInvoiceNumber ");
            sb.Append(",CustomerAccountNumber ");
            sb.Append(",CustomerName ");
            sb.Append("From JDE_IMPORT.dbo.TempTestResults ");

            return sb.ToString();
        }

        public IList<OutboundTestCertificateDto> FormatOutboundTestCertificate(SqlDataReader reader)
        {
            var returnVal = new List<OutboundTestCertificateDto>();

            while (reader.Read())
            {

                var tsDto = new OutboundTestCertificateDto
                {
                    ReportGroup = int.Parse(reader["ReportGroup"].ToString().Trim()),
                    PurchaseOrder = reader["PurchaseOrder"].ToString().Trim(),
                    RowNumber = int.Parse(reader["RowNumber"].ToString().Trim()),
                    FirstLevelLot = reader["FirstLevelLot"].ToString().Trim(),
                    HeatNumber = reader["HeatNumber"].ToString().Trim(),
                    SapMaterialNumber = reader["SapMaterialNumber"].ToString().Trim(),
                    SalesOrderNumber = reader["SalesOrderNumber"].ToString().Trim(),
                    SaleInvoiceNumber = reader["SaleInvoiceNumber"].ToString().Trim(),
                    CustomerAccountNumber = reader["CustomerAccountNumber"].ToString().Trim(),
                    CustomerName = reader["CustomerName"].ToString().Trim()
                };

                returnVal.Add(tsDto);
            }

            return returnVal;
        }

        public PdfDto ToPdfDto(OutboundTestCertificateDto outboundTestCertificateDto, string certificateId)
        {
            var pdfDto = new PdfDto
            {
                CertificateId = certificateId,
                PurchaseOrder = outboundTestCertificateDto.PurchaseOrder,
                ReportGroup = outboundTestCertificateDto.ReportGroup,
                CustomerAccountNumber = outboundTestCertificateDto.CustomerAccountNumber,
                CustomerName = outboundTestCertificateDto.CustomerName,
                SalesInvoiceNumber = outboundTestCertificateDto.SaleInvoiceNumber,
                SalesOrderNumber = outboundTestCertificateDto.SalesOrderNumber,
                PdfFormat = "PDF"
            };

            return pdfDto;
        }

        public string CreateUniqueCertificateNumber()
        {
            //Create a unique transaction number
            var dateBegin = new DateTime(2016, 1, 1);
            var currentDate = DateTime.Now;
            var elapsedTicks = currentDate.Ticks - dateBegin.Ticks;

            return elapsedTicks.ToString();
        }
    }

    public class OutboundTestCertificateDto
    {
        public string Certificateid { get; set; }
        public int ReportGroup { get; set; }
        public string PurchaseOrder { get; set; }
        public int RowNumber { get; set; }
        public string FirstLevelLot { get; set; }
        public string HeatNumber { get; set; }
        public string SapMaterialNumber { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SaleInvoiceNumber { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerName { get; set; }
    }

    public class PdfDto
    {
        public string CertificateId { get; set; }
        public int ReportGroup { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SalesInvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string PdfFormat { get; set; }
        public string PurchaseOrder { get; set; }
    }
}