using System.Collections.Generic;
using Spm.OrrSys.Service.Domain;
using Spm.Shared;

namespace Spm.OrrSys.Service.Repositories
{
    public interface IFormatData<T> where T : IMarkAsDomain
    {
        IEnumerable<T> CastObjectToOrrSysF3460Z1(string ftedbt, string ftedtn, decimal fteddt, IEnumerable<object[]> resultList);
        IList<T> ExecuteGroupByQuery(IEnumerable<T> f3460Z1List);
        IList<T> InsertLineNumbers(IList<T> groupedF340Z1);
    }

    public interface IFormatPlannedOrderData : IFormatData<OrrSysF3460Z1>
    { }
}