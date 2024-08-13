using System.Reflection;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Hosting.Profiles;
using NServiceBus.Persistence;
using Spm.Service.Persistence;
using Spm.Shared;

namespace Spm.Service.Config
{
    public class DevelopmentProfileConfig : IHandleProfile<DevelopmentProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.DevelopmentSpmService;
            var assembly = Assembly.Load(Shared.AssemblyName.SpmService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */

            config.UsePersistence<NHibernatePersistence>().ConnectionString(defaultConnectionString);
            config.UsePersistence<NHibernatePersistence, StorageType.Sagas>();
            config.UsePersistence<NHibernatePersistence, StorageType.Subscriptions>();
            config.UsePersistence<NHibernatePersistence, StorageType.Timeouts>();

            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.EnableFeature<NServiceBus.Features.Sagas>();
            config.EnableFeature<TimeoutManager>();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x =>
                x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }

    public class TestProfileConfig : IHandleProfile<TestProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.TestSpmService;
            var assembly = Assembly.Load(Shared.AssemblyName.SpmService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */

            config.UsePersistence<NHibernatePersistence>().ConnectionString(defaultConnectionString);
            config.UsePersistence<NHibernatePersistence, StorageType.Sagas>();
            config.UsePersistence<NHibernatePersistence, StorageType.Subscriptions>();
            config.UsePersistence<NHibernatePersistence, StorageType.Timeouts>();

            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.EnableFeature<NServiceBus.Features.Sagas>();
            config.EnableFeature<TimeoutManager>();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x =>
                x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }

    public class ProductionProfileConfig : IHandleProfile<ProductionProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.ProductionSpmService;
            var assembly = Assembly.Load(Shared.AssemblyName.SpmService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */

            config.UsePersistence<NHibernatePersistence>().ConnectionString(defaultConnectionString);
            config.UsePersistence<NHibernatePersistence, StorageType.Sagas>();
            config.UsePersistence<NHibernatePersistence, StorageType.Subscriptions>();
            config.UsePersistence<NHibernatePersistence, StorageType.Timeouts>();

            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.EnableFeature<NServiceBus.Features.Sagas>();
            config.EnableFeature<TimeoutManager>();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x =>
                x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }
}