using System.Globalization;
using System.Linq;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IProductionOrderMesageMap : IMarkAsMapper
    {
        ZPP_CHNG MakeSoapMessage(ProductionOrderStatusSapCommand message);
    }

    public class ProductionOrderMesageMap : IProductionOrderMesageMap
    {
        public ZPP_CHNG MakeSoapMessage(ProductionOrderStatusSapCommand message)
        {
            var returnValue = new ZPP_CHNG
            {
                IDOC = new ZPP_CHNGZPP_CHNG
                {
                    EDI_DC40 = new EDI_DC40ZPP_CHNGZPP_CHNG
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DOCNUM = message.SagaReferenceId,
                        DIRECT = EDI_DC40ZPP_CHNGZPP_CHNGDIRECT.Item1,
                        IDOCTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        MESTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPOR = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    },
                    Z1PLINE = message.Payload.ProductionOrderStatusPayloadItem.Select(x => new ZPP_CHNGZ1PLINE
                    {
                        AUFNR = x.ProductionOrderNumber,
                        CFLAG = x.CompleteFlag, 
                        GAMNG = x.OrderQuantity.HasValue ? x.OrderQuantity.Value.ToString(CultureInfo.InvariantCulture) : "0", 
                        GLTRI = x.FinishDate.HasValue ? x.FinishDate.Value.ToString("yyyyMMdd") : string.Empty, 
                        GMEIN = x.OrderQuantityUom, 
                        RFLAG = x.ReleaseFlag
                    }).ToArray()
                }
            };

            return returnValue;
        }
    }
}