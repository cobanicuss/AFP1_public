using System;
using Spm.Shared;

namespace Spm.Service.SagaTransitions
{
    public interface ITransitionsBase : IMarkAsTransition
    {
        void Start(string number, Guid sagaId, string sagaName, string sagaReferenceId);
        void End(string number, Guid sagaId, string sagaName, string currentState, string sagaReferenceId);
        void NoResponse(string number, Guid sagaId, string sagaName, string currentState, string sagaReferenceId);
    }
}