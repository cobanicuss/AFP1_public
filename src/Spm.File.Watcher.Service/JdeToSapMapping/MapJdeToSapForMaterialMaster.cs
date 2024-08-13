using System;
using System.Collections.Generic;
using System.Linq;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MapJdeToSapForMaterialMaster : IMapJdeToSapForMaterialMaster
    {
        private readonly ICreateMappingByLineItem _mappingByLineItem;
        private readonly IMapPayloads _mapPayloads;
        private readonly ICastDto _castDto;

        public MapJdeToSapForMaterialMaster(
            ICreateMappingByLineItem mappingByLineItem,
            IMapPayloads mapPayloads,
            ICastDto castDto)
        {
            _mappingByLineItem = mappingByLineItem;
            _mapPayloads = mapPayloads;
            _castDto = castDto;
        }

        public MaterialMasterMappingResultSplitDto SplitMappingResult(IEnumerable<MaterialMasterDto> dataDtoList)
        {
            var returnVal = new MaterialMasterMappingResultSplitDto();
            var mappingDictionary = new Dictionary<MaterialMasterSagaDto, MaterialMasterSapDto>();
            var resultAsStringList = new List<string>();
            var rowNumber = 0;

            foreach (var item in dataDtoList)
            {
                rowNumber++;

                var sagaReferenceId = Guid.NewGuid().ToString();
                var resultList = _mappingByLineItem.ForMaterialMaster(item, rowNumber, sagaReferenceId);

                if (resultList.MappingProblemList.Any())
                {
                    var resultAsString = _castDto.AsString(resultList.MappingProblemList);

                    resultAsStringList.Add(resultAsString);
                }
                else
                {
                    mappingDictionary.Add(resultList.Mapped.Key, resultList.Mapped.Value);
                }
            }

            returnVal.MappingResultList = resultAsStringList;
            returnVal.MappedDataDtoList = mappingDictionary;

            return returnVal;
        }

        public MaterialMasterCommand MapPayload(MaterialMasterSapDto input)
        {
            var returnVal = new MaterialMasterCommand
            {
                Payload = _mapPayloads.MapMaterialMasterPayload(input)
            };

            return returnVal;
        }
    }
}