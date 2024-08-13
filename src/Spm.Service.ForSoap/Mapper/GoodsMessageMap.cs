using System;
using System.Linq;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IGoodsMessageMap : IMarkAsMapper
    {
        ZMBGMCR02_EXTND MakeSoapMessage(GoodsReceiptSapCommand message);
    }

    public class GoodsMessageMap : IGoodsMessageMap
    {
        public ZMBGMCR02_EXTND MakeSoapMessage(GoodsReceiptSapCommand message)
        {
            if(!message.Payload.GoodsPayloadItem.Any()) throw new ArgumentException("No Payload found to make Soap Message. Cannot proceed!");

            var pickFirstForHeader = message.Payload.GoodsPayloadItem[0]; 

            var returnVal = new ZMBGMCR02_EXTND
            {
                IDOC = new ZMBGMCRZMBGMCR02_EXTND
                {
                    E1BP2017_GM_HEAD_01 = new ZMBGMCR02_EXTNDE1BP2017_GM_HEAD_01
                    {
                        PSTNG_DATE = pickFirstForHeader.HeadPostingDate,
                        DOC_DATE = pickFirstForHeader.HeadDocDate,
                        REF_DOC_NO = pickFirstForHeader.HeadRefDocNo,
                        HEADER_TXT = pickFirstForHeader.HeadHeaderTxt
                    },
                    E1BP2017_GM_ITEM_CREATE = message.Payload.GoodsPayloadItem.Select(x => new ZMBGMCR02_EXTNDE1BP2017_GM_ITEM_CREATE
                    {
                        PLANT = x.ItemCreatePlant,
                        STGE_LOC = x.ItemCreateStgeLoc,
                        MOVE_TYPE = x.ItemCreateMoveType,
                        ENTRY_QNT = x.ItemCreateEntryQnt,
                        ENTRY_UOM = x.ItemCreateEntryUom,
                        PO_NUMBER = x.ItemCreatePoNumber,
                        PO_ITEM = x.ItemCreatePoItem,
                        MVT_IND =  x.ItemCreateMvtInd,
                        NO_MORE_GR = x.ItemCreateNoMoreGr
                    }).ToArray(),
                    E1BP2017_GM_CODE = new ZMBGMCR02_EXTNDE1BP2017_GM_CODE
                    {
                        GM_CODE = pickFirstForHeader.Code
                    },
                    EDI_DC40 = new EDI_DC40ZMBGMCRZMBGMCR02_EXTND
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DIRECT = EDI_DC40ZMBGMCRZMBGMCR02_EXTNDDIRECT.Item1,
                        DOCNUM = message.SagaReferenceId,
                        IDOCTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        MESTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPOR = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    }
                }
            };

            return returnVal;
        }
    }
}