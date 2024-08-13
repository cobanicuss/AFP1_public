using System;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.Service.TestClient
{
    public class Program
    {
        private static string _lotNumber;

        public static void Main()
        {
            Configure.Serialization.Xml();
            Configure.Features.Enable<Sagas>();

            var configure = Configure.With();
            configure.DefineEndpointName("Spm.Service.TestClient");
            configure.DefaultBuilder();
            configure.InMemorySagaPersister();
            configure.UseInMemoryTimeoutPersister();
            configure.InMemorySubscriptionStorage();
            configure.UseTransport<Msmq>();

            using (var startableBus = configure.UnicastBus().CreateBus())
            {
                var bus = startableBus.Start(() => configure.ForInstallationOn<Windows>().Install());

                ConsoleKeyInfo cki;
                Console.TreatControlCAsInput = true;

                Console.WriteLine("Press ALT, S to send START SAGA.");
                Console.WriteLine("Press the Escape (Esc) key to quit.");

                do
                {
                    cki = Console.ReadKey();

                    if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && cki.KeyChar == 's')
                    {
                        SendStartSaga(bus);
                    }
                    else
                    {
                        Console.WriteLine("Wrong Key");
                    }

                } while (cki.Key != ConsoleKey.Escape);
            }
        }

        public static void SendStartSaga(IBus bus)
        {
            _lotNumber = TestClientFile.CreateLotNumber();

            var startSaga = new ProductAchievementCommand
            {
                LotNumber =_lotNumber,
                SagaReferenceId = Guid.NewGuid().ToString()
                //InventoryMovementHeader = new InventoryMovementHeader(),
                //InventoryMovementLine = new InventoryMovementLine()
            };

            bus.Send("Spm.Service", startSaga);

            Console.WriteLine("========================================================================");
            Console.WriteLine("Sent SAGA START with LotNumber: {0}", startSaga.LotNumber);
        }
    }
}
