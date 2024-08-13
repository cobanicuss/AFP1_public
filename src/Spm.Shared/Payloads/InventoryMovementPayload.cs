using System;

namespace Spm.Shared.Payloads
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
            var str = $@"MovementNumber = {inventoryMovementPayload.MovementNumber},
            MovementDateTime = {inventoryMovementPayload.MovementDateTime},
            MovementDateTimeFormatText = {inventoryMovementPayload.MovementDateTimeFormatText},
            StockControllerPartyText = {inventoryMovementPayload.StockControllerPartyText},
            MovementHeaderNoteText = {inventoryMovementPayload.MovementHeaderNoteText},
            InventoryNatureCode = {inventoryMovementPayload.InventoryNatureCode},
            InventoryMovementLineNumber = {inventoryMovementPayload.InventoryMovementLineNumber},
            IdentifierText = {inventoryMovementPayload.IdentifierText},
            CustomerArticleNumber = {inventoryMovementPayload.CustomerArticleNumber},
            SupplierArticleNumber = {inventoryMovementPayload.SupplierArticleNumber},
            LocationIdentifierText = {inventoryMovementPayload.LocationIdentifierText},
            QuantityUnitCode = {inventoryMovementPayload.QuantityUnitCode},
            QuantityText = {inventoryMovementPayload.QuantityText},
            WarehouseSerialNumber = {inventoryMovementPayload.WarehouseSerialNumber},
            PackageWarehouseLocationText = {inventoryMovementPayload.PackageWarehouseLocationText},
            PartPack = {inventoryMovementPayload.PartPack}";

            return str;
        }
    }
}