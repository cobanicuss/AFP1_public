using System;
using Spm.Shared;

namespace Spm.Service.Domain
{
    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class TransitionHistoryBase : IMarkAsDomain
    {
        public virtual Guid Id { get; set; }
        public virtual Guid SagaId { get; set; }
        public virtual string SagaName { get; set; }
        public virtual string SagaReferenceId { get; set; }
        public virtual string TransitionFrom { get; set; }
        public virtual string TransitionTo { get; set; }
        public virtual DateTime DateTimeOfTransition { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class ProductAchievementTransitionHistory : TransitionHistoryBase
    {
        public virtual string LotNumber { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class ProductionOrderTransitionHistory : TransitionHistoryBase
    {
        public virtual string ProductionOrderId { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class PurchaseOrderTransitionHistory : TransitionHistoryBase
    {
        public virtual string PurchaseOrderNumber { get; set; }
        public virtual string PurchaseOrderActionType { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class GoodsReceiptTransitionHistory : TransitionHistoryBase
    {
        public virtual string GoodsReceiptId { get; set; }
        public virtual string Type { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class GeneralLedgerTransitionHistory : TransitionHistoryBase
    {
        public virtual string GeneralLedgerId { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class MaterialMasterTransitionHistory : TransitionHistoryBase
    {
        public virtual string ShortItemNumber { get; set; }
    }

    //IMPORTANT: This is a domain model do NOT inherit from an Interface except marker interfaces//
    public class TestCertificateTransitionHistory : TransitionHistoryBase
    {
        public virtual string CertificateId { get; set; }
    }
}
