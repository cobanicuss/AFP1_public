using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace Spm.Service.ForSoap.SendToSapImplementation
{
    public class MessageToFile : ISerializeMessagesToFile
    {
        private readonly IFileBuffer _fileBuffer;

        public MessageToFile(IFileBuffer fileBuffer)
        {
            _fileBuffer = fileBuffer;
        }

        public void SaveMessageToXmlFile<T>(T soapMessage, string identifier, string outputFolder, int fileCount) where T : class
        {
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            else
            {
                _fileBuffer.DeleteOverflow(outputFolder, fileCount);
            }

            var dataFileName = $"{outputFolder}{identifier}_{Guid.NewGuid()}.xml";
            var fs = new FileStream(dataFileName, FileMode.CreateNew);
            var dcs = new DataContractSerializer(typeof(T));
            var xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8);

            dcs.WriteObject(xdw, soapMessage);

            xdw.Close();
            fs.Dispose();
        }
    }
}