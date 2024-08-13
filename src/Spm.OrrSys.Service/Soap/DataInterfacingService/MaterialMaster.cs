using System;
using NServiceBus.Logging;
using Spm.OrrSys.Messages;

namespace Spm.OrrSys.Service.Soap.DataInterfacingService
{
    public interface IDoMaterialMasterCommunication : ICommunicateWithOrrSys
    {
        void SendSoapMessageForMatrialMasterUpdate(MaterialMasterUpdateRequestCommand message);
    }

    public class MaterialMaster : IDoMaterialMasterCommunication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievement));
        private DataInterfacingServiceClient _client;
        private readonly IMapSoap _soap;

        public MaterialMaster(IMapSoap soap)
        {
            _soap = soap;
        }

        public void SendSoapMessageForMatrialMasterUpdate(MaterialMasterUpdateRequestCommand message)
        {
            try
            {
                Logger.Info("Setting up the client.");
                _client = new DataInterfacingServiceClient();

                Logger.Info("Setting up data-contract.");
                var request = _soap.CreateMaterialMasterUpdateRequest(message);

                Logger.Debug("Calling the method:UpdateMaterialMaster(...).");
                _client.UpdateMaterialMaster(request);

                Logger.Info("Cool, SOAP message sent successfully.");
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
    }
}