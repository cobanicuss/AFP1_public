using System;

namespace Spm.Shared
{
    public class TestClientFile
    {
        public static string CreateCertificateId()
        {
            //0000000012375022
            var certificateId = $"{DateTime.Now.ToString("yyMMddss")}";
            certificateId = certificateId.PadLeft(16, '0');

            return certificateId;
        }

        public static string CreateLotNumber()
        {
            var minutesFromBeggingOfThisYear = (long)DateTime.Now.Subtract(new DateTime(DateTime.Now.Year, 1, 1)).TotalMinutes;
            var afterSlashValue = $"{DateTime.Now.Second}{DateTime.Now.Millisecond}";
            var lotNumber = $"{minutesFromBeggingOfThisYear}/{afterSlashValue}";

            return lotNumber;
        }

        public static string CreatePurchaseOrderNumber()
        {
            var secondsFromBeggingOfThisYear = (long)DateTime.Now.Subtract(new DateTime(DateTime.Now.Year, 1, 1)).TotalSeconds;
            var eightDigitsOnly = secondsFromBeggingOfThisYear.ToString().Substring(0, 8);
            var result = $"00000000{eightDigitsOnly}";

            return result;
        }

        public static string CreateGoodsReceiptId()
        {
            var goodsReceiptId = Guid.NewGuid().ToString();

            return goodsReceiptId;
        }
    }
}