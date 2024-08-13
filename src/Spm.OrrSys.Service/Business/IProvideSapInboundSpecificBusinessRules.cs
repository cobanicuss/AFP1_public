using System.Collections.Generic;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.Business
{
    public interface IProvideSapInboundSpecificBusinessRules
    {
        PlannedOrderDto CreatePlannedOrderDto();
        ProductionOrderDto CreateProductionOrderDto();

        List<OrrSysF3460Z1> CreateJdePlannedOrders(
            IList<DemandSapJde> inboundPlannedOrderList, 
            IList<JdeItemMasterDetailDto> jdeItemMasterDetailList,
            int startCount,
            PlannedOrderDto plannedOrderDto,
            IList<OrrSysF3460Z1> openPlannedOrders);

        List<OrrSysF554801Z> CreateJdeProductionOrders(IEnumerable<DemandSapJdeWo> inboundProductionOrderList, ProductionOrderDto productionOrderDto);

        List<int> GetDestinctJdeShortItemNumber(IEnumerable<DemandSapJde> inboundPlannedOrderList);
    }
}