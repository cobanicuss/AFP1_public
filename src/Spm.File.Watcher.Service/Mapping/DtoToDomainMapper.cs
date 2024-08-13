using System;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.Mapping
{
    public class DtoToDomainMapper
    {
        public static PurchaseOrderCreateFileData ToPurchaseOrderCreate(PurchaseOrderDto dto, string fileName, string sagaReferenceId)
        {
            var purchaseOrder = new PurchaseOrderCreateFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PoNumber = dto.PoNumber,
                CompCode = dto.CompCode,
                DocType = dto.DocType,
                CreatDate = dto.CreatDate,
                CreatedBy = dto.CreatedBy,
                ItemIntvl = dto.ItemIntvl,
                Vendor = dto.Vendor,
                PurchOrg = dto.PurchOrg,
                PurGroup = dto.PurGroup,
                Currency = dto.Currency,
                DocDate = dto.DocDate,
                PoItem = dto.PoItem,
                DeleteInd = dto.DeleteInd,
                ShortText = dto.ShortText,
                Plant = dto.Plant,
                StgeLoc = dto.StgeLoc,
                MatlGroup = dto.MatlGroup,
                VendMat = dto.VendMat,
                Quantity = dto.Quantity,
                PoUnit = dto.PoUnit,
                OrderprUn = dto.OrderprUn,
                NetPrice = dto.NetPrice,
                PriceUnit = dto.PriceUnit,
                OverDlvTol = dto.OverDlvTol,
                NoMoreGr = dto.NoMoreGr,
                Acctasscat = dto.Acctasscat,
                PreqName = dto.PreqName,
                SerialNo = dto.SerialNo,
                GlAccount = dto.GlAccount,
                Costcenter = dto.Costcenter,
                DeliveryDate = dto.DeliveryDate,
                LnType = dto.LnType,
                Taxable = dto.Taxable
            };

            return purchaseOrder;
        }

        public static PurchaseOrderChangeFileData ToPurchaseOrderChange(PurchaseOrderDto dto, string fileName, string sagaReferenceId)
        {
            var purchaseOrder = new PurchaseOrderChangeFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PoNumber = dto.PoNumber,
                CompCode = dto.CompCode,
                DocType = dto.DocType,
                CreatDate = dto.CreatDate,
                CreatedBy = dto.CreatedBy,
                ItemIntvl = dto.ItemIntvl,
                Vendor = dto.Vendor,
                PurchOrg = dto.PurchOrg,
                PurGroup = dto.PurGroup,
                Currency = dto.Currency,
                DocDate = dto.DocDate,
                PoItem = dto.PoItem,
                DeleteInd = dto.DeleteInd,
                ShortText = dto.ShortText,
                Plant = dto.Plant,
                StgeLoc = dto.StgeLoc,
                MatlGroup = dto.MatlGroup,
                VendMat = dto.VendMat,
                Quantity = dto.Quantity,
                PoUnit = dto.PoUnit,
                OrderprUn = dto.OrderprUn,
                NetPrice = dto.NetPrice,
                PriceUnit = dto.PriceUnit,
                OverDlvTol = dto.OverDlvTol,
                NoMoreGr = dto.NoMoreGr,
                Acctasscat = dto.Acctasscat,
                PreqName = dto.PreqName,
                SerialNo = dto.SerialNo,
                GlAccount = dto.GlAccount,
                Costcenter = dto.Costcenter,
                DeliveryDate = dto.DeliveryDate,
                LnType = dto.LnType,
                Taxable = dto.Taxable
            };

            return purchaseOrder;
        }

        public static GoodsReceiptFileData ToGoodsReceipt(GoodsDto dto, string fileName, string sagaReferenceId)
        {
            var goodsReceipt = new GoodsReceiptFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PstngDate = dto.PstngDate,
                DocDate = dto.DocDate,
                RefDocNo = dto.RefDocNo,
                HeaderTxt = dto.HeaderTxt,
                GmCode = dto.GmCode,
                Plant = dto.Plant,
                StgeLoc = dto.StgeLoc,
                MoveType = dto.MoveType,
                EntryQnt = dto.EntryQnt,
                EntryUom = dto.EntryUom,
                PoNumber = dto.PoNumber,
                PoItem = dto.PoItem,
                MvtInd = dto.MvtInd,
                NoMoreGr = dto.NoMoreGr,
                TranTypeInd = dto.TranTypeInd,
                Id = dto.Id,
                OrderType = dto.OrderType,
                DocType = dto.DocType
            };

            return goodsReceipt;
        }

        public static GoodsReversalFileData ToGoodsReversal(GoodsDto dto, string fileName, string sagaReferenceId)
        {
            var goodsReceipt = new GoodsReversalFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PstngDate = dto.PstngDate,
                DocDate = dto.DocDate,
                RefDocNo = dto.RefDocNo,
                HeaderTxt = dto.HeaderTxt,
                GmCode = dto.GmCode,
                Plant = dto.Plant,
                StgeLoc = dto.StgeLoc,
                MoveType = dto.MoveType,
                EntryQnt = dto.EntryQnt,
                EntryUom = dto.EntryUom,
                PoNumber = dto.PoNumber,
                PoItem = dto.PoItem,
                MvtInd = dto.MvtInd,
                NoMoreGr = dto.NoMoreGr,
                TranTypeInd = dto.TranTypeInd,
                Id = dto.Id,
                OrderType = dto.OrderType,
                DocType = dto.DocType,
                ReceiptDoc = dto.ReceiptDoc
            };

            return goodsReceipt;
        }

        public static GeneralLedgerData ToGeneralLeder(GeneralLedgerDto dto, string fileName, string sagaReferenceId)
        {
            var generalLeder = new GeneralLedgerData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                SagaReferenceId = sagaReferenceId,

                FileName = fileName,
                HeaderTxt = dto.HeaderTxt,
                CompCode = dto.CompCode,
                DocDate = dto.DocDate,
                PstingDate = dto.PstingDate,
                RefDocNo = dto.RefDocNo,
                AllocNmbr = dto.AllocNmbr,
                CostCentre = dto.CostCentre,
                GlAccount = dto.GlAccount,
                Currency = dto.Currency,
                AmtDoccur = dto.AmtDoccur
            };

            return generalLeder;
        }
    }
}