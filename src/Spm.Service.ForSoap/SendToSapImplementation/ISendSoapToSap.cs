using NServiceBus;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISendSoapToSap<in T> where T : ICommand
    {
        void SendSoapMessageToSap(T message);
    }

    public abstract class SetupForCommunication
    {
        internal virtual void SettingUpClient() { }
        internal virtual void SettingUpSecurity() { }
    }
}