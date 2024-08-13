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
    public class MaterialMasterHandler : IHandleMessages<MaterialMasterCommand>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MaterialMasterHandler));

        private readonly IGetDataForMaterialMaster _materialMasterData;
        private readonly IFileWatcherRepository _fileWatcherRepository;
        private readonly IMapJdeToSapForMaterialMaster _mapJdeToSapForMaterialMaster;
        private readonly IWorkWithFiles _files;
        private readonly IBus _bus;

        private string _messageType;
        private string _endpoint;

        public MaterialMasterHandler(
            IGetDataForMaterialMaster materialMasterData,
            IFileWatcherRepository fileWatcherRepository,
            IMapJdeToSapForMaterialMaster mapJdeToSapForMaterialMaster,
            IWorkWithFiles files,
            IBus bus)
        {
            _materialMasterData = materialMasterData;
            _fileWatcherRepository = fileWatcherRepository;
            _mapJdeToSapForMaterialMaster = mapJdeToSapForMaterialMaster;
            _files = files;
            _bus = bus;
        }

        public void Handle(MaterialMasterCommand message)
        {
            const float leg = 1.0F;

            _messageType = typeof(MaterialMasterCommand).FullName;
            _endpoint = typeof(MaterialMasterCommand).FullName;

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

            var dataDtoList = _materialMasterData.ExtractDataFromFile(message.Path, message.PathToError, message.FileName, message.ErrorFileName);
            if (dataDtoList == null || !dataDtoList.Any())
            {
                Logger.Warn(Constants.FileDataListIsNull);
                
                _files.UploadFile(message.FileName, message.Path, message.PathToBakup);
                _files.DeleteFile(message.Path, message.FileName);

                return;
            }

            var materialMasterSplittingDto = _mapJdeToSapForMaterialMaster.SplitMappingResult(dataDtoList);
            
            var problemWithMapping = materialMasterSplittingDto.MappingResultList.Any();
            if (problemWithMapping)
            {
                var errorListAsString = new StringBuilder();

                foreach (var item in materialMasterSplittingDto.MappingResultList) { errorListAsString.AppendLine(item); }

                _files.CreateErrorFileForIssue(message.Path, message.PathToError, message.FileName, message.ErrorFileName, errorListAsString.ToString());

                return;
            }

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                return;

            _fileWatcherRepository.InsertMaterialMasterData(materialMasterSplittingDto.MappedDataDtoList.Keys.ToList(), message.FileName);

            SendAuditTrailMessageOnly(materialMasterSplittingDto.MappedDataDtoList.Keys.ToList(), leg);

            SendSagaMessage(materialMasterSplittingDto.MappedDataDtoList.Values.ToList());

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.DownloadFile(message.FileName, message.PathToLocalDestination, message.Path, message.BufferFileCount);

            if (_files.IsFileMissing(message.Path, message.FileName, message.PathToError)) 
                throw new Exception(Constants.FileMissing);

            _files.UploadFile(message.FileName, message.PathToLocalDestination, message.PathToBakup);

            _files.DeleteFile(message.Path, message.FileName);
        }

        private void SendAuditTrailMessageOnly(ICollection<MaterialMasterSagaDto> dtoList, float leg)
        {
            foreach (var item in dtoList)
            {
                var materialMasterAuditCommand = new MaterialMasterAuditCommand
                {
                    MessageType = _messageType,
                    ReferenceId = item.Aitm,
                    SagaReferenceId = item.SagaReferenceId,
                    FromEndpoint = _endpoint,
                    DateTimeMessageSendToHere = DateTime.Now,
                    Action = (int)AuditAction.FileWatcherInit,
                    MessageData = $"ItemsInMessage={dtoList.Count}",
                    Leg = leg
                };

                _bus.Send(materialMasterAuditCommand);
            }
        }

        private void SendSagaMessage(IEnumerable<MaterialMasterSapDto> sapDtoList)
        {
            foreach (var item in sapDtoList)
            {
                var sagaMessage = _mapJdeToSapForMaterialMaster.MapPayload(item);
                sagaMessage.ShortItemNumber = item.E1MaramBismt;
                sagaMessage.SagaReferenceId = item.SagaReferenceId;

                _bus.Send(sagaMessage);
            }
        }
    }
}