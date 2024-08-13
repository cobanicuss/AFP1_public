using NServiceBus;

namespace Spm.File.Watcher.Messages
{

    public class FileBaseCommand : ICommand
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string PathToBakup { get; set; }
        public string ErrorFileName { get; set; }
        public string PathToError { get; set; }
        public string PathToLocalDestination { get; set; }
        public int BufferFileCount { get; set; }
    }

    public class PurchaseOrderCreateCommand : FileBaseCommand { }
    public class PurchaseOrderChangeCommand : FileBaseCommand { }
    public class GoodsReceiptCommand : FileBaseCommand { }
    public class GoodsReversalCommand : FileBaseCommand { }
    public class GeneralLedgerCommand : FileBaseCommand { }
    public class MaterialMasterCommand : FileBaseCommand { }
}