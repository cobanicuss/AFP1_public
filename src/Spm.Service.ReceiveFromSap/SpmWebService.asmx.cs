using System;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using NServiceBus.Logging;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ReceiveFromSap.Di;
using Spm.Service.ReceiveFromSap.SoapMessages;
using Spm.Shared;

namespace Spm.Service.ReceiveFromSap
{
    [WebService(Namespace = "Spm.Service.ReceiveFromSap")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [SoapDocumentService(SoapBindingUse.Literal, SoapParameterStyle.Bare)]
    public class SpmWebService : WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SpmWebService));
        private readonly ILogIncommingMessages _logIncommingMessages = IocResolver.GetLoggerForMessage();
        private readonly IValidateIncommingMessages _validateIncommingMessages = IocResolver.GetValidatorForMessage();
        private readonly IMapSoapMessage _mapSoapMessage = IocResolver.GetMapperForMessage();

        [WebMethod]
        public void ProductAchievementResponse(SYSTAT01_PRODUCTACHIEVEMENTRESPONSE SYSTAT01_PRODUCTACHIEVEMENTRESPONSE)
        {
            Logger.Info("Starting: inside ProductAchievementResponse.");

            if (SYSTAT01_PRODUCTACHIEVEMENTRESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_PRODUCTACHIEVEMENTRESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_PRODUCTACHIEVEMENTRESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_PRODUCTACHIEVEMENTRESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToProductAchievementSapResponse(SYSTAT01_PRODUCTACHIEVEMENTRESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void PurchaseOrderCreateResponse(SYSTAT01_PURCHASEORDERCREATERESPONSE SYSTAT01_PURCHASEORDERCREATERESPONSE)
        {
            Logger.Info("Starting: inside PurchaseOrderCreateResponse.");

            if (SYSTAT01_PURCHASEORDERCREATERESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_PURCHASEORDERCREATERESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_PURCHASEORDERCREATERESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_PURCHASEORDERCREATERESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");

            var message = _mapSoapMessage.ToPurchaseOrderCreateSapResponse(SYSTAT01_PURCHASEORDERCREATERESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void PurchaseOrderChangeResponse(SYSTAT01_PURCHASEORDERCHANGERESPONSE SYSTAT01_PURCHASEORDERCHANGERESPONSE)
        {
            Logger.Info("Starting: inside PurchaseOrderChangeResponse.");

            if (SYSTAT01_PURCHASEORDERCHANGERESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_PURCHASEORDERCHANGERESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_PURCHASEORDERCHANGERESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_PURCHASEORDERCHANGERESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToPurchaseOrderChangeSapResponse(SYSTAT01_PURCHASEORDERCHANGERESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void ProductionOrderStatusResponse(SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE)
        {
            Logger.Info("Starting: inside ProductionOrderStatusResponse.");

            if (SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToProductionOrderStatusSapResponse(SYSTAT01_PRODUCTIONORDERSTATUSRESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void GoodsReceiptResponse(SYSTAT01_GOODSRECEIPTRESPONSE SYSTAT01_GOODSRECEIPTRESPONSE)
        {
            Logger.Info("Starting: inside GoodsReceiptResponse.");

            if (SYSTAT01_GOODSRECEIPTRESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_MATERIALMASTERRESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_GOODSRECEIPTRESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_GOODSRECEIPTRESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToGoodsReceiptSapResponse(SYSTAT01_GOODSRECEIPTRESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void MaterialMasterCreateResponse(SYSTAT01_MATERIALMASTERRESPONSE SYSTAT01_MATERIALMASTERRESPONSE)
        {
            Logger.Info("Starting: inside MaterialMasterCreateResponse.");

            if (SYSTAT01_MATERIALMASTERRESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_MATERIALMASTERRESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_MATERIALMASTERRESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_MATERIALMASTERRESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToMaterialMasterSapResponse(SYSTAT01_MATERIALMASTERRESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void ProductionOrderRequest(string productionOrderTrigger)
        {
            Logger.Info("Starting: inside ProductionOrderRequest.");

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = new ProductionOrderTriggerRequest
            {
                InboundId = Guid.NewGuid().ToString()
            };

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void PlannedOrderRequest(string plannedOrderTrigger)
        {
            Logger.Info("Starting: inside PlannedOrderRequest.");

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = new PlannedOrderTriggerRequest
            {
                InboundId = Guid.NewGuid().ToString()
            };

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void TestCertificateRequest(ZOBTC01 ZOBTC01)
        {
            Logger.Info("Starting: inside TestCertificateRequest.");

            if (ZOBTC01 == null)
            {
                UnexpectedError("SPM says: ZOBTC01 is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP request message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(ZOBTC01.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(ZOBTC01);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToTestCertificateSapRequest(ZOBTC01.IDOC);
            message.InboundId = Guid.NewGuid().ToString();

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void MaterialMasterUpdateRequest(ZMATMAS5 ZMATMAS5)
        {
            Logger.Info("Starting: inside MaterialMasterUpdateRequest.");

            if (ZMATMAS5 == null)
            {
                UnexpectedError("SPM says: ZMATMAS5 is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP request message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(ZMATMAS5.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(ZMATMAS5);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToMatarialMasterUpdateSapRequest(ZMATMAS5.IDOC);
            message.InboundId = Guid.NewGuid().ToString();

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void GeneralLedgerResponse(SYSTAT01_GENERALLEDGERRESPONSE SYSTAT01_GENERALLEDGERRESPONSE)
        {
            Logger.Info("Starting: inside GoodsReceiptResponse.");

            if (SYSTAT01_GENERALLEDGERRESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_GENERALLEDGERRESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_GENERALLEDGERRESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_GENERALLEDGERRESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToGeneralLedgerSapResponse(SYSTAT01_GENERALLEDGERRESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void TestCertificateOutboundResponse(SYSTAT01_TESTCERTIFICATERESPONSE SYSTAT01_TESTCERTIFICATERESPONSE)
        {
            Logger.Info("Starting: inside TestCertificateOutboundResponse.");

            if (SYSTAT01_TESTCERTIFICATERESPONSE == null)
            {
                UnexpectedError("SPM says: SYSTAT01_TESTCERTIFICATERESPONSE is NULL?? Critical Error. Cannot proceed, have no data.");
                return;
            }

            Logger.Info("Validating the SOAP response message.");
            if (!_validateIncommingMessages.IsValidMessageDetails(SYSTAT01_TESTCERTIFICATERESPONSE.IDOC)) return;

            Logger.Info("Logging message, as text in xml format.");
            _logIncommingMessages.Log(SYSTAT01_TESTCERTIFICATERESPONSE);

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = _mapSoapMessage.ToTestCertificateSapResponse(SYSTAT01_TESTCERTIFICATERESPONSE.IDOC);

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        [WebMethod]
        public void TestCertificateOutboundTriggerRequest(string testCertificateOutboundTrigger)
        {
            Logger.Info("Starting: inside TestCertificateOutboundTriggerRequest.");

            Logger.Info($"Sending NServicebus message to {EndPointName.SpmServiceForSoap}.");
            var message = new TestCertificateTriggerRequest
            {
                InboundId = Guid.NewGuid().ToString()
            };

            Global.Bus.Send(EndPointName.SpmServiceForSoap, message);
            Logger.Info("All done. All good.");
        }

        private static void UnexpectedError(string error)
        {
            Logger.Error(error);
            throw new ArgumentException(error);
        }
    }
}