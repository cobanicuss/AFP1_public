﻿using NServiceBus;

namespace Spm.Service.ForSoap.Messages
{
    public class PurchaseOrderChangeSapResponse : IMessage
    {
        public string SagaReferenceId { get; set; }

        public override string ToString()
        {
            var str = $"SagaReferenceId={SagaReferenceId}.";

            return str;
        }
    }
}