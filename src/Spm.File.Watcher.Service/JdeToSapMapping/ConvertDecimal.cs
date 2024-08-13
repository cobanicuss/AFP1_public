using System;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class ConvertDecimal : IConvertDecimal
    {
        public int UpscaleDenominator(double input)
        {
            var balance = 10d;
            var c = 0;

            for (var i = 1; i <= 99999; i++)
            {
                var totDenominator = i * input;

                if (totDenominator >= 99999d) break;

                var round = Math.Round(totDenominator, 0);
                var bal = Math.Abs(totDenominator - round);

                if (bal >= -0.000001d && bal <= 0.000001d) return i;

                if (bal < balance)
                {
                    c = i;
                    balance = bal;
                }
            }

            return c;
        }
    }
}