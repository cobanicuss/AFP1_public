using Spm.Shared;

namespace Spm.OrrSys.Service.Business
{
    public interface IDoProductAchievementBusiness : IMarkAsBusinessRule
    {
        void SetInProgress(string lotNumber);
    }
}