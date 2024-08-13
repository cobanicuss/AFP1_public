using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MapJdeToSapForPurchaseOrderChange : IMapJdeToSapForPurchaseOrderChange
    {
        private readonly ICreateMappingByLineItem _mappingByLineItem;
        private readonly IMapPayloads _mapPayloads;

        public MapJdeToSapForPurchaseOrderChange(
            ICreateMappingByLineItem mappingByLineItem,
            IMapPayloads mapPayloads)
        {
            _mappingByLineItem = mappingByLineItem;
            _mapPayloads = mapPayloads;
        }

        public MappingResultPurchaseOrderDto CreateMapping(IList<PurchaseOrderSapDto> input)
        {
            var mappingProblemList = new List<ProblemDto>();
            var mappedList = new List<PurchaseOrderSapDto>();
            var rowNumber = 0;

            foreach (var inItem in input)
            {
                rowNumber++;

                var mappedItem = _mappingByLineItem.ForPurchaseOrderChange(inItem, mappingProblemList, rowNumber);//Down-casting inItem is OK//

                mappedList.Add(mappedItem);
            }

            var returnVal = new MappingResultPurchaseOrderDto
            {
                MappedList = mappedList,
                MappingProblemList = mappingProblemList
            };

            return returnVal;
        }

        public PurchaseOrderChangeCommand MapPayload(IList<PurchaseOrderSapDto> input)
        {
            var returnVal = new PurchaseOrderChangeCommand
            {
                Payload = _mapPayloads.MapPurchaseOrderPayload(input)
            };

            return returnVal;
        }
    }
}