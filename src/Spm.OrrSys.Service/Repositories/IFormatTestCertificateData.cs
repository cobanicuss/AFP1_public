using System.Collections.Generic;
using Spm.OrrSys.Messages;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IFormatTestCertificateData
    {
        IList<OutboundTestCertificateDto> FormatOutboundTestCertificate(IEnumerable<object[]> resultList);
    }
}