using NServiceBus;
using NServiceBus.Logging;
using Spm.File.Watcher.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Repositories;

namespace Spm.OrrSys.Service.Handlers
{
    public class CacheMapUpdateRequestCommandHandler : IHandleMessages<CacheMapUpdateRequestCommand>
    {
        private readonly IBus _bus;
        private readonly IOrrSysMappingRepository _orrSysMappingRepository;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(CacheMapUpdateRequestCommandHandler));

        public CacheMapUpdateRequestCommandHandler(IBus bus, IOrrSysMappingRepository orrSysMappingRepository)
        {
            _bus = bus;
            _orrSysMappingRepository = orrSysMappingRepository;
        }

        public void Handle(CacheMapUpdateRequestCommand message)
        {
            var branchList = _orrSysMappingRepository.GetBranchMapping();
            var companyCodeList = _orrSysMappingRepository.GetCompanyCodeMapping();
            var costCentreGlPostingList = _orrSysMappingRepository.GetCostCentreGlPostingMapping();
            var docTypeList = _orrSysMappingRepository.GetDocTypeMapping();
            var glAccountsGlPostingList = _orrSysMappingRepository.GetGlAccountsGlPostingMapping();
            var locationList = _orrSysMappingRepository.GetLocationMapping();
            var materialGroupList = _orrSysMappingRepository.GetMaterialGroupMapping();
            var profitCentreGlPostingList = _orrSysMappingRepository.GetProfitCentreGlPostingMapping();
            var purchaseGroupList = _orrSysMappingRepository.GetPurchaseGroupMapping();
            var unitOfMeasureList = _orrSysMappingRepository.GetUnitOfMeasureMapping();
            var plantList = _orrSysMappingRepository.GetPlantMapping();

            var responseMessage = new CacheMapResponseCommand
            {
                FileWatcherSagaId = message.FileWatcherSagaId,
                MapBranchList = branchList,
                MapCompanyCodeList = companyCodeList,
                MapCostCentreGlPostingList = costCentreGlPostingList,
                MapDocTypesList = docTypeList,
                MapGlAccountsGlPostingList = glAccountsGlPostingList,
                MapLocationList = locationList,
                MapMaterialGroupList = materialGroupList,
                MapProfitCentreGlPostingList = profitCentreGlPostingList,
                MapPurchaseGroupList = purchaseGroupList,
                MapUnitOfMeasureList = unitOfMeasureList,
                MapPlantList = plantList
            };

            Logger.Info("======================================");
            Logger.Info("Extracting mapping for Cache Map.");
            Logger.Info($"MapBranchList={branchList.Count}");
            Logger.Info($"MapCompanyCodeList={companyCodeList.Count}");
            Logger.Info($"MapCostCentreGlPostingList={costCentreGlPostingList.Count}");
            Logger.Info($"MapDocTypeList={docTypeList.Count}");
            Logger.Info($"MapGlAccountsGlPostingList={glAccountsGlPostingList.Count}");
            Logger.Info($"MapLocationList={locationList.Count}");
            Logger.Info($"MapMaterialGroupList={materialGroupList.Count}");
            Logger.Info($"MapPlantList={plantList.Count}");
            Logger.Info($"MapProfitCentreGlPostingList={profitCentreGlPostingList.Count}");
            Logger.Info($"MapPurchaseGroupList={purchaseGroupList.Count}");
            Logger.Info($"MapUnitOfMeasureList={unitOfMeasureList.Count}");
            
            _bus.Send(responseMessage);

            Logger.Info("Message send successfully.");
        }
    }
}