using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Spm.Shared
{
    public interface IMockServiceRepository
    {
        string[] GetNumbers(int system);
        void AddNumber(int system, string number);
        void DeleteNumber(int system, string number);

        void AddUpdateSoapData(int system, string number, string soapData);
        void DeleteSoapData(int system, string number);
    }

    public class MockServiceRepository : IMockServiceRepository
    {
        private SqlConnection _connection;
        
        public string[] GetNumbers(int system)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Select [NumberIdentifierValue] ");
                sb.Append("From [SPM.TestHarness].[dbo].[SimulatedNumberIdentier] ");
                sb.Append("Where [SystemIdentifier] = @SystemIdentifier ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);
                var returnList = new List<string>();

                try
                {
                    _connection.Open();

                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));

                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read()) { returnList.Add(reader[0] == DBNull.Value ? null : reader.GetString(0)); }

                    return returnList.ToArray();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        public void AddNumber(int system, string number)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Insert Into [SPM.TestHarness].[dbo].[SimulatedNumberIdentier] ");
                sb.Append("(SystemIdentifier,NumberIdentifierValue) ");
                sb.Append("values ");
                sb.Append("(@SystemIdentifier,@NumberIdentifierValue) ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);

                try
                {
                    _connection.Open();
                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        public void AddUpdateSoapData(int system, string number, string soapData)
        {
            if(DoesSoapDataIdentifierExist(system, number))
                UpdateSoapData(system, number, soapData);
            else
                AddSoapData(system, number, soapData);
        }

        private void AddSoapData(int system, string number, string soapData)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Insert Into [SPM.TestHarness].[dbo].[SimulatedSapSoapMessage] ");
                sb.Append("(SystemIdentifier,NumberIdentifierValue, SoapData) ");
                sb.Append("values ");
                sb.Append("(@SystemIdentifier,@NumberIdentifierValue, @SoapData) ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);

                try
                {
                    _connection.Open();

                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));
                    sqlCommand.Parameters.Add(CreateSoapData(soapData));

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        private void UpdateSoapData(int system, string number, string soapData)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Update [SPM.TestHarness].[dbo].[SimulatedSapSoapMessage] ");
                sb.Append("Set SoapData = @SoapData ");
                sb.Append("Where SystemIdentifier = @SystemIdentifier ");
                sb.Append("And NumberIdentifierValue = @NumberIdentifierValue ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);

                try
                {
                    _connection.Open();

                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));
                    sqlCommand.Parameters.Add(CreateSoapData(soapData));

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        private bool DoesSoapDataIdentifierExist(int system, string number)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Select NumberIdentifierValue ");
                sb.Append("From [SPM.TestHarness].[dbo].[SimulatedSapSoapMessage] ");
                sb.Append("Where SystemIdentifier = @SystemIdentifier ");
                sb.Append("And NumberIdentifierValue = @NumberIdentifierValue ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);
                var returnList = new List<string>();

                try
                {
                    _connection.Open();

                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));

                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read()) { returnList.Add(reader[0] == DBNull.Value ? null : reader.GetString(0)); }

                    return returnList.Any() && returnList.Count == 1;
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        public void DeleteSoapData(int system, string number)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Delete ");
                sb.Append("From [SPM.TestHarness].[dbo].[SimulatedSapSoapMessage] ");
                sb.Append("Where SystemIdentifier = @SystemIdentifier ");
                sb.Append("And NumberIdentifierValue = @NumberIdentifierValue ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);

                try
                {
                    _connection.Open();
                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        public void DeleteNumber(int system, string number)
        {
            _connection = new SqlConnection(ProfileConnectionString.DevelopmentSpmTestHarness);

            using (_connection)
            {
                var sb = new StringBuilder();
                sb.Append("Delete ");
                sb.Append("From [SPM.TestHarness].[dbo].[SimulatedNumberIdentier] ");
                sb.Append("Where SystemIdentifier = @SystemIdentifier ");
                sb.Append("And NumberIdentifierValue = @NumberIdentifierValue ");

                var sqlCommand = new SqlCommand(sb.ToString(), _connection);

                try
                {
                    _connection.Open();
                    sqlCommand.Parameters.Add(CreateSimSystemEnumParamters(system));
                    sqlCommand.Parameters.Add(CreateNumberIdentifierValue(number));

                    sqlCommand.ExecuteNonQuery();
                }
                finally
                {
                    sqlCommand.Dispose();
                }
            }
        }

        private static SqlParameter CreateSimSystemEnumParamters(int system)
        {
            return new SqlParameter
            {
                ParameterName = "@SystemIdentifier",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = system
            };
        }

        private static SqlParameter CreateNumberIdentifierValue(string number)
        {
            return new SqlParameter
            {
                ParameterName = "@NumberIdentifierValue",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Value = number
            };
        }

        private static SqlParameter CreateSoapData(string soapData)
        {
            return new SqlParameter
            {
                ParameterName = "@SoapData",
                SqlDbType = SqlDbType.NVarChar,
                Direction = ParameterDirection.Input,
                Value = soapData,
                Size = -1
            };
        }
    }

    public enum SimSystemEnum
    {
        GeneralLedger = 1,
        GoodsReceipt,
        ProductAchievement,
        ProductionOrderStatus,
        PurchaseOrderChange,
        PuchaseOrderCreate,
        MaterialMaster,
        TestCertificate,
        ResponseToSapRequest
    }
}