using System;
using NHibernate;
using Spm.Service.Domain;

namespace Spm.Service.SagaTransitions
{
    public interface ITestCertificateTransitions : ITransitionsBase { }

    public class TestCertificateTransition : ITestCertificateTransitions
    {
        public ISessionFactory SessionFactory { get; set; }
        public ISession Session => SessionFactory.GetCurrentSession();

        public void Start(string number, Guid sagaId, string sagaName, string sagaReferenceId)
        {
            var sagaTransition = new TestCertificateTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                CertificateId = number,
                TransitionFrom = SagaStates.Init.ToString(),
                TransitionTo = SagaStates.Started.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }

        public void End(string number, Guid sagaId, string sagaName, string currentState, string sagaReferenceId)
        {
            var sagaTransition = new TestCertificateTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                CertificateId = number,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.Completed.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }

        public void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string sagaReferenceId)
        {
            var sagaTransition = new TestCertificateTransitionHistory
            {
                Id = Guid.NewGuid(),
                SagaId = sagaId,
                SagaName = sagaName,
                SagaReferenceId = sagaReferenceId,
                CertificateId = number,
                TransitionFrom = currentState,
                TransitionTo = SagaStates.NoResponse.ToString(),
                DateTimeOfTransition = DateTime.Now
            };

            Session.Save(sagaTransition);
        }
    }
}