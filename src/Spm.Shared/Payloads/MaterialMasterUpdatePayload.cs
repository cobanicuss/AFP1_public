using System.Collections.Generic;

namespace Spm.Shared.Payloads
{
    public class MaterialMasterUpdatePayload
    {
        public List<MaterialMasterUpdatePayloadItem> MaterialMasterUpdatePayloadItem { get; set; }
    }

    public class MaterialMasterUpdatePayloadItem
    {
        public string SapMaterialNumber { get; set; }
        public string OldMaterialNumber { get; set; }
        public string PrimeGtin { get; set; }

        public string RejectCode { get; set; }
        public string RejectGtin { get; set; }

        public string SprasLanguage0 { get; set; }
        public string SapDescription0 { get; set; }
        public string SprasIsoLanguage0 { get; set; }

        public string SprasLanguage1 { get; set; }
        public string SapDescription1 { get; set; }
        public string SprasIsoLanguage1 { get; set; }
    }

    public class MaterialMasterUpdatePayloadItemAsString
    {
        public static string ToString(MaterialMasterUpdatePayloadItem materialMasterUpdatePayloadItem)
        {
            var str = $"SapMaterialNumber={materialMasterUpdatePayloadItem.SapMaterialNumber}";

            return str;
        }
    }
}