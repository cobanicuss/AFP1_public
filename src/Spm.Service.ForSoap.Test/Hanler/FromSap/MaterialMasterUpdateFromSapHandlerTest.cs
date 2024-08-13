using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class MaterialMasterUpdateFromSapHandlerTest
    {
        private Mock<ISendResponseOnRequestToSap> _toSapMock;
        private NServiceBus.Testing.Handler<MaterialMasterUpdateFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendResponseOnRequestToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<ResponseToSapRequestCommand>()));

            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new MaterialMasterUpdateFromSapHandler(bus, _toSapMock.Object));
        }

        [Test]
        public void HandlerMustSendAuditlogTwice()
        {
            this.Given("Material-Master-Update from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendMaterialMasterUpdateAuditCommandMessage())

                .BDDfy();
        }

        [Test]
        public void HandlerMustImplementThisLogic()
        {
            this.Given("Material-Master-Update from SAP handler")
                .When("Handler is called")
                .Then(_ => RequestCommandMustBeSendToOrrSysService())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendMaterialMasterUpdateAuditCommandMessage()
        {
            _handler.ExpectSend<MaterialMasterUpdateAuditCommand>(command => command.Action == (int) AuditAction.RequestReceived)
                .ExpectSend<MaterialMasterUpdateAuditCommand>(command => command.Action == (int) AuditAction.SendResponseToSap)
                .OnMessage(new MaterialMasterUpdateSapRequest { ShortItemNumber = Constants.ShortItemNumber });
        }

        private void RequestCommandMustBeSendToOrrSysService()
        {
            _handler.ExpectSend<MaterialMasterUpdateRequestCommand>(command => command.ShortItemNumber == Constants.ShortItemNumber)
                .OnMessage(new MaterialMasterUpdateSapRequest { ShortItemNumber = Constants.ShortItemNumber });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<ResponseToSapRequestCommand>()), Times.Once());
        }
    }
}