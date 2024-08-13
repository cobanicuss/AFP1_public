using System;
using NHibernate;
using Spm.Service.Domain;
using Spm.Shared;

namespace Spm.Service.SagaTransitions
{
    public interface IGoodsReceiptTransitions : IMarkAsTransition
    {
        void Start(string number, Guid sagaId, string sagaName, string type);
        void End(string number, Guid sagaId, string sagaName, string currentState, string type);
        void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string type);
    }

    public class GoodsReceiptTransitions : IGoodsReceiptTransitions
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session { get { return SessionFactory.GetCurrentSession(); } }

        public void Start(string number, Guid sagaId, string sagaName, string type)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                GoodsReceiptId = number,
                TransitionFrom = SagaStates.Init.ToString(),
                TransitionTo = SagaStates.Started.ToString(),
                DateTimeOfTransition = DateTime.Now,
                Type = type
            };

            Session.Save(sagaTransition);
        }

        public void End(string number, Guid sagaId, string sagaName, string currentState, string type)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                GoodsReceiptId = number,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.Completed.ToString(),
                DateTimeOfTransition = DateTime.Now,
                Type = type
            };

            Session.Save(sagaTransition);
        }

        public void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string type)
        {
            var sagaTransition = new GoodsReceiptTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
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