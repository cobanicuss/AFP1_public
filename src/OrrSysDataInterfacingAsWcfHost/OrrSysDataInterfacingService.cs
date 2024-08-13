using System;
using System.Text;
using OrrSys.DataInterface.DataContract;

namespace OrrSysDataInterfacingAsWcfHost
{
    public class OrrSysDataInterfacingService : IDataInterfacingService
    {
        public FinalizeProductAchievementResponse FinalizeProductAchievement(string lotNumber)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Inside FinalizeProductAchievement()");
            Console.WriteLine($"LotNumber={lotNumber}");
            Console.WriteLine("TEST ONLY. NO DB INTERACTION!!!");

            return null;
        }

        public ProductionOrderStatusResponse RetrieveProductionOrdresStatusLongSequence()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Inside RetrieveProductionOrdresStatusLongSequence()");

            var productionOrderId = Guid.NewGuid().ToString();
            var sagaReferenceId = Guid.NewGuid().ToString();

            Console.WriteLine($"ProductionOrderId={productionOrderId}");
            Console.WriteLine($"SagaReferenceId={sagaReferenceId}");
            Console.WriteLine("TEST ONLY. NO DB INTERACTION!!!");

            var returnVal = new ProductionOrderStatusResponse
            {
                ProductionOrderId = productionOrderId,
                SagaReferenceId = sagaReferenceId,
                Payload = new[]
                {
                   new ProductionOrderStatus
                   {
                        CompleteFlag = "A1",
                        FinishDate = DateTime.Now,
                        OrderQuantity = 12.34D,
                        OrderQuantityUom = "A2",
                        ProductionOrderNumber = "0000000123456",
                        ReleaseFlag = "A3"
                   },
                   new ProductionOrderStatus
                   {
                        CompleteFlag = "A4",
                        FinishDate = DateTime.Now,
                        OrderQuantity = 12.34D,
                        OrderQuantityUom = "A5",
                        ProductionOrderNumber = "0000000123456",
                        ReleaseFlag = "A6"
                   }
                }
            };

            return returnVal;
        }

        public ProductionOrderStatusResponse RetrieveProductionOrdresStatusShortSequence()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Inside RetrieveProductionOrdresStatusShortSequence()");

            var productionOrderId = Guid.NewGuid().ToString();
            var sagaReferenceId = Guid.NewGuid().ToString();

            Console.WriteLine($"ProductionOrderId={productionOrderId}");
            Console.WriteLine($"SagaReferenceId={sagaReferenceId}");
            Console.WriteLine("TEST ONLY. NO DB INTERACTION!!!");

            var returnVal = new ProductionOrderStatusResponse
            {
                ProductionOrderId = productionOrderId,
                SagaReferenceId = sagaReferenceId,
                Payload = new[]
                {
                    new ProductionOrderStatus
                    {
                        CompleteFlag = "B1",
                        FinishDate = DateTime.Now,
                        OrderQuantity = 34.56D,
                        OrderQuantityUom = "B2",
                        ProductionOrderNumber = "0000000234567",
                        ReleaseFlag = "B3"
                    },
                    new ProductionOrderStatus
                    {
                        CompleteFlag = "B4",
                        FinishDate = DateTime.Now,
                        OrderQuantity = 34.56D,
                        OrderQuantityUom = "B5",
                        ProductionOrderNumber = "0000000234567",
                        ReleaseFlag = "B6"
                    }
                }
            };

            return returnVal;
        }

        public void UpdateMaterialMaster(MaterialMasterUpdateRequest request)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Inside UpdateMaterialMaster()");

            var sb = new StringBuilder();
            sb.AppendLine($"OldMaterialNumber={request.MaterialMasterUpdateArray[0].OldMaterialNumber}");
            sb.AppendLine($"PrimeGtin={request.MaterialMasterUpdateArray[0].PrimeGtin}");
            sb.AppendLine($"RejectCode={request.MaterialMasterUpdateArray[0].RejectCode}");
            sb.AppendLine($"RejectGtin={request.MaterialMasterUpdateArray[0].RejectGtin}");
            sb.AppendLine($"SapDescription0={request.MaterialMasterUpdateArray[0].SapDescription0}");
            sb.AppendLine($"SapDescription1={request.MaterialMasterUpdateArray[0].SapDescription1}");
            sb.AppendLine($"SapMaterialNumber={request.MaterialMasterUpdateArray[0].SapMaterialNumber}");
            sb.AppendLine($"SprasIsoLanguage0={request.MaterialMasterUpdateArray[0].SprasIsoLanguage0}");
            sb.AppendLine($"SprasIsoLanguage1={request.MaterialMasterUpdateArray[0].SprasIsoLanguage1}");
            sb.AppendLine($"SprasLanguage0={request.MaterialMasterUpdateArray[0].SprasLanguage0}");
            sb.AppendLine($"SprasLanguage1={request.MaterialMasterUpdateArray[0].SprasLanguage1}");

            Console.WriteLine(sb.ToString());
            Console.WriteLine("TEST ONLY. NO DB INTERACTION!!!");
        }

        public WorkOrderCompletionResponse CompleteWorkOrder(WorkOrderCompletionRequest request)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Inside CompleteWorkOrder()");
            Console.WriteLine("TEST ONLY. NO DB INTERACTION!!!");

            return new WorkOrderCompletionResponse
            {
                LotNumber = "123456/12",
                ItemNumber = "test1",
                WorkOrder = "test2"
            };
        }
    }
}