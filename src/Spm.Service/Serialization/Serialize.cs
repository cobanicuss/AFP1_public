using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NHibernate;
using Spm.Service.Domain;

namespace Spm.Service.Serialization
{
    public interface ISerializeMessage
    {
        void Serialize<T>(T objectToSerialize, Guid messageId, Guid sagaId);
        T DeSerialize<T>(Guid messageId);
        void DeleteSerialization(Guid messageId);
    }

    public class SerializeMessage : ISerializeMessage
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void Serialize<T>(T objectToSerialize, Guid messageId, Guid sagaId)
        {
            string strXml;

            var serializer = new XmlSerializer(typeof(T));
            using (var output = new StringWriter())
            {
                serializer.Serialize(output, objectToSerialize);
                strXml = output.ToString();
            }

            var sagaMessageSerializer = new SagaMessageSerializer
            {
                SagaId = sagaId,
                MessageId = messageId,
                Message = strXml
            };

            Session.Save(sagaMessageSerializer);
        }

        public T DeSerialize<T>(Guid messageId)
        {
            var strXmlList = Session.QueryOver<SagaMessageSerializer>().Where(x => x.MessageId == messageId).List();

            if (!strXmlList.Any())
            {
                throw new ArgumentException($"No xml string found in the database for sagaId={messageId}. Cannot DeSerialize. Cannot Proceed.");
            }
            if (string.IsNullOrEmpty(strXmlList[0].Message))
            {
                throw new ArgumentException($"Xml string is null or empty for sagaId={messageId}. Cannot DeSerialize. Cannot Proceed.");
            }

            var mySerializer = new XmlSerializer(typeof(T));

            T returnValue;
            using (TextReader reader = new StringReader(strXmlList[0].Message))
            {
                returnValue = (T)mySerializer.Deserialize(reader);
            }

            return returnValue;
        }

        public void DeleteSerialization(Guid messageId)
        {
            var strXmlList = Session.QueryOver<SagaMessageSerializer>().Where(x => x.MessageId == messageId).List();

            if (!strXmlList.Any()) { return; }

            Session.Delete(strXmlList[0]);
        }
    }
}