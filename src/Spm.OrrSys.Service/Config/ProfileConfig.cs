using System.Reflection;
using NServiceBus;
using NServiceBus.Hosting.Profiles;
using Spm.OrrSys.Service.Persistence;
using Spm.Shared;

namespace Spm.OrrSys.Service.Config
{
    public class DevelopmentProfileConfig : IHandleProfile<DevelopmentProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.DevelopmentOrrSys;

            ProfileConfigVariables.JdeImport = ProfileConnectionString.DevelopmentJdeImport;
            ProfileConfigVariables.Sap = ProfileConnectionString.DevelopmentSap;
            ProfileConfigVariables.TestCertFolder = Constants.TestCertificateFolderDev;
            ProfileConfigVariables.TestCertBakupFolder = Constants.TestCertificateBakupFolderDev;
            ProfileConfigVariables.RestrictTestCertFiles = true;
            ProfileConfigVariables.TestCertificateBufferSize = Constants.TestCertificateBufferSizeDev;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmOrrSysService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x => x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y => y.SessionFactory, sessionFactory));

            config.RegisterComponents(x => x.RegisterSingleton(sessionFactory));
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

            var defaultConnectionString = ProfileConnectionString.TestOrrSys;

            ProfileConfigVariables.JdeImport = ProfileConnectionString.TestJdeImport;
            ProfileConfigVariables.Sap = ProfileConnectionString.TestSap;
            ProfileConfigVariables.TestCertFolder = Constants.TestCertificateFolderTest;
            ProfileConfigVariables.TestCertBakupFolder = Constants.TestCertificateBackupFolderTest;
            ProfileConfigVariables.RestrictTestCertFiles = true;
            ProfileConfigVariables.TestCertificateBufferSize = Constants.TestCertificateBufferSizeTest;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmOrrSysService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x => x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y => y.SessionFactory, sessionFactory));

            config.RegisterComponents(x => x.RegisterSingleton(sessionFactory));
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

            var defaultConnectionString = ProfileConnectionString.ProductionOrrSys;

            ProfileConfigVariables.JdeImport = ProfileConnectionString.ProductionJdeImport;
            ProfileConfigVariables.Sap = ProfileConnectionString.ProductionSap;
            ProfileConfigVariables.TestCertFolder = Constants.TestCertificateFolderProd;
            ProfileConfigVariables.TestCertBakupFolder = Constants.TestCertificateBakupFolderProd;
            ProfileConfigVariables.RestrictTestCertFiles = false;
            ProfileConfigVariables.TestCertificateBufferSize = Constants.TestCertificateBufferSizeProd;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmOrrSysService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly);/* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x => x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y => y.SessionFactory, sessionFactory));

            config.RegisterComponents(x => x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }
}