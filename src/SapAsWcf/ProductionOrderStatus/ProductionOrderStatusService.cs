using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.ProductionOrderStatus
{
    public class ProductionOrderStatusService : ZPP_CHNG_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void ZPP_CHNG_OB_Async_MI(ZPP_CHNG_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.ProductionOrderStatus;

            var productionOrderId = request.ZPP_CHNG.IDOC.EDI_DC40.DOCNUM;
            var productionOrderIdList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !productionOrderIdList.Any(x => x.Equals(productionOrderId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, productionOrderId);

            var serializer = new DataContractSerializer(typeof(ZPP_CHNG_OB_Async_MI1));
            var sb = new StringBuilder();
            string soapAsString;

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.WriteObject(writer, request);
                writer.Flush();
                soapAsString = sb.ToString();
            }

            _mockServiceRepository.AddUpdateSoapData(simSystemEnum, productionOrderId, soapAsString);

            Console.WriteLine("======================================");
            Console.WriteLine("ProductionOrderStatusService");
            Console.WriteLine("Inside ZPP_CHNG()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginZPP_CHNG_OB_Async_MI(ZPP_CHNG_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndZPP_CHNG_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
