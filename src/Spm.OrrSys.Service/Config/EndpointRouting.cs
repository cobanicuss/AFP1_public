using System.Configuration;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;
using Spm.Shared;

namespace Spm.OrrSys.Service.Config
{
    public class EndpointRouting : IProvideConfiguration<UnicastBusConfig>
    {
        public UnicastBusConfig GetConfiguration()
        {
            var config = (UnicastBusConfig)ConfigurationManager.GetSection(typeof(UnicastBusConfig).Name);

            if (config == null)
            {
                config = new UnicastBusConfig
                {
                    MessageEndpointMappings = new MessageEndpointMappingCollection()
                };
            }

            var spmAuditLogService = new MessageEndpointMapping
            {
                AssemblyName = AssemblyName.SpmAuditLogService,
                Endpoint = EndPointName.SpmAuditLogService,
                Messages = AssemblyName.SpmAuditLogMessages
            };

            var spmService = new MessageEndpointMapping
            {
                AssemblyName = AssemblyName.SpmService,
                Endpoint = EndPointName.SpmService,
                Messages = AssemblyName.SpmServiceMessages
            };

            var spmFileWatcherService = new MessageEndpointMapping
            {
                AssemblyName = AssemblyName.SpmFileWatcherService,
                Endpoint = EndPointName.SpmFileWatcherService,
                Messages = AssemblyName.SpmFileWatcherMessages
            };

            config.MessageEndpointMappings.Add(spmAuditLogService);
            config.MessageEndpointMappings.Add(spmService);
            config.MessageEndpointMappings.Add(spmFileWatcherService);

            return config;
        }
    }
}