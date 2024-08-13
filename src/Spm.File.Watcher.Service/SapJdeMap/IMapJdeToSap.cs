using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public interface IMapJdeToSapForGeneralLedgerUsing<T, out TK> where T : IMarkAsDto
    {
        TK MapPayload(IList<T> input);
        MappingValidationGeneralLedgerDto ValidateMapping(IList<T> input);
    }
    public interface IMapJdeToSapForGeneralLedger : IMapJdeToSapForGeneralLedgerUsing<GeneralLedgerDto, GeneralLedgerCommand> { }
    

    public interface IMapJdeToSapForPurchaseOrder<T, out TK> where T : IMarkAsDto
    {
        TK MapPayload(IList<T> input);
        MappingValidationPurchaseOrderDto ValidateMapping(IList<T> input);
    }
    public interface IMapJdeToSapForPurchaseOrderCreate : IMapJdeToSapForPurchaseOrder<PurchaseOrderDto, PurchaseOrderCreateCommand> { }
    public interface IMapJdeToSapForPurchaseOrderChange : IMapJdeToSapForPurchaseOrder<PurchaseOrderDto, PurchaseOrderChangeCommand> { }
    

    public interface IMapJdeToSapForGoods<in T, out TK> where T : IMarkAsDto
    {
        TK MapPayload(T input);
        MappingValidationGoodsDto ValidateMapping(T input);
    }
    public interface IMapJdeToSapForGoodsReceipt : IMapJdeToSapForGoods<GoodsDto, GoodsCommand> { }
    public interface IMapJdeToSapForGoodsReversal : IMapJdeToSapForGoods<GoodsDto, GoodsCommand> { }
}