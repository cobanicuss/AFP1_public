using System;
using System.Linq;
using NServiceBus.Logging;
using Spm.Service.ReceiveFromSap.SoapMessages;

namespace Spm.Service.ReceiveFromSap
{
    public interface IValidateIncommingMessages
    {
        bool IsValidMessageDetails(SYSTAT01IDOC message);
        bool IsValidMessageDetails(ZOBTCZOBTC01 message);
        bool IsValidMessageDetails(MATMASMATMAS05ZMATMAS5 message);
        void UnexpectedError(string error);
    }

    public class ValidateIncommingMessages : IValidateIncommingMessages
    {
        //Must be SpmWebService not ValidateIncommingMessages//
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SpmWebService));

        public bool IsValidMessageDetails(SYSTAT01IDOC message)
        {
            if (message == null)
            {
                UnexpectedError("SPM says, validation error: IDOC is null. In SYSTAT01.IDOC?? Cannot proceed.");
                return false;
            }

            if (message.E1STATS == null)
            {
                UnexpectedError("SPM says, validation error: E1STATS is null. In SYSTAT01.IDOC.E1STATS?? Cannot proceed.");
                return false;
            }

            if (message.EDI_DC40 == null)
            {
                UnexpectedError("SPM says, validation error: EDI_DC40 is null. In SYSTAT01.IDOC.EDI_DC40?? Cannot proceed.");
                return false;
            }

            if (string.IsNullOrEmpty(message.E1STATS.DOCNUM))
            {
                UnexpectedError("SPM says, validation error: Number identifier (DOCNUM) is null. In E1STATS?? Cannot proceed.");
                return false;
            }
            return true;
        }

        public bool IsValidMessageDetails(ZOBTCZOBTC01 message)
        {
            if (message == null)
            {
                UnexpectedError("SPM says, validation error: IDOC is null. In ZOBTC01.IDOC?? Cannot proceed.");
                return false;
            }

            if (message.Z1TCLIN == null)
            {
                UnexpectedError("SPM says, validation error: Z1TCLIN is null. In ZOBTC01.IDOC.Z1TCLIN?? Cannot proceed.");
                return false;
            }

            if (message.EDI_DC40 == null)
            {
                UnexpectedError("SPM says, validation error: EDI_DC40 is null. In ZOBTC01.IDOC.EDI_DC40?? Cannot proceed.");
                return false;
            }

            if (string.IsNullOrEmpty(message.EDI_DC40.DOCNUM))
            {
                UnexpectedError("SPM says, validation error: Number identifier is null in ZOBTC01.IDOC.EDI_DC40.DOCNUM?? Cannot proceed.");
                return false;
            }

            if (message.Z1TCLIN == null || !message.Z1TCLIN.Any())
            {
                UnexpectedError("SPM says, validation error: Z1TCEML is null. In ZOBTC01.IDOC.Z1TCLIN?? Cannot proceed.");
                return false;
            }

            if (message.Z1TCLIN.Any(x => x.MATNR == null) || message.Z1TCLIN.Any(x => string.IsNullOrWhiteSpace(x.MATNR)))
            {
                UnexpectedError("SPM says, validation error: MATNR is null. In ZOBTC01.IDOC.Z1TCLIN.MATNR?? Critical Error. Cannot proceed.");
                return false;
            }

            if (message.Z1TCLIN.SelectMany(x => x.Z1TCEML).Any(x => string.IsNullOrWhiteSpace(x.EMAIL)))
            {
                UnexpectedError("SPM says, validation error: EMAIL is null. In ZOBTC01.IDOC.Z1TCLIN.Z1TCEML.EMAIL?? Critical Error. Cannot proceed.");
                return false;
            }

            if (message.Z1TCLIN.Any(x => x.HEAT == null) || message.Z1TCLIN.Any(x => string.IsNullOrWhiteSpace(x.HEAT)))
            {
                UnexpectedError("SPM says, validation error: HEAT is null. In ZOBTC01.IDOC.Z1TCLIN.HEAT?? Critical Error. Cannot proceed.");
                return false;
            }

            return true;
        }

        public bool IsValidMessageDetails(MATMASMATMAS05ZMATMAS5 message)
        {
            if (message == null)
            {
                UnexpectedError("SPM says, validation error: IDOC is null. In ZMATMAS5.IDOC?? Cannot proceed.");
                return false;
            }

            if (message.EDI_DC40 == null)
            {
                UnexpectedError("SPM says, validation error: EDI_DC40 is null. In ZMATMAS5.IDOC.EDI_DC40?? Cannot proceed.");
                return false;
            }

            if (string.IsNullOrEmpty(message.EDI_DC40.DOCNUM))
            {
                UnexpectedError("SPM says, validation error: Number identifier is null in ZMATMAS5.IDOC.EDI_DC40.DOCNUM?? Cannot proceed.");
                return false;
            }

            if (message.E1MARAM == null || !message.E1MARAM.Any())
            {
                UnexpectedError("SPM says, validation error: E1MARAM is null. In ZMATMAS5.IDOC.E1MARAM?? Cannot proceed.");
                return false;
            }

            var rejectGtinList = message.E1MARAM.SelectMany(x => x.Z1MARAM).Select(y => y.ZZREJTEAN).ToList();
            var rejectCodeList = message.E1MARAM.SelectMany(x => x.Z1MARAM).Select(y => y.ZZREJT).ToList();

            var allRejectGtinIsNull = rejectGtinList.Where(string.IsNullOrEmpty).ToList();
            var allRejectCodeListIsNull = rejectCodeList.Where(string.IsNullOrEmpty).ToList();

            if (allRejectGtinIsNull.Any())
            {
                UnexpectedError($"SPM says, validation error: One or more (count={allRejectGtinIsNull.Count}) of ZZREJTEAN is null. In ZMATMAS5.IDOC.E1MARAM.Z1MARAM?? Cannot proceed.");
                return false;
            }

            if (allRejectCodeListIsNull.Any())
            {
                UnexpectedError($"SPM says, validation error: One or more (count={allRejectCodeListIsNull}) of ZZREJT is null. In ZMATMAS5.IDOC.E1MARAM.Z1MARAM?? Cannot proceed.");
                return false;
            }

            return true;
        }

        public void UnexpectedError(string error)
        {
            Logger.Fatal(error);
            throw new ArgumentException(error);
        }
    }
}