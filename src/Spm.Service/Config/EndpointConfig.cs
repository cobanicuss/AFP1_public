using Castle.Windsor;
using NServiceBus;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.Service.Di;
using Spm.Shared;

namespace Spm.Service.Config
{
    public class EndpointConfig : IConfigureThisEndpoint, INeedInitialization, AsA_Server
    {
        public EndpointConfig()
        {
            LogManager.Use<Log4NetFactory>();
        }

        public void Customize(BusConfiguration configuration)
        {
            configuration.UseSerialization<XmlSerializer>();
            configuration.EndpointName(EndPointName.SpmService);
            configuration.UseContainer<WindsorBuilder>(x => { x.ExistingContainer(CreateIocContainer()); });
            configuration.UseTransport<MsmqTransport>();
        }
        
        protected WindsorContainer CreateIocContainer()
        {
            var container = new WindsorContainer();
            container.Install(new IocInstaller());
            return container;
        }
    }
}