using System.Collections.Generic;

namespace Spm.File.Watcher.Service.Dto
{
    public class MappingResultDto
    {
        public List<string> MappingResultList { get; set; }
    }

    public class GoodsMappingResultSplitDto : MappingResultDto
    {
        public IList<GoodsSagaDto> MappedDataDtoList { get; set; }
    }

    public class MaterialMasterMappingResultSplitDto : MappingResultDto
    {
        public Dictionary<MaterialMasterSagaDto, MaterialMasterSapDto> MappedDataDtoList { get; set; }
    }
}