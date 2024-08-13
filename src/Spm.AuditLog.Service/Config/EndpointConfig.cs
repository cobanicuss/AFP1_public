using Castle.Windsor;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.AuditLog.Service.Di;
using Spm.Shared;

namespace Spm.AuditLog.Service.Config
{
    public class EndpointConfig : IConfigureThisEndpoint, INeedInitialization, AsA_Server
    {
        public EndpointConfig()
        {
            LogManager.Use<Log4NetFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<XmlSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.EnableFeature<NServiceBus.Features.Sagas>();
            configuration.EnableFeature<TimeoutManager>();
            configuration.EndpointName(EndPointName.SpmAuditLogService);
            configuration.UseContainer<WindsorBuilder>(x => { x.ExistingContainer(CreateIocContainer()); });
        }

        private static WindsorContainer CreateIocContainer()
        {
            var container = new WindsorContainer();
            container.Install(new IocInstaller());
            return container;
        }
    }
}