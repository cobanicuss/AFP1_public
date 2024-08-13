using System;
using System.Collections.Generic;
using NServiceBus;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.File.Watcher.Messages;
using Spm.OrrSys.Messages;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.OrrSys.TestClient
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("To exit, Ctrl + C");
            Console.WriteLine("Send a NSB message to Spm.OrrSys.Service");
            Console.WriteLine("alt A) To send Product-Achievement-Command press Alt A.");
            Console.WriteLine("alt D) To send Production-Order-Status-Command press Alt D.");
            Console.WriteLine("sft D) To send Production-Order-Status-Reset-Command press SHIFT D.");
            Console.WriteLine("       (First change sheduled time in OrrSys.OrrSysProcessVariable.)");
            Console.WriteLine("sft C) To send Cache-Map-Refresh-Command press Shift C.");
            Console.WriteLine("sft L) To get Test-Certificate-data press Shift L.");
            Console.WriteLine("======================================");

            var busConfiguration = new BusConfiguration();
            busConfiguration.EndpointName("Spm.OrrSys.TestClient");
            busConfiguration.UseSerialization<XmlSerializer>();
            busConfiguration.EnableInstallers();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            LogManager.Use<Log4NetFactory>(); /* NULL logging configuration to switch NServiceBus logging off */

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey();

                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'a' || cki.KeyChar == 'A'))
                    {
                        SendProductAchievementCommand(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'd' || cki.KeyChar == 'D'))
                    {
                        ProductionOrderStatusCommand(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Shift) != 0 && (cki.KeyChar == 'd' || cki.KeyChar == 'D'))
                    {
                        ProductionOrderStatusResetEvent(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Shift) != 0 && (cki.KeyChar == 'c' || cki.KeyChar == 'C'))
                    {
                        SendCacheMapRefreshCommand(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Shift) != 0 && (cki.KeyChar == 'l' || cki.KeyChar == 'L'))
                    {
                        TestCertificateTestServiceMethod();
                    }

                } while (cki.Key != ConsoleKey.Escape);
            }
        }

        private static void TestCertificateTestServiceMethod()
        {
            var test = new TestCertificateServiceMethodTest();
            var dtoList = test.GetData();
            var pdfData = test.GetPdf(dtoList);

            Console.WriteLine($"=> byte[].length={pdfData.Length}");
        }

        private static void SendCacheMapRefreshCommand(IBus bus)
        {
            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new CacheMapUpdateRequestCommand message");
            Console.WriteLine("From OrrSys to Spm.OrrSys.Service.");
            Console.WriteLine("Refresh db tables: from OrrSys to Spm.File.Watcher");

            var message = new CacheMapUpdateRequestCommand
            {
                FileWatcherSagaId = FileWatcherSagaInitConst.FileWatcherSagaId
            };

            bus.Send(EndPointName.SpmOrrSysService, message);
        }

        public static void ProductionOrderStatusResetEvent(IBus bus)
        {
            var message = new ProductionOrderStatusRuntimeReset
            {
                OrrSysSchedulerSagaInitId = OrrSysSagaInitConst.OrrSysSchedulerSagaId
            };

            bus.Send(EndPointName.SpmOrrSysService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new ProductionOrderStatusRuntimeReset message");
            Console.WriteLine("From OrrSys to SAGA.");
            Console.WriteLine($"SagaId={message.OrrSysSchedulerSagaInitId}");
            Console.WriteLine("Reset runtime to db value");
        }

        public static void ProductionOrderStatusCommand(IBus bus)
        {
            var payload = new ProductionOrderStatusPayload
            {
                ProductionOrderStatusPayloadItem = new List<ProductionOrderStatusPayloadItem>
                {
                    new ProductionOrderStatusPayloadItem
                    {
                        CompleteFlag = "test2",
                        FinishDate = DateTime.Now,
                        ProductionOrderNumber = "Test1"
                    }
                }
            };

            var message = new ProductionOrderStatusCommand
            {
                SagaReferenceId = Guid.NewGuid().ToString(),
                ProductionOrderId = Guid.NewGuid().ToString(),
                Payload = payload
            };

            bus.Send(EndPointName.SpmOrrSysService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new ProductionOrderStatusCommand message");
            Console.WriteLine("From OrrSys to SAGA.");
            Console.WriteLine($"ProductionOrderId={message.ProductionOrderId}");
            Console.WriteLine($"SagaReferenceId={message.SagaReferenceId}");
        }

        public static void SendProductAchievementCommand(IBus bus)
        {
            var lotNumber = TestClientFile.CreateLotNumber();

            var payload = CreateInventoryMovementPayload(lotNumber);

            var message = new ProductAchievementCommand
            {
                LotNumber = lotNumber,
                SagaReferenceId = Guid.NewGuid().ToString(),
                Payload = payload
            };

            bus.Send(EndPointName.SpmOrrSysService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new ProductAchievementCommand message");
            Console.WriteLine("From OrrSys to SAGA.");
            Console.WriteLine($"With LotNumber={message.LotNumber}");
        }

        private static InventoryMovementPayload CreateInventoryMovementPayload(string id)
        {
            var payload = new InventoryMovementPayload
            {
                QuantityUnitCode = "C62",
                CustomerArticleNumber = "test1",
                IdentifierText = "test2",
                InventoryMovementLineNumber = "test4",
                InventoryNatureCode = "test5",
                LocationIdentifierText = "test6",
                MovementDateTime = "test7",
                MovementDateTimeFormatText = "test8",
                MovementHeaderNoteText = "test9",
                MovementNumber = "test10",
                PackageWarehouseLocationText = "test11",
                QuantityText = "123",
                StockControllerPartyText = "test17",
                SupplierArticleNumber = "test19",
                WarehouseSerialNumber = id,
                PartPack = "PA"
            };
            return payload;
        }
    }
}