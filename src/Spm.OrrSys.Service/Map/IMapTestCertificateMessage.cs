using System.Collections.Generic;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Dto;
using Spm.Service.Messages;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Service.Map
{
    public interface IMapTestCertificateMessage
    {
        TestCertificateFileRequestCommand CreateTestCertificateFileCommandMessage(IList<OutboundTestCertificateDto> groupedDtoList, int current, int total, string inboundId);
        TestCertificateCommand CreateCommandMessage(TestCertificateOutboundPayload payload, string inboundId, int index, int count);
        TestCertDto ToPdfDto(OutboundTestCertificateDto outboundTestCertificateDto, string certificateId);
    }
}