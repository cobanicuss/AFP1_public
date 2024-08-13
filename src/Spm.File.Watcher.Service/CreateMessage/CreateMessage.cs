using System.Collections.Generic;
using System.Text;
using NServiceBus;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.SapJdeMap;
using Spm.Shared;

namespace Spm.File.Watcher.Service.CreateMessage
{
    public class CreateMessage : ICreateMessage
    {
        private readonly IMapJdeToSapForPurchaseOrderCreate _mapJdeToSapForCreate;
        private readonly IMapJdeToSapForPurchaseOrderChange _mapJdeToSapForChange;
        private readonly IMapJdeToSapForGeneralLedger _mapJdeToSapForGeneralLedger;
        private readonly IMapJdeToSapForGoodsReversal _mapJdeToSapForGoodsReversal;
        private readonly IMapJdeToSapForGoodsReceipt _mapJdeToSapForGoodsReceipt;

        public CreateMessage(IMapJdeToSapForPurchaseOrderCreate mapJdeToSapForCreate,
                            IMapJdeToSapForPurchaseOrderChange mapJdeToSapForChange,
                            IMapJdeToSapForGoodsReceipt mapJdeToSapForGoodsReceipt,
                            IMapJdeToSapForGoodsReversal mapJdeToSapForGoodsReversal,
                            IMapJdeToSapForGeneralLedger mapJdeToSapForGeneralLedger)
        {
            _mapJdeToSapForCreate = mapJdeToSapForCreate;
            _mapJdeToSapForChange = mapJdeToSapForChange;
            _mapJdeToSapForGeneralLedger = mapJdeToSapForGeneralLedger;
            _mapJdeToSapForGoodsReversal = mapJdeToSapForGoodsReversal;
            _mapJdeToSapForGoodsReceipt = mapJdeToSapForGoodsReceipt;
        }

        public MappingValidationPurchaseOrderDto ValidateMappingPurchaseOrderCreate(IList<PurchaseOrderDto> dtoList)
        {
            var returnVal = _mapJdeToSapForCreate.ValidateMapping(dtoList);
            return returnVal;
        }

        public ICommand MakePurchaseOrderCreateCommand(IList<PurchaseOrderDto> dtoList, string sagaReferenceId)
        {
            var message = _mapJdeToSapForCreate.MapPayload(dtoList);
            message.PurchaseOrderNumber = dtoList[0].PoNumber.TrimStart('0'); //null-check done previousely//
            message.SagaReferenceId = sagaReferenceId;
            return message;
        }

        public MappingValidationPurchaseOrderDto ValidateMappingPurchaseOrderChange(IList<PurchaseOrderDto> dtoList)
        {
            var returnVal = _mapJdeToSapForChange.ValidateMapping(dtoList);
            return returnVal;
        }

        public ICommand MakePurchaseOrderChangeCommand(IList<PurchaseOrderDto> dtoList, string sagaReferenceId)
        {
            var message = _mapJdeToSapForChange.MapPayload(dtoList);
            message.PurchaseOrderNumber = dtoList[0].PoNumber.TrimStart('0'); //null-check done previousely//
            message.SagaReferenceId = sagaReferenceId;
            return message;
        }

        public MappingValidationGoodsDto ValidateMappingGoodsReceipt(GoodsDto dto)
        {
            var returnVal = _mapJdeToSapForGoodsReceipt.ValidateMapping(dto);
            return returnVal;
        }

        public ICommand MakeGoodsReceiptCommand(GoodsDto dto, string sagaReferenceId)
        {
            var message = _mapJdeToSapForGoodsReceipt.MapPayload(dto);
            message.GoodsReceiptId = dto.PoNumber.TrimStart('0'); 
            message.SagaReferenceId = sagaReferenceId;
            message.Type = Constants.GoodsReceipt;
            return message;
        }

        public MappingValidationGoodsDto ValidateMappingGoodsReversal(GoodsDto dto)
        {
            var returnVal = _mapJdeToSapForGoodsReversal.ValidateMapping(dto);
            return returnVal;
        }

        public ICommand MakeGoodsReversalCommand(GoodsDto dto, string sagaReferenceId)
        {
            var message = _mapJdeToSapForGoodsReversal.MapPayload(dto);
            message.GoodsReceiptId = dto.PoNumber.TrimStart('0'); 
            message.SagaReferenceId = sagaReferenceId;
            message.Type = Constants.GoodsReversal;
            return message;
        }

        public MappingValidationGeneralLedgerDto ValidateMappingGeneralLedger(IList<GeneralLedgerDto> dtoList)
        {
            var returnVal = _mapJdeToSapForGeneralLedger.ValidateMapping(dtoList);
            return returnVal;
        }

        public ICommand MakeGeneralLedgerCommand(IList<GeneralLedgerDto> dtoList, string sagaReferenceId, string generalLedgerId)
        {
            var message = _mapJdeToSapForGeneralLedger.MapPayload(dtoList);
            message.GeneralLedgerId = generalLedgerId;
            message.SagaReferenceId = sagaReferenceId;
            return message;
        }

        public string MappingValidationResultAsString(IEnumerable<ValidationResult> resultList)
        {
            var sb = new StringBuilder();
            foreach (var result in resultList)
            {
                sb.AppendLine(string.Format("Error in line {0}.", result.RowNumber));
                sb.AppendLine(string.Format("Error: {0}.", result.Result));
            }
            var resultAsString = sb.ToString();
            return resultAsString;
        }
    }
}