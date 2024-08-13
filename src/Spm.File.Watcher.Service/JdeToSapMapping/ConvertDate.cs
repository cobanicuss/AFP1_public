using System;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class ConvertDate : IConvertDate
    {
        public string ConvertDateForSap(string input)
        {
            //A parse check MUST HAVE been done previousely in validation
            var returnVal = DateTime.Parse(input).Date.ToString(Constants.SapDateFormat);

            return returnVal;
        }

        public string ConvertDateForSapToTodayIfSmaller(string input)
        {
            //A parse check MUST HAVE been done previousely in validation
            var date = DateTime.Parse(input).Date;

            var returnVal = date < DateTime.Today.Date ? DateTime.Today.Date.ToString(Constants.SapDateFormat) : date.ToString(Constants.SapDateFormat);

            return returnVal;
        }
    }
}