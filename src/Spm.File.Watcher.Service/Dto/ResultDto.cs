namespace Spm.File.Watcher.Service.Dto
{
    public class ResultDto
    {
        public ResultDto(){}

        public ResultDto(bool isOk)
        {
            IsOk = isOk;
        }

        public bool IsOk { get; set; }
        public string Output { get; set; }
    }
}