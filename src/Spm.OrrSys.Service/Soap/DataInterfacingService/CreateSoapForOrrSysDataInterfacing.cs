using System.Linq;
using OrrSys.DataInterface.DataContract;
using Spm.OrrSys.Messages;

namespace Spm.OrrSys.Service.Soap.DataInterfacingService
{
    public interface IMapSoap
    {
        MaterialMasterUpdateRequest CreateMaterialMasterUpdateRequest(MaterialMasterUpdateRequestCommand message);
    }

    public class MapSoap : IMapSoap
    {
        public MaterialMasterUpdateRequest CreateMaterialMasterUpdateRequest(MaterialMasterUpdateRequestCommand message)
        {
            var list = message.Payload.MaterialMasterUpdatePayloadItem.Select(x => new MaterialMasterUpdate
            {
                PrimeGtin = x.PrimeGtin,
                RejectGtin = x.RejectGtin,
                RejectCode = x.RejectCode,
                SapMaterialNumber = x.SapMaterialNumber,
                OldMaterialNumber = x.OldMaterialNumber,
                SprasLanguage0 = x.SprasLanguage0,
                SapDescription0 = x.SapDescription0,
                SprasIsoLanguage0 = x.SprasIsoLanguage0,
                SprasLanguage1 = x.SprasLanguage1,
                SapDescription1 = x.SapDescription1,
                SprasIsoLanguage1 = x.SprasIsoLanguage1
            }).ToArray();

            var returnVal = new MaterialMasterUpdateRequest
            {
                MaterialMasterUpdateArray = list
            };

            return returnVal;
        }
    }
}