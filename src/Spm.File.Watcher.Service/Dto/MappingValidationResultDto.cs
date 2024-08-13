using System.Collections.Generic;
using Spm.Shared;

namespace Spm.File.Watcher.Service.Dto
{
    public class MappingResultBase : IMarkAsDto
    {
        public List<ProblemDto> MappingProblemList { get; set; }
    }

    public class ProblemDto : IMarkAsDto
    {
        public int RowNumber { get; set; }
        public string Result { get; set; }
    }

    public class MappingResultGeneralLedgerDto : MappingResultBase
    {
        public List<GeneralLedgerSapDto> MappedList { get; set; }
    }

    public class MappingResultPurchaseOrderDto : MappingResultBase
    {
        public List<PurchaseOrderSapDto> MappedList { get; set; }
    }

    public class MappingResultMaterialMasterDto : MappingResultBase
    {
        public KeyValuePair<MaterialMasterSagaDto, MaterialMasterSapDto> Mapped { get; set; }
    }

    public class MappingResultGoodsDto : MappingResultBase
    {
        public GoodsSagaDto Mapped { get; set; } 
    }
}