using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using NServiceBus.Logging;

namespace Spm.Service.ReceiveFromSap
{
    public interface ILogIncommingMessages
    {
        void Log<T>(T message);
    }

    public class LogIncommingMessages : ILogIncommingMessages
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LogIncommingMessages));

        public void Log<T>(T message)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var sb = new StringBuilder();

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.WriteObject(writer, message);
                writer.Flush();
            }

            Logger.Info(sb.ToString());
        }
    }
}