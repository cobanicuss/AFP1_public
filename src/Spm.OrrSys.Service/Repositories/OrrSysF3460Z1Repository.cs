using System.Collections.Generic;
using System.Data;
using NHibernate;
using Spm.OrrSys.Service.Domain;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public class OrrSysF3460Z1Repository : IPlannedOrderRepository
    {
        private readonly IDoBulkInsert _quick;
        private const string Table = Constants.TableNameF3460Z1;
        private const string HistoryTable = Constants.TableNameF3460Z1History;
        private const int HistoryKeptDays = Constants.PlannedOrderHistoryKeptDays;

        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public OrrSysF3460Z1Repository(IDoBulkInsert quick)
        {
            _quick = quick;
        }

        public void DeleteAllByTable()
        {
            var sql = $"DELETE FROM {Table}";

            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }

        public void BulkInsertByTypeList(IEnumerable<OrrSysF3460Z1> workOrderList)
        {
            var dt = DataTableSetupForBulkInsert.CreateF3460Z1(Table);
            DataTableSetupForBulkInsert.PopulateF3460Z1(workOrderList, dt);

            _quick.BulkInsert(dt, Table);
        }

        public void CreateHistoryTraking()
        {
            const string dateColumnReference = "DateTimeImplemented";

            var sqlDelete = SqlStringQuery.DeleteHistoryNDaysBack(HistoryTable, HistoryKeptDays, dateColumnReference);
            var sqlInsert = SqlStringQuery.InsertHistoryForToday(HistoryTable, Table);

            if (Session.Connection.State != ConnectionState.Closed) Session.Connection.Close();
            Session.Connection.Open();

            using (var transaction = Session.BeginTransaction())
            {
                Session.CreateSQLQuery(sqlDelete).ExecuteUpdate();

                Session.CreateSQLQuery(sqlInsert).ExecuteUpdate();
                transaction.Commit();
            }
        }
    }
}