namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public interface ISerializeMessagesToFile
    {
        void SaveMessageToXmlFile<T>(T soapMessage, string identifier, string outputFolder, int fileCount) where T : class;
    }
}