namespace Spm.File.Watcher.Service.Dto
{
    public class GeneralLedgerSapDto : GeneralLedgerItemDto
    {
        public string UserName { get; set; }
        public string DocType { get; set; }
        public string ItemText { get; set; }
        public string GlProfitCenter { get; set; }
    }
}