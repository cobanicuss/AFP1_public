using System;
using System.Text;

namespace Spm.Shared
{
    [Serializable]
    public class InventoryMovementPayload
    {
        public string MovementNumber { get; set; }
        public string MovementDateTime { get; set; }
        public string MovementDateTimeFormatText { get; set; }
        public string StockControllerPartyText { get; set; }
        public string MovementHeaderNoteText { get; set; }
        public string InventoryNatureCode { get; set; }
        public string InventoryMovementLineNumber { get; set; }
        public string IdentifierText { get; set; }
        public string CustomerArticleNumber { get; set; }
        public string SupplierArticleNumber { get; set; }
        public string LocationIdentifierText { get; set; }
        public string QuantityUnitCode { get; set; }
        public string QuantityText { get; set; }
        public string WarehouseSerialNumber { get; set; }
        public string PackageWarehouseLocationText { get; set; }
        public string PartPack { get; set; }
    }

    public class InventoryMovementPayloadAsString
    {
        public static string ToString(InventoryMovementPayload inventoryMovementPayload)
        {
            var sb = new StringBuilder();
            sb.Append("MovementNumber={0},");
            sb.Append("MovementDateTime={1},");
            sb.Append("MovementDateTimeFormatText={2},");
            sb.Append("StockControllerPartyText={3},");
            sb.Append("MovementHeaderNoteText={4},");
            sb.Append("InventoryNatureCode={5},");
            sb.Append("InventoryMovementLineNumber={6},");
            sb.Append("IdentifierText={7},");
            sb.Append("CustomerArticleNumber={8},");
            sb.Append("SupplierArticleNumber={9},");
            sb.Append("LocationIdentifierText={10},");
            sb.Append("QuantityUnitCode={11},");
            sb.Append("QuantityText={12},");
            sb.Append("WarehouseSerialNumber={13},");
            sb.Append("PackageWarehouseLocationText={14},");
            sb.Append("PartPack={15}.");

            var str = string.Format(sb.ToString(),
                inventoryMovementPayload.MovementNumber,
                inventoryMovementPayload.MovementDateTime,
                inventoryMovementPayload.MovementDateTimeFormatText,
                inventoryMovementPayload.StockControllerPartyText,
                inventoryMovementPayload.MovementHeaderNoteText,
                inventoryMovementPayload.InventoryNatureCode,
                inventoryMovementPayload.InventoryMovementLineNumber,
                inventoryMovementPayload.IdentifierText,
                inventoryMovementPayload.CustomerArticleNumber,
                inventoryMovementPayload.SupplierArticleNumber,
                inventoryMovementPayload.LocationIdentifierText,
                inventoryMovementPayload.QuantityUnitCode,
                inventoryMovementPayload.QuantityText,
                inventoryMovementPayload.WarehouseSerialNumber,
                inventoryMovementPayload.PackageWarehouseLocationText,
                inventoryMovementPayload.PartPack

                );

            return str;
        }
    }
}