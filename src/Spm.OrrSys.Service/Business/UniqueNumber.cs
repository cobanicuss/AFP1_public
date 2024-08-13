using System;

namespace Spm.OrrSys.Service.Business
{
    public class UniqueNumbers : IUniqueNumbers
    {
        private readonly object _thisLock = new object();

        public string GetUniqueNDigitNumberAsString(int n)
        {
            lock (_thisLock)
            {
                var centuryBegin = new DateTime(2015, 1, 1);
                var currentDate = DateTime.Now;
                var elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                var returnValue = elapsedTicks.ToString().Length < n ? elapsedTicks.ToString().PadLeft(n, '0') : elapsedTicks.ToString().Substring(0, n);

                return returnValue;

            }
        }

        public string CreateUniqueCertificateNumber()
        {
            //Create a unique transaction number
            var dateBegin = new DateTime(2016, 1, 1);
            var currentDate = DateTime.Now;
            var elapsedTicks = currentDate.Ticks - dateBegin.Ticks;

            return elapsedTicks.ToString();
        }
    }
}