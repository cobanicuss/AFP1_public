using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class GoodsReceiptHandler : IHandleMessages<GoodsReceiptCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsReceiptHandler));

        private readonly IGetDataForGoods _goodsData;
        private readonly IFileWatcherRepository _fileWatcherRepository;
        private readonly IMapJdeToSapForAllGoods _mapJdeToSapForGoods;
        private readonly IWorkWithFiles _files;
        private readonly IBus _bus;

        private string _messageType;
        private string _endpoint;
        private const string GoodsIdent = Constants.GoodsReceiptType;
        private const string GoodsType = Shared.Constants.GoodsReceiptType;

        public GoodsReceiptHandler(
            IGetDataForGoods goodsData,
            IFileWatcherRepository fileWatcherRepository,
            IMapJdeToSapForAllGoods mapJdeToSapForGoods,
            IWorkWithFiles files,
            IBus bus)
        {
            _goodsData = goodsData;
            _fileWatcherRepository = fileWatcherRepository;
            _mapJdeToSapForGoods = mapJdeToSapForGoods;
            _files = files;
            _bus = bus;
        }

        public void Handle(GoodsReceiptCommand message)
        {
            const float leg = 1.0F;

            _messageType = typeof(GoodsReceiptCommand).FullName;
            _endpoint = typeof(GoodsReceiptHandler).FullName;

            Logger.Info($"Now procesing; fileName={message.FileName}.");

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

            var dataDtoList = _goodsData.ExtractDataFromFile(message.Path, message.PathToError, message.FileName, message.ErrorFileName);
            if (dataDtoList == null || !dataDtoList.Any())
            {
                Logger.Warn(Constants.FileDataListIsNull);

                _files.UploadFile(message.FileName, message.Path, message.PathToBakup);
                _files.DeleteFile(message.Path, message.FileName);

                return;
            }

            var goodsSplittingDto = _mapJdeToSapForGoods.SplitMappingResult(dataDtoList, GoodsIdent);
            
            var problemWithMapping = goodsSplittingDto.MappingResultList.Any();
            if (problemWithMapping)
            {
                var errorListAsString = new StringBuilder();

                foreach (var item in goodsSplittingDto.MappingResultList) { errorListAsString.AppendLine(item); }

                _files.CreateErrorFileForIssue(message.Path, message.PathToError, message.FileName, message.ErrorFileName, errorListAsString.ToString());

                return;
            }

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                return;

            _fileWatcherRepository.InsertGoodsReceiptData(goodsSplittingDto.MappedDataDtoList, message.FileName, GoodsType);

            SendMessage(goodsSplittingDto.MappedDataDtoList, dataDtoList.Count, leg);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.DownloadFile(message.FileName, message.PathToLocalDestination, message.Path, message.BufferFileCount);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.UploadFile(message.FileName, message.PathToLocalDestination, message.PathToBakup);

            _files.DeleteFile(message.Path, message.FileName);
        }

        private void SendMessage(IEnumerable<GoodsSagaDto> goodsDataDtoList, int messageItemCount, float leg)
        {
            foreach (var item in goodsDataDtoList)
            {
                SendAuditTrailMessage(item.SagaReferenceId, item.PoNumber.TrimStart('0'), messageItemCount, leg);

                SendSagaMessage(item);
            }
        }

        private void SendSagaMessage(GoodsSagaDto item)
        {
            var sagaMessage = _mapJdeToSapForGoods.MapPayload(item);
            sagaMessage.GoodsReceiptId = item.PoNumber.TrimStart('0');
            sagaMessage.SagaReferenceId = item.SagaReferenceId;
            sagaMessage.Type = GoodsType;

            _bus.Send(sagaMessage);
        }

        private void SendAuditTrailMessage(string sagaReferenceId, string goodsReceiptId, int messageItemCount, float leg)
        {
            var productionOrderStatusAuditCommand = new GoodsReceiptAuditCommand
            {
                MessageType = _messageType,
                GoodsReceiptId = goodsReceiptId,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = _endpoint,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.FileWatcherInit,
                MessageData = $"ItemsInMessage={messageItemCount}",
                Type = GoodsType,
                Leg = leg
            };

            _bus.Send(productionOrderStatusAuditCommand);
        }
    }
}