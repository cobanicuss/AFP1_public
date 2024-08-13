using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NServiceBus;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;

namespace Spm.Service.ForSoap.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IMarkAsMapper>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDuplicateSetupInSap>().WithService.FromInterface(),
                Classes.FromAssemblyContaining<FileBuffer>().BasedOn<IFileBuffer>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISerializeMessagesToFile>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendSoapToSap<ICommand>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendGeneralLedgerToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendGoodsReceiptToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendMaterialMasterToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendProductAchievementToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendProductionOrderStatusToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendPurchaseOrderChangeToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendPurchaseOrderCreateToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendTestCertificateToSap>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISendResponseOnRequestToSap>().WithService.FromInterface());
        }
    }
}