using System;
using System.ServiceModel;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Mapper;
using Spm.Service.ForSoap.Messages;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendTestCertificateToSap : ISendSoapToSap<TestCertificateSapCommand> { }
    
    public class SendTestCertificateToSap : SetupForCommunication, ISendTestCertificateToSap
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SendTestCertificateToSap));
        private readonly ITestCertificateMap _testCertificateMessageMap;
        private readonly IDuplicateSetupInSap _duplicateSetupInSap;
        private readonly ISerializeMessagesToFile _messagesToFile;
        private TestCert.Certificate_OB_Async_Enh_MIClient _client;

        public SendTestCertificateToSap(ITestCertificateMap testCertificateMessageMap, IDuplicateSetupInSap duplicateSetupInSap, ISerializeMessagesToFile messagesToFile)
        {
            _testCertificateMessageMap = testCertificateMessageMap;
            _duplicateSetupInSap = duplicateSetupInSap;
            _messagesToFile = messagesToFile;
        }

        public void SendSoapMessageToSap(TestCertificateSapCommand message)
        {
            SettingUpClient();

            SettingUpSecurity();

            var soapMessage = _testCertificateMessageMap.MakeSoapMessage(message);
            var sagaReferenceId = soapMessage.CertificateHeader.MessageID;
            var certificateId = soapMessage.CertificateHeader.CertificateNumber;
            var queryString = _client.Endpoint.Address.Uri.Query;
            var absoluteUri = _client.Endpoint.Address.Uri.AbsoluteUri;
            var newUri = _duplicateSetupInSap.Setup(sagaReferenceId, queryString, absoluteUri);

            _client.Endpoint.Address = new EndpointAddress(newUri);

            SendToSapLogAssist.WriteLoggingHeader(Logger, sagaReferenceId);
            Logger.Info($"certificateId={certificateId}");

            _client.Certificate_OB_Async_Enh_MI(soapMessage);

            Logger.Info("Writing copy of message to file.");
            _messagesToFile.SaveMessageToXmlFile(soapMessage, certificateId, Constants.TestCertificateOutputFolder, Shared.Constants.TestCertificateFileCount);

            Logger.Info("Cool, SOAP message sent successfully.");
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
            _client = new TestCert.Certificate_OB_Async_Enh_MIClient();
        }
    }
}