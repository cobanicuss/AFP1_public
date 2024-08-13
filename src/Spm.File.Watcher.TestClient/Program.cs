using System;
using System.Collections.Generic;
using NServiceBus;
using NServiceBus.Features;
using Spm.Service.Messages;
using Spm.Shared;
using Spm.Shared.Payloads;

namespace Spm.File.Watcher.TestClient
{
    public class Program
    {
        public static void Main()
        {
            var busConfiguration = new BusConfiguration();
            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.DisableFeature<TimeoutManager>();
            busConfiguration.EndpointName("Spm.File.Watcher.TestClient");
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            Console.WriteLine("======================================");
            Console.WriteLine("Send a NSB message to Saga (Spm.Service)");
            Console.WriteLine("To exit, Ctrl + C");
            Console.WriteLine("B) To send Purchase-Order-Create press Alt B.");
            Console.WriteLine("C) To send Purchase-Order-Change press Alt C.");
            Console.WriteLine("H) To send Goods-Receipt press Alt H.");
            Console.WriteLine("======================================");

            using (var bus = Bus.Create(busConfiguration).Start())
            {
                ConsoleKeyInfo cki;
                do
                {
                    cki = Console.ReadKey();

                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'b' || cki.KeyChar == 'B'))
                    {
                        SendPurchaseOrderCreateCommand(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'c' || cki.KeyChar == 'C'))
                    {
                        SendPurchaseOrderChangeCommand(bus);
                    }
                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'h' || cki.KeyChar == 'H'))
                    {
                        SendGoodsReceiptCommand(bus);
                    }

                } while (cki.Key != ConsoleKey.Escape);
            }
        }
        
        static void SendPurchaseOrderCreateCommand(IBus bus)
        {
            var purchaseOrderCreateNumber = TestClientFile.CreatePurchaseOrderNumber();

            var payloadItem = new List<PurchaseOrderPayloadItem>
            {
                new PurchaseOrderPayloadItem
                {
                    EdiDc40Docnum = purchaseOrderCreateNumber
                }
            };

            var payload = new PurchaseOrderPayload {PurchaseOrderPayloadItemList = payloadItem};
            var message = new PurchaseOrderCreateCommand
            {
                PurchaseOrderNumber = purchaseOrderCreateNumber,
                Payload = payload
            };

            bus.Send(EndPointName.SpmService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new PurchaseOrderCreateCommand message");
            Console.WriteLine("From File-Watcher to SAGA (SAP).");
            Console.WriteLine($"With PurchaseOrderCreateNumber={purchaseOrderCreateNumber}");
        }

        static void SendPurchaseOrderChangeCommand(IBus bus)
        {
            var purchaseOrderChangeNumber = TestClientFile.CreatePurchaseOrderNumber();

            var payloadList = new List<PurchaseOrderPayloadItem>
            {
               new PurchaseOrderPayloadItem
               {
                    EdiDc40Docnum = purchaseOrderChangeNumber
               }
            };

            var payload = new PurchaseOrderPayload {PurchaseOrderPayloadItemList = payloadList};
            var message = new PurchaseOrderChangeCommand
            {
                PurchaseOrderNumber = purchaseOrderChangeNumber,
                Payload = payload,
            };

            bus.Send(EndPointName.SpmService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new PurchaseOrderChangeCommand message");
            Console.WriteLine("From File-Watcher to SAGA (SAP).");
            Console.WriteLine($"With PurchaseOrderChangeNumber={purchaseOrderChangeNumber}");
        }

        static void SendGoodsReceiptCommand(IBus bus)
        {
            var goodsReceiptNumber = TestClientFile.CreateGoodsReceiptId();

            var payloadList = new List<GoodsPayloadItem>
            {
               new GoodsPayloadItem
               {
                   ItemCreatePoNumber = TestClientFile.CreatePurchaseOrderNumber(),
                   HeadRefDocNo = "Test1",
                   ItemCreateStgeLoc = "Test2",
                   HeadDocDate = "123123123"
               }
            };

            var payload = new GoodsPayload { GoodsPayloadItem = payloadList };

            var message = new GoodsCommand
            {
                GoodsReceiptId = goodsReceiptNumber,
                Payload = payload
            };

            bus.Send(EndPointName.SpmService, message);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent a new GoodsReceiptCommand message");
            Console.WriteLine("From File-Watcher to SAGA (SAP).");
            Console.WriteLine($"With GoodsReceiptNumber={goodsReceiptNumber}");
        }
    }
}