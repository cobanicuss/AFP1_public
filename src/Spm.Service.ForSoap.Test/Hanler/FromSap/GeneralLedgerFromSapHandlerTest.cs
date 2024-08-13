using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.Service.ForSoap.Handlers.FromSap;
using Spm.Service.ForSoap.Messages;
using Spm.Service.Messages;
using TestStack.BDDfy;

namespace Spm.Service.ForSoap.Test.Hanler.FromSap
{
    [TestFixture]
    public class GeneralLedgerFromSapHandlerTest
    {
        private NServiceBus.Testing.Handler<GeneralLedgerFromSapHandler> _handler;

        [SetUp]
        public void Setup()
        {
            NServiceBus.Testing.Test.Initialize();

            _handler = NServiceBus.Testing.Test.Handler(bus => new GeneralLedgerFromSapHandler(bus));
        }

        [Test]
        public void HandlerMustSendTheseMessages()
        {
            this.Given("General-Ledger from SAP handler")
                .When("Handler is called")
                .Then(_ => HandlerMustSendGeneralLedgerAuditCommandMessage())
                    .And(_ => HandlerMustSendGeneralLedgerResponseCommand())

                .BDDfy();
        }

        private void HandlerMustSendGeneralLedgerAuditCommandMessage()
        {
            _handler.ExpectSend<GeneralLedgerAuditCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId);
        }

        private void HandlerMustSendGeneralLedgerResponseCommand()
        {
            _handler.ExpectSend<GeneralLedgerResponseCommand>(command => command.SagaReferenceId == Constants.SagaReferenceId)
                .OnMessage(new GeneralLedgerSapResponse { SagaReferenceId = Constants.SagaReferenceId });
        }
    }
}