using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Spm.Service.ReceiveFromSap.TestClient.SpmWebServiceProxy;
using Spm.Shared;

namespace Spm.Service.ReceiveFromSap.TestClient
{
    public class Program
    {
        private static string[] _productAchievementList;
        private static string[] _purchaseOrderCreateList;
        private static string[] _purchaseOrderChangeList;
        private static string[] _productionOrderIdList;
        private static string[] _goodsReceiptIdList;
        private static string[] _generalLedgerIdList;
        private static string[] _materialMasterIdList;
        private static string[] _testCertificateList;

        private static IMockServiceRepository _mockServiceRepository;

        public static void Main(string[] args)
        {
            _mockServiceRepository = new MockServiceRepository();

            Console.WriteLine("======================================");
            Console.WriteLine("Send SOAP messages to SpmWebService");
            Console.WriteLine("To exit, Ctrl + C");
            Console.WriteLine("A)_To send Product-Acheivement-Response press Alt A.");
            Console.WriteLine("B)_To send Purchase-Order-Create-Response press Alt B.");
            Console.WriteLine("C)_To send Purchase-Order-Change-Response press Alt C.");
            Console.WriteLine("D)_To send Production-Order-Status-Response press Alt D.");
            Console.WriteLine("E)_To send Production-Order-Request press Alt E.");
            Console.WriteLine("F)_To send Planned-Order-Request press Alt F.");
            Console.WriteLine("G)_To send INBOUND Test-Certificate-Request press Alt G.");
            Console.WriteLine("H)_To send Goods-Receipt-Response press Alt H.");
            Console.WriteLine("I)_To send General-Ledger-Response press Alt I.");
            Console.WriteLine("J)_To send Material-Master-Response press Alt J.");
            Console.WriteLine("K)_To send Material-Master-Update-Reqeust press Alt K.");
            Console.WriteLine("L)_To send OUTBOUND Test-Certificate-Trigger-Request press Alt L.");
            Console.WriteLine("sft L)_To send OUTBOUND Test-Certificate-Response press SHIFT L.");
            Console.WriteLine("======================================");

            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();

                if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'a' || cki.KeyChar == 'A'))
                {
                    const int sysEnum = (int)SimSystemEnum.ProductAchievement;

                    _productAchievementList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_productAchievementList.Any())
                    {
                        Console.WriteLine("No ProductAchievement numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendProductAchievementMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _productAchievementList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _productAchievementList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'b' || cki.KeyChar == 'B'))
                {
                    const int sysEnum = (int)SimSystemEnum.PuchaseOrderCreate;

                    _purchaseOrderCreateList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_purchaseOrderCreateList.Any())
                    {
                        Console.WriteLine("No PuchaseOrderCreate numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendPurchaseOrderCreateMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _purchaseOrderCreateList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _purchaseOrderCreateList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'c' || cki.KeyChar == 'C'))
                {
                    const int sysEnum = (int)SimSystemEnum.PurchaseOrderChange;

                    _purchaseOrderChangeList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_purchaseOrderChangeList.Any())
                    {
                        Console.WriteLine("No PuchaseOrderChange numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendPurchaseOrderChangeMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _purchaseOrderChangeList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _purchaseOrderChangeList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'd' || cki.KeyChar == 'D'))
                {
                    const int sysEnum = (int)SimSystemEnum.ProductionOrderStatus;

                    _productionOrderIdList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_productionOrderIdList.Any())
                    {
                        Console.WriteLine("No ProductionOrderStatus numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendProductionOrderStatusMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _productionOrderIdList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _productionOrderIdList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'e' || cki.KeyChar == 'E'))
                {
                    using (var svc = new SpmWebService()) { SendProductionOrderRequestMessage(svc); }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'f' || cki.KeyChar == 'F'))
                {
                    using (var svc = new SpmWebService()) { SendPlannedOrderRequestMessage(svc); }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'g' || cki.KeyChar == 'G'))
                {
                    const int sysEnum = (int)SimSystemEnum.ResponseToSapRequest;

                    var message = CreateSoapMessage.TestCertificate();
                    var sagaReferenceId = message.IDOC.EDI_DC40.DOCNUM;

                    _mockServiceRepository.AddNumber(sysEnum, sagaReferenceId);

                    var serializer = new DataContractSerializer(typeof(ZOBTC01));
                    var sb = new StringBuilder();
                    string soapAsString;

                    using (var writer = XmlWriter.Create(sb))
                    {
                        serializer.WriteObject(writer, message);
                        writer.Flush();
                        soapAsString = sb.ToString();
                    }

                    _mockServiceRepository.AddUpdateSoapData(sysEnum, sagaReferenceId, soapAsString);

                    using (var svc = new SpmWebService()) { SendTestCertificateRequestMessage(svc, message); }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'h' || cki.KeyChar == 'H'))
                {
                    const int sysEnum = (int)SimSystemEnum.GoodsReceipt;

                    _goodsReceiptIdList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_goodsReceiptIdList.Any())
                    {
                        Console.WriteLine("No GoodsReceipt numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendGoodsReceiptMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _goodsReceiptIdList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _goodsReceiptIdList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'i' || cki.KeyChar == 'I'))
                {
                    const int sysEnum = (int)SimSystemEnum.GeneralLedger;

                    _generalLedgerIdList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_generalLedgerIdList.Any())
                    {
                        Console.WriteLine("No GeneralLedger numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendGeneralLedgerMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _generalLedgerIdList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _generalLedgerIdList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'j' || cki.KeyChar == 'J'))
                {
                    const int sysEnum = (int)SimSystemEnum.MaterialMaster;

                    _materialMasterIdList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_materialMasterIdList.Any())
                    {
                        Console.WriteLine("No MaterialMaster numbers found  to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendMaterialMasterResponseMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _materialMasterIdList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _materialMasterIdList[0]);
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'k' || cki.KeyChar == 'K'))
                {
                    const int sysEnum = (int)SimSystemEnum.ResponseToSapRequest;

                    var message = CreateSoapMessage.MaterialMasterUpdate();
                    var referenceId = message.IDOC.EDI_DC40.DOCNUM;

                    _mockServiceRepository.AddNumber(sysEnum, referenceId);

                    var serializer = new DataContractSerializer(typeof(ZMATMAS5));
                    var sb = new StringBuilder();
                    string soapAsString;

                    using (var writer = XmlWriter.Create(sb))
                    {
                        serializer.WriteObject(writer, message);
                        writer.Flush();
                        soapAsString = sb.ToString();
                    }

                    _mockServiceRepository.AddUpdateSoapData(sysEnum, referenceId, soapAsString);

                    using (var svc = new SpmWebService()) { SendMaterialMasterUpdateRequestMessage(svc, message); }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Alt) != 0 && (cki.KeyChar == 'l' || cki.KeyChar == 'L'))
                {
                    using (var svc = new SpmWebService()) { SendTestCertificateTriggerRequestMessage(svc); }
                }
                else if ((cki.Modifiers & ConsoleModifiers.Shift) != 0 && (cki.KeyChar == 'l' || cki.KeyChar == 'L'))
                {
                    const int sysEnum = (int)SimSystemEnum.TestCertificate;

                    _testCertificateList = _mockServiceRepository.GetNumbers(sysEnum);

                    if (!_testCertificateList.Any())
                    {
                        Console.WriteLine("No TestCerticicate numbers found to process.");
                        Console.WriteLine("Cannot Proceed without a number!");
                        continue;
                    }

                    using (var svc = new SpmWebService()) { SendTestCertificateMessage(svc); }

                    _mockServiceRepository.DeleteNumber(sysEnum, _testCertificateList[0]);
                    _mockServiceRepository.DeleteSoapData(sysEnum, _testCertificateList[0]);
                }
                else
                {
                    Console.WriteLine("There are no more numbers left to process!");
                }

            } while (cki.Key != ConsoleKey.Escape);
        }

        private static void SendTestCertificateMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                if (!_testCertificateList.Any()) return;

                var sagaReferenceId = _testCertificateList[0];
                var message = CreateSoapMessage.TestCertificate(sagaReferenceId);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"SagaReferenceId={sagaReferenceId}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.TestCertificateOutboundResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendProductAchievementMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var message = CreateSoapMessage.ProductAchievement(_productAchievementList[0]);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"SagaReferenceId={_productAchievementList[0]}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.ProductAchievementResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendPurchaseOrderCreateMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                if (!_purchaseOrderCreateList.Any()) return;

                var sagaReferenceId = _purchaseOrderCreateList[0];
                var message = CreateSoapMessage.PurchaseOrderCreate(sagaReferenceId);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"SagaReferenceId={sagaReferenceId}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.PurchaseOrderCreateResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendPurchaseOrderChangeMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var sagaReferenceId = _purchaseOrderChangeList[0];
                var message = CreateSoapMessage.PurchaseOrderChange(sagaReferenceId);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"SagaReferenceId={sagaReferenceId}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.PurchaseOrderChangeResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendProductionOrderStatusMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var message = CreateSoapMessage.ProductionOrderStatus(_productionOrderIdList[0]);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"SagaReferenceId={_productionOrderIdList[0]}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.ProductionOrderStatusResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendProductionOrderRequestMessage(SpmWebService svc)
        {
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.ProductionOrderRequest("ProductionOrder Trigger");

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendPlannedOrderRequestMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.PlannedOrderRequest("PlannedOrder Trigger");

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendTestCertificateTriggerRequestMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.TestCertificateOutboundTriggerRequest("Outbound TestCertifcate Trigger");

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendTestCertificateRequestMessage(SpmWebService svc, ZOBTC01 message)
        {
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"CertificateId={message.IDOC.EDI_DC40.DOCNUM}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.TestCertificateRequest(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendGoodsReceiptMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var message = CreateSoapMessage.GoodsReceipt(_goodsReceiptIdList[0]);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"GoodsReceiptId={_goodsReceiptIdList[0]}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.GoodsReceiptResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendGeneralLedgerMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var message = CreateSoapMessage.GeneralLedger(_generalLedgerIdList[0]);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"GeneralLedgerId={_generalLedgerIdList[0]}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.GeneralLedgerResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendMaterialMasterResponseMessage(SpmWebService svc)
        {
            Console.WriteLine("======================================");
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                var message = CreateSoapMessage.MaterialMaster(_materialMasterIdList[0]);

                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"MaterialMasterId={_materialMasterIdList[0]}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.MaterialMasterCreateResponse(message);

                Console.WriteLine("Success sending a SOAP message");
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        private static void SendMaterialMasterUpdateRequestMessage(SpmWebService svc, ZMATMAS5 message)
        {
            Console.WriteLine($"Invoking web service. Url={svc.Url}");

            try
            {
                Console.WriteLine("Sending SOAP message.");
                Console.WriteLine($"ShortItemNumber={message.IDOC.EDI_DC40.DOCNUM}");
                Console.WriteLine($"Date-Time={DateTime.Now}");

                svc.MaterialMasterUpdateRequest(message);

                Console.WriteLine("Success sending a SOAP message");

            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }
    }
}