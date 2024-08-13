using System;
using System.Collections.Generic;
using System.Data;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;

namespace Spm.OrrSys.Service.Repositories
{
    public class DataTableSetupForBulkInsert
    {
        public static DataTable CreateF3460Z1(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("FTEDUS"));
            dt.Columns.Add(new DataColumn("FTEDBT"));
            dt.Columns.Add(new DataColumn("FTEDTN"));
            dt.Columns.Add(new DataColumn("FTEDLN", typeof(int)));
            dt.Columns.Add(new DataColumn("FTTYTN"));
            dt.Columns.Add(new DataColumn("FTEDDT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("FTDRIN"));
            dt.Columns.Add(new DataColumn("FTEDSP"));
            dt.Columns.Add(new DataColumn("FTTNAC"));
            dt.Columns.Add(new DataColumn("FTITM", typeof(double)));
            dt.Columns.Add(new DataColumn("FTLITM"));
            dt.Columns.Add(new DataColumn("FTAITM"));
            dt.Columns.Add(new DataColumn("FTMCU"));
            dt.Columns.Add(new DataColumn("FTDRQJ", typeof(decimal)));
            dt.Columns.Add(new DataColumn("FTUORG", typeof(double)));
            dt.Columns.Add(new DataColumn("FTFQT", typeof(double)));
            dt.Columns.Add(new DataColumn("FTTYPF"));
            dt.Columns.Add(new DataColumn("FTDCTO"));
            return dt;
        }

        public static void PopulateF3460Z1(IEnumerable<OrrSysF3460Z1> openWorkOrderList, DataTable dt)
        {
            foreach (var item in openWorkOrderList)
            {
                var row = dt.NewRow();

                row["FTEDUS"] = item.FTEDUS;
                row["FTEDBT"] = item.FTEDBT;
                row["FTEDTN"] = item.FTEDTN;
                row["FTEDLN"] = item.FTEDLN;
                row["FTTYTN"] = item.FTTYTN;
                row["FTEDDT"] = item.FTEDDT;
                row["FTDRIN"] = item.FTDRIN;
                row["FTEDSP"] = item.FTEDSP;
                row["FTTNAC"] = item.FTTNAC;
                row["FTITM"] = item.FTITM;
                row["FTLITM"] = item.FTLITM;
                row["FTAITM"] = item.FTAITM;
                row["FTMCU"] = item.FTMCU;
                row["FTDRQJ"] = item.FTDRQJ;
                row["FTUORG"] = item.FTUORG;
                row["FTFQT"] = item.FTFQT;
                row["FTTYPF"] = item.FTTYPF;
                row["FTDCTO"] = item.FTDCTO;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreateF554801Z(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("SYEDUS"));
            dt.Columns.Add(new DataColumn("SYEDBT"));
            dt.Columns.Add(new DataColumn("SYEDTN"));
            dt.Columns.Add(new DataColumn("SYEDLN", typeof(int)));
            dt.Columns.Add(new DataColumn("SYEDCT"));
            dt.Columns.Add(new DataColumn("SYTYTN"));
            dt.Columns.Add(new DataColumn("SYEDDT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SYDRIN"));
            dt.Columns.Add(new DataColumn("SYEDDL", typeof(int)));
            dt.Columns.Add(new DataColumn("SYEDSP"));
            dt.Columns.Add(new DataColumn("SYTNAC"));
            dt.Columns.Add(new DataColumn("SYDCTO"));
            dt.Columns.Add(new DataColumn("SYITM", typeof(int)));
            dt.Columns.Add(new DataColumn("SYLITM"));
            dt.Columns.Add(new DataColumn("SYDRQJ", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SYUORG", typeof(double)));
            dt.Columns.Add(new DataColumn("SYSOQS", typeof(double)));
            dt.Columns.Add(new DataColumn("SYUM"));
            dt.Columns.Add(new DataColumn("SYMCU"));
            dt.Columns.Add(new DataColumn("SYVR01"));
            return dt;
        }

        public static DataTable CreateF554801ZHistory(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("SYEDUS"));
            dt.Columns.Add(new DataColumn("SYEDBT"));
            dt.Columns.Add(new DataColumn("SYEDTN"));
            dt.Columns.Add(new DataColumn("SYEDLN", typeof(int)));
            dt.Columns.Add(new DataColumn("SYEDCT"));
            dt.Columns.Add(new DataColumn("SYTYTN"));
            dt.Columns.Add(new DataColumn("SYEDDT", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SYDRIN"));
            dt.Columns.Add(new DataColumn("SYEDDL", typeof(int)));
            dt.Columns.Add(new DataColumn("SYEDSP"));
            dt.Columns.Add(new DataColumn("SYTNAC"));
            dt.Columns.Add(new DataColumn("SYDCTO"));
            dt.Columns.Add(new DataColumn("SYITM", typeof(int)));
            dt.Columns.Add(new DataColumn("SYLITM"));
            dt.Columns.Add(new DataColumn("SYDRQJ", typeof(decimal)));
            dt.Columns.Add(new DataColumn("SYUORG", typeof(double)));
            dt.Columns.Add(new DataColumn("SYSOQS", typeof(double)));
            dt.Columns.Add(new DataColumn("SYUM"));
            dt.Columns.Add(new DataColumn("SYMCU"));
            dt.Columns.Add(new DataColumn("SYVR01"));
            dt.Columns.Add(new DataColumn("DateTimeImplemented", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("DateRequested", typeof(DateTime)));
            return dt;
        }

        public static void PopulateF554801Z(IEnumerable<OrrSysF554801Z> list, DataTable dt)
        {
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["SYEDUS"] = item.SYEDUS;
                row["SYEDBT"] = item.SYEDBT;
                row["SYEDTN"] = item.SYEDTN;
                row["SYEDLN"] = item.SYEDLN;
                row["SYEDCT"] = item.SYEDCT;
                row["SYTYTN"] = item.SYTYTN;
                row["SYEDDT"] = item.SYEDDT;
                row["SYDRIN"] = item.SYDRIN;
                row["SYEDDL"] = item.SYEDDL;
                row["SYEDSP"] = item.SYEDSP;
                row["SYTNAC"] = item.SYTNAC;
                row["SYDCTO"] = item.SYDCTO;
                row["SYITM"] = item.SYITM;
                row["SYLITM"] = item.SYLITM;
                row["SYDRQJ"] = item.SYDRQJ;
                row["SYUORG"] = item.SYUORG;
                row["SYSOQS"] = item.SYSOQS;
                row["SYUM"] = item.SYUM;
                row["SYMCU"] = item.SYMCU;
                row["SYVR01"] = item.SYVR01;

                dt.Rows.Add(row);
            }
        }

        public static void PopulateF554801ZHistory(IEnumerable<OrrSysF554801ZHistory> list, DataTable dt)
        {
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["SYEDUS"] = item.SYEDUS;
                row["SYEDBT"] = item.SYEDBT;
                row["SYEDTN"] = item.SYEDTN;
                row["SYEDLN"] = item.SYEDLN;
                row["SYEDCT"] = item.SYEDCT;
                row["SYTYTN"] = item.SYTYTN;
                row["SYEDDT"] = item.SYEDDT;
                row["SYDRIN"] = item.SYDRIN;
                row["SYEDDL"] = item.SYEDDL;
                row["SYEDSP"] = item.SYEDSP;
                row["SYTNAC"] = item.SYTNAC;
                row["SYDCTO"] = item.SYDCTO;
                row["SYITM"] = item.SYITM;
                row["SYLITM"] = item.SYLITM;
                row["SYDRQJ"] = item.SYDRQJ;
                row["SYUORG"] = item.SYUORG;
                row["SYSOQS"] = item.SYSOQS;
                row["SYUM"] = item.SYUM;
                row["SYMCU"] = item.SYMCU;
                row["SYVR01"] = item.SYVR01;
                row["DateTimeImplemented"] = item.DateTimeImplemented;
                row["DateRequested"] = item.DateRequested;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreateTestCert(string tableName)
        {
            var dt = new DataTable(tableName);

            dt.Columns.Add(new DataColumn("SalesOrderNumber"));
            dt.Columns.Add(new DataColumn("SaleInvoiceNumber"));
            dt.Columns.Add(new DataColumn("CustomerName"));
            dt.Columns.Add(new DataColumn("CustomerAccountNumber"));
            dt.Columns.Add(new DataColumn("PackNumber"));
            dt.Columns.Add(new DataColumn("Item"));
            dt.Columns.Add(new DataColumn("DateUpdated"));
            dt.Columns.Add(new DataColumn("TimeUpdated"));
            dt.Columns.Add(new DataColumn("State"));
            dt.Columns.Add(new DataColumn("ShipTo"));
            dt.Columns.Add(new DataColumn("EmailAddress"));
            dt.Columns.Add(new DataColumn("Processed"));
            dt.Columns.Add(new DataColumn("PurchaseOrder"));
            dt.Columns.Add(new DataColumn("sapMatNum"));
            dt.Columns.Add(new DataColumn("DateTimeCreated", typeof(DateTime)));
            
            return dt;
        }

        public static void PopulateTestCert(List<DespatchedPacksByCustomerOrderDto> list, DataTable dt)
        {
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["SalesOrderNumber"] = item.SalesOrderNumber;
                row["SaleInvoiceNumber"] = item.SaleInvoiceNumber;
                row["CustomerName"] = item.CustomerName;
                row["CustomerAccountNumber"] = item.CustomerAccountNumber;
                row["PackNumber"] = item.PackNumber;
                row["Item"] = item.Item;
                row["DateUpdated"] = item.DateUpdated;
                row["TimeUpdated"] = item.TimeUpdated;
                row["State"] = item.State;
                row["ShipTo"] = item.ShipTo;
                row["EmailAddress"] = item.EmailAddress;
                row["Processed"] = item.Processed;
                row["PurchaseOrder"] = item.PurchaseOrder;
                row["sapMatNum"] = item.SapMatNum;
                row["DateTimeCreated"] = item.DateTimeCreated;

                dt.Rows.Add(row);
            }
        }
    }
}