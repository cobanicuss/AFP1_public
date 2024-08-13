using System.Collections.Generic;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Repository;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public class MappingGoodsReversal : IMapJdeToSapForGoodsReversal
    {
        private readonly IDoMappingBusinessRules _mappingBusinessRules;
        private readonly IMapPayloads _mapPayloads;

        private readonly List<CacheMapUnitOfMeasure> _unitOfMeasureMapping;
        private readonly List<CacheMapPlant> _plantMapping;
        private readonly List<CacheMapLocation> _locationMapping;

        public MappingGoodsReversal(ICacheMapRepository cacheMapRepository, IDoMappingBusinessRules mappingBusinessRules, IMapPayloads mapPayloads)
        {
            _mapPayloads = mapPayloads;
            _mappingBusinessRules = mappingBusinessRules;
            _unitOfMeasureMapping = cacheMapRepository.GetUnitOfMeasureMapping();
            _plantMapping = cacheMapRepository.GetPlantMapping();
            _locationMapping = cacheMapRepository.GetLocationMapping();
        }
        
        public MappingValidationGoodsDto ValidateMapping(GoodsDto input)
        {
            var validationList = new List<ValidationResult>();
            const int rowNumber = 1;
            var outItem = new GoodsDto();

            CreateMappingByItem(input, outItem, validationList, rowNumber);

            var returnVal = new MappingValidationGoodsDto
            {
                Mapped = outItem,
                ValidationList = validationList
            };

            return returnVal;
        }

        public GoodsCommand MapPayload(GoodsDto input)
        {
            var output = _mapPayloads.MapGoodsPayload(input);

            return output;
        }

        private void CreateMappingByItem(GoodsDto inItem, GoodsDto outItem, ICollection<ValidationResult> validationList, int rowNumber)
        {
            var postingDateResult = _mappingBusinessRules.MapPostingDate(inItem.PstngDate);
            if (postingDateResult.IsMappingOk) outItem.PstngDate = postingDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = postingDateResult.Output });

            var docDateResult = _mappingBusinessRules.MapGoodsDocDate(inItem.DocDate);
            if (docDateResult.IsMappingOk) outItem.DocDate = docDateResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = docDateResult.Output });

            outItem.RefDocNo = inItem.RefDocNo;

            var headerTextResult = _mappingBusinessRules.MapHeaderText(inItem.Id);
            if (headerTextResult.IsMappingOk) outItem.HeaderTxt = headerTextResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = headerTextResult.Output });

            var gmCodeResult = _mappingBusinessRules.MapGmCode(inItem.GmCode);
            if (gmCodeResult.IsMappingOk) outItem.GmCode = gmCodeResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = gmCodeResult.Output });

            var plantResult = _mappingBusinessRules.MapPlant(_plantMapping, inItem.Plant);
            if (plantResult.IsMappingOk) outItem.Plant = plantResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = plantResult.Output });

            var locationResult = _mappingBusinessRules.MapLocation(_locationMapping, inItem.StgeLoc);
            if (locationResult.IsMappingOk) outItem.StgeLoc = locationResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = locationResult.Output });

            outItem.MoveType = MapDefaults.GoodsReversalType;

            outItem.EntryQnt = inItem.EntryQnt;

            var poUnitResult = _mappingBusinessRules.MapPoUnit(_unitOfMeasureMapping, inItem.EntryUom);
            if (poUnitResult.IsMappingOk) outItem.EntryUom = poUnitResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = poUnitResult.Output });

            var poNumberResult = _mappingBusinessRules.MapPoNumber(inItem.PoNumber);
            if (poNumberResult.IsMappingOk) outItem.PoNumber = poNumberResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = poNumberResult.Output });

            var poItemResult = _mappingBusinessRules.MapPoItem(inItem.PoItem);
            if (poItemResult.IsMappingOk) outItem.PoItem = poItemResult.Output;
            else validationList.Add(new ValidationResult { RowNumber = rowNumber, Result = poItemResult.Output });

            outItem.MvtInd = inItem.MvtInd;

            outItem.NoMoreGr = string.Empty;
        }
    }
}