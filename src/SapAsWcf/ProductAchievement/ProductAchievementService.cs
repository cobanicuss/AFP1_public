using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Shared;

namespace SapAsWcf.ProductAchievement
{
    public class ProductAchievementService : InventoryMovement_OB_Async_MI
    {
        private IMockServiceRepository _mockServiceRepository;

        public void InventoryMovement_OB_Async_MI(InventoryMovement_OB_Async_MI1 request)
        {
            _mockServiceRepository = new MockServiceRepository();
            const int simSystemEnum = (int)SimSystemEnum.ProductAchievement;

            var sagaReferenceId = request.InventoryMovement.InventoryMovementHeader.MessageID;
            var sagaReferenceIdList = _mockServiceRepository.GetNumbers(simSystemEnum);
            var isNew = !sagaReferenceIdList.Any(x => x.Equals(sagaReferenceId));

            if (isNew) _mockServiceRepository.AddNumber(simSystemEnum, sagaReferenceId);

            var serializer = new DataContractSerializer(typeof(InventoryMovement_OB_Async_MI1));
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
            Console.WriteLine("ProductAchievementService");
            Console.WriteLine("Inside InventoryMovement()");
            Console.WriteLine("Request Received by SAP-XI");
        }

        public IAsyncResult BeginInventoryMovement_OB_Async_MI(InventoryMovement_OB_Async_MI1 request, AsyncCallback callback, object asyncState)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }

        public void EndInventoryMovement_OB_Async_MI(IAsyncResult result)
        {
            //Leave here. Showing implementation of the inteface is working//
            throw new NotImplementedException();
        }
    }
}
