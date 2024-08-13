using System.Linq;
using NHibernate;
using Spm.Service.Domain;
using Spm.Shared;

namespace Spm.Service.Validation
{
    public interface IValidateProductAchievement : IMarkAsHistoryChecker
    {
        bool HasPreviouselyBeenCreated(string lotNumber);
    }

    public class ValidateProductAchievement : IValidateProductAchievement
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public bool HasPreviouselyBeenCreated(string lotNumber)
        {
            var purchaseOrderList = Session.QueryOver<ProductAchievementTransitionHistory>()
              .Where(x => x.LotNumber == lotNumber).List();

            return purchaseOrderList.Any();
        }
    }
}