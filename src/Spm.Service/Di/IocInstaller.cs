using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Spm.Service.Serialization;
using Spm.Shared;

namespace Spm.Service.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IMarkAsMessageCreator>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsHistoryChecker>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISerializeMessage>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsTransition>().WithService.FromInterface());
        }
    }
}