using System;
using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.Soap.SqlReportService
{
    public interface IDoTestCertificateCommunication : ICommunicateWithSqlReportingServices
    {
        byte[] GetPdf(TestCertDto testCertDto);
    }

    public class TestCertificateOutbound : IDoTestCertificateCommunication
    {
        private const string TestCertManufacturingReportName = @"/Quality/ManufacturingTestCertificate/Test Cert Manufacturing";
        private const string Language = "en-us";
        private const string UserName = @"STEEL\orrconintegration";

        public byte[] GetPdf(TestCertDto testCertDto)
        {
            var rs = new ReportExecutionServiceSoapClient("ReportExecutionServiceSoap");

            if (rs.ClientCredentials == null) throw new Exception("ClientCredentials must not be null at this point. Unexepected error. Cannot proceed.");

            rs.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            rs.ClientCredentials.UserName.UserName = UserName;

            ExecutionInfo execInfo;
            var trusteduserHeader = new TrustedUserHeader();
            ServerInfoHeader serviceInfo;

            rs.LoadReport(trusteduserHeader, TestCertManufacturingReportName, null, out serviceInfo, out execInfo);

            var execHeader = new ExecutionHeader { ExecutionID = execInfo.ExecutionID };
            var paramval = new ParameterValue[2];

            paramval[0] = new ParameterValue
            {
                Name = "PackNumber",
                Value = testCertDto.ReportGroup.ToString()
            };

            paramval[1] = new ParameterValue
            {
                Name = "TestCertNumber",
                Value = testCertDto.CertificateId
            };

            rs.SetExecutionParameters(execHeader, trusteduserHeader, paramval, Language, out execInfo);

            byte[] result;
            string extension;
            string mimeType;
            string encoding;
            Warning[] warnings;
            string[] streamIDs;

            rs.Render(execHeader, 
                trusteduserHeader, 
                testCertDto.PdfFormat, 
                string.Empty, 
                out result, 
                out extension, 
                out mimeType,
                out encoding, 
                out warnings, 
                out streamIDs);

            return result;
        }
    }
}