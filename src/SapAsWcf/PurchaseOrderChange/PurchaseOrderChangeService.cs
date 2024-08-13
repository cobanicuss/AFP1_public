using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.PurchaseOrderChange
{
    public class PurchaseOrderChangeService : PORDCH03_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void PORDCH03_OB_Async_MI(PORDCH03_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.PurchaseOrderChange;

            var purchaseOrderChangeNumber = request.PORDCH03.IDOC.EDI_DC40.DOCNUM;
            var purchaseOrderChangeNumberList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !purchaseOrderChangeNumberList.Any(x => x.Contains(purchaseOrderChangeNumber));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, purchaseOrderChangeNumber);

            var serializer = new DataContractSerializer(typeof(PORDCH03_OB_Async_MI1));
            var sb = new StringBuilder();
            string soapAsString;

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.WriteObject(writer, request);
                writer.Flush();
                soapAsString = sb.ToString();
            }

            _mockServiceRepository.AddUpdateSoapData(simSystemEnum, purchaseOrderChangeNumber, soapAsString);

            Console.WriteLine("======================================");
            Console.WriteLine("PurchaseOrderChangeService");
            Console.WriteLine("Inside PORDCH03()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginPORDCH03_OB_Async_MI(PORDCH03_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndPORDCH03_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
