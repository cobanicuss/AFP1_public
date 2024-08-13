using System.Configuration;
using NServiceBus.Config;
using NServiceBus.Config.ConfigurationSource;
using Spm.Shared;

namespace Spm.Service.Config
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

            var spmOrrSysService = new MessageEndpointMapping
            {
                AssemblyName = AssemblyName.SpmOrrSysService,
                Endpoint = EndPointName.SpmOrrSysService,
                Messages = AssemblyName.SpmOrrSysMessages
            };

            var spmServiceForSoap = new MessageEndpointMapping
            {
                AssemblyName = AssemblyName.SpmServiceForSoap,
                Endpoint = EndPointName.SpmServiceForSoap,
                Messages = AssemblyName.SpmServiceForSoapMessages
            };

            config.MessageEndpointMappings.Add(spmAuditLogService);
            config.MessageEndpointMappings.Add(spmOrrSysService);
            config.MessageEndpointMappings.Add(spmServiceForSoap);

            return config;
        }
    }
}