using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Config;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IJdeImportRepository
    {
        IList<MaterialCrossReferenceNumberDto> GetCrossReferenceNumberList(List<string> sapMaterialNrList);
        IList<JdeItemMasterDetailDto> GetJdeItemDetails(List<int> jdeShortItemList);
        IList<OrrSysF3460Z1> GetOpenPlannedOrderList(string ftedbt, string ftedtn, decimal fteddt);
        IList<OutboundTestCertificateDto> GetOutboundTestCerticateData();
    }

    public class JdeImportRepository : IJdeImportRepository
    {
        private readonly IFormatTestCertificateData _formatTestCertificateData;
        private readonly IFormatPlannedOrderData _formatPlannedOrderData;
        private readonly IDbConnection _dbConnection;
        public ISessionFactory SessionFactory { get; set; }

        public JdeImportRepository(
            IFormatPlannedOrderData formatPlannedOrderData, 
            IFormatTestCertificateData formatTestCertificateData)
        {
            _formatPlannedOrderData = formatPlannedOrderData;
            _formatTestCertificateData = formatTestCertificateData;
            _dbConnection = new SqlConnection(ProfileConfigVariables.JdeImport);
        }

        public IList<MaterialCrossReferenceNumberDto> GetCrossReferenceNumberList(List<string> sapMaterialNrList)
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var f4104List = session.QueryOver<F4104>()
                    .Where(x => x.IVXRT == "P")
                    .List();

                _dbConnection.Close();

                var result = (from f4104 in f4104List
                              join matNrs in sapMaterialNrList
                                  on f4104.IVCITM.Trim() equals matNrs
                              select new MaterialCrossReferenceNumberDto
                              {
                                  Ivcitm = f4104.IVCITM.Trim(),
                                  Ivlitm = f4104.IVLITM,
                                  Ivitm = f4104.IVITM,
                                  Ivaitm = f4104.IVAITM
                              }).ToList();
                return result;
            }
        }

        public IList<JdeItemMasterDetailDto> GetJdeItemDetails(List<int> jdeShortItemList)
        {
            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }
            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                var f4101List = session.QueryOver<F4101>().List();

                _dbConnection.Close();

                var result = (from f4101 in f4101List
                              join shortItems in jdeShortItemList
                                  on f4101.IMITM equals shortItems
                              select new JdeItemMasterDetailDto
                              {
                                  Imlitm = f4101.IMLITM,
                                  Imitm = f4101.IMITM,
                                  Imaitm = f4101.IMAITM
                              }).ToList();
                return result;
            }
        }

        public IList<OrrSysF3460Z1> GetOpenPlannedOrderList(string ftedbt, string ftedtn, decimal fteddt)
        {
            var sql = SqlStringQuery.GetOpenWorkOrderSqlString();

            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }

            IList<object[]> resultList;

            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                resultList = session.CreateSQLQuery(sql).List<object[]>();

                _dbConnection.Close();
            }

            var f3460Z1List = _formatPlannedOrderData.CastObjectToOrrSysF3460Z1(ftedbt, ftedtn, fteddt, resultList);
            var groupedF340Z1 = _formatPlannedOrderData.ExecuteGroupByQuery(f3460Z1List);
            var lineNumberedF3460Z1 = _formatPlannedOrderData.InsertLineNumbers(groupedF340Z1);

            return lineNumberedF3460Z1;
        }

        public IList<OutboundTestCertificateDto> GetOutboundTestCerticateData()
        {
            var sql = SqlStringQuery.GetOutboundTestCertificate();

            if (_dbConnection.State != ConnectionState.Closed) { _dbConnection.Close(); }

            IList<object[]> resultList;

            _dbConnection.Open();

            using (var session = SessionFactory.OpenSession(_dbConnection))
            {
                resultList = session.CreateSQLQuery(sql).List<object[]>();

                _dbConnection.Close();
            }

            var returnVal = _formatTestCertificateData.FormatOutboundTestCertificate(resultList);

            return returnVal;
        }
    }
}