using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus.Logging;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Config;
using Spm.OrrSys.Service.Dto;
using Spm.OrrSys.Service.Map;
using Spm.OrrSys.Service.Repositories;
using Spm.OrrSys.Service.Soap.SqlReportService;
using Spm.OrrSys.Service.TestCertificates;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Service.Business
{
    public class TestCertificateBusiness : IDoTestCertificateBusiness
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TestCertificateBusiness));

        private readonly IJdeImportRepository _jde;
        private readonly ITestCertRepository _testCert;
        private readonly IDoTestCertificateCommunication _soap;
        private readonly IMapTestCertificateMessage _map;
        private readonly IWriteTestCertificates _write;
        private readonly IDeleteTestCertificates _del;
        private readonly IUniqueNumbers _uniqueNumber;

        public TestCertificateBusiness(
            IJdeImportRepository jde,
            ITestCertRepository testCert,
            IDoTestCertificateCommunication soap,
            IMapTestCertificateMessage map,
            IWriteTestCertificates write,
            IDeleteTestCertificates del,
            IUniqueNumbers uniqueNumber)
        {
            _jde = jde;
            _testCert = testCert;
            _soap = soap;
            _map = map;
            _write = write;
            _del = del;
            _uniqueNumber = uniqueNumber;
        }

        public void CreateInboundTestCertificate(TestCertificateRequestCommand message)
        {
            Logger.Info("Doing inbound Test-Certificate business.");

            if (message.Payload?.TestCertificatePayloadItemList == null || !message.Payload.TestCertificatePayloadItemList.Any())
                throw new Exception("Input Test-Certificate list is null or empty. Cannot Proceed!");

            var sapMaterialNrList = message.Payload.TestCertificatePayloadItemList.Select(x => x.SapMaterialNumber).Distinct().ToList();
            var crossReferenceNumberList = _jde.GetCrossReferenceNumberList(sapMaterialNrList);

            if (crossReferenceNumberList == null || crossReferenceNumberList.Count == 0)
                throw new Exception("No Cross-Reference-Numbers??? MUST have a value. Cannot Proceed!");

            Logger.Info($"crossReferenceNumberList.Count={crossReferenceNumberList.Count}");
            Logger.Info($"message.Payload.Count={message.Payload.TestCertificatePayloadItemList.Count}");

            var despatchedList = (from item in message.Payload.TestCertificatePayloadItemList
                                  let crossRefTemp = crossReferenceNumberList.FirstOrDefault(x => x.Ivcitm.Equals(item.SapMaterialNumber))
                                  select new DespatchedPacksByCustomerOrderDto
                                  {
                                      SalesOrderNumber = item.SalesOrderNumber,
                                      SaleInvoiceNumber = item.SaleInvoiceNumber,
                                      CustomerName = item.CustomerName.Length > 30 ? item.CustomerName.Substring(0, 29) : item.CustomerName,
                                      CustomerAccountNumber = item.CustomerAccountNumber,
                                      PackNumber = item.PackNumber,
                                      Item = crossRefTemp == null ? "-" : crossRefTemp.Ivaitm,
                                      DateUpdated = item.DateUpdated,
                                      TimeUpdated = DateTime.Now.ToString("HHmmss"),
                                      State = "-",
                                      ShipTo = item.ShipTo,
                                      EmailAddress = item.EmailAddress,
                                      Processed = "N",
                                      PurchaseOrder = item.PurchaseOrder,
                                      SapMatNum = item.SapMaterialNumber,
                                      DateTimeCreated = DateTime.Now
                                  }).ToList();

            Logger.Info($"Inserting: DespatchedPacksByCustomerOrder.Count={despatchedList.Count}");

            _testCert.BulkInsertTestCertificate(despatchedList);
        }

        public IList<OutboundTestCertificateDto> GetOutboundTestCertificateData()
        {
            var returnVal = _jde.GetOutboundTestCerticateData();

            return returnVal;
        }

        public IList<int> GetByUniqueReportGroup(IList<OutboundTestCertificateDto> dtoList)
        {
            var uniqueIdList = dtoList.Select(x => x.ReportGroup).Distinct().ToList();

            return uniqueIdList;
        }

        public void DeletePreviousTestCertificates(bool restrictTestCertFiles, string testCertBakupFolder, int pdfFileBufferSize)
        {
            if (restrictTestCertFiles)
            {
                _del.DeletePreviousTestCertficateFiles(ProfileConfigVariables.TestCertFolder);
            }

            _del.DeleteBakupTestCertificateFilesIfFolderIsFull(testCertBakupFolder, pdfFileBufferSize);
        }

        public void CreateOutboundTestCertificateResponse(string[] lotNumberArray)
        {
            _testCert.BulkUpdateTestCertificate(lotNumberArray);
        }

        public TestCertificateOutboundPayload CreatePayloadForOutboundTestCertificateRequest(IList<OutboundTestCertificateDto> outboundTestCertificateList)
        {
            var certificateId = _uniqueNumber.CreateUniqueCertificateNumber();
            var outboundTestCertificateDto = outboundTestCertificateList.First();
            var pdfDto = _map.ToPdfDto(outboundTestCertificateDto, certificateId);

            Logger.Info("Get pdf from SSRS Service.");
            var pdf = _soap.GetPdf(pdfDto);

            _write.ToFile(pdf, pdfDto, ProfileConfigVariables.TestCertFolder);
            _write.ToBakup(pdf, pdfDto, ProfileConfigVariables.TestCertBakupFolder);
            var encodedPdf = _write.ToBase64EncodingWithLineBreaks(pdf);

            Logger.Info("Create payload for command:");
            Logger.Info($"CertificateId={pdfDto.CertificateId}");
            Logger.Info($"Pdf bufferSize={pdf.Length}");

            var returnVal = new TestCertificateOutboundPayload
            {
                CertificateNumber = pdfDto.CertificateId,
                CertificateDateTime = DateTime.Now,
                CertificateFunctionCode = Service.Constants.CertificateFunctionCode,
                SellerPartyIdScheme = Service.Constants.SellerPartyIdScheme,
                CustomerPartyIdentifier = Service.Constants.CustomerPartyIdentifier,
                Attachment = encodedPdf,
                AttachmentType = Service.Constants.FileType,
                AttachmentEncoding = Service.Constants.EncodingType,
                CustomerOrderNumber = outboundTestCertificateDto.PurchaseOrder,
                LineItems = outboundTestCertificateList
                    .Select(y => new TestCertificateOutboundPayloadItem
                    {
                        MaterialIdentifier = y.RowNumber.ToString(),
                        MaterialReferenceTypeCode = Service.Constants.MaterialReferenceTypeCode,
                        MaterialReferenceNumber = y.FirstLevelLot,
                        HeatNumber = y.HeatNumber,
                        CustomerOrderNumber = y.PurchaseOrder,
                        CustomerArticleNumber = y.SapMaterialNumber
                    }).ToList()
            };
            Logger.Info($"PayloadItems.Count={returnVal.LineItems.Count}");

            return returnVal;
        }
    }
}