using System.Reflection;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Spm.Shared
{
    public interface IHibernateConfig
    {
        ISessionFactory BuildSessionFactory();
        FluentConfiguration CreateFluentHibernateConfiguration(Configuration hibernateConfiguration, Assembly assembly);
        Configuration NHibernateConfiguration { get; }
    }

    public class HibernateConfig : IHibernateConfig
    {
        private readonly Assembly _assembly;

        public Configuration NHibernateConfiguration { get; private set; }


        public HibernateConfig(string connectionString, Assembly assembly)
        {
            _assembly = assembly;

            NHibernateConfiguration = CreateHibernateConfiguration(connectionString);
        }

        public ISessionFactory BuildSessionFactory()
        {
            var fluentConfiguration = CreateFluentHibernateConfiguration(NHibernateConfiguration, _assembly);
            var sessionFactory = fluentConfiguration.BuildSessionFactory();

            return sessionFactory;
        }

        public FluentConfiguration CreateFluentHibernateConfiguration(Configuration hibernateConfiguration, Assembly assembly)
        {
            var fluentConfiguration = Fluently.Configure(hibernateConfiguration)
                .Mappings(x =>
                {
                    x.FluentMappings.AddFromAssembly(assembly).Conventions.AddAssembly(assembly);
                });

            fluentConfiguration.ProxyFactoryFactory("NHibernate.Bytecode.DefaultProxyFactoryFactory, NHibernate");
            fluentConfiguration.ExposeConfiguration(x => x.SetProperty("current_session_context_class", typeof(ThreadStaticSessionContext).AssemblyQualifiedName));

            return fluentConfiguration;
        }

        private static Configuration CreateHibernateConfiguration(string connetionString)
        {
            var hibernateConfiguration = new Configuration();
            hibernateConfiguration.Properties["connection.provider"] = "NHibernate.Connection.DriverConnectionProvider";
            hibernateConfiguration.Properties["connection.driver_class"] = "NHibernate.Driver.Sql2008ClientDriver";
            hibernateConfiguration.Properties["connection.connection_string"] = connetionString;
            hibernateConfiguration.Properties["dialect"] = "NHibernate.Dialect.MsSql2008Dialect";
            return hibernateConfiguration;
        }
    }
}