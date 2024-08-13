using System;
using System.Linq;
using NHibernate;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IOrrSysProcessVariableRepository
    {
        DateTime RunProductinoOrderStutusOnTime();
    }

    public class OrrSysProcessVariableRepositoryRepository : IOrrSysProcessVariableRepository
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public DateTime RunProductinoOrderStutusOnTime()
        {
            var nextRunTime = Session.QueryOver<OrrSysProcessVariable>().List();

            if (nextRunTime == null || !nextRunTime.Any())
                throw new Exception("Cannot find Production-Order-Status Long-Sequence next run time in database. Cannot Proceed!");

            return nextRunTime[0].RunProductionOrderStatusOnTime;
        }
    }
}