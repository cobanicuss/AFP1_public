using NServiceBus;

namespace Spm.Service.Messages
{
    public class ProductAchievementNoResponse : IMessage { }

    public class PurchaseOrderCreateNoResponse : IMessage { }

    public class PurchaseOrderChangeNoResponse : IMessage { }

    public class ProductionOrderStatusNoResponse : IMessage { }

    public class GoodsReceiptNoResponse : IMessage { }

    public class GeneralLedgerNoResponse : IMessage { }

    public class MaterialMasterNoResponse : IMessage { }

    public class TestCertificateNoResponse : IMessage { }
}
