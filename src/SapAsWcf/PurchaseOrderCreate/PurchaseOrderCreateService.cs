using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.PurchaseOrderCreate
{
    public class PurchaseOrderCreateService : PORDCR103_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void PORDCR103_OB_Async_MI(PORDCR103_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.PuchaseOrderCreate;

            var sagaReferenceId = request.PORDCR103.IDOC.EDI_DC40.DOCNUM;
            var purchaseOrderCreateNumberList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !purchaseOrderCreateNumberList.Any(x => x.Equals(sagaReferenceId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, sagaReferenceId);

            var serializer = new DataContractSerializer(typeof(PORDCR103_OB_Async_MI1));
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
            Console.WriteLine("PurchaseOrderCreateService");
            Console.WriteLine("Inside PORDCR103()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginPORDCR103_OB_Async_MI(PORDCR103_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndPORDCR103_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
