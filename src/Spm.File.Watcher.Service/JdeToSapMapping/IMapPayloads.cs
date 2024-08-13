using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared.Payloads;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapPayloads
    {
        GoodsPayload MapGoodsPayload(GoodsDto input);
        PurchaseOrderPayload MapPurchaseOrderPayload(IList<PurchaseOrderSapDto> input);
        MaterialMasterPayload MapMaterialMasterPayload(MaterialMasterSapDto input);
        GeneralLedgerPayload MapGeneralLedgerPayload(IList<GeneralLedgerSapDto> input);
    }
}