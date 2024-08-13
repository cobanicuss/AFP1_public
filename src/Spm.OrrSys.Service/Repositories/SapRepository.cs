using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NHibernate;
using Spm.OrrSys.Service.Config;
using Spm.OrrSys.Service.Domain;

namespace Spm.OrrSys.Service.Repositories
{
    public interface ISapRepository
    {
        IList<DemandSapJde> GetInboundPlannedOrderList();
        IList<DemandSapJdeWo> GetInboundProductionOrderList();

        void TriggerBulkInsertProductionOrdersIntoOracle();
        void TriggerBulkInsertPlannedOrdersIntoOracle();
    }

    public class SapRepository : ISapRepository
    {
        private readonly IDbConnection _dbConnection;
        public ISessionFactory SessionFactory { get; set; }

        public SapRepository()
        {
            _dbConnection = new SqlConnection(ProfileConfigVariables.Sap);
        }

        public IList<DemandSapJde> GetInboundPlannedOrderList()
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var returnVal = session.QueryOver<DemandSapJde>().List();

                _dbConnection.Close();
                return returnVal;
            }
        }

        public IList<DemandSapJdeWo> GetInboundProductionOrderList()
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var returnVal = session.QueryOver<DemandSapJdeWo>().List();

                _dbConnection.Close();
                return returnVal;
            }
        }

        public void TriggerBulkInsertProductionOrdersIntoOracle()
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var query = session.CreateSQLQuery("exec SsisTransfereOracleProductionOrders");
                query.ExecuteUpdate();

                _dbConnection.Close();
            }
        }

        public void TriggerBulkInsertPlannedOrdersIntoOracle()
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var query = session.CreateSQLQuery("exec SsisTransfereOraclePlannedOrders");
                query.ExecuteUpdate();

                _dbConnection.Close();
            }
        }
    }
}