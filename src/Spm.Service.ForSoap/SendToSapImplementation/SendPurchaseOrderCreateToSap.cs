using System;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;
using System.ServiceModel;
using Spm.Service.ForSoap.Config;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendPurchaseOrderCreateToSap : ISendSoapToSap<PurchaseOrderCreateSapCommand> { }

    public class SendPurchaseOrderCreateToSap : SetupForCommunication, ISendPurchaseOrderCreateToSap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendPurchaseOrderCreateToSap));
        private readonly IPurchaseOrderMessageMap _purchaseOrderMessageMap;
        private readonly IDuplicateSetupInSap _duplicateSetupInSap;
        private readonly ISerializeMessagesToFile _messagesToFile;
        private PORDCR103_OB_Async_MIClient _client;

        public SendPurchaseOrderCreateToSap(IPurchaseOrderMessageMap purchaseOrderMessageMap,
            IDuplicateSetupInSap duplicateSetupInSap,
            ISerializeMessagesToFile messagesToFile)
        {
            _purchaseOrderMessageMap = purchaseOrderMessageMap;
            _duplicateSetupInSap = duplicateSetupInSap;
            _messagesToFile = messagesToFile;
        }

        public void SendSoapMessageToSap(PurchaseOrderCreateSapCommand message)
        {
            try
            {
                SettingUpClient();

                SettingUpSecurity();

                var soapMessage = _purchaseOrderMessageMap.MakePurchaseOrderCreateSoapMessage(message);
                var sagaReferenceId = soapMessage.IDOC.EDI_DC40.DOCNUM;
                var queryString = _client.Endpoint.Address.Uri.Query;
                var absoluteUri = _client.Endpoint.Address.Uri.AbsoluteUri;
                var newUri = _duplicateSetupInSap.Setup(sagaReferenceId, queryString, absoluteUri);

                _client.Endpoint.Address = new EndpointAddress(newUri);

                SendToSapLogAssist.WriteLoggingHeader(Logger, sagaReferenceId);

                _client.PORDCR103_OB_Async_MI(soapMessage);

                Logger.Info("Writing copy of message to file.");
                _messagesToFile.SaveMessageToXmlFile(soapMessage, message.PurchaseOrderNumber, Constants.PurchaseOrderCreateOutputFolder, Shared.Constants.PurchaseOrderCreateFileCount);

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
            _client = new PORDCR103_OB_Async_MIClient();
        }
    }
}