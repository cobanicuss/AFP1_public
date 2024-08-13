using System;
using System.Linq;
using Spm.Shared;

namespace SapAsWcf.ResponseToSapRequest
{
    public class ResponseToSapRequestService : SYSTAT01_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void SYSTAT01_OB_Async_MI(SYSTAT01_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();

            const int sysEnum = (int)SimSystemEnum.ResponseToSapRequest;

            var responseFromSapRequestList = _mockServiceRepository.GetNumbers(sysEnum);

            if (!responseFromSapRequestList.Any()) return;

            _mockServiceRepository.DeleteNumber(sysEnum, responseFromSapRequestList[0]);
            _mockServiceRepository.DeleteSoapData(sysEnum, responseFromSapRequestList[0]);

            Console.WriteLine("======================================");
            Console.WriteLine("ResponseToSapRequestService");
            Console.WriteLine("Inside SYSTAT01()");
            Console.WriteLine("RESPONSE received by SAP-XI");
        }

        public IAsyncResult BeginSYSTAT01_OB_Async_MI(SYSTAT01_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndSYSTAT01_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
