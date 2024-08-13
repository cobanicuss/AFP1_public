using System;
using System.Collections.Generic;
using System.Text;

namespace Spm.Shared
{
    [Serializable]
    public class ProductionOrderStatusPayload
    {
        public List<ProductionOrderStatusPayloadItem> ProductionOrderStatusPayloadItem { get; set; }
    }

    [Serializable]
    public class ProductionOrderStatusPayloadItem
    {
        public string ProductionOrderNumber { get; set; }
        public string ReleaseFlag { get; set; }
        public string CompleteFlag { get; set; }
        public double? OrderQuantity { get; set; }
        public string OrderQuantityUom { get; set; }
        public DateTime? FinishDate { get; set; }
    }

    public class ProductionOrderPayloadAsString
    {
        public static string ToString(ProductionOrderStatusPayloadItem productionOrderStatusPayload)
        {
            var sb = new StringBuilder();
            sb.Append("ProductionOrderNumber={0},");
            sb.Append("ReleaseFlag={1},");
            sb.Append("CompleteFlag={2},");
            sb.Append("OrderQuantity={3},");
            sb.Append("FinishDate={4},");

            var str = string.Format(sb.ToString(),
                productionOrderStatusPayload.ProductionOrderNumber,
                productionOrderStatusPayload.ReleaseFlag,
                productionOrderStatusPayload.CompleteFlag,
                productionOrderStatusPayload.OrderQuantity,
                productionOrderStatusPayload.FinishDate
                );

            return str;
        }
    }
}