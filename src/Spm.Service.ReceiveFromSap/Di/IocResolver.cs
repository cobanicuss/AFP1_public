namespace Spm.Service.ReceiveFromSap.Di
{
    public class IocResolver
    {
        public static IValidateIncommingMessages GetValidatorForMessage()
        {
            return Global.IocContainer.Resolve<IValidateIncommingMessages>();
        }
        public static IMapSoapMessage GetMapperForMessage()
        {
            return Global.IocContainer.Resolve<IMapSoapMessage>();
        }
        public static ILogIncommingMessages GetLoggerForMessage()
        {
            return Global.IocContainer.Resolve<ILogIncommingMessages>();
        }
    }
}