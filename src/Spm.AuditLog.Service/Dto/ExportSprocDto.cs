namespace Spm.AuditLog.Service.Dto
{
    public class ExportSprocDto
    {
        public string OutputPath        { get; set; }
        public string BcpFormatFileName { get; set; }
        public string BcpDataFileName   { get; set; }
        public string ErrorFileName     { get; set; }
        public string LogFileName       { get; set; }
        public string TableName         { get; set; }
    }
}