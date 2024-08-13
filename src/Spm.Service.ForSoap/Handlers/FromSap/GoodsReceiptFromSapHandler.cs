using NServiceBus;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;

namespace Spm.Service.ForSoap.Handlers.FromSap
{
    public class GoodsReceiptFromSapHandler : IHandleMessages<GoodsReceiptSapResponse>
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GoodsReceiptFromSapHandler));
        private readonly IBus _bus;

        public GoodsReceiptFromSapHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(GoodsReceiptSapResponse message)
        {
            Logger.Info("======================================");
            Logger.Info("Hooray, response from SAP received!");
            Logger.Info("GoodsReceiptSapResponse:");
            Logger.Info("Sending message to SAGA in response.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}");

            //Cannot send an AuditLog here because in SAP there is no GoodReceipt and GoodsReversal endpoint//
            //only a GoodsReceipt endpoint exists that does both Goods-Receitping and Goods-Reversal.//
            //At this point we don't know which is which.//

            var sagaCompleteCommand = new GoodsReceiptResponseCommand { SagaReferenceId = message.SagaReferenceId };
            _bus.Send(sagaCompleteCommand);
        }
    }
}