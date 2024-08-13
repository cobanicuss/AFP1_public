using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class MaterialMasterFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<MaterialMasterFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new MaterialMasterFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("Material-Master from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendMaterialMasterAuditCommandMessage())
                    .And(_ => HandlerMustSendToSpmService())

                .BDDfy();
        }

        private void HandlerMustSendMaterialMasterAuditCommandMessage()
        {
            _handler.ExpectSend<MaterialMasterAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendToSpmService()
        {
            _handler.ExpectSend<MaterialMasterResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new MaterialMasterSapResponse { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}