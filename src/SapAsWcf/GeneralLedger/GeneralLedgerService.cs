using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.GeneralLedger
{
    public class GeneralLedgerService : ACC_DOCUMENT03_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;
        
        public void ACC_DOCUMENT03_OB_Async_MI(ACC_DOCUMENT03_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int) SimSystemEnum.GeneralLedger;

            var sagaReferenceId = request.ACC_DOCUMENT03.IDOC.EDI_DC40.DOCNUM;
            var generalLedgerNumberList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !generalLedgerNumberList.Any(x => x.Equals(sagaReferenceId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, sagaReferenceId);  
            
            var serializer = new DataContractSerializer(typeof(ACC_DOCUMENT03_OB_Async_MI1));
            var sb = new StringBuilder();
            string soapAsString;

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.WriteObject(writer, request);
                writer.Flush();
                soapAsString = sb.ToString();
            }

            _mockServiceRepository.AddUpdateSoapData(simSystemEnum, sagaReferenceId, soapAsString);

            Console.WriteLine("======================================");
            Console.WriteLine("GeneralLedgerService");
            Console.WriteLine("Inside ACC_DOCUMENT03()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginACC_DOCUMENT03_OB_Async_MI(ACC_DOCUMENT03_OB_Async_MI1 request, AsyncCallback callback,object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndACC_DOCUMENT03_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
