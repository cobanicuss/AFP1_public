using Spm.Service.ForSoap.Config;
using Spm.Service.ForSoap.Messages;
using Spm.Shared;

namespace Spm.Service.ForSoap.Mapper
{
    public interface IResponseToSapRequestMessageMap : IMarkAsMapper
    {
        SYSTAT01 CreateResponseToSapMessage(ResponseToSapRequestCommand message);
    }

    public class ResponseToSapRequestMessageMap : IResponseToSapRequestMessageMap
    {
        public SYSTAT01 CreateResponseToSapMessage(ResponseToSapRequestCommand message)
        {
            var returnVal = new SYSTAT01
            {
                IDOC = new STATUSSYSTAT01
                {
                    E1STATS = new[]
                    {
                        new SYSTAT01E1STATS
                        {
                            DOCNUM = message.NumberId,
                            LOGDAT = DefaultSapVariables.DateNow,
                            LOGTIM = DefaultSapVariables.TimeNow,
                            STATUS = DefaultSapVariables.Status,
                            UNAME = DefaultSapVariables.Uname,
                            REPID = DefaultSapVariables.RepId,
                            STACOD = DefaultSapVariables.StaCod,
                            STATXT = DefaultSapVariables.StaTxt,
                            STATYP = DefaultSapVariables.StaTyp
                        }
                    },
                    EDI_DC40 = new EDI_DC40STATUSSYSTAT01
                    {
                        TABNAM = DefaultSapVariables.Tabnam,
                        MANDT = DefaultSapVariables.Mandt,
                        DIRECT = EDI_DC40STATUSSYSTAT01DIRECT.Item1,
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