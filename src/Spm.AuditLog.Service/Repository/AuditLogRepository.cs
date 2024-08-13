using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using NHibernate;
using NServiceBus.Logging;
using Spm.AuditLog.Service.Config;
using Spm.AuditLog.Service.Domain;
using Spm.AuditLog.Service.Dto;
using Spm.Shared;

namespace Spm.AuditLog.Service.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AuditLogRepository));

        public ISessionFactory SessionFactory { get; set; }
        public ISession Session { get { return SessionFactory.GetCurrentSession(); } }

        private const string SprocName = "[SPM.Auditlog].[dbo].[ExportAuditLogToFile]";
        private const string TruncateAuditlogTable = "Truncate Table [SPM.Auditlog].[dbo].";

        public void ExportAuditLogToFile(ExportSprocDto dto)
        {
            using (var connection = new SqlConnection(ProfileConfigVariables.SpmAuditLogDatabase))
            {
                using (var sqlCommand = new SqlCommand(SprocName, connection))
                {
                    try
                    {
                        sqlCommand.CommandTimeout = 0;
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        sqlCommand.Parameters.Add("@OutputPath", SqlDbType.VarChar).Value = dto.OutputPath;
                        sqlCommand.Parameters.Add("@BcpFormatFileName", SqlDbType.VarChar).Value = dto.BcpFormatFileName;
                        sqlCommand.Parameters.Add("@BcpDataFileName", SqlDbType.VarChar).Value = dto.BcpDataFileName;
                        sqlCommand.Parameters.Add("@ErrorFileName", SqlDbType.VarChar).Value = dto.ErrorFileName;
                        sqlCommand.Parameters.Add("@LogFileName", SqlDbType.VarChar).Value = dto.LogFileName;
                        sqlCommand.Parameters.Add("@TableName", SqlDbType.VarChar).Value = dto.TableName;

                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Error in 'public void ExportAuditLogToFile()':{ex.Message}");
                        throw;
                    }
                    finally
                    {
                        sqlCommand.Dispose();
                    }
                }
            }
        }
        
        public int GetCountOfThisAuditLogTable(string auditLogTable)
        {
            var sql = SqlQueryForAuditLogCount(auditLogTable);

            var returnVal = GetCountOfThisTable(sql);

            return returnVal;
        }

        public void SaveThisAuditLog(IMarkAsDomain domain)
        {
            Session.Save(domain);
        }

        public void TruncateThisAuditLogTable(string auditLogTable)
        {
            var sql = $"{TruncateAuditlogTable}[{auditLogTable}]";

            Session.CreateSQLQuery(sql).UniqueResult();
        }

        public void CreateAllNewAuditActionTypes()
        {
            TruncateThisAuditLogTable(Constants.AuditActionTypeTableName);

            foreach (AuditAction auditActionItem in Enum.GetValues(typeof(AuditAction)))
            {
                var auditActionType = new AuditActionType
                {
                    Id = Guid.NewGuid().ToString(),
                    ActionId = (int) auditActionItem,
                    ActionName = auditActionItem.ToString()
                };

                Session.Save(auditActionType);
            }
        }

        private int GetCountOfThisTable(string sql)
        {
            var count = Session.CreateSQLQuery(sql).UniqueResult();

            int returnVal;
            var isParseable = int.TryParse(count.ToString(), out returnVal);

            if (isParseable) return returnVal;

            throw new ArgumentException($"Cannot parse '{count}' into int. Cannot proceed!!");
        }

        private static string SqlQueryForAuditLogCount(string tableName)
        {
            var sqlText = new StringBuilder();

            sqlText.AppendLine("Select (SPS.Rows) As [RowCount]");
            sqlText.AppendLine("From sys.objects As SOS");
            sqlText.AppendLine("Inner Join sys.partitions As SPS");
            sqlText.AppendLine("On SOS.object_id = SPS.object_id");
            sqlText.AppendLine("Where");
            sqlText.AppendLine("SOS.type = 'U' And");
            sqlText.AppendLine("SPS.index_id < 2 And");
            sqlText.AppendLine($"SOS.name = '{tableName}'");
            
            return sqlText.ToString();
        }
    }
}