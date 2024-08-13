using System;
using System.Transactions;
using NHibernate;
using NHibernate.Context;
using NServiceBus.UnitOfWork;

namespace Spm.AuditLog.Service.Persistence
{
    public class FluentNHibernateMessageModule : IManageUnitsOfWork
    {
        public ISessionFactory SessionFactory { get; set; }

        private static bool NoAmbientTransaction()
        {
            return Transaction.Current == null;
        }

        public void Begin()
        {
            if (SessionFactory == null) return;

            var session = SessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            if (NoAmbientTransaction()) { session.BeginTransaction(); }
        }

        public void End(Exception ex = null)
        {
            if ((SessionFactory == null) || !NoAmbientTransaction()) return;

            var session = SessionFactory.GetCurrentSession();
            session.Transaction.Commit();
            session.Transaction.Dispose();
            session.Close();
        }
    }
}
