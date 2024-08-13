using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Spm.Service.ReceiveFromSap.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<ILogIncommingMessages>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IValidateIncommingMessages>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapSoapMessage>().WithService.FromInterface());
        }
    }
}