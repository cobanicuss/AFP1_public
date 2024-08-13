using System;
using System.Collections.Generic;
using System.Data;
using Spm.File.Watcher.Service.Domain;

namespace Spm.File.Watcher.Service.Repository
{
    public class DataTableSetupForBulkInsert
    {
        public static DataTable CreateGoodsReceiptFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid)));
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("PstngDate"));
            dt.Columns.Add(new DataColumn("DocDate"));
            dt.Columns.Add(new DataColumn("RefDocNo"));
            dt.Columns.Add(new DataColumn("HeaderTxt"));
            dt.Columns.Add(new DataColumn("GmCode"));
            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("StgeLoc"));
            dt.Columns.Add(new DataColumn("MoveType"));
            dt.Columns.Add(new DataColumn("EntryQnt"));
            dt.Columns.Add(new DataColumn("EntryUom"));
            dt.Columns.Add(new DataColumn("PoNumber"));
            dt.Columns.Add(new DataColumn("PoItem"));
            dt.Columns.Add(new DataColumn("MvtInd"));
            dt.Columns.Add(new DataColumn("NoMoreGr"));
            dt.Columns.Add(new DataColumn("TranTypeInd"));
            dt.Columns.Add(new DataColumn("Id"));
            dt.Columns.Add(new DataColumn("OrderType"));
            dt.Columns.Add(new DataColumn("DocType"));
            dt.Columns.Add(new DataColumn("ReceiptDoc"));

            return dt;
        }

        public static void PopulateGoodsReceipt(IEnumerable<GoodsBaseFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();
                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["PstngDate"] = item.PstngDate;
                row["DocDate"] = item.DocDate;
                row["RefDocNo"] = item.RefDocNo;
                row["HeaderTxt"] = item.HeaderTxt;
                row["GmCode"] = item.GmCode;
                row["Plant"] = item.Plant;
                row["StgeLoc"] = item.StgeLoc;
                row["MoveType"] = item.MoveType;
                row["EntryQnt"] = item.EntryQnt;
                row["EntryUom"] = item.EntryUom;
                row["PoNumber"] = item.PoNumber;
                row["PoItem"] = item.PoItem;
                row["MvtInd"] = item.MvtInd;
                row["NoMoreGr"] = item.NoMoreGr;
                row["TranTypeInd"] = item.TranTypeInd;
                row["Id"] = item.Id;
                row["OrderType"] = item.OrderType;
                row["DocType"] = item.DocType;
                row["ReceiptDoc"] = item.ReceiptDoc;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreateGoodsReversalFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid)));
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("PstngDate"));
            dt.Columns.Add(new DataColumn("DocDate"));
            dt.Columns.Add(new DataColumn("RefDocNo"));
            dt.Columns.Add(new DataColumn("HeaderTxt"));
            dt.Columns.Add(new DataColumn("GmCode"));
            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("StgeLoc"));
            dt.Columns.Add(new DataColumn("MoveType"));
            dt.Columns.Add(new DataColumn("EntryQnt"));
            dt.Columns.Add(new DataColumn("EntryUom"));
            dt.Columns.Add(new DataColumn("PoNumber"));
            dt.Columns.Add(new DataColumn("PoItem"));
            dt.Columns.Add(new DataColumn("MvtInd"));
            dt.Columns.Add(new DataColumn("NoMoreGr"));
            dt.Columns.Add(new DataColumn("TranTypeInd"));
            dt.Columns.Add(new DataColumn("Id"));
            dt.Columns.Add(new DataColumn("OrderType"));
            dt.Columns.Add(new DataColumn("DocType"));
            dt.Columns.Add(new DataColumn("ReceiptDoc"));

            return dt;
        }

        public static void PopulateGoodsReversal(IEnumerable<GoodsBaseFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();
                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["PstngDate"] = item.PstngDate;
                row["DocDate"] = item.DocDate;
                row["RefDocNo"] = item.RefDocNo;
                row["HeaderTxt"] = item.HeaderTxt;
                row["GmCode"] = item.GmCode;
                row["Plant"] = item.Plant;
                row["StgeLoc"] = item.StgeLoc;
                row["MoveType"] = item.MoveType;
                row["EntryQnt"] = item.EntryQnt;
                row["EntryUom"] = item.EntryUom;
                row["PoNumber"] = item.PoNumber;
                row["PoItem"] = item.PoItem;
                row["MvtInd"] = item.MvtInd;
                row["NoMoreGr"] = item.NoMoreGr;
                row["TranTypeInd"] = item.TranTypeInd;
                row["Id"] = item.Id;
                row["OrderType"] = item.OrderType;
                row["DocType"] = item.DocType;
                row["ReceiptDoc"] = item.ReceiptDoc;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreateGeneralLedgerFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid)));
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("HeaderTxt"));
            dt.Columns.Add(new DataColumn("CompCode"));
            dt.Columns.Add(new DataColumn("DocDate"));
            dt.Columns.Add(new DataColumn("PstingDate"));
            dt.Columns.Add(new DataColumn("RefDocNo"));
            dt.Columns.Add(new DataColumn("AllocNmbr"));
            dt.Columns.Add(new DataColumn("CostCentre"));
            dt.Columns.Add(new DataColumn("GlAccount"));
            dt.Columns.Add(new DataColumn("Currency"));
            dt.Columns.Add(new DataColumn("AmtDoccur"));

            return dt;
        }

        public static void PopulateGeneralLedger(IList<GeneralLedgerFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();

                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["HeaderTxt"] = item.HeaderTxt;
                row["CompCode"] = item.CompCode;
                row["DocDate"] = item.DocDate;
                row["PstingDate"] = item.PstingDate;
                row["RefDocNo"] = item.RefDocNo;
                row["AllocNmbr"] = item.AllocNmbr;
                row["CostCentre"] = item.CostCentre;
                row["GlAccount"] = item.GlAccount;
                row["Currency"] = item.Currency;
                row["AmtDoccur"] = item.AmtDoccur;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreatePurchaseOrderChangeFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid)));
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("PoNumber"));
            dt.Columns.Add(new DataColumn("CompCode"));
            dt.Columns.Add(new DataColumn("DocType"));
            dt.Columns.Add(new DataColumn("CreatDate"));
            dt.Columns.Add(new DataColumn("CreatedBy"));
            dt.Columns.Add(new DataColumn("ItemIntvl"));
            dt.Columns.Add(new DataColumn("Vendor"));
            dt.Columns.Add(new DataColumn("PurchOrg"));
            dt.Columns.Add(new DataColumn("PurGroup"));
            dt.Columns.Add(new DataColumn("Currency"));
            dt.Columns.Add(new DataColumn("DocDate"));
            dt.Columns.Add(new DataColumn("PoItem"));
            dt.Columns.Add(new DataColumn("DeleteInd"));
            dt.Columns.Add(new DataColumn("ShortText"));
            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("StgeLoc"));
            dt.Columns.Add(new DataColumn("MatlGroup"));
            dt.Columns.Add(new DataColumn("VendMat"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("PoUnit"));
            dt.Columns.Add(new DataColumn("OrderprUn"));
            dt.Columns.Add(new DataColumn("NetPrice"));
            dt.Columns.Add(new DataColumn("PriceUnit"));
            dt.Columns.Add(new DataColumn("OverDlvTol"));
            dt.Columns.Add(new DataColumn("NoMoreGr"));
            dt.Columns.Add(new DataColumn("Acctasscat"));
            dt.Columns.Add(new DataColumn("PreqName"));
            dt.Columns.Add(new DataColumn("SerialNo"));
            dt.Columns.Add(new DataColumn("GlAccount"));
            dt.Columns.Add(new DataColumn("Costcenter"));
            dt.Columns.Add(new DataColumn("DeliveryDate"));
            dt.Columns.Add(new DataColumn("LnType"));
            dt.Columns.Add(new DataColumn("Taxable"));

            return dt;
        }

        public static void PopulatePurchaseOrderChange(IList<PurchaseOrderChangeFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();

                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["PoNumber"] = item.PoNumber;
                row["CompCode"] = item.CompCode;
                row["DocType"] = item.DocType;
                row["CreatDate"] = item.CreatDate;
                row["CreatedBy"] = item.CreatedBy;
                row["ItemIntvl"] = item.ItemIntvl;
                row["Vendor"] = item.Vendor;
                row["PurchOrg"] = item.PurchOrg;
                row["PurGroup"] = item.PurGroup;
                row["Currency"] = item.Currency;
                row["DocDate"] = item.DocDate;
                row["PoItem"] = item.PoItem;
                row["DeleteInd"] = item.DeleteInd;
                row["ShortText"] = item.ShortText;
                row["Plant"] = item.Plant;
                row["StgeLoc"] = item.StgeLoc;
                row["MatlGroup"] = item.MatlGroup;
                row["VendMat"] = item.VendMat;
                row["Quantity"] = item.Quantity;
                row["PoUnit"] = item.PoUnit;
                row["OrderprUn"] = item.OrderprUn;
                row["NetPrice"] = item.NetPrice;
                row["PriceUnit"] = item.PriceUnit;
                row["OverDlvTol"] = item.OverDlvTol;
                row["NoMoreGr"] = item.NoMoreGr;
                row["Acctasscat"] = item.Acctasscat;
                row["PreqName"] = item.PreqName;
                row["SerialNo"] = item.SerialNo;
                row["GlAccount"] = item.GlAccount;
                row["Costcenter"] = item.Costcenter;
                row["DeliveryDate"] = item.DeliveryDate;
                row["LnType"] = item.LnType;
                row["Taxable"] = item.Taxable;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreatePurchaseOrderCreateFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid)));
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("PoNumber"));
            dt.Columns.Add(new DataColumn("CompCode"));
            dt.Columns.Add(new DataColumn("DocType"));
            dt.Columns.Add(new DataColumn("CreatDate"));
            dt.Columns.Add(new DataColumn("CreatedBy"));
            dt.Columns.Add(new DataColumn("ItemIntvl"));
            dt.Columns.Add(new DataColumn("Vendor"));
            dt.Columns.Add(new DataColumn("PurchOrg"));
            dt.Columns.Add(new DataColumn("PurGroup"));
            dt.Columns.Add(new DataColumn("Currency"));
            dt.Columns.Add(new DataColumn("DocDate"));
            dt.Columns.Add(new DataColumn("PoItem"));
            dt.Columns.Add(new DataColumn("DeleteInd"));
            dt.Columns.Add(new DataColumn("ShortText"));
            dt.Columns.Add(new DataColumn("Plant"));
            dt.Columns.Add(new DataColumn("StgeLoc"));
            dt.Columns.Add(new DataColumn("MatlGroup"));
            dt.Columns.Add(new DataColumn("VendMat"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("PoUnit"));
            dt.Columns.Add(new DataColumn("OrderprUn"));
            dt.Columns.Add(new DataColumn("NetPrice"));
            dt.Columns.Add(new DataColumn("PriceUnit"));
            dt.Columns.Add(new DataColumn("OverDlvTol"));
            dt.Columns.Add(new DataColumn("NoMoreGr"));
            dt.Columns.Add(new DataColumn("Acctasscat"));
            dt.Columns.Add(new DataColumn("PreqName"));
            dt.Columns.Add(new DataColumn("SerialNo"));
            dt.Columns.Add(new DataColumn("GlAccount"));
            dt.Columns.Add(new DataColumn("Costcenter"));
            dt.Columns.Add(new DataColumn("DeliveryDate"));
            dt.Columns.Add(new DataColumn("LnType"));
            dt.Columns.Add(new DataColumn("Taxable"));

            return dt;
        }

        public static void PopulatePurchaseOrderCreate(IList<PurchaseOrderCreateFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();

                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["PoNumber"] = item.PoNumber;
                row["CompCode"] = item.CompCode;
                row["DocType"] = item.DocType;
                row["CreatDate"] = item.CreatDate;
                row["CreatedBy"] = item.CreatedBy;
                row["ItemIntvl"] = item.ItemIntvl;
                row["Vendor"] = item.Vendor;
                row["PurchOrg"] = item.PurchOrg;
                row["PurGroup"] = item.PurGroup;
                row["Currency"] = item.Currency;
                row["DocDate"] = item.DocDate;
                row["PoItem"] = item.PoItem;
                row["DeleteInd"] = item.DeleteInd;
                row["ShortText"] = item.ShortText;
                row["Plant"] = item.Plant;
                row["StgeLoc"] = item.StgeLoc;
                row["MatlGroup"] = item.MatlGroup;
                row["VendMat"] = item.VendMat;
                row["Quantity"] = item.Quantity;
                row["PoUnit"] = item.PoUnit;
                row["OrderprUn"] = item.OrderprUn;
                row["NetPrice"] = item.NetPrice;
                row["PriceUnit"] = item.PriceUnit;
                row["OverDlvTol"] = item.OverDlvTol;
                row["NoMoreGr"] = item.NoMoreGr;
                row["Acctasscat"] = item.Acctasscat;
                row["PreqName"] = item.PreqName;
                row["SerialNo"] = item.SerialNo;
                row["GlAccount"] = item.GlAccount;
                row["Costcenter"] = item.Costcenter;
                row["DeliveryDate"] = item.DeliveryDate;
                row["LnType"] = item.LnType;
                row["Taxable"] = item.Taxable;

                dt.Rows.Add(row);
            }
        }

        public static DataTable CreateMaterialMasterFileData(string tableName)
        {
            var dt = new DataTable(tableName);
            dt.Columns.Add(new DataColumn("DataId", typeof(Guid))); 
            dt.Columns.Add(new DataColumn("DateTimeFileUploaded", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("SagaReferenceId"));
            dt.Columns.Add(new DataColumn("Itm"));
            dt.Columns.Add(new DataColumn("Litm"));
            dt.Columns.Add(new DataColumn("Aitm"));
            dt.Columns.Add(new DataColumn("Dsc1"));
            dt.Columns.Add(new DataColumn("Dsc2"));
            dt.Columns.Add(new DataColumn("Dsc12"));
            dt.Columns.Add(new DataColumn("LongDsc"));
            dt.Columns.Add(new DataColumn("Mcu"));
            dt.Columns.Add(new DataColumn("Stkt"));
            dt.Columns.Add(new DataColumn("Draw"));
            dt.Columns.Add(new DataColumn("Srp3Desc"));
            dt.Columns.Add(new DataColumn("Srp4Desc"));
            dt.Columns.Add(new DataColumn("Srp4Desc2"));
            dt.Columns.Add(new DataColumn("Srp5Desc"));
            dt.Columns.Add(new DataColumn("Srp6Desc"));
            dt.Columns.Add(new DataColumn("Srp7Desc"));
            dt.Columns.Add(new DataColumn("Srp8Desc"));
            dt.Columns.Add(new DataColumn("Prp0"));
            dt.Columns.Add(new DataColumn("Prp0M"));
            dt.Columns.Add(new DataColumn("Prp1Desc"));
            dt.Columns.Add(new DataColumn("Prp1Desc2"));
            dt.Columns.Add(new DataColumn("Prp2Desc"));
            dt.Columns.Add(new DataColumn("Prp3Desc"));
            dt.Columns.Add(new DataColumn("Sec1"));
            dt.Columns.Add(new DataColumn("Sec2"));
            dt.Columns.Add(new DataColumn("Prp4Desc"));
            dt.Columns.Add(new DataColumn("Prp5Desc"));
            dt.Columns.Add(new DataColumn("Prp6Desc"));
            dt.Columns.Add(new DataColumn("Imprp3"));
            dt.Columns.Add(new DataColumn("Imprp3Desc"));
            dt.Columns.Add(new DataColumn("Imprp4"));
            dt.Columns.Add(new DataColumn("Imprp4Desc"));
            dt.Columns.Add(new DataColumn("Imuom1"));
            dt.Columns.Add(new DataColumn("Bq"));
            dt.Columns.Add(new DataColumn("BwCk"));
            dt.Columns.Add(new DataColumn("Kgs"));
            dt.Columns.Add(new DataColumn("Mt"));
            dt.Columns.Add(new DataColumn("GhMm"));
            dt.Columns.Add(new DataColumn("GwMm"));
            dt.Columns.Add(new DataColumn("Kg"));

            return dt;
        }

        public static void PopulateMaterialMaster(IList<MaterialMasterFileData> dtoList, DataTable dt)
        {
            foreach (var item in dtoList)
            {
                var row = dt.NewRow();

                row["DataId"] = item.DataId;
                row["DateTimeFileUploaded"] = item.DateTimeFileUploaded;
                row["FileName"] = item.FileName;
                row["SagaReferenceId"] = item.SagaReferenceId;
                row["Itm"] = item.Itm;
                row["Litm"] = item.Litm;
                row["Aitm"] = item.Aitm;
                row["Dsc1"] = item.Dsc1;
                row["Dsc2"] = item.Dsc2;
                row["Dsc12"] = item.Dsc12;
                row["LongDsc"] = item.LongDsc;
                row["Mcu"] = item.Mcu;
                row["Stkt"] = item.Stkt;
                row["Draw"] = item.Draw;
                row["Srp3Desc"] = item.Srp3Desc;
                row["Srp4Desc"] = item.Srp4Desc;
                row["Srp4Desc2"] = item.Srp4Desc2;
                row["Srp5Desc"] = item.Srp5Desc;
                row["Srp6Desc"] = item.Srp6Desc;
                row["Srp7Desc"] = item.Srp7Desc;
                row["Srp8Desc"] = item.Srp8Desc;
                row["Prp0"] = item.Prp0;
                row["Prp0M"] = item.Prp0M;
                row["Prp1Desc"] = item.Prp1Desc;
                row["Prp1Desc2"] = item.Prp1Desc2;
                row["Prp2Desc"] = item.Prp2Desc;
                row["Prp3Desc"] = item.Prp3Desc;
                row["Sec1"] = item.Sec1;
                row["Sec2"] = item.Sec2;
                row["Prp4Desc"] = item.Prp4Desc;
                row["Prp5Desc"] = item.Prp5Desc;
                row["Prp6Desc"] = item.Prp6Desc;
                row["Imprp3"] = item.Imprp3;
                row["Imprp3Desc"] = item.Imprp3Desc;
                row["Imprp4"] = item.Imprp4;
                row["Imprp4Desc"] = item.Imprp4Desc;
                row["Imuom1"] = item.Imuom1;
                row["Bq"] = item.Bq;
                row["BwCk"] = item.BwCk;
                row["Kgs"] = item.Kgs;
                row["Mt"] = item.Mt;
                row["GhMm"] = item.GhMm;
                row["GwMm"] = item.GwMm;
                row["Kg"] = item.Kg;

                dt.Rows.Add(row);
            }
        }
    }
}