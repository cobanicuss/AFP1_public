using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.GoodsReceipt
{
    public class GoodsReceiptService : ZMBGMCR02_EXTND_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void ZMBGMCR02_EXTND_OB_Async_MI(ZMBGMCR02_EXTND_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.GoodsReceipt;

            var goodsReceiptId = request.ZMBGMCR02_EXTND.IDOC.EDI_DC40.DOCNUM;
            var goodsReceiptList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !goodsReceiptList.Any(x => x.Equals(goodsReceiptId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, goodsReceiptId);

            var serializer = new DataContractSerializer(typeof(ZMBGMCR02_EXTND_OB_Async_MI1));
            var sb = new StringBuilder();
            string soapAsString;

            using (var writer = XmlWriter.Create(sb))
            {
                serializer.WriteObject(writer, request);
                writer.Flush();
                soapAsString = sb.ToString();
            }

            _mockServiceRepository.AddUpdateSoapData(simSystemEnum, goodsReceiptId, soapAsString);

            Console.WriteLine("======================================");
            Console.WriteLine("GoodsReceiptService");
            Console.WriteLine("Inside ZMBGMCR02_EXTND()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginZMBGMCR02_EXTND_OB_Async_MI(ZMBGMCR02_EXTND_OB_Async_MI1 request, AsyncCallback callback,
            object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndZMBGMCR02_EXTND_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
