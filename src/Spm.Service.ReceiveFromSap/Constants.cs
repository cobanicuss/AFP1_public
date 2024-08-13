namespace Spm.Service.ReceiveFromSap
{
    public class Constants
    {
        public const string InboundMessageLogPath = @"C:\LogFiles\Spm\SpmWebService\InboundSoapMessages.log";
        public const string MethodLogPath = @"C:\LogFiles\Spm\SpmWebService\ServiceMethod.log";
        public const string FileLayout = "[%date{ISO8601}]%m%n";
        public const string EndPoitName = "Spm.OrrSys.TestClient";
        public const string PayloadNameSpace = "Spm.Service.ReceiveFromSap.Payload";
    }
}