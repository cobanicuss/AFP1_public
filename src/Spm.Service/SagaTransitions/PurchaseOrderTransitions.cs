using System;
using NHibernate;
using Spm.Service.Domain;

namespace Spm.Service.SagaTransitions
{
    public interface IPurchaseOrderTransitions : ITransitionTypeBase { }

    public class PurchaseOrderTransitions : IPurchaseOrderTransitions
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void Start(string purchaseOrderNumber, Guid sagaId, string sagaName, string actionType, string sagaReferenceId)
        {
            var sagaTransition = new PurchaseOrderTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                PurchaseOrderNumber = purchaseOrderNumber,
                PurchaseOrderActionType = actionType,
                TransitionFrom = SagaStates.Init.ToString(),
                TransitionTo = SagaStates.Started.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }

        public void End(string purchaseOrderNumber, Guid sagaId, string sagaName, string currentState, string actionType, string sagaReferenceId)
        {
            var sagaTransition = new PurchaseOrderTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                PurchaseOrderNumber = purchaseOrderNumber,
                PurchaseOrderActionType = actionType,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.Completed.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }

        public void NoResponse(string purchaseOrderNumber, Guid sagaId, string sagaName, string currentState, string actionType, string sagaReferenceId)
        {
            var sagaTransition = new PurchaseOrderTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                PurchaseOrderNumber = purchaseOrderNumber,
                PurchaseOrderActionType = actionType,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.NoResponse.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }
    }
}