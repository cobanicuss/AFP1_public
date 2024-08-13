using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MapJdeToSapForGeneralLedger : IMapJdeToSapForGeneralLedger
    {
        private readonly ICreateMappingByLineItem _mappingByLineItem;
        private readonly IMapPayloads _mapPayloads;

        public MapJdeToSapForGeneralLedger(
            ICreateMappingByLineItem mappingByLineItem,
            IMapPayloads mapPayloads)
        {
            _mappingByLineItem = mappingByLineItem;
            _mapPayloads = mapPayloads;
        }

        public MappingResultGeneralLedgerDto CreateMapping(IList<GeneralLedgerSapDto> input)
        {
            var mappingProblemList = new List<ProblemDto>();
            var mappedList = new List<GeneralLedgerSapDto>();
            var rowNumber = 0;

            foreach (var inItem in input)
            {
                rowNumber++;

                var outItem = _mappingByLineItem.ForGeneralLedger(inItem, mappingProblemList, rowNumber);

                mappedList.Add(outItem);
            }

            var returnVal = new MappingResultGeneralLedgerDto
            {
                MappedList = mappedList,
                MappingProblemList = mappingProblemList
            };

            return returnVal;
        }

        public GeneralLedgerCommand MapPayload(IList<GeneralLedgerSapDto> input)
        {
            var returnVal = new GeneralLedgerCommand
            {
                Payload = _mapPayloads.MapGeneralLedgerPayload(input)
            };

            return returnVal;
        }
    }
}