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
    public class GeneralLedgerHandler : IHandleMessages<GeneralLedgerCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GeneralLedgerHandler));

        private readonly IGetDataForGeneralLedger _generalLedgerData;
        private readonly IFileWatcherRepository _fileWatcherRepository;
        private readonly IMapJdeToSapForGeneralLedger _mapJdeToSapForGeneralLedger;
        private readonly IWorkWithFiles _files;
        private readonly ICastDto _castDto;
        private readonly IBus _bus;

        private string _messageType;
        private string _endpoint;
        private string _fileName;

        public GeneralLedgerHandler(
            IGetDataForGeneralLedger generalLedgerData,
            IFileWatcherRepository fileWatcherRepository,
            IMapJdeToSapForGeneralLedger mapJdeToSapForGeneralLedger,
            IWorkWithFiles files,
            ICastDto castDto,
            IBus bus)
        {
            _generalLedgerData = generalLedgerData;
            _fileWatcherRepository = fileWatcherRepository;
            _mapJdeToSapForGeneralLedger = mapJdeToSapForGeneralLedger;
            _files = files;
            _castDto = castDto;
            _bus = bus;
        }

        public void Handle(GeneralLedgerCommand message)
        {
            const float leg = 1.0F;

            _messageType = typeof(GeneralLedgerCommand).FullName;
            _endpoint = typeof(GeneralLedgerHandler).FullName;
            _fileName = message.FileName;

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

            var dataDtoList = _generalLedgerData.ExtractDataFromFile(message.Path, message.PathToError, _fileName, message.ErrorFileName);
            if (dataDtoList == null || !dataDtoList.Any())
            {
                Logger.Warn(Constants.FileDataListIsNull);

                _files.UploadFile(message.FileName, message.Path, message.PathToBakup);
                _files.DeleteFile(message.Path, message.FileName);

                return;
            }

            var dataDtoListSort = OrderDataByHeaderFields(dataDtoList);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                return;

            var rowCountDataDtoList = _castDto.AsGeneralLedgerItemDto(dataDtoListSort);
            
            var successfull = ProcessMessageByGroup(rowCountDataDtoList, leg, message.Path, message.PathToError, message.FileName, message.ErrorFileName);
            if (!successfull) return;

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.DownloadFile(message.FileName, message.PathToLocalDestination, message.Path, message.BufferFileCount);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.UploadFile(message.FileName, message.PathToLocalDestination, message.PathToBakup);

            _files.DeleteFile(message.Path, message.FileName);
        }

        private static IEnumerable<GeneralLedgerDto> OrderDataByHeaderFields(IEnumerable<GeneralLedgerDto> dataDtoList)
        {
            var returnVal = dataDtoList.OrderBy(x => x.PstingDate)
                .ThenBy(x => x.HeaderTxt)
                .ThenBy(x => x.DocDate)
                .ThenBy(x => x.CompCode)
                .ThenBy(x => x.RefDocNo).ToList();

            return returnVal;
        }

        private bool ProcessMessageByGroup(IList<GeneralLedgerItemDto> dataDtoListSort, float leg, string path, string pathToError, string fileName, string errorFileName)
        {
            var dtoGroupForMessage = new List<GeneralLedgerItemDto>();
            var previousDto = dataDtoListSort[0]; //NULL-check done previously//
            var rowCount = 0;

            foreach (var dto in dataDtoListSort)
            {
                rowCount++;

                var isSame = IsHeaderSectionTheSame(previousDto, dto);
                var isDifferent = !isSame;

                if (isSame)
                {
                    dto.ItemNoAcc = rowCount;
                    dtoGroupForMessage.Add(dto);
                }

                if (isDifferent)
                {
                    var problemInGroup = !ProcessThisGroup(dtoGroupForMessage, leg, path, pathToError, fileName, errorFileName);

                    if (problemInGroup) return false;

                    dtoGroupForMessage.Clear();

                    rowCount = 1;
                    dto.ItemNoAcc = rowCount;
                    dtoGroupForMessage.Add(dto);
                }
                previousDto = dto;
            }

            var successful = ProcessThisGroup(dtoGroupForMessage, leg, path, pathToError, fileName, errorFileName);

            return successful;
        }

        private bool ProcessThisGroup(IList<GeneralLedgerItemDto> dtoGroupForMessage, float leg, string path, string pathToError, string fileName, string errorFileName)
        {
            var sagaReferenceId = Guid.NewGuid().ToString();
            var generalLedgerId = dtoGroupForMessage[0].RefDocNo; //Null check done previously//
            var messageItemCount = dtoGroupForMessage.Count;
            var sapDtoGroupForMessage = _castDto.AsGeneralLedgerSapDto(dtoGroupForMessage);
            var resultList = _mapJdeToSapForGeneralLedger.CreateMapping(sapDtoGroupForMessage);

            if (resultList.MappingProblemList.Any())
            {
                var resultAsString = _castDto.AsString(resultList.MappingProblemList);
                _files.CreateErrorFileForIssue(path, pathToError, fileName, errorFileName, resultAsString);
                
                return false;
            }

            _fileWatcherRepository.InsertGeneralLedgerData(dtoGroupForMessage, _fileName, sagaReferenceId);

            SendAuditTrailMessage(sagaReferenceId, generalLedgerId, messageItemCount, leg);
            SendSagaMessage(resultList.MappedList, sagaReferenceId, generalLedgerId);

            return true;
        }

        private void SendAuditTrailMessage(string sagaReferenceId, string generalLedgerId, int messageItemCount, float leg)
        {
            var generalLedgerAuditCommand = new GeneralLedgerAuditCommand
            {
                MessageType = _messageType,
                GeneralLedgerId = generalLedgerId,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = _endpoint,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.FileWatcherInit,
                MessageData = $"ItemsInMessage={messageItemCount}",
                Leg = leg
            };

            _bus.Send(generalLedgerAuditCommand);
        }

        private void SendSagaMessage(IList<GeneralLedgerSapDto> mappedGroupForMessage, string sagaReferenceId, string generalLedgerId)
        {
            var sagaMessage = _mapJdeToSapForGeneralLedger.MapPayload(mappedGroupForMessage);
            sagaMessage.GeneralLedgerId = generalLedgerId;
            sagaMessage.SagaReferenceId = sagaReferenceId;

            _bus.Send(sagaMessage);
        }

        private static bool IsHeaderSectionTheSame(GeneralLedgerDto initDto, GeneralLedgerDto dto)
        {
            return initDto.HeaderTxt.Equals(dto.HeaderTxt) &&
                   initDto.CompCode.Equals(dto.CompCode) &&
                   initDto.PstingDate.Equals(dto.PstingDate) &&
                   initDto.RefDocNo.Equals(dto.RefDocNo);
        }
    }
}