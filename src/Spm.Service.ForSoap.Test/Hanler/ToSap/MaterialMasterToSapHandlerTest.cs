using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.ToSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.ForSoap.SendToSapImplementation;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.ToSap
{
    [TestFixture]
    public class MaterialMasterToSapHandlerTest
    {
        private Mock<ISendMaterialMasterToSap> _toSapMock;

        [SetUp]
        public void Setup()
        {
            _toSapMock = new Mock<ISendMaterialMasterToSap>();
            _toSapMock.Setup(x => x.SendSoapMessageToSap(It.IsAny<MaterialMasterSapCommand>()));

            NServiceBus.Testing.Test.Initialize();
        }

        [Test]
        public void HandlerMustSendAuditlog()
        {
            this.Given("Material-Master to SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendMaterialMasterAuditCommandMessage())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void HandlerMustSendMaterialMasterAuditCommandMessage()
        {
            NServiceBus.Testing.Test.Handler(bus => new MaterialMasterToSapHandler(bus, _toSapMock.Object))
                .ExpectSend<MaterialMasterAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new MaterialMasterSapCommand { SagaReferenceId = Constants.SagaReferenceId });
        }

        private void SoapMessageMustBeSend()
        {
            _toSapMock.Verify(x => x.SendSoapMessageToSap(It.IsAny<MaterialMasterSapCommand>()), Times.Once());
        }
    }
}
