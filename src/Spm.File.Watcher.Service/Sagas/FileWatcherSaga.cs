using System;
using System.Linq;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Spm.File.Watcher.Messages;
using Spm.File.Watcher.Service.Config;
using Spm.File.Watcher.Service.Downloader;
using Spm.File.Watcher.Service.Repository;
using Spm.File.Watcher.Service.SagaData;
using Spm.OrrSys.Messages;

namespace Spm.File.Watcher.Service.Sagas
{
    public class FileWatcherSaga : Saga<FileWatcherSagaData>,
                                            IAmStartedByMessages<FileWatcherSagaInit>,
                                            IHandleMessages<CacheMapResponseCommand>,
                                            IHandleTimeouts<TimeToUploadFiles>
    {
        private static readonly string FolderContainingFiles = $@"{EnvironmentConfig.FolderContainingFiles}\";
        private static readonly string BakupAfterProcessing = $@"{EnvironmentConfig.FolderContainingBakupFiles}\";
        private static readonly string BakupAfterError = $@"{EnvironmentConfig.FolderContainingErrorFiles}\";

        private readonly string _path = $"{Shared.Constants.BaseLocation}{FolderContainingFiles}";
        private readonly string _pathToBakup = $"{Shared.Constants.BaseLocation}{BakupAfterProcessing}";
        private readonly string _pathToError = $"{Shared.Constants.BaseLocation}{BakupAfterError}";

        private static readonly ILog Logger = LogManager.GetLogger(typeof(FileWatcherSaga));
        public IWorkWithFiles FileWorker { get; set; }
        public ICacheMapRepository CacheMapRepository { get; set; }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<FileWatcherSagaData> mapper)
        {
            mapper.ConfigureMapping<FileWatcherSagaInit>(m => m.FileWatcherSagaInitId).ToSaga(s => s.FileWatcherSagaInitId);
            mapper.ConfigureMapping<CacheMapResponseCommand>(m => m.FileWatcherSagaId).ToSaga(s => s.FileWatcherSagaInitId);
        }

        public void Handle(FileWatcherSagaInit message)
        {
            Logger.Info("======================================");
            Logger.Info("Saga Started!");

            var sagaId = message.FileWatcherSagaInitId;

            Data.FileWatcherSagaInitId = sagaId;

            Logger.Info($"SagaId={sagaId}.");

            Bus.Send(new CacheMapUpdateRequestCommand { FileWatcherSagaId = sagaId });

            RunDataExtractionProcess();

            SetNextTimeout();
        }

        public void Timeout(TimeToUploadFiles state)
        {
            Logger.Info("======================================");
            Logger.Info("Uploading files after waiting period.");

            RunDataExtractionProcess();

            SetNextTimeout();
        }

        public void Handle(CacheMapResponseCommand message)
        {
            Logger.Info("======================================");
            Logger.Info("Updating cache.");

            if (Data.CacheMapRetryOnSagaBusyCount >= Constants.MaxCacheMapRetryOnSagaBusy) return;

            if (Data.IsBusyExtractingData && Data.CacheMapRetryOnSagaBusyCount < Constants.MaxCacheMapRetryOnSagaBusy)
            {
                Data.CacheMapRetryOnSagaBusyCount++;

                Logger.Info("Saga is busy cannot update cache now. Retry later.");
                Logger.Info($"Data.CacheMapRetryOnSagaBusyCount={Data.CacheMapRetryOnSagaBusyCount}");
                Bus.SendLocal(message);
                return;
            }

            Logger.Info($"MapBranchList={message.MapBranchList.Count}");
            Logger.Info($"MapCompanyCodeList={message.MapCompanyCodeList.Count}");
            Logger.Info($"MapCostCentreGlPostingList={message.MapCostCentreGlPostingList.Count}");
            Logger.Info($"MapDocTypesList={message.MapDocTypesList.Count}");
            Logger.Info($"MapGlAccountsGlPostingList={message.MapGlAccountsGlPostingList.Count}");
            Logger.Info($"MapLocationList={message.MapLocationList.Count}");
            Logger.Info($"MapMaterialGroupList={message.MapMaterialGroupList.Count}");
            Logger.Info($"MapPlantList={message.MapPlantList.Count}");
            Logger.Info($"MapProfitCentreGlPostingList={message.MapProfitCentreGlPostingList.Count}");
            Logger.Info($"MapPurchaseGroupList={message.MapPurchaseGroupList.Count}");
            Logger.Info($"MapUnitOfMeasureList={message.MapUnitOfMeasureList.Count}");

            CacheMapRepository.LoadNewCacheMapping(message);

            Logger.Info("All good. Cache updated.");
        }

