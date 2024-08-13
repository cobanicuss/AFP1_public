using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace OrrSysDataInterfacingAsWcfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseAddress1 = new Uri("http://localhost/OrrSysDataInterface/DataInterfacingService.svc");
            var selfHost1 = new ServiceHost(typeof(OrrSysDataInterfacingService), baseAddress1);

            try
            {
                var binding = new BasicHttpBinding
                {
                    MaxBufferPoolSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                    ReaderQuotas =
                    {
                        MaxDepth = 10487760,
                        MaxStringContentLength = 10487760,
                        MaxBytesPerRead = 10487760,
                        MaxArrayLength = 10487760,
                        MaxNameTableCharCount = 10487760
                    }
                };

                selfHost1.AddServiceEndpoint(typeof(IDataInterfacingService), binding, "");

                var smb1 = new ServiceMetadataBehavior { HttpGetEnabled = true };

                selfHost1.Description.Behaviors.Add(smb1);
                selfHost1.Open();

                Console.WriteLine("The service is ready.");
                Console.WriteLine($"Service is now listening on: {baseAddress1}");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                selfHost1.Close();
            }
            catch (CommunicationException ex)
            {
                selfHost1.Abort();

                Console.WriteLine($"An exception occurred: {ex.Message}");
                Console.ReadLine();
            }
        }
    }
}