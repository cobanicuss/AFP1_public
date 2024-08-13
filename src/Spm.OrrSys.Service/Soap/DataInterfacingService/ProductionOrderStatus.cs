using System;
using System.Linq;
using NServiceBus.Logging;
using OrrSys.DataInterface.DataContract;
using Spm.OrrSys.Messages;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.Service.Soap.DataInterfacingService
{
    public interface IDoProductionOrderStatusCommunication : ICommunicateWithOrrSys
    {
        ProductionOrderStatusCommand GetShortSequence();
        ProductionOrderStatusCommand GetLongSequence();
    }

    public class ProductionOrderStatus : IDoProductionOrderStatusCommunication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderStatus));
        private DataInterfacingServiceClient _client;

        public ProductionOrderStatusCommand GetShortSequence()
        {
            try
            {
                Logger.Info("Setting up the client.");
                _client = new DataInterfacingServiceClient();

                Logger.Debug("Get data for ProductionOrderStatus Short-Sequence.");

                var response = _client.RetrieveProductionOrdresStatusShortSequence();
                var returnMessage = CreateReturnMessage(response);

                return returnMessage;
            }
            finally
            {
                try
                {
                    Logger.Info("Now closing the client.");
                    _client?.Close();
                }
                catch (Exception ex)
                {
                    _client?.Abort();
                    Logger.Error("An error occured now aborting the client.");
                    Logger.Error(ex.Message);
                    if (ex.InnerException != null) Logger.Error(ex.InnerException.ToString());
                    Logger.Error(ex.StackTrace);

                    throw;
                }
            }
        }

        public ProductionOrderStatusCommand GetLongSequence()
        {
            try
            {
                Logger.Info("Setting up the client.");
                _client = new DataInterfacingServiceClient();

                Logger.Debug("Get data for ProductionOrderStatus Short-Sequence.");

                var response = _client.RetrieveProductionOrdresStatusLongSequence();
                var returnMessage = CreateReturnMessage(response);

                return returnMessage;
            }
            finally
            {
                try
                {
                    Logger.Info("Now closing the client.");
                    _client?.Close();
                }
                catch (Exception ex)
                {
                    _client?.Abort();
                    Logger.Error("An error occured now aborting the client.");
                    Logger.Error(ex.Message);
                    if (ex.InnerException != null) Logger.Error(ex.InnerException.ToString());
                    Logger.Error(ex.StackTrace);

                    throw;
                }
            }
        }

        private static ProductionOrderStatusCommand CreateReturnMessage(ProductionOrderStatusResponse response)
        {
            Logger.Info("Cool, SOAP message sent successfully.");

            if (response?.Payload == null || string.IsNullOrEmpty(response.SagaReferenceId) || string.IsNullOrEmpty(response.ProductionOrderId))
            {
                return null;
            }

            var newPayload = response.Payload.Select(item => new ProductionOrderStatusPayloadItem
            {
                CompleteFlag = item.CompleteFlag,
                FinishDate = item.FinishDate,
                OrderQuantity = item.OrderQuantity,
                OrderQuantityUom = item.OrderQuantityUom,
                ProductionOrderNumber = item.ProductionOrderNumber,
                ReleaseFlag = item.ReleaseFlag
            }).ToList();

            var returnMessage = new ProductionOrderStatusCommand
            {
                SagaReferenceId = response.SagaReferenceId,
                ProductionOrderId = response.ProductionOrderId,
                Payload = new ProductionOrderStatusPayload
                {
                    ProductionOrderStatusPayloadItem = newPayload
                }
            };
            return returnMessage;
        }
    }
}