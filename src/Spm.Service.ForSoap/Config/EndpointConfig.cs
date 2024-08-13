using Castle.Windsor;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Di;
using Spm.Shared;

namespace Spm.Service.ForSoap.Config
{
    public class EndpointConfig : IConfigureThisEndpoint, INeedInitialization, AsA_Server
    {
        public EndpointConfig()
        {
            LogManager.Use<Log4NetFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            //Polling for timeouts at//

            configuration.UseTransport<MsmqTransport>();
            configuration.DisableFeature<Sagas>();
            //configuration.DisableFeature<TimeoutManager>();
            configuration.DisableFeature<TimeoutManager>();
            //configuration.DisableFeature<SecondLevelRetries>();
            configuration.DisableFeature<SecondLevelRetries>();
            configuration.UseSerialization<XmlSerializer>();
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.EndpointName(EndPointName.SpmServiceForSoap);
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