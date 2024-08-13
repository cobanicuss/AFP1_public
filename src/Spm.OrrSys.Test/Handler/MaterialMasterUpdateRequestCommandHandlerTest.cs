using System;
using Moq;
using NUnit.Framework;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.Handlers;
using Spm.OrrSys.Service.Soap;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.Shared;
using TestStack.BDDfy;

namespace Spm.OrrSys.Test.Handler
{
    [TestFixture]
    public class MaterialMasterUpdateRequestCommandHandlerTest
    {
        private Mock<IDoMaterialMasterCommunication> _soap;
        private NServiceBus.Testing.Handler<MaterialMasterUpdateRequestCommandHandler> _handlerUnderTest;

        [SetUp]
        public void Setup()
        {
            _soap = new Mock<IDoMaterialMasterCommunication>();
            _soap.Setup(x => x.SendSoapMessageForMatrialMasterUpdate(It.IsAny<MaterialMasterUpdateRequestCommand>()));

            NServiceBus.Testing.Test.Initialize();

            _handlerUnderTest = NServiceBus.Testing.Test.Handler(bus => new MaterialMasterUpdateRequestCommandHandler(bus, _soap.Object));
        }

        [Test]
        public void InboundMaterialMasterUpdateRequestReceivedFromSap()
        {
            this.Given("Material-Master-Update-Request from SAP")
                .When("Handler is called")
                .Then(_ => AuditCommandMustBeSend())
                    .And(_ => SoapMessageMustBeSend())

                .BDDfy();
        }

        private void SoapMessageMustBeSend()
        {
            _soap.Verify(x => x.SendSoapMessageForMatrialMasterUpdate(It.IsAny<MaterialMasterUpdateRequestCommand>()), Times.Once());
        }

        private void AuditCommandMustBeSend()
        {
            _handlerUnderTest
                .ExpectSend<MaterialMasterUpdateAuditCommand>(command =>
                    command.ShortItemNumber == Constants.ShortItemNumber
                    && command.InboundId == Constants.InboundId
                    && Math.Abs(command.Leg - 2.0F) < 0.01
                    && command.DateTimeMessageSendToHere.Date == DateTime.Now.Date
                    && command.FromEndpoint == EndPointName.SpmServiceForSoap
                    && command.MessageType == typeof(MaterialMasterUpdateRequestCommand).FullName
                    && command.Action == (int)AuditAction.MessageImplemented
                    && !string.IsNullOrEmpty(command.MessageData)
                    )
                .OnMessage(new MaterialMasterUpdateRequestCommand
                {
                    ShortItemNumber = Constants.ShortItemNumber,
                    InboundId = Constants.InboundId
                });
        }
    }
}