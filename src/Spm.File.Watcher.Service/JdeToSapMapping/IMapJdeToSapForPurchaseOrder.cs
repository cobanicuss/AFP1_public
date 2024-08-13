using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapJdeToSapForPurchaseOrder<T> where T : IMarkAsDto
    {
        MappingResultPurchaseOrderDto CreateMapping(IList<T> input);
    }

    public interface IMapJdeToSapPurchaseOrderCreate :
        IMapJdeToSapBase<PurchaseOrderSapDto, PurchaseOrderCreateCommand>,
        IMapJdeToSapForPurchaseOrder<PurchaseOrderSapDto>
    { }

    public interface IMapJdeToSapForPurchaseOrderChange :
        IMapJdeToSapBase<PurchaseOrderSapDto, PurchaseOrderChangeCommand>,
        IMapJdeToSapForPurchaseOrder<PurchaseOrderSapDto>
    { }
}