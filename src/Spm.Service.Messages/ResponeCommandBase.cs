namespace Spm.Service.Messages
{
    public class ResponseCommandBase : CommandBase
    {
        public override string ToString()
        {
            var str = $@"SagaReferenceId={SagaReferenceId}.";

            return str;
        }
    }

    public class ProductAchievementResponseCommand : ResponseCommandBase { }

    public class GeneralLedgerResponseCommand : ResponseCommandBase { }

    public class GoodsReceiptResponseCommand : ResponseCommandBase { }

    public class MaterialMasterResponseCommand : ResponseCommandBase { }

    public class ProductionOrderStatusResponseCommand : ResponseCommandBase
    {
        public string ProductionOrderId { get; set; }
    }

    public class PurchaseOrderChangeResponseCommand : ResponseCommandBase { }

    public class PurchaseOrderCreateResponseCommand : ResponseCommandBase { }

    public class TestCertificateResponseCommand : ResponseCommandBase { }
}