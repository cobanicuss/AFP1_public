using System;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.CreateMessage
{
    public interface ICreateMessage : IMarkAsMessageCreator
    {
        ProductAchievementSapCommand ProductAchievementSapCommand(ProductAchievementCommand message);
        ProductionOrderStatusSapCommand ProductionOrderStatusSapCommand(ProductionOrderStatusCommand message);
        PurchaseOrderCreateSapCommand PurchaseOrderCreateSapCommand(PurchaseOrderCreateCommand message);
        PurchaseOrderChangeSapCommand PurchaseOrderChangeSapCommand(PurchaseOrderChangeCommand message);
        GoodsReceiptSapCommand GoodsReceiptSapCommand(GoodsCommand message);
        GeneralLedgerSapCommand GeneralLedgerSapCommand(GeneralLedgerCommand message);
        MaterialMasterSapCommand MaterialMasterSapCommand(MaterialMasterCommand message);
        TestCertificateSapCommand TestCertificateSapCommand(TestCertificateCommand message);

        ProductAchievementAuditCommand ProductAchievementAuditCommand(int transition, string messageType, string endpointName, string messageData, string lotNumber, string sagaReferenceId, float leg);
        ProductionOrderStatusAuditCommand ProductionOrderAuditCommand(int transition, string messageType, string endpointName, string messageData, string productionOrderId, string sagaReferenceId, float leg);
        GoodsReceiptAuditCommand GoodsReceiptAuditCommand(int transition, string messageType, string endpointName, string messageData, string goodsReceiptId, string sagaReferenceId, float leg, string type);
        MaterialMasterAuditCommand MaterialMasterAuditCommand(int transition, string messageType, string endpointName, string messageData, string shortItemNumber, string sagaReferenceId, float leg);
        GeneralLedgerAuditCommand GeneralLedgerAuditCommand(int transition, string messageType, string endpointName, string messageData, string generalLedgerId, string sagaReferenceId, float leg);
        PurchaseOrderAuditCommand PurchaseOrderAuditCommand(int transition, string messageType, string endpointName, string messageData, string purchaseOrderNumber, string sagaReferenceId, float leg, string type);
        TestCertificateAuditCommand TestCertificateAuditCommand(int transition, string messageType, string endpointName, string messageData, string certificateId, string sagaReferenceId, string inboundId, int index, int count, float leg);
    }

    public class CreateMessage : ICreateMessage
    {
        public TestCertificateSapCommand TestCertificateSapCommand(TestCertificateCommand message)
        {
            var testCertificateSapCommand = new TestCertificateSapCommand
            {
                Inboundid = message.InboundId,
                SagaReferenceId = message.SagaReferenceId,
                MessageIndex = message.MessageIndex,
                MessageCount = message.MessageCount,
                Payload = message.Payload
            };

            return testCertificateSapCommand;
        }

        public ProductAchievementAuditCommand ProductAchievementAuditCommand(int transition, string messageType, string endpointName, string messageData, string lotNumber, string sagaReferenceId, float leg)
        {
            var productAchievementAuditCommand = new ProductAchievementAuditCommand
            {
                MessageType = messageType,
                LotNumber = lotNumber,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg
            };
            return productAchievementAuditCommand;
        }

        public ProductionOrderStatusAuditCommand ProductionOrderAuditCommand(int transition, string messageType, string endpointName, string messageData, string productionOrderId, string sagaReferenceId, float leg)
        {
            var productionOrderStatusAuditCommand = new ProductionOrderStatusAuditCommand
            {
                ProductionOrderId = productionOrderId,
                SagaReferenceId = sagaReferenceId,
                MessageType = messageType,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg
            };
            return productionOrderStatusAuditCommand;
        }

        public GoodsReceiptAuditCommand GoodsReceiptAuditCommand(int transition, string messageType, string endpointName, string messageData, string goodsReceiptId, string sagaReferenceId, float leg, string type)
        {
            var productionOrderStatusAuditCommand = new GoodsReceiptAuditCommand
            {
                MessageType = messageType,
                SagaReferenceId = sagaReferenceId,
                GoodsReceiptId = goodsReceiptId,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg,
                Type = type
            };
            return productionOrderStatusAuditCommand;
        }

        public MaterialMasterAuditCommand MaterialMasterAuditCommand(int transition, string messageType, string endpointName, string messageData, string shortItemNumber, string sagaReferenceId, float leg)
        {
            var materialMasterAuditCommand = new MaterialMasterAuditCommand
            {
                MessageType = messageType,
                ReferenceId = shortItemNumber,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg
            };
            return materialMasterAuditCommand;
        }

        public GeneralLedgerAuditCommand GeneralLedgerAuditCommand(int transition, string messageType, string endpointName, string messageData, string generalLedgerId, string sagaReferenceId, float leg)
        {
            var generalLedgerAuditCommand = new GeneralLedgerAuditCommand
            {
                MessageType = messageType,
                GeneralLedgerId = generalLedgerId,
                SagaReferenceId = sagaReferenceId,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg
            };
            return generalLedgerAuditCommand;
        }

        public PurchaseOrderAuditCommand PurchaseOrderAuditCommand(int transition, string messageType, string endpointName, string messageData, string purchaseOrderNumber, string sagaReferenceId, float leg, string type)
        {
            var purchaseOrderAuditCommand = new PurchaseOrderAuditCommand
            {
                SagaReferenceId = sagaReferenceId,
                MessageType = messageType,
                PurchaseOrderNumber = purchaseOrderNumber,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Type = type,
                Leg = leg
            };

            return purchaseOrderAuditCommand;
        }

        public TestCertificateAuditCommand TestCertificateAuditCommand(int transition, string messageType, string endpointName, string messageData, string certificateId, string sagaReferenceId, string inboundId, int index, int count, float leg)
        {
            var testCertificateResponseAuditCommand = new TestCertificateAuditCommand
            {
                InboundId = inboundId,
                SagaReferenceId = sagaReferenceId,
                MessageType = messageType,
                CertificateId = certificateId,
                MessageIndex = index,
                MessageCount = count,
                FromEndpoint = endpointName,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = transition,
                MessageData = messageData,
                Leg = leg
            };
            return testCertificateResponseAuditCommand;
        }

        public ProductAchievementSapCommand ProductAchievementSapCommand(ProductAchievementCommand message)
        {
            var productAchievementSapCommand = new ProductAchievementSapCommand
            {
                LotNumber = message.LotNumber,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return productAchievementSapCommand;
        }

        public ProductionOrderStatusSapCommand ProductionOrderStatusSapCommand(ProductionOrderStatusCommand message)
        {
            var productionOrderStatusSapCommand = new ProductionOrderStatusSapCommand
            {
                ProductionOrderId = message.ProductionOrderId,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return productionOrderStatusSapCommand;
        }

        public PurchaseOrderCreateSapCommand PurchaseOrderCreateSapCommand(PurchaseOrderCreateCommand message)
        {
            var purchaseOrderSapCommand = new PurchaseOrderCreateSapCommand
            {
                PurchaseOrderNumber = message.PurchaseOrderNumber,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return purchaseOrderSapCommand;
        }

        public PurchaseOrderChangeSapCommand PurchaseOrderChangeSapCommand(PurchaseOrderChangeCommand message)
        {
            var purchaseOrderChangeSapCommand = new PurchaseOrderChangeSapCommand
            {
                PurchaseOrderNumber = message.PurchaseOrderNumber,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return purchaseOrderChangeSapCommand;
        }

        public GoodsReceiptSapCommand GoodsReceiptSapCommand(GoodsCommand message)
        {
            var goodsReceiptSapCommand = new GoodsReceiptSapCommand
            {
                GoodsReceiptId = message.GoodsReceiptId,
                SagaReferenceId = message.SagaReferenceId,
                Type = message.Type,
                Payload = message.Payload
            };

            return goodsReceiptSapCommand;
        }

        public MaterialMasterSapCommand MaterialMasterSapCommand(MaterialMasterCommand message)
        {
            var matrialMasterSapCommand = new MaterialMasterSapCommand
            {
                ShortItemNumber = message.ShortItemNumber,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return matrialMasterSapCommand;
        }

        public GeneralLedgerSapCommand GeneralLedgerSapCommand(GeneralLedgerCommand message)
        {
            var generalLedgerSapCommand = new GeneralLedgerSapCommand
            {
                GeneralLedgerId = message.GeneralLedgerId,
                SagaReferenceId = message.SagaReferenceId,
                Payload = message.Payload
            };

            return generalLedgerSapCommand;
        }
    }
}