using Spm.OrrSys.Service.Repositories;

namespace Spm.OrrSys.Service.Business
{
    public class ProductAchievementBusiness : IDoProductAchievementBusiness
    {
        private readonly IProductAchievementRepository _productAchievementRepository;

        public ProductAchievementBusiness(IProductAchievementRepository productAchievementRepository)
        {
            _productAchievementRepository = productAchievementRepository;
        }

        public void SetInProgress(string lotNumber)
        {
            _productAchievementRepository.SetF4801Z1InProgressTrue(lotNumber);
        }
    }
}