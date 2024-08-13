using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.MaterialMaster
{
    public class MaterialMasterService : MATMAS05_ZMATMAS5_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void MATMAS05_ZMATMAS5_OB_Async_MI(MATMAS05_ZMATMAS5_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.MaterialMaster;

            var sagaReferenceId = request.ZMATMAS5.IDOC.EDI_DC40.DOCNUM;
            var materialMasterNumberList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !materialMasterNumberList.Any(x => x.Equals(sagaReferenceId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, sagaReferenceId);

            var serializer = new DataContractSerializer(typeof(MATMAS05_ZMATMAS5_OB_Async_MI1));
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
            Console.WriteLine("MaterialMasterService");
            Console.WriteLine("Inside MATMAS05_ZMATMAS5()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginMATMAS05_ZMATMAS5_OB_Async_MI(MATMAS05_ZMATMAS5_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndMATMAS05_ZMATMAS5_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
