using System.Reflection;
using NServiceBus;
using NServiceBus.Hosting.Profiles;
using Spm.AuditLog.Service.Persistence;
using Spm.Shared;

namespace Spm.AuditLog.Service.Config
{
    public class DevelopmentProfileConfig : IHandleProfile<DevelopmentProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.DevelopmentSpmAuditlog;

            ProfileConfigVariables.FolderContainingHistory = Constants.HistoryForDev;
            ProfileConfigVariables.SpmAuditLogDatabase = defaultConnectionString;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmAuditLogService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */
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
            var defaultConnectionString = ProfileConnectionString.TestSpmAuditlog;

            ProfileConfigVariables.FolderContainingHistory = Constants.HistoryForTest;
            ProfileConfigVariables.SpmAuditLogDatabase = defaultConnectionString;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmAuditLogService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x => x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }

    public class ProductionProfileConfig : IHandleProfile<ProductionProfile>
    {
        public void ProfileActivated(BusConfiguration config)
        {
            var defaultConnectionString = ProfileConnectionString.ProductionSpmAuditlog;

            ProfileConfigVariables.FolderContainingHistory = Constants.HistoryForProd;
            ProfileConfigVariables.SpmAuditLogDatabase = defaultConnectionString;

            var assembly = Assembly.Load(Shared.AssemblyName.SpmAuditLogService);
            var nhibernateConfig = new HibernateConfig(defaultConnectionString, assembly); /* Don't inject configuration */
            var sessionFactory = nhibernateConfig.BuildSessionFactory();

            config.RegisterComponents(x =>
                x.ConfigureComponent<FluentNHibernateMessageModule>(DependencyLifecycle.SingleInstance)
                .ConfigureProperty(y =>
                y.SessionFactory, sessionFactory));

            config.RegisterComponents(x => x.RegisterSingleton(sessionFactory));
        }

        public void ProfileActivated(Configure config) { /* ok to leave empty */ }
    }
}