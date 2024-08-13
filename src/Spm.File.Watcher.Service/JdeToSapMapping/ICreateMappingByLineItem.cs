using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface ICreateMappingByLineItem
    {
        GeneralLedgerSapDto ForGeneralLedger(GeneralLedgerItemDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        MappingResultGoodsDto ForGoods(GoodsDto jde, int rowNumber, string type, string sagaReferenceId);
        MappingResultMaterialMasterDto ForMaterialMaster(MaterialMasterDto jde, int rowNumber, string sagaReferenceId);
        PurchaseOrderSapDto ForPurchaseOrderCreate(PurchaseOrderDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber);
        PurchaseOrderSapDto ForPurchaseOrderChange(PurchaseOrderDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber);
    }
}