using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MapJdeToSapPurchaseOrderCreate : IMapJdeToSapPurchaseOrderCreate
    {
        private readonly ICreateMappingByLineItem _mappingByLineItem;
        private readonly IMapPayloads _mapPayloads;

        public MapJdeToSapPurchaseOrderCreate(
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

                var mappedItem = _mappingByLineItem.ForPurchaseOrderCreate(inItem, mappingProblemList, rowNumber);

                mappedList.Add(mappedItem);
            }

            var returnVal = new MappingResultPurchaseOrderDto
            {
                MappedList = mappedList,
                MappingProblemList = mappingProblemList
            };

            return returnVal;
        }

        public PurchaseOrderCreateCommand MapPayload(IList<PurchaseOrderSapDto> input)
        {
            var returnVal = new PurchaseOrderCreateCommand
            {
                Payload = _mapPayloads.MapPurchaseOrderPayload(input)
            };

            return returnVal;
        }
    }
}