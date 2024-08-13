using Spm.Shared;

namespace Spm.OrrSys.Service.Business
{
    public interface IDoProductionOrderBusiness : IMarkAsBusinessRule
    {
        void CreateProductionOrders();
    }
}