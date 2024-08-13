using System.Collections.Generic;
using NServiceBus;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.CreateMessage
{
    public interface ICreateMessage
    {
        MappingValidationPurchaseOrderDto ValidateMappingPurchaseOrderCreate(IList<PurchaseOrderDto> dtoList);
        ICommand MakePurchaseOrderCreateCommand(IList<PurchaseOrderDto> dtoList, string sagaReferenceId);

        MappingValidationPurchaseOrderDto ValidateMappingPurchaseOrderChange(IList<PurchaseOrderDto> dtoList);
        ICommand MakePurchaseOrderChangeCommand(IList<PurchaseOrderDto> dtoList, string sagaReferenceId);

        MappingValidationGoodsDto ValidateMappingGoodsReceipt(GoodsDto dto);
        ICommand MakeGoodsReceiptCommand(GoodsDto dto, string sagaReferenceId);

        MappingValidationGoodsDto ValidateMappingGoodsReversal(GoodsDto dto);
        ICommand MakeGoodsReversalCommand(GoodsDto dto, string sagaReferenceId);

        MappingValidationGeneralLedgerDto ValidateMappingGeneralLedger(IList<GeneralLedgerDto> dtoList);
        ICommand MakeGeneralLedgerCommand(IList<GeneralLedgerDto> dtoList, string sagaReferenceId, string generalLedgerId);

        string MappingValidationResultAsString(IEnumerable<ValidationResult> resultList);
    }
}