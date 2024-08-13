using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapJdeToSapForMaterialMaster : IMapJdeToSapBaseSingle<MaterialMasterSapDto, MaterialMasterCommand>
    {
        MaterialMasterMappingResultSplitDto SplitMappingResult(IEnumerable<MaterialMasterDto> dataDtoList);
    }
}