using System;
using NServiceBus.Logging;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public class SendToSapLogAssist
    {
        public static void WriteLoggingHeader(ILog logger, string id)
        {
            logger.Info("Sending message.");
            logger.Info($"Identifier={id}");
        }

        public static void WriteErrorLog(ILog logger, Exception ex)
        {
            logger.Warn("Now aborting client on ERROR condition.");
            logger.Error($"ERROR:{ex.Message}");
            logger.Error($"STACK:{ex.StackTrace}");
            logger.Error($"INNER-EX:{ex.InnerException}");
        }
    }
}