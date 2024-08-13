using System.Collections.Generic;
using Spm.OrrSys.Messages;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Service.Business
{
    public interface IDoTestCertificateBusiness : IMarkAsBusinessRule
    {
        void CreateInboundTestCertificate(TestCertificateRequestCommand message);

        IList<OutboundTestCertificateDto> GetOutboundTestCertificateData();
        IList<int> GetByUniqueReportGroup(IList<OutboundTestCertificateDto> dtoList);

        void DeletePreviousTestCertificates(bool restrictTestCertFiles, string testCertBakupFolder, int pdfFileBufferSize);

        void CreateOutboundTestCertificateResponse(string[] lotNumberArray);
        TestCertificateOutboundPayload CreatePayloadForOutboundTestCertificateRequest(IList<OutboundTestCertificateDto> outboundTestCertificateList);
    }
}