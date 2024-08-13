using System;
using System.Collections.Generic;

namespace Spm.Shared.Payloads
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
        public static string ToString(ProductionOrderStatusPayloadItem item)
        {
            var str = $@"item.ProductionOrderNumber={item.ProductionOrderNumber},
            item.ReleaseFlag={item.ReleaseFlag},
            item.CompleteFlag={item.CompleteFlag},
            item.OrderQuantity={item.OrderQuantity},
            item.FinishDate={item.FinishDate}";

            return str;
        }
    }
}