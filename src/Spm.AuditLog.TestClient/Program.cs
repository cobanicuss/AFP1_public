using System;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Installation.Environments;
using Spm.AuditLog.Messages;
using Spm.Shared;

namespace Spm.AuditLog.TestClient
{
    public class Program
    {
        public static void Main()
        {
        //    Configure.Serialization.Xml();
        //    Configure.Features.Disable<Sagas>();
        //    Configure.Features.Disable<TimeoutManager>();

        //    var configure = Configure.With();
        //    configure.DefineEndpointName("Spm.AuditLog.TestClient");
        //    configure.DefaultBuilder();
        //    configure.UseTransport<Msmq>();

        //    using (var startableBus = configure.UnicastBus().CreateBus())
        //    {
        //        var bus = startableBus.Start(() => configure.ForInstallationOn<Windows>().Install());

        //        SendProductAchievementAuditCommand(bus);
        //    }
        }

        //static void SendProductAchievementAuditCommand(IBus bus)
        //{
        //    Console.WriteLine("========================================================================");
        //    Console.WriteLine("Press 'Enter' to send ProductAchievementAuditCommand.");
        //    Console.WriteLine("To exit press 'Ctrl + C'");

        //    while (Console.ReadLine() != null)
        //    {
        //        var productAchievementAuditCommand = new ProductAchievementAuditCommand
        //        {
        //            LotNumber = TestClientNumber.CreateLotNumber(),
        //            FromEndpoint = "TestClient For AuditLog",
        //            DateTimeMessageSendToHere = DateTime.Now,
        //            Action = (int)AuditAction.TestAudit,
        //            MessageData = "Logging=Test"
        //        };

        //        bus.Send("Spm.AuditLog.Service", productAchievementAuditCommand);

        //        Console.WriteLine("========================================================================");
        //        Console.WriteLine("Sent a new ProductAchievementAuditCommand message with:");
        //        Console.WriteLine("LotNumber={0}", productAchievementAuditCommand.LotNumber);
        //    }
        //}
    }
}
