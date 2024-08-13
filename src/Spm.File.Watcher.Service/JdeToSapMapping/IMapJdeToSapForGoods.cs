using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IMapJdeToSapForAllGoods : IMapJdeToSapBaseSingle<GoodsDto, GoodsCommand>
    {
        GoodsMappingResultSplitDto SplitMappingResult(IEnumerable<GoodsDto> dataDtoList, string type);
    }
}