using System;
using System.Collections.Generic;
using System.Linq;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Dto;
using Spm.Service.Messages;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Service.Map
{
    public class TestCertifiateMessageMap : IMapTestCertificateMessage
    {
        public TestCertificateFileRequestCommand CreateTestCertificateFileCommandMessage(IList<OutboundTestCertificateDto> groupedDtoList, int current, int total, string inboundId)
        {
            return new TestCertificateFileRequestCommand
            {
                InboundId = inboundId,
                GroupedDtoList = groupedDtoList as List<OutboundTestCertificateDto>,
                CurrentMessageCount = current,
                TotalMessages = total
            };
        }

        public TestCertificateCommand CreateCommandMessage(TestCertificateOutboundPayload payload, string inboundId, int index, int count)
        {
            return new TestCertificateCommand
            {
                InboundId = inboundId,
                SagaReferenceId = Guid.NewGuid().ToString(),
                MessageIndex = index,
                MessageCount = count,
                LotNumberList = payload.LineItems.Select(x => x.MaterialReferenceNumber).ToArray(),
                Payload = payload
            };
        }

        public TestCertDto ToPdfDto(OutboundTestCertificateDto outboundTestCertificateDto, string certificateId)
        {
            var pdfDto = new TestCertDto
            {
                CertificateId = certificateId,
                PurchaseOrder = outboundTestCertificateDto.PurchaseOrder,
                ReportGroup = outboundTestCertificateDto.ReportGroup,
                CustomerAccountNumber = outboundTestCertificateDto.CustomerAccountNumber,
                CustomerName = outboundTestCertificateDto.CustomerName,
                SalesInvoiceNumber = outboundTestCertificateDto.SaleInvoiceNumber,
                SalesOrderNumber = outboundTestCertificateDto.SalesOrderNumber,
                PdfFormat = Constants.FileType
            };

            return pdfDto;
        }
    }
}