using System;
using System.Collections.Generic;
using System.Linq;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MapJdeToSapForGoods : IMapJdeToSapForAllGoods
    {
        private readonly ICreateMappingByLineItem _mappingByLineItem;
        private readonly IMapPayloads _mapPayloads;
        private readonly ICastDto _castDto;

        public MapJdeToSapForGoods(
            ICreateMappingByLineItem mappingByLineItem,
            IMapPayloads mapPayloads,
            ICastDto castDto)
        {
            _mappingByLineItem = mappingByLineItem;
            _mapPayloads = mapPayloads;
            _castDto = castDto;
        }

        public GoodsMappingResultSplitDto SplitMappingResult(IEnumerable<GoodsDto> dataDtoList, string type)
        {
            var returnVal = new GoodsMappingResultSplitDto();
            var resultAsStringList = new List<string>();
            var mappedDataDtoList = new List<GoodsSagaDto>();
            var rowNumber = 0;

            foreach (var item in dataDtoList)
            {
                rowNumber++;

                var sagaReferenceId = Guid.NewGuid().ToString();
                var resultList = _mappingByLineItem.ForGoods(item, rowNumber, type, sagaReferenceId);

                if (resultList.MappingProblemList.Any())
                {
                    var resultAsString = _castDto.AsString(resultList.MappingProblemList);

                    resultAsStringList.Add(resultAsString);
                }
                else
                {
                    mappedDataDtoList.Add(resultList.Mapped);
                }
            }

            returnVal.MappingResultList = resultAsStringList;
            returnVal.MappedDataDtoList = mappedDataDtoList;

            return returnVal;
        }

        public GoodsCommand MapPayload(GoodsDto input)
        {
            var returnVal = new GoodsCommand
            {
                Payload = _mapPayloads.MapGoodsPayload(input)
            };

            return returnVal;
        }
    }
}