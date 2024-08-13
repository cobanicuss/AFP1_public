using System;
using System.ServiceModel;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendProductAchievementToSap : ISendSoapToSap<ProductAchievementSapCommand> { }

    public class SendProductAchievementToSap : SetupForCommunication, ISendProductAchievementToSap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendProductAchievementToSap));
        private readonly IInventoryMovementMessageMap _inventoryMovementMessageMap;
        private readonly IDuplicateSetupInSap _duplicateSetupInSap;
        private readonly ISerializeMessagesToFile _messagesToFile;
        private InventoryMovement_OB_Async_MIClient _client;

        public SendProductAchievementToSap(IInventoryMovementMessageMap inventoryMovementMessageMap,
            IDuplicateSetupInSap duplicateSetupInSap,
            ISerializeMessagesToFile messagesToFile)
        {
            _inventoryMovementMessageMap = inventoryMovementMessageMap;
            _duplicateSetupInSap = duplicateSetupInSap;
            _messagesToFile = messagesToFile;
        }

        public void SendSoapMessageToSap(ProductAchievementSapCommand message)
        {
            try
            {
                SettingUpClient();

                SettingUpSecurity();

                var soapMessage = _inventoryMovementMessageMap.MakeSoapMessage(message);
                var sagaReferenceId = soapMessage.InventoryMovementHeader.MessageID;
                var lotNumber = soapMessage.InventoryMovementLine[0].InventoryMovementDetails[0].PackageLine[0].PackageInformation.WarehouseSerialNumber;
                var queryString = _client.Endpoint.Address.Uri.Query;
                var absoluteUri = _client.Endpoint.Address.Uri.AbsoluteUri;
                var newUri = _duplicateSetupInSap.Setup(sagaReferenceId, queryString, absoluteUri);

                _client.Endpoint.Address = new EndpointAddress(newUri);

                SendToSapLogAssist.WriteLoggingHeader(Logger, sagaReferenceId);
                Logger.Info($"lotNumber={lotNumber}");

                _client.InventoryMovement_OB_Async_MI(soapMessage);

                Logger.Info("Writing copy of message to file.");
                _messagesToFile.SaveMessageToXmlFile(soapMessage, lotNumber.Replace("/", "_"), Constants.ProductAchievementOutputFolder, Shared.Constants.ProductAchievementFileCount);

                Logger.Info("Cool, SOAP message sent successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                throw;
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
                    SendToSapLogAssist.WriteErrorLog(Logger, ex);
                    throw;
                }
            }
        }

        internal override void SettingUpSecurity()
        {
            Logger.Info("Setting up security");

            if (_client.ClientCredentials == null) throw new ArgumentException("ClientCredentials is null cannot set security!");

            _client.ClientCredentials.UserName.UserName = ProfileConfigVariables.ComsUserName;
            _client.ClientCredentials.UserName.Password = ProfileConfigVariables.ComsPasword;
        }

        internal override void SettingUpClient()
        {
            Logger.Info("Setting up the client.");
            _client = new InventoryMovement_OB_Async_MIClient();
        }
    }
}
