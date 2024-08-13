using Castle.Windsor;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.File.Watcher.Service.Di;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Config
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
            configuration.EnableFeature<NServiceBus.Features.Sagas>();
            configuration.EnableFeature<TimeoutManager>();
            configuration.EndpointName(EndPointName.SpmFileWatcherService);
            configuration.UseContainer<WindsorBuilder>(x => { x.ExistingContainer(CreateIocContainer()); });
        }

        protected WindsorContainer CreateIocContainer()
        {
            var container = new WindsorContainer();
            container.Install(new IocInstaller());
            return container;
        }
    }
}