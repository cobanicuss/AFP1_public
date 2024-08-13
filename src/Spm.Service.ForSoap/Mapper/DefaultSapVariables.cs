using System;

namespace Spm.Service.ForSoap.Mapper
{
    public class DefaultSapVariables
    {
        public const string Status = "12";
        public static string DateNow = DateTime.Now.ToString("yyyyMMdd");
        public static string TimeNow = DateTime.Now.ToString("HHmmss");
        public const string Uname = "SAP";
        public const string RepId = "SAP";
        public const string StaCod = "00";
        public const string StaTxt = "Document received by ORRSYS - Orrcon";
        public const string StaTyp = "I";
        public const string Mandt = "301";
        public const string SndPrt = "LS";
        public const string RcvPor = "SAP";
        public const string RcvPrt = "LS";
        public const string RcvPrn = "BSLDistribution";
        public const string Tabnam = "EDI_DC40";
        public const string OrrSysDevAndTest = "ORRSYSD";
        public const string OrrSysProd = "ORRSYS";
    }
}