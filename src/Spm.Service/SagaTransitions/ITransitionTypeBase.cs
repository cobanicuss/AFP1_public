using System;
using Spm.Shared;

namespace Spm.Service.SagaTransitions
{
    public interface ITransitionTypeBase : IMarkAsTransition
    {
        void Start(string number, Guid sagaId, string sagaName, string type, string sagaReferenceId);
        void End(string number, Guid sagaId, string sagaName, string currentState, string type, string sagaReferenceId);
        void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string type, string sagaReferenceId);
    }
}