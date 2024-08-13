using System.Data;
using NHibernate;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IProductAchievementRepository
    {
        void SetF4801Z1InProgressTrue(string lotNumber);
    }

    public class ProductAchievementRepository : IProductAchievementRepository
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void SetF4801Z1InProgressTrue(string lotNumber)
        {
            var sqlUpdate = SqlStringQuery.UpdateF4801Z1ProcessedInSap(lotNumber, 'I');

            if (Session.Connection.State != ConnectionState.Closed) Session.Connection.Close();
            Session.Connection.Open();

            Session.CreateSQLQuery(sqlUpdate).ExecuteUpdate();
        }
    }
}