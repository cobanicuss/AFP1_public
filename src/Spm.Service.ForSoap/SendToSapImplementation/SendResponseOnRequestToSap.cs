using System;
using System.ServiceModel;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendResponseOnRequestToSap : ISendSoapToSap<ResponseToSapRequestCommand> { }

    public class SendResponseOnRequestToSap : SetupForCommunication, ISendResponseOnRequestToSap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendResponseOnRequestToSap));
        private readonly IResponseToSapRequestMessageMap _responseToSapRequestMessageMap;
        private readonly IDuplicateSetupInSap _duplicateSetupInSap;
        private readonly ISerializeMessagesToFile _messagesToFile;
        private SYSTAT01_OB_Async_MIClient _client;

        public SendResponseOnRequestToSap(IResponseToSapRequestMessageMap responseToSapRequestMessageMap,
            IDuplicateSetupInSap duplicateSetupInSap,
            ISerializeMessagesToFile messagesToFile)
        {
            _responseToSapRequestMessageMap = responseToSapRequestMessageMap;
            _duplicateSetupInSap = duplicateSetupInSap;
            _messagesToFile = messagesToFile;
        }

        public void SendSoapMessageToSap(ResponseToSapRequestCommand message)
        {
            try
            {
                SettingUpClient();

                SettingUpSecurity();

                var soapMessage = _responseToSapRequestMessageMap.CreateResponseToSapMessage(message);
                var sagaReferenceId = soapMessage.IDOC.E1STATS[0].DOCNUM;
                var queryString = _client.Endpoint.Address.Uri.Query;
                var absoluteUri = _client.Endpoint.Address.Uri.AbsoluteUri;
                var newUri = _duplicateSetupInSap.Setup(sagaReferenceId, queryString, absoluteUri);

                _client.Endpoint.Address = new EndpointAddress(newUri);

                SendToSapLogAssist.WriteLoggingHeader(Logger, sagaReferenceId);

                _client.SYSTAT01_OB_Async_MI(soapMessage);
                
                string outputFolder;

                switch (message.InboundType)
                {
                    case EnumInboundType.TestCertificate:
                        outputFolder = Constants.TestCertificateInboundOutputFolder;
                        break;
                    case EnumInboundType.MaterialMasterUpdate:
                        outputFolder = Constants.MaterialMasterUpdateInboundOutputFolder;
                        break;
                    default:
                        throw new Exception("Cannot determine where to write a copy of the message to file for Response-on-Request. The folder cannot be created because it is unknown. Cannot proceed!!!");
                }

                Logger.Info("Writing copy of message to file.");
                _messagesToFile.SaveMessageToXmlFile(soapMessage, message.NumberId, outputFolder, Shared.Constants.ResponseOnRequestFileCount);

                Logger.Info("Cool, SOAP message sent successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
                var isRealError = !ex.Message.ToUpper().Contains(Constants.EmptySoapBodyOn200ResponseIsOk);
                if (isRealError) throw;
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
            _client = new SYSTAT01_OB_Async_MIClient();
        }
    }
}