        private void RunDataExtractionProcess()
        {
            if (Data.IsBusyExtractingData) return;

            var fileServerIsOffLine = !FileWorker.IsBaseLocationAvailable(_path);

            if (fileServerIsOffLine)
            {
                Logger.Warn("Server containing files maybe OFF-LINE,");
                Logger.Warn("or the Base-Directory does NOT EXIST!!!");
                Logger.Warn("Will retry later.");
                return;
            }

            Data.IsBusyExtractingData = true;

            Logger.Info("Get a list of files to download.");
            var fileList = FileWorker.GetAllFilesInFolderByType(_path, Shared.Constants.FileExtension);

            if (fileList == null || !fileList.Any())
            {
                Logger.Info("No files found to process. Retry later. All good!");
                Data.IsBusyExtractingData = false;
                return;
            }

            Logger.Info($"Total of {fileList.Count} files found to work with.");
            foreach (var file in fileList.Take(Constants.FileWatcherUploadBatchSize))
            {
                var fileName = file.ToUpper();

                if (fileName.Contains(Shared.Constants.PurchaseOrderCreateFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoPurchaseOrderCreateWorkflow(file);
                if (fileName.Contains(Shared.Constants.PurchaseOrderChangeFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoPurchaseOrderChangeWorkflow(file);
                if (fileName.Contains(Shared.Constants.GoodsReceiptFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoGoodsReceiptWorkflow(file);
                if (fileName.Contains(Shared.Constants.GoodsReversalFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoGoodsReversalWorkflow(file);
                if (fileName.Contains(Shared.Constants.GeneralLedgerFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoGeneralLedgerWorkflow(file);
                if (fileName.Contains(Shared.Constants.MaterialMasterFileName) && fileName.Contains(Shared.Constants.FileExtension)) DoMaterialMasterWorkflow(file);
            }

            Data.IsBusyExtractingData = false;
        }

        private void SetNextTimeout()
        {
            RequestTimeout<TimeToUploadFiles>(TimeSpan.FromMinutes(Constants.FileWatcherUploadPeriodMinutes));
        }

        private void DoGoodsReversalWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new GoodsReversalCommand
            {
                FileName = fileName,
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.GoodsReversal}",
                BufferFileCount = Shared.Constants.GoodsReversalFileCount
            };

            Bus.SendLocal(message);
        }

        private void DoGoodsReceiptWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new GoodsReceiptCommand
            {
                FileName = fileName,
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.GoodsReceipt}",
                BufferFileCount = Shared.Constants.GoodsReceiptFileCount
            };

            Bus.SendLocal(message);
        }

        private void DoPurchaseOrderCreateWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new PurchaseOrderCreateCommand
            {
                FileName = fileName, 
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.PurchaseOrderCreate}",
                BufferFileCount = Shared.Constants.PurchaseOrderCreateFileCount
            };

            Bus.SendLocal(message);
        }

        private void DoPurchaseOrderChangeWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new PurchaseOrderChangeCommand
            {
                FileName = fileName,
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.PurchaseOrderChange}",
                BufferFileCount = Shared.Constants.PurchaseOrderChangeFileCount
            };

            Bus.SendLocal(message);
        }

        private void DoGeneralLedgerWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new GeneralLedgerCommand
            {
                FileName = fileName,
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.GeneralLedger}",
                BufferFileCount = Shared.Constants.GeneralLedgerFileCount
            };

            Bus.SendLocal(message);
        }

        private void DoMaterialMasterWorkflow(string fileName)
        {
            Logger.Info($"Send local for processing; fileName={fileName}.");

            var message = new MaterialMasterCommand
            {
                FileName = fileName,
                Path = _path,
                PathToBakup = _pathToBakup,
                ErrorFileName = MakeErrorFileNameFromFileName(fileName),
                PathToError = _pathToError,
                PathToLocalDestination = $@"{Shared.Constants.LogFilePath}{Constants.LocalDestnDir}{Shared.Constants.MaterialMaster}",
                BufferFileCount = Shared.Constants.MaterailMasterFileCount
            };

            Bus.SendLocal(message);
        }

        private static string MakeErrorFileNameFromFileName(string fileName)
        {
            var errorFileName = $"{fileName.Substring(0, fileName.IndexOf('.'))}_ERROR.txt";
            return errorFileName;
        }
    }
}