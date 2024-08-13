namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public interface IConvertDate
    {
        string ConvertDateForSap(string input);
        string ConvertDateForSapToTodayIfSmaller(string input);
    }
}