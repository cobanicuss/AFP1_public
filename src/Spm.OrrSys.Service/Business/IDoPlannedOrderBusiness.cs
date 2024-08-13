using Spm.Shared;

namespace Spm.OrrSys.Service.Business
{
    public interface IDoPlannedOrderBusiness : IMarkAsBusinessRule
    {
        void CreatePlannedOrders();
    }
}