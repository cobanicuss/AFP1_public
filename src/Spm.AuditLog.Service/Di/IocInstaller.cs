using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Spm.AuditLog.Service.Repository;
using Spm.Shared;

namespace Spm.AuditLog.Service.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IAuditLogRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IImplementAuditLogRollOver>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMarkAsMapper>().WithService.FromInterface());
        }
    }
}