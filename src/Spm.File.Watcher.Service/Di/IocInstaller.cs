using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NServiceBus;
using Spm.File.Watcher.Service.Downloader;
using Spm.File.Watcher.Service.JdeToSapMapping;
using Spm.File.Watcher.Service.Repository;
using Spm.File.Watcher.Service.Validation;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IMarkAsDto>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsDomain>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsValidator>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IHelpMoveFiles>().WithService.FromInterface(),
                Classes.FromAssemblyContaining<FileBuffer>().BasedOn<IFileBuffer>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IWorkWithFiles>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IConvertDate>().WithService.FromInterface(),
                Classes.FromAssemblyContaining<DoBulkInsert>().BasedOn<IDoBulkInsert>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDisplayErrors>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsBusinessRule>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IGetDataFromFile<IMarkAsDto>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IGetDataForGeneralLedger>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IGetDataForGoods>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IGetDataForPurchaseOrder>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IGetDataForMaterialMaster>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ICacheMapRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ICreateMappingByLineItem>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapPayloads>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapBase<IMarkAsDto, ICommand>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapBaseSingle<IMarkAsDto, ICommand>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapPurchaseOrderCreate>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapForPurchaseOrderChange>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapForAllGoods>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapForMaterialMaster>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapJdeToSapForGeneralLedger>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IFileWatcherRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ICastDto>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IConvertDecimal>().WithService.FromInterface());
        }
    }
}