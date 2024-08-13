using System.Text;

namespace Spm.OrrSys.Service.Repositories
{
    public class SqlStringQuery
    {
        public static string DeleteHistoryNDaysBack(string historyTable, int historyKeptDays, string dateColumn)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"DELETE FROM {historyTable}");
            sb.AppendLine($"WHERE {dateColumn} < DATEADD(dd, {historyKeptDays}, GETDATE())");

            return sb.ToString();
        }

        public static string InsertHistoryForToday(string historyTable, string table)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"INSERT INTO {historyTable}");
            sb.AppendLine("SELECT *, GETDATE() DateTimeImplemented");
            sb.AppendLine($"FROM {table}");

            return sb.ToString();
        }

        public static string GetOpenWorkOrderSqlString()
        {
            var sb = new StringBuilder();

            sb.Append("SELECT 'BATCH08' EDUS, ");
            sb.Append("'JDEFC' TYTN, ");
            sb.Append("'1' DRIN, ");
            sb.Append("'N' EDSP, ");
            sb.Append("'A' TNAC, ");
            sb.Append("WAITM, ");
            sb.Append("WALITM, ");
            sb.Append("WAAITM, ");
            sb.Append("WAMMCU, ");
            sb.Append("WADRQJ, ");
            sb.Append("case when F4111.ILTRQT is null then WAUORG else (WAUORG - F4111.ILTRQT)end UORG, ");
            sb.Append("case when F4111.ILTRQT is null then WAUORG else (WAUORG - F4111.ILTRQT)end FQT, ");
            sb.Append("'OW' TYPF, ");
            sb.Append("'FC' DCTO, ");
            sb.Append("WADOCO, ");
            sb.Append("WASRST ");
            sb.Append("FROM F4801 F4801 ");
            sb.Append("JOIN (SELECT IBLITM,IBSTKT,IBMCU ");
            sb.Append("FROM F4102 ");
            sb.Append("WHERE IBSTKT = 'S') ");
            sb.Append("F4102 ON F4102.IBLITM = F4801.WALITM ");
            sb.Append("left JOIN (SELECT SUM(ILTRQT) ILTRQT , ILITM, ILDOCO ");
            sb.Append("FROM F4111 ");
            sb.Append("WHERE ILDCT = 'IC'  GROUP BY ILITM , ILDOCO) ");
            sb.Append("F4111 ON ILDOCO = WADOCO AND ILITM = WAITM ");
            sb.Append("WHERE IBSTKT = 'S' AND ");
            sb.Append("(WASRST >= '10' AND WASRST < '90') AND ");
            sb.Append("((WAUORG - F4111.ILTRQT) >0 OR F4111.ILTRQT is null)");

            return sb.ToString();
        }

        public static string GetOutboundTestCertificate()
        {
            var sb = new StringBuilder();

            sb.Append("Select reportgroup ReportGroup ");
            sb.Append(",PurchaseOrder ");
            sb.Append(",rownumber RowNumber ");
            sb.Append(",Firstlevellot FirstLevelLot ");
            sb.Append(",HeatNumber ");
            sb.Append(",SapMaterialNumber ");
            sb.Append(",CASE WHEN SalesOrderNumber IS NULL THEN '-' ELSE SalesOrderNumber END AS SalesOrderNumber ");
            sb.Append(",SaleInvoiceNumber ");
            sb.Append(",CustomerAccountNumber ");
            sb.Append(",CustomerName ");
            sb.Append("From JDE_IMPORT.dbo.TempTestResults ");

            return sb.ToString();
        }

        public static string UpdateF4801Z1ProcessedInSap(string lotNumber, char flag)
        {
            var sb = new StringBuilder();
            sb.AppendLine("UPDATE [OrrSys].[dbo].[OrrSysF4801Z1] ");
            sb.AppendLine($"SET [ProcessedInSAP] = '{flag}' ");
            sb.AppendLine($"WHERE LTRIM(RTRIM([SYLOTN])) = '{lotNumber}'");

            return sb.ToString();
        }

        public static string BulkUpdateDespatchedPacksByCustomerOrder(string[] lotNumberList)
        {
            var sb = new StringBuilder();

            var lotNumberListWithQuotes = $"'{string.Join("','", lotNumberList)}'";

            sb.AppendLine("Update [OrrSys].[dbo].[DespatchedPacksByCustomerOrder] ");
            sb.AppendLine("Set Processed = 'Y' ");
            sb.AppendLine($"Where PackNumber IN ({lotNumberListWithQuotes}) ");
            sb.AppendLine("And Processed = 'N'");

            return sb.ToString();
        }
    }
}