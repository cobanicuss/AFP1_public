using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using NHibernate;
using Spm.OrrSys.Service.Domain;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public class OrrSysF554801ZRepository : IProductionOrderRepository
    {
        private readonly IDoBulkInsert _quick;
        private const string Table = Constants.TableNameF554801Z;
        private const string HistoryTable = Constants.TableNameF554801ZHistory;
        private const int HistoryKeptDays = Constants.ProductionOrderHistoryKeptDays;

        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public OrrSysF554801ZRepository(IDoBulkInsert quick)
        {
            _quick = quick;
        }

        public void DeleteAllByTable()
        {
            var sql = $"DELETE FROM {Table}";

            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }

        public void BulkInsertByTypeList(IEnumerable<OrrSysF554801Z> workOrderList)
        {
            var dt = DataTableSetupForBulkInsert.CreateF554801Z(Table);
            DataTableSetupForBulkInsert.PopulateF554801Z(workOrderList, dt);

            _quick.BulkInsert(dt, Table);
        }

        public void InsertNewHistory()
        {
            var previousList = Session.QueryOver<OrrSysF554801Z>().List();

            var addedColumns = previousList.Select(x => new OrrSysF554801ZHistory
            {
                SYEDUS = x.SYEDUS,
                SYEDBT = x.SYEDBT,
                SYEDTN = x.SYEDTN,
                SYEDLN = x.SYEDLN,
                SYEDCT = x.SYEDCT,
                SYTYTN = x.SYTYTN,
                SYEDDT = x.SYEDDT,
                SYDRIN = x.SYDRIN,
                SYEDDL = x.SYEDDL,
                SYEDSP = x.SYEDSP,
                SYTNAC = x.SYTNAC,
                SYDCTO = x.SYDCTO,
                SYITM = x.SYITM,
                SYLITM = x.SYLITM,
                SYDRQJ = x.SYDRQJ,
                SYUORG = x.SYUORG,
                SYSOQS = x.SYSOQS,
                SYUM = x.SYUM,
                SYMCU = x.SYMCU,
                SYVR01 = x.SYVR01,
                DateTimeImplemented = DateTime.Now,
                DateRequested = ConvertDate.JdeDateToDate(x.SYDRQJ.ToString(CultureInfo.InvariantCulture))
            }).ToList();

            var dt = DataTableSetupForBulkInsert.CreateF554801ZHistory(HistoryTable);
            DataTableSetupForBulkInsert.PopulateF554801ZHistory(addedColumns, dt);

            _quick.BulkInsert(dt, HistoryTable);
        }

        public void DeleteOldHistory()
        {
            const string dateColumnReference = "DateRequested";

            var sqlDelete = SqlStringQuery.DeleteHistoryNDaysBack(HistoryTable, HistoryKeptDays, dateColumnReference);

            if (Session.Connection.State != ConnectionState.Closed) Session.Connection.Close();
            Session.Connection.Open();

            Session.CreateSQLQuery(sqlDelete).ExecuteUpdate();

        }
    }
}