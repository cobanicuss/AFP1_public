using System.Reflection;
using NServiceBus;
using NServiceBus.Hosting.Profiles;
using Spm.File.Watcher.Service.Persistence;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Config
{
    public class DevelopmentProfileConfig : IHandleProfile<DevelopmentProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.DevelopmentSpmFileWatcher;

            EnvironmentConfig.FolderContainingFiles = "JdeToSapDev";
            EnvironmentConfig.FolderContainingErrorFiles = "JdeToSapDevError";
            EnvironmentConfig.FolderContainingBakupFiles = "JdeToSapDevBackup";

            var assembly = Assembly.Load(Shared.AssemblyName.SpmFileWatcherService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

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
            config.UsePersistence<InMemoryPersistence>();
            config.EnableFeature<NServiceBus.Features.Sagas>();
            config.EnableFeature<NServiceBus.Features.TimeoutManager>();

            var defaultConnectionString = ProfileConnectionString.TestSpmFileWatcher;

            EnvironmentConfig.FolderContainingFiles = "JdeToSapTest";
            EnvironmentConfig.FolderContainingErrorFiles = "JdeToSapTestError";
            EnvironmentConfig.FolderContainingBakupFiles = "JdeToSapTestBackup";

            var assembly = Assembly.Load(Shared.AssemblyName.SpmFileWatcherService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

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
            config.UsePersistence<InMemoryPersistence>();
            config.EnableFeature<NServiceBus.Features.Sagas>();
            config.EnableFeature<NServiceBus.Features.TimeoutManager>();

            var defaultConnectionString = ProfileConnectionString.ProductionSpmFileWatcher;

            EnvironmentConfig.FolderContainingFiles = "JdeToSapProd";
            EnvironmentConfig.FolderContainingErrorFiles = "JdeToSapProdError";
            EnvironmentConfig.FolderContainingBakupFiles = "JdeToSapProdBackup";

            var assembly = Assembly.Load(Shared.AssemblyName.SpmFileWatcherService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x =>
                x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }

    public class EnvironmentConfig
    {
        public static string FolderContainingFiles { get; set; }
        public static string FolderContainingErrorFiles { get; set; }
        public static string FolderContainingBakupFiles { get; set; }
    }
}