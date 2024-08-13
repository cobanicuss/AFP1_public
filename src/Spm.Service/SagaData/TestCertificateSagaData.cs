using System;
using NServiceBus.Saga;

namespace Spm.Service.SagaData
{
    public class TestCertificateSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        [Unique] //VERY IMPORTANT: to prevent concurrency on new Saga//
        public virtual string SagaReferenceId { get; set; }

        public virtual string InboundId { get; set; }
        public virtual string CertificateId { get; set; }
        public virtual string LotNumberList { get; set; }
        public virtual int MessageIndex { get; set; }
        public virtual int MessageCount { get; set; }
        public virtual DateTime LastUpdatedDateTime { get; set; }
        public virtual string SagaState { get; set; }
        public virtual int SagaRetry { get; set; }
        public virtual Guid SerializedMessageId { get; set; }
    }
}