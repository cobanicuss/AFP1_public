using System;
using NServiceBus.Logging;

namespace Spm.OrrSys.Service.Soap.DataInterfacingService
{
    public interface IDoProductAchievementCommunication : ICommunicateWithOrrSys
    {
        void SendSoapMessageForProductAchievement(string lotNumber);
    }

    public class ProductAchievement : IDoProductAchievementCommunication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductAchievement));
        private DataInterfacingServiceClient _client;

        public void SendSoapMessageForProductAchievement(string lotNumber)
        {
            try
            {
                Logger.Info("Setting up the client.");
                _client = new DataInterfacingServiceClient();

                Logger.Debug("Calling the method:FinalizeProductAchievement().");
                _client.FinalizeProductAchievement(lotNumber);

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