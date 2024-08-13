using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class ProductionOrderTriggerRequest : IMessage
    {
        public string InboundId { get; set; }
        public string ProductionOrderNumber { get; set; }
        public string OrderType { get; set; }
        public string MaterialNumber { get; set; }
        public string SupplyPlant { get; set; }
        public string FinishDate { get; set; }
        public string ProductionQty { get; set; }
        public string ProductionUnitOfMeasure { get; set; }

        public override string ToString()
        {
            var str = $@"ProductionOrderNumber={ProductionOrderNumber}
            OrderType={OrderType}
            MaterialNumber={MaterialNumber}
            SupplyPlant={SupplyPlant}
            FinishDate={FinishDate}
            ProductionQty={ProductionQty}
            ProductionUnitOfMeasure={ProductionUnitOfMeasure}
            InboundId={InboundId}";

            return str;
        }
    }
}