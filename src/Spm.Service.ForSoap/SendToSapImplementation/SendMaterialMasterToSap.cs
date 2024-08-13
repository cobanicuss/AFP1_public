﻿using System;
using System.ServiceModel;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendMaterialMasterToSap : ISendSoapToSap<MaterialMasterSapCommand> { }

    public class SendMaterialMasterToSap : SetupForCommunication, ISendMaterialMasterToSap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendMaterialMasterToSap));
        private readonly IMaterialMasterMessageMap _materialMasterMessageMap;
        private readonly IDuplicateSetupInSap _duplicateSetupInSap;
        private readonly ISerializeMessagesToFile _messagesToFile;
        private MATMAS05_ZMATMAS5_OB_Async_MIClient _client;

        public SendMaterialMasterToSap(IMaterialMasterMessageMap materialMasterMessageMap,
            IDuplicateSetupInSap duplicateSetupInSap,
            ISerializeMessagesToFile messagesToFile)
        {
            _materialMasterMessageMap = materialMasterMessageMap;
            _duplicateSetupInSap = duplicateSetupInSap;
            _messagesToFile = messagesToFile;
        }

        public void SendSoapMessageToSap(MaterialMasterSapCommand message)
        {
            try
            {
                SettingUpClient();

                SettingUpSecurity();

                var soapMessage = _materialMasterMessageMap.MakeSoapMessage(message);
                var sagaReferenceId = soapMessage.IDOC.EDI_DC40.DOCNUM;
                var queryString = _client.Endpoint.Address.Uri.Query;
                var absoluteUri = _client.Endpoint.Address.Uri.AbsoluteUri;
                var newUri = _duplicateSetupInSap.Setup(sagaReferenceId, queryString, absoluteUri);

                _client.Endpoint.Address = new EndpointAddress(newUri);

                SendToSapLogAssist.WriteLoggingHeader(Logger, sagaReferenceId);

                _client.MATMAS05_ZMATMAS5_OB_Async_MI(soapMessage);

                Logger.Info("Writing copy of message to file.");
                _messagesToFile.SaveMessageToXmlFile(soapMessage, message.ShortItemNumber, Constants.MaterialMasterOutputFolder, Shared.Constants.MaterailMasterFileCount);

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
            _client = new MATMAS05_ZMATMAS5_OB_Async_MIClient();
        }
    }
}
