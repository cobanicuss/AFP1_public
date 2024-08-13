using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Spm.Shared;
using TestCert;

namespace SapAsWcf.TestCertificate
{
    public class TestCertificateService : Certificate_OB_Async_Enh_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void Certificate_OB_Async_Enh_MI(Certificate_OB_Async_Enh_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.TestCertificate;

            var sagaReferenceId = request.Certificate.CertificateHeader.MessageID;
            var sagaReferenceIdList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !sagaReferenceIdList.Any(x => x.Equals(sagaReferenceId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, sagaReferenceId);

            string soapAsString;
            var serializer = new XmlSerializer(typeof(Certificate_OB_Async_Enh_MI1));
            using (var output = new StringWriter())
            {
                serializer.Serialize(output, request);
                soapAsString = output.ToString();
            }
            
            _mockServiceRepository.AddUpdateSoapData(simSystemEnum, sagaReferenceId, soapAsString);

            Console.WriteLine("======================================");
            Console.WriteLine("ProductAchievementService");
            Console.WriteLine("Inside InventoryMovement()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginCertificate_OB_Async_Enh_MI(Certificate_OB_Async_Enh_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndCertificate_OB_Async_Enh_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}