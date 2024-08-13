using System;
using System.Linq;
using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IGeneralLedgerMessageMap : IMarkAsMapper
    {
        ACC_DOCUMENT03 MakeSoapMessage(GeneralLedgerSapCommand message);
    }

    public class GeneralLedgerMessageMap : IGeneralLedgerMessageMap
    {
        public ACC_DOCUMENT03 MakeSoapMessage(GeneralLedgerSapCommand message)
        {
            if (!message.Payload.GeneralLedgerPayloadItem.Any()) throw new ArgumentException("No Payload found to make Soap Message. Cannot proceed!");

            var pickFirstForHeader = message.Payload.GeneralLedgerPayloadItem[0];

            var returnVal = new ACC_DOCUMENT03
            {
                IDOC = new ACC_DOCUMENTACC_DOCUMENT03
                {
                    EDI_DC40 = new EDI_DC40ACC_DOCUMENTACC_DOCUMENT03
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DOCNUM = message.SagaReferenceId,
                        DIRECT = EDI_DC40ACC_DOCUMENTACC_DOCUMENT03DIRECT.Item1,
                        IDOCTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        MESTYP = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPOR = string.Empty,//Must be here; even if not needed. Welcome to SAP//
                        SNDPRT = DefaultSapVariables.SndPrt,
                        SNDPRN = ProfileConfigVariables.SndPrn,
                        RCVPOR = DefaultSapVariables.RcvPor,
                        RCVPRT = DefaultSapVariables.RcvPrt,
                        RCVPRN = DefaultSapVariables.RcvPrn
                    },
                    E1BPACHE09 = new ACC_DOCUMENT03E1BPACHE09
                    {
                        USERNAME = pickFirstForHeader.UserName,
                        HEADER_TXT = pickFirstForHeader.HeaderTxt,
                        COMP_CODE = pickFirstForHeader.CompanyCode,
                        DOC_DATE = pickFirstForHeader.DocDate,
                        PSTNG_DATE = pickFirstForHeader.PstngDate,
                        DOC_TYPE = pickFirstForHeader.DocType,
                        REF_DOC_NO = pickFirstForHeader.RefDocNo
                    },
                    E1BPACGL09 = message.Payload.GeneralLedgerPayloadItem.Select(x => new ACC_DOCUMENT03E1BPACGL09
                    {
                        ITEMNO_ACC = x.AcItemNoAcc,
                        ITEM_TEXT = x.ItemText,
                        ALLOC_NMBR = x.AllocNmbr,
                        COSTCENTER = x.CostCentre,
                        GL_ACCOUNT = x.Account,
                        PROFIT_CTR = x.ProfitCentre
                    }).ToArray(),
                    E1BPACCR09 = message.Payload.GeneralLedgerPayloadItem.Select(x => new ACC_DOCUMENT03E1BPACCR09
                    {
                        ITEMNO_ACC = x.AcItemNoAcc,
                        CURRENCY = x.Currency,
                        AMT_DOCCUR = x.Doccur
                    }).ToArray()
                }
            };

            return returnVal;
        }
    }
}