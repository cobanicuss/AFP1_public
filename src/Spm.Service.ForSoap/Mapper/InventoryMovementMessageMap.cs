using System;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IInventoryMovementMessageMap : IMarkAsMapper
    {
        InventoryMovementDetails MakeSoapMessage(ProductAchievementSapCommand message);
    }

    public class InventoryMovementMessageMap : IInventoryMovementMessageMap
    {
        public InventoryMovementDetails MakeSoapMessage(ProductAchievementSapCommand message)
        {
            var intventoryMovemenDetails = new InventoryMovementDetails
            {
                InventoryMovementHeader = CreateInventoryMovementHeader(message.Payload, message.SagaReferenceId),
                InventoryMovementLine = CreateInventoryMovementLine(message.Payload, message.LotNumber)
            };

            return intventoryMovemenDetails;
        }

        protected InventoryMovementDetailsInventoryMovementLine[] CreateInventoryMovementLine(
            InventoryMovementPayload payload,
            string lotNumber)
        {
            var returnValue = new[]
            {
                new InventoryMovementDetailsInventoryMovementLine
                {
                    InventoryMovementLineNumber = payload.InventoryMovementLineNumber,
                    CustomerParty = new PartyDetails
                    {
                        PartyIdentification = new PartyIdentificationDetails
                        {
                            PartyIdentifier = new PartyIdentifierType
                            {
                                Value = payload.IdentifierText
                            }
                        }
                    },

                    ItemSpecification = new ItemSpecificationDetails
                    {
                        ItemIdentification = new ItemIdentificationDetails
                        {
                            CustomerArticleNumber = new IdentifierType
                            {
                                Value = payload.CustomerArticleNumber
                            },
                            SupplierArticleNumber = new IdentifierType
                            {
                                Value = payload.SupplierArticleNumber
                            }
                        }
                    },
                    InventoryMovementDetails = new []
                    {
                        new InventoryMovementDetailsInventoryMovementLineInventoryMovementDetails
                        {
                            TransferNumber = payload.QuantityUnitCode,

                            MoveToLocation = new DeliveryLocationDetails
                            {
                                PlaceOfDestination = new LocationDetails
                                {
                                    LocationIdentifier = new IdentifierType
                                    {
                                        Value = payload.LocationIdentifierText
                                    }
                                }
                            },
                            MovementQuantity = new []
                            {
                                new QuantityType
                                {
                                    quantityUnitCode  = (MeasureUnitList)Enum.Parse(typeof(MeasureUnitList), payload.QuantityUnitCode),
                                    quantityUnitCodeListIdentifier = string.Empty,
                                    quantityUnitCodeListAgencyIdentifier = string.Empty,
                                    Value = decimal.Parse(payload.QuantityText)
                                }
                            },
                            PackageLine = new []
                            {
                                new InventoryMovementDetailsInventoryMovementLineInventoryMovementDetailsPackageLine
                                {
                                    PackageInformation = new WarehousePackageDetails
                                    {
                                        WarehouseSerialNumber = lotNumber,
                                        PackageWarehouseLocation = new TextType
                                        {
                                            Value = payload.PackageWarehouseLocationText
                                        },
                                        PackageTypeCode = string.IsNullOrEmpty(payload.PartPack) ? null : new PackageTypeCode{Value = PackageTypeList.Other}
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return returnValue;
        }

        protected InventoryMovementDetailsInventoryMovementHeader CreateInventoryMovementHeader(
            InventoryMovementPayload payload,
            string sagaReferenceId)
        {

            var inventoryMovementHeader = new InventoryMovementDetailsInventoryMovementHeader
            {
                MessageID = sagaReferenceId,
                InventoryMovementNumber = payload.MovementNumber,
                InventoryMovementDateTime = new DateTimeType
                {
                    dateTimeFormatText = payload.MovementDateTimeFormatText,
                    Value = payload.MovementDateTime
                },
                StockControllerParty = new PartyDetails
                {
                    PartyIdentification = new PartyIdentificationDetails
                    {
                        PartyIdentifier = new PartyIdentifierType
                        {
                            Value = ProfileConfigVariables.SndPrn
                        }
                    }
                },
                InventoryMovementHeaderNote = new TextType
                {
                    Value = payload.MovementHeaderNoteText
                },
                InventoryNatureCode = new InventoryNatureCode
                {
                    Value = payload.InventoryNatureCode.ToUpper().Equals("PRIME") ? InventoryNatureList.Prime : InventoryNatureList.NonPrime
                }
            };

            return inventoryMovementHeader;
        }
    }
}