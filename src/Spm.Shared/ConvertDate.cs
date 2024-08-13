using System;
using System.Globalization;

namespace Spm.Shared
{
    public class ConvertDate
    {
        public static decimal ToTodayIfIsSmallerThanToday(decimal julianDate)
        {
            var todayAsJulian = DateToJdeDate(DateTime.Today);

            var returnVal = julianDate < todayAsJulian ? todayAsJulian : julianDate;

            return returnVal;
        }

        public static DateTime SapStringToDate(string sapDateString)
        {
            var dateTime = DateTime.ParseExact(sapDateString, "yyyyMMdd", new DateTimeFormatInfo());
            return dateTime;
        }

        public static decimal DateToJdeDate(DateTime date)
        {
            var iDays = date.DayOfYear;
            var nYears = date.Year - 1900;

            return ((nYears * 1000) + iDays);
        }

        public static uint DateTimeToJdeTime(DateTime dateTime)
        {
            var jdeTime = dateTime.ToString("hhmmss");
            var jdeTimeAsInt = uint.Parse(jdeTime);

            return jdeTimeAsInt;
        }

        public static DateTime JdeDateToDate(string sJdeDate)
        {
            var dDate = DateTime.Parse("1/1/1900");
            if (int.Parse(sJdeDate) > 0)
            {
                var iDays = int.Parse(sJdeDate.Substring(3, 3)) - 1;
                long nYears = int.Parse(sJdeDate) / 1000;
                dDate = dDate.AddYears(int.Parse(nYears.ToString()));
                dDate = dDate.AddDays(iDays);
            }
            else
            {
                return DateTime.MinValue;
            }

            return dDate;
        }
    }
}