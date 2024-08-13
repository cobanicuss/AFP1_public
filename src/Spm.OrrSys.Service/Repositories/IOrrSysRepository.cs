using System.Collections.Generic;
using Spm.OrrSys.Service.Domain;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IOrrSysBaseRepository<in T> where T : IMarkAsDomain
    {
        void DeleteAllByTable();

        void BulkInsertByTypeList(IEnumerable<T> workOrderList);
    }

    public interface IPlannedOrderRepository : IOrrSysBaseRepository<OrrSysF3460Z1>
    {
        void CreateHistoryTraking();
    }

    public interface IProductionOrderRepository : IOrrSysBaseRepository<OrrSysF554801Z>
    {
        void InsertNewHistory();

        void DeleteOldHistory();
    }
}