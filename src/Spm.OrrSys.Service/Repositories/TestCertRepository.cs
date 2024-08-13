using System.Collections.Generic;
using NHibernate;
using Spm.OrrSys.Service.Dto;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public interface ITestCertRepository
    {
        void BulkInsertTestCertificate(List<DespatchedPacksByCustomerOrderDto> despatchedPacksByCustomerOrderList);
        void BulkUpdateTestCertificate(string[] lotNumberList);
    }

    public class TestCertRepository : ITestCertRepository
    {
        private readonly IDoBulkInsert _quick;
        private const string Table = Constants.TableNameTestCert;

        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public TestCertRepository(IDoBulkInsert quick)
        {
            _quick = quick;
        }

        public void BulkInsertTestCertificate(List<DespatchedPacksByCustomerOrderDto> despatchedPacksByCustomerOrderList)
        {
            var dt = DataTableSetupForBulkInsert.CreateTestCert(Table);
            DataTableSetupForBulkInsert.PopulateTestCert(despatchedPacksByCustomerOrderList, dt);

            _quick.BulkInsert(dt, Table);
        }

        public void BulkUpdateTestCertificate(string[] lotNumberList)
        {
            var sql = SqlStringQuery.BulkUpdateDespatchedPacksByCustomerOrder(lotNumberList);

            Session.CreateSQLQuery(sql).ExecuteUpdate();
        }
    }
}