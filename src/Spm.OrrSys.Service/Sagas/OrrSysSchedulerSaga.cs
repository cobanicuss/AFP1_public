using System;
using NHibernate.Util;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Saga;
using Spm.AuditLog.Messages;
using Spm.OrrSys.Messages;
using Spm.OrrSys.Service.SagaData;
using Spm.OrrSys.Service.Scheduler;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.Shared;

namespace Spm.OrrSys.Service.Sagas
{
    public class OrrSysSchedulerSaga : Saga<OrrSysSchedulerSagaData>,
        IAmStartedByMessages<OrrSysSchedulerSagaInit>,
        IHandleTimeouts<TimeToSendProductionOrderStatusShortSequence>,
        IHandleTimeouts<TimeToSendProductionOrderStatusLongSequence>,
        IHandleMessages<ProductionOrderStatusRuntimeReset>
    {
        //IMPORTANT: SagaId is a constant (single instance) and is passed INTO the Saga.
        //This is deliberately abstracted away for maintainability.

        private static readonly ILog Logger = LogManager.GetLogger(typeof(OrrSysSchedulerSaga));

        public IDoProductionOrderStatusCommunication ProductionOrderStatus { get; set; }
        public IScheduleProductionOrderStutus ScheduleProductionOrderStutus { get; set; }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<OrrSysSchedulerSagaData> mapper)
        {
            mapper.ConfigureMapping<OrrSysSchedulerSagaInit>(m => m.OrrSysSchedulerSagaInitId).ToSaga(s => s.OrrSysSchedulerSagaInitId);
            mapper.ConfigureMapping<ProductionOrderStatusRuntimeReset>(m => m.OrrSysSchedulerSagaInitId).ToSaga(s => s.OrrSysSchedulerSagaInitId);
        }

        public void Handle(OrrSysSchedulerSagaInit message)
        {
            Logger.Info("======================================");
            Logger.Info("Saga started.");

            Data.OrrSysSchedulerSagaInitId = message.OrrSysSchedulerSagaInitId;
            Logger.Info($"SagaId={Data.OrrSysSchedulerSagaInitId}.");

            SetNextTimeoutForShortSequence();
            SetNextTimeoutForLongSequence();
        }

        private void SetNextTimeoutForShortSequence()
        {
            var everyXminutes = ScheduleProductionOrderStutus.SetNextTimeoutForShortSequence();

            Logger.Info("Short-Squence:");
            Logger.Info($"Scheduled to run {everyXminutes} minutes from now.");

            RequestTimeout<TimeToSendProductionOrderStatusShortSequence>(TimeSpan.FromMinutes(everyXminutes));
        }

        private void SetNextTimeoutForLongSequence()
        {
            Logger.Info("Long-Squence:");
            var nextDateTimeToRun = ScheduleProductionOrderStutus.SetNextTimeoutForLongSequence();

            Logger.Info("Scheduled to run again on:");
            Logger.Info($"{nextDateTimeToRun.ToString("f")}");

            RequestTimeout<TimeToSendProductionOrderStatusLongSequence>(nextDateTimeToRun);
        }

        public void Timeout(TimeToSendProductionOrderStatusShortSequence state)
        {
            //state is NOT used. Thats Ok.

            Logger.Info("======================================");
            Logger.Info("Time to process ProductionOrderStatus Short Sequence.");

            DoProductionOrderStatusShortSequence();

            SetNextTimeoutForShortSequence();
        }

        public void Timeout(TimeToSendProductionOrderStatusLongSequence state)
        {
            //state is NOT used. Thats Ok.

            Logger.Info("======================================");
            Logger.Info("Time to process ProductionOrderStatus Long Sequence.");

            DoProductionOrderStatusLongSequence();

            SetNextTimeoutForLongSequence();
        }

        public void Handle(ProductionOrderStatusRuntimeReset message)
        {
            SetNextTimeoutForLongSequence();
        }

        private void DoProductionOrderStatusShortSequence()
        {
            Logger.Info("======================================");
            Logger.Info("Getting ProductionOrderStatus for Short Sequence.");

            var message = ProductionOrderStatus.GetShortSequence();

            SendMessages(message);
        }

        private void DoProductionOrderStatusLongSequence()
        {
            Logger.Info("======================================");
            Logger.Info("Getting ProductionOrderStatus for Long Sequence.");

            var message = ProductionOrderStatus.GetLongSequence();

            SendMessages(message);
        }

        private void SendMessages(ProductionOrderStatusCommand message)
        {
            const float leg = 1.0F;

            if (message?.Payload == null || !message.Payload.ProductionOrderStatusPayloadItem.Any())
            {
                Logger.Info("Response message contains NO items.");
                Logger.Info("No need to send message to SAP.");
                Logger.Info("All good. Continue processing.");
                return;
            }

            Logger.Info($"ProductionOrderId={message.ProductionOrderId}.");
            Logger.Info($"SagaReferenceId={message.SagaReferenceId}.");
            Logger.Info($"ProductionOrderStatus itemCount={message.Payload.ProductionOrderStatusPayloadItem.Count}.");

            Logger.Info("Now sending a message to the Saga.");
            var sagaStartMessage = new Spm.Service.Messages.ProductionOrderStatusCommand
            {
                SagaReferenceId = message.SagaReferenceId,
                ProductionOrderId = message.ProductionOrderId,
                Payload = message.Payload
            };
            Bus.Send(sagaStartMessage);

            Logger.Info("Now sending an audit log.");
            var productionOrderAuditCommand = new ProductionOrderStatusAuditCommand
            {
                ProductionOrderId = message.ProductionOrderId,
                SagaReferenceId = message.SagaReferenceId,
                MessageType = typeof(ProductionOrderStatusCommand).FullName,
                FromEndpoint = EndPointName.SpmOrrSysService,
                DateTimeMessageSendToHere = DateTime.Now,
                Action = (int)AuditAction.SendToSaga,
                MessageData = message.ToString(),
                Leg = leg
            };
            Bus.Send(productionOrderAuditCommand);
        }
    }
}