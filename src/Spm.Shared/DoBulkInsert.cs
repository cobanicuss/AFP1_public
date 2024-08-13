using System.Data;
using System.Data.SqlClient;
using NHibernate;
using NHibernate.Engine;

namespace Spm.Shared
{
    public interface IDoBulkInsert
    {
        void BulkInsert(DataTable dt, string destinationTablename);
    }

    public class DoBulkInsert : IDoBulkInsert
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void BulkInsert(DataTable dt, string destinationTablename)
        {
            using (var connection = ((ISessionFactoryImplementor)SessionFactory).ConnectionProvider.GetConnection())
            {
                var s = (SqlConnection)connection;

                var copy = new SqlBulkCopy(s)
                {
                    BulkCopyTimeout = 120,
                    DestinationTableName = destinationTablename
                };

                foreach (DataColumn column in dt.Columns)
                {
                    copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                copy.WriteToServer(dt);
            }
        }
    }
}