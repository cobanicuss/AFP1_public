using System;
using System.IdentityModel.Selectors;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using SapAsWcf.GeneralLedger;
using SapAsWcf.GoodsReceipt;
using SapAsWcf.MaterialMaster;
using SapAsWcf.ProductAchievement;
using SapAsWcf.ProductionOrderStatus;
using SapAsWcf.PurchaseOrderChange;
using SapAsWcf.PurchaseOrderCreate;
using SapAsWcf.ResponseToSapRequest;
using SapAsWcf.TestCertificate;
using Spm.Shared;

namespace SapAsWcfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseAddress1 = new Uri("http://localhost:8730/SapAsWcf/ProductAchievementService");
            var selfHost1 = new ServiceHost(typeof(ProductAchievementService), baseAddress1);

            var baseAddress2 = new Uri("http://localhost:8730/SapAsWcf/PurchaseOrderCreateService");
            var selfHost2 = new ServiceHost(typeof(PurchaseOrderCreateService), baseAddress2);

            var baseAddress3 = new Uri("http://localhost:8730/SapAsWcf/PurchaseOrderChangeService");
            var selfHost3 = new ServiceHost(typeof(PurchaseOrderChangeService), baseAddress3);

            var baseAddress4 = new Uri("http://localhost:8730/SapAsWcf/ProductionOrderStatusService");
            var selfHost4 = new ServiceHost(typeof(ProductionOrderStatusService), baseAddress4);

            var baseAddress5 = new Uri("http://localhost:8730/SapAsWcf/GoodsReceiptService");
            var selfHost5 = new ServiceHost(typeof(GoodsReceiptService), baseAddress5);

            var baseAddress6 = new Uri("http://localhost:8730/SapAsWcf/ResponseToSapRequestService");
            var selfHost6 = new ServiceHost(typeof(ResponseToSapRequestService), baseAddress6);

            var baseAddress7 = new Uri("http://localhost:8730/SapAsWcf/GeneralLedgerService");
            var selfHost7 = new ServiceHost(typeof(GeneralLedgerService), baseAddress7);

            var baseAddress8 = new Uri("http://localhost:8730/SapAsWcf/MaterialMasterService");
            var selfHost8 = new ServiceHost(typeof(MaterialMasterService), baseAddress8);

            var baseAddress9 = new Uri("http://localhost:8730/SapAsWcf/TestCertificateService");
            var selfHost9 = new ServiceHost(typeof(TestCertificateService), baseAddress9);

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
                    },
                    Security =
                    {
                        Mode = BasicHttpSecurityMode.TransportCredentialOnly,
                        Transport = {ClientCredentialType = HttpClientCredentialType.Basic}
                    }
                };

                selfHost1.AddServiceEndpoint(typeof(InventoryMovement_OB_Async_MI), binding, "");
                selfHost1.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost1.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost2.AddServiceEndpoint(typeof(PORDCR103_OB_Async_MI), binding, "");
                selfHost2.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost2.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost3.AddServiceEndpoint(typeof(PORDCH03_OB_Async_MI), binding, "");
                selfHost3.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost3.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost4.AddServiceEndpoint(typeof(ZPP_CHNG_OB_Async_MI), binding, "");
                selfHost4.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost4.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost5.AddServiceEndpoint(typeof(ZMBGMCR02_EXTND_OB_Async_MI), binding, "");
                selfHost5.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost5.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost6.AddServiceEndpoint(typeof(SYSTAT01_OB_Async_MI), binding, "");
                selfHost6.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost6.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost7.AddServiceEndpoint(typeof(ACC_DOCUMENT03_OB_Async_MI), binding, "");
                selfHost7.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost7.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost8.AddServiceEndpoint(typeof(MATMAS05_ZMATMAS5_OB_Async_MI), binding, "");
                selfHost8.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost8.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                selfHost9.AddServiceEndpoint(typeof(TestCert.Certificate_OB_Async_Enh_MI), binding, "");
                selfHost9.Credentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
                selfHost9.Credentials.UserNameAuthentication.CustomUserNamePasswordValidator = new SecurityValidator();

                var smb1 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb2 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb3 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb4 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb5 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb6 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb7 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb8 = new ServiceMetadataBehavior { HttpGetEnabled = true };
                var smb9 = new ServiceMetadataBehavior { HttpGetEnabled = true };

                selfHost1.Description.Behaviors.Add(smb1);
                selfHost1.Open();

                selfHost2.Description.Behaviors.Add(smb2);
                selfHost2.Open();

                selfHost3.Description.Behaviors.Add(smb3);
                selfHost3.Open();

                selfHost4.Description.Behaviors.Add(smb4);
                selfHost4.Open();

                selfHost5.Description.Behaviors.Add(smb5);
                selfHost5.Open();

                selfHost6.Description.Behaviors.Add(smb6);
                selfHost6.Open();

                selfHost7.Description.Behaviors.Add(smb7);
                selfHost7.Open();

                selfHost8.Description.Behaviors.Add(smb8);
                selfHost8.Open();

                selfHost9.Description.Behaviors.Add(smb9);
                selfHost9.Open();

                Console.WriteLine("The services is ready.");
                Console.WriteLine($"Alive on: {baseAddress1}");
                Console.WriteLine($"Alive on: {baseAddress2}");
                Console.WriteLine($"Alive on: {baseAddress3}");
                Console.WriteLine($"Alive on: {baseAddress4}");
                Console.WriteLine($"Alive on: {baseAddress5}");
                Console.WriteLine($"Alive on: {baseAddress6}");
                Console.WriteLine($"Alive on: {baseAddress7}");
                Console.WriteLine($"Alive on: {baseAddress8}");
                Console.WriteLine($"Alive on: {baseAddress9}");
                Console.WriteLine("Press <ENTER> to terminate services.");
                Console.WriteLine();
                Console.ReadLine();

                selfHost1.Close();
                selfHost2.Close();
                selfHost3.Close();
                selfHost4.Close();
                selfHost5.Close();
                selfHost6.Close();
                selfHost7.Close();
                selfHost8.Close();
                selfHost9.Close();
            }
            catch (CommunicationException ex)
            {
                selfHost1.Abort();
                selfHost2.Abort();
                selfHost3.Abort();
                selfHost4.Abort();
                selfHost5.Abort();
                selfHost6.Abort();
                selfHost7.Abort();
                selfHost8.Abort();
                selfHost9.Abort();

                Console.WriteLine($"An exception occurred: {ex.Message}");
                Console.ReadLine();
            }
        }
    }

    public class SecurityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            var failedUn = false;
            var failedPw = false;
            var invalidUn = !string.Equals(userName, Constants.SapSoapDevAndTestUserName);
            var invalidPw = !string.Equals(password, Constants.SapSoapDevAndTestPassword);

            if (invalidUn) { failedUn = true; }

            if (invalidPw) { failedPw = true; }

            if (failedUn && failedPw) { Console.WriteLine("ERROR: Invalid Username and Password."); return; }

            if (failedUn) { Console.WriteLine("ERROR: Invalid Username."); return; }

            if (failedPw) Console.WriteLine("ERROR: Invalid Password.");

            Console.WriteLine("COOL: Un and Pw OK!");
        }
    }
}