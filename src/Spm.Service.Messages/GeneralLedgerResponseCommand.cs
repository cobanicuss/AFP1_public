﻿using NServiceBus;

namespace Spm.Service.Messages
{
    public class GeneralLedgerResponseCommand : ICommand
    {
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = string.Format(@"SagaReferenceId={0}", SagaReferenceId);

            return str;
        }
    }
}