using System.Collections.Generic;
using NServiceBus;

namespace Spm.OrrSys.Messages
{
    public class TestCertificateFileRequestCommand : ICommand
    {
        public TestCertificateFileRequestCommand()
        {
            GroupedDtoList = new List<OutboundTestCertificateDto>();
        }

        public string InboundId { get; set; }
        public List<OutboundTestCertificateDto> GroupedDtoList { get; set; }
        public int CurrentMessageCount { get; set; }
        public int TotalMessages { get; set; }
    }
}