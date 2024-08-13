using System;
using System.Collections.Generic;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
using Spm.AuditLog.Messages;
using Spm.File.Watcher.Messages;
using Spm.File.Watcher.Service.Downloader;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.File.Watcher.Service.Repository;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Handlers
{
    public class PurchaseOrderCreateHandler : IHandleMessages<PurchaseOrderCreateCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PurchaseOrderCreateHandler));

        private readonly IGetDataForPurchaseOrder _purchaseOrderData;
        private readonly IFileWatcherRepository _fileWatcherRepository;
        private readonly IMapJdeToSapPurchaseOrderCreate _mapJdeToSapPurchaseOrder;
        private readonly IWorkWithFiles _files;
        private readonly ICastDto _castDto;
        private readonly IBus _bus;

        private string _messageType;
        private string _endpoint;

        public PurchaseOrderCreateHandler(
            IGetDataForPurchaseOrder purchaseOrderData,
            IFileWatcherRepository fileWatcherRepository,
            IMapJdeToSapPurchaseOrderCreate mapJdeToSapPurchaseOrder,
            IWorkWithFiles files,
            ICastDto castDto,
            IBus bus)
        {
            _purchaseOrderData = purchaseOrderData;
            _fileWatcherRepository = fileWatcherRepository;
            _mapJdeToSapPurchaseOrder = mapJdeToSapPurchaseOrder;
            _files = files;
            _castDto = castDto;
            _bus = bus;
        }

        public void Handle(PurchaseOrderCreateCommand message)
        {
            const float leg = 1.0F;

            _messageType = typeof(PurchaseOrderCreateCommand).FullName;
            _endpoint = typeof(PurchaseOrderCreateHandler).FullName;

            Logger.Info($"Now procesing; fileName={message.FileName}.");

            var sagaReferenceId = Guid.NewGuid().ToString();

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError))
            {
                Logger.Warn(Constants.FileIsMissingEarly);
                return;
            }
            if (_files.IsFileLocked(message.Path, message.FileName))
            {
                Logger.Warn(Constants.FileIsLocked);
                return;
            }

            var dataDtoList = _purchaseOrderData.ExtractDataFromFile(message.Path, message.PathToError, message.FileName, message.ErrorFileName);
            if (dataDtoList == null || !dataDtoList.Any())
            {
                Logger.Warn(Constants.FileDataListIsNull);

                _files.UploadFile(message.FileName, message.Path, message.PathToBakup);
                _files.DeleteFile(message.Path, message.FileName);

                return;
            }

            var sapDtoList = _castDto.AsPurchaseOrderSapDto(dataDtoList);

            var resultList = _mapJdeToSapPurchaseOrder.CreateMapping(sapDtoList);
            if (resultList.MappingProblemList.Any())
            {
                var resultAsString = _castDto.AsString(resultList.MappingProblemList);

                _files.CreateErrorFileForIssue(message.Path, message.PathToError, message.FileName, message.ErrorFileName, resultAsString);

                return;
            }

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                return;

            SaveAndSendMessages(message, dataDtoList, sagaReferenceId, leg, resultList);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.DownloadFile(message.FileName, message.PathToLocalDestination, message.Path, message.BufferFileCount);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.UploadFile(message.FileName, message.PathToLocalDestination, message.PathToBakup);

            _files.DeleteFile(message.Path, message.FileName);
        }

        private void SaveAndSendMessages(
            FileBaseCommand message,
            IList<PurchaseOrderDto> dataDtoList,
            string sagaReferenceId,
            float leg,
            MappingResultPurchaseOrderDto resultList)
        {
            _fileWatcherRepository.InsertPurchaseOrderCreateData(dataDtoList, message.FileName, sagaReferenceId);

            SendAuditTrailMessage(sagaReferenceId, dataDtoList[0].PoNumber, dataDtoList.Count, leg);//null-check done previousely//

            var sagaMessage = _mapJdeToSapPurchaseOrder.MapPayload(resultList.MappedList);
            sagaMessage.PurchaseOrderNumber = resultList.MappedList[0].PoNumber.TrimStart('0'); //null-check done previousely//
            sagaMessage.SagaReferenceId = sagaReferenceId;

            _bus.Send(sagaMessage);
        }

        private void SendAuditTrailMessage(string sagaReferenceId, string purchaseOrderNumber, int messageItemCount, float leg)
        {
            var productionOrderStatusAuditCommand = new PurchaseOrderAuditCommand
            {
                MessageType = _messageType,
                PurchaseOrderNumber = purchaseOrderNumber,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = _endpoint,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.FileWatcherInit,
                MessageData = $"ItemsInMessage={messageItemCount}",
                Type = Shared.Constants.PoCreate,
                Leg = leg
            };

            _bus.Send(productionOrderStatusAuditCommand);
        }
    }
}