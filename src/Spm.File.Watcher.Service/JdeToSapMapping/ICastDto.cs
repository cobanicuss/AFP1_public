using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface ICastDto
    {
        string AsString(IEnumerable<ProblemDto> resultList);

        List<PurchaseOrderSapDto> AsPurchaseOrderSapDto(IEnumerable<PurchaseOrderDto> dataDtoList);
        List<GeneralLedgerSapDto> AsGeneralLedgerSapDto(IEnumerable<GeneralLedgerItemDto> dtoGroupForMessage);
        List<GeneralLedgerItemDto> AsGeneralLedgerItemDto(IEnumerable<GeneralLedgerDto> dataDtoListSort);

        IList<PurchaseOrderCreateFileData> AsPurchaseOrderCreateFileData(IList<PurchaseOrderDto> dto, string fileName, string sagaReferenceId);
        IList<PurchaseOrderChangeFileData> AsPurchaseOrderChangeFileData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId);
        IList<GoodsBaseFileData> AsGoodsFileData(IEnumerable<GoodsSagaDto> dtoList, string fileName, string type);
        IList<GeneralLedgerFileData> AsGeneralLederFileData(IList<GeneralLedgerItemDto> dtoList, string fileName, string sagaReferenceId);
        IList<MaterialMasterFileData> AsMaterialMasterFileData(IList<MaterialMasterSagaDto> dto, string fileName);

        MaterialMasterSagaDto AsMaterialMasterSagaDto(MaterialMasterDto dto);
    }
}