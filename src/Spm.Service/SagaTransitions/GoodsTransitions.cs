using System;
using NHibernate;
using Spm.Service.Domain;

namespace Spm.Service.SagaTransitions
{
    public interface IGoodsTransitions : ITransitionTypeBase { }

    public class GoodsTransitions : IGoodsTransitions
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void Start(string number, Guid sagaId, string sagaName, string type, string sagaReferenceId)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                GoodsReceiptId = number,
                TransitionFrom = SagaStates.Init.ToString(),
                TransitionTo = SagaStates.Started.ToString(),
                DateTimeOfTransition = DateTime.Now,
                Type = type
            };

            Session.Save(sagaTransition);
        }

        public void End(string number, Guid sagaId, string sagaName, string currentState, string type, string sagaReferenceId)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                GoodsReceiptId = number,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.Completed.ToString(),
                DateTimeOfTransition = DateTime.Now,
                Type = type
            };

            Session.Save(sagaTransition);
        }

        public void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string type, string sagaReferenceId)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                GoodsReceiptId = number,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.NoResponse.ToString(),
                DateTimeOfTransition = DateTime.Now,
                Type = type
            };

            Session.Save(sagaTransition);
        }
    }
}