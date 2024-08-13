using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spm.File.Watcher.Service.Domain;
using Spm.File.Watcher.Service.Dto;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class CastDto : ICastDto
    {
        public string AsString(IEnumerable<ProblemDto> resultList)
        {
            var sb = new StringBuilder();
            foreach (var result in resultList)
            {
                sb.AppendLine($"Error in line {result.RowNumber}.");
                sb.AppendLine($"Error: {result.Result}.");
            }
            var resultAsString = sb.ToString();

            return resultAsString;
        }

        public List<PurchaseOrderSapDto> AsPurchaseOrderSapDto(IEnumerable<PurchaseOrderDto> dataDtoList)
        {
            var sapDtoList = dataDtoList.Select(x => new PurchaseOrderSapDto
            {
                PoNumber = x.PoNumber,
                CompCode = x.CompCode,
                DocType = x.DocType,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                ItemIntvl = x.ItemIntvl,
                Vendor = x.Vendor,
                PurchOrg = x.PurchOrg,
                PurGroup = x.PurGroup,
                Currency = x.Currency,
                DocDate = x.DocDate,
                PoItem = x.PoItem,
                DeleteInd = x.DeleteInd,
                ShortText = x.ShortText,
                Plant = x.Plant,
                StgeLoc = x.StgeLoc,
                MatlGroup = x.MatlGroup,
                VendMat = x.VendMat,
                Quantity = x.Quantity,
                PoUnit = x.PoUnit,
                OrderPrUn = x.OrderPrUn,
                NetPrice = x.NetPrice,
                PriceUnit = x.PriceUnit,
                OverDlvTol = x.OverDlvTol,
                NoMoreGr = x.NoMoreGr,
                Acctasscat = x.Acctasscat,
                PreqName = x.PreqName,
                SerialNo = x.SerialNo,
                GlAccount = x.GlAccount,
                CostCenter = x.CostCenter,
                DeliveryDate = x.DeliveryDate,
                LnType = x.LnType,
                Taxable = x.Taxable
            }).ToList();

            return sapDtoList;
        }

        public List<GeneralLedgerSapDto> AsGeneralLedgerSapDto(IEnumerable<GeneralLedgerItemDto> dtoGroupForMessage)
        {
            var sapDtoGroupForMessage = dtoGroupForMessage.Select(x => new GeneralLedgerSapDto
            {
                HeaderTxt = x.HeaderTxt,
                CompCode = x.CompCode,
                DocDate = x.DocDate,
                PstingDate = x.PstingDate,
                RefDocNo = x.RefDocNo,
                AllocNmbr = x.AllocNmbr,
                CostCentre = x.CostCentre,
                GlAccount = x.GlAccount,
                Currency = x.Currency,
                AmtDoccur = x.AmtDoccur,
                ItemNoAcc = x.ItemNoAcc
            }).ToList();
            return sapDtoGroupForMessage;
        }

        public List<GeneralLedgerItemDto> AsGeneralLedgerItemDto(IEnumerable<GeneralLedgerDto> dataDtoListSort)
        {
            var rowCountDataDtoList = dataDtoListSort.Select(x => new GeneralLedgerItemDto
            {
                ItemNoAcc = 0,
                HeaderTxt = x.HeaderTxt,
                CompCode = x.CompCode,
                DocDate = x.DocDate,
                PstingDate = x.PstingDate,
                RefDocNo = x.RefDocNo,
                AllocNmbr = x.AllocNmbr,
                CostCentre = x.CostCentre,
                GlAccount = x.GlAccount,
                Currency = x.Currency,
                AmtDoccur = x.AmtDoccur
            }).ToList();
            return rowCountDataDtoList;
        }

        public IList<PurchaseOrderCreateFileData> AsPurchaseOrderCreateFileData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId)
        {
            var returnVal = dtoList.Select(dto => new PurchaseOrderCreateFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PoNumber = dto.PoNumber,
                CompCode = dto.CompCode,
                DocType = dto.DocType,
                CreatDate = dto.CreateDate,
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
                OrderprUn = dto.OrderPrUn,
                NetPrice = dto.NetPrice,
                PriceUnit = dto.PriceUnit,
                OverDlvTol = dto.OverDlvTol,
                NoMoreGr = dto.NoMoreGr,
                Acctasscat = dto.Acctasscat,
                PreqName = dto.PreqName,
                SerialNo = dto.SerialNo,
                GlAccount = dto.GlAccount,
                Costcenter = dto.CostCenter,
                DeliveryDate = dto.DeliveryDate,
                LnType = dto.LnType,
                Taxable = dto.Taxable
            }).ToList();

            return returnVal;
        }

        public IList<PurchaseOrderChangeFileData> AsPurchaseOrderChangeFileData(IList<PurchaseOrderDto> dtoList, string fileName, string sagaReferenceId)
        {
            var returnVal = dtoList.Select(dto => new PurchaseOrderChangeFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,

                PoNumber = dto.PoNumber,
                CompCode = dto.CompCode,
                DocType = dto.DocType,
                CreatDate = dto.CreateDate,
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
                OrderprUn = dto.OrderPrUn,
                NetPrice = dto.NetPrice,
                PriceUnit = dto.PriceUnit,
                OverDlvTol = dto.OverDlvTol,
                NoMoreGr = dto.NoMoreGr,
                Acctasscat = dto.Acctasscat,
                PreqName = dto.PreqName,
                SerialNo = dto.SerialNo,
                GlAccount = dto.GlAccount,
                Costcenter = dto.CostCenter,
                DeliveryDate = dto.DeliveryDate,
                LnType = dto.LnType,
                Taxable = dto.Taxable
            }).ToList();

            return returnVal;
        }

        public IList<GoodsBaseFileData> AsGoodsFileData(IEnumerable<GoodsSagaDto> dtoList, string fileName, string type)
        {
            var returnVal = dtoList.Select(dto => new GoodsBaseFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = dto.SagaReferenceId,
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
                ReceiptDoc = string.Equals(type.ToUpper(), Shared.Constants.GoodsReversalType.ToUpper()) ? dto.ReceiptDoc : string.Empty
            }).ToList();

            return returnVal;
        }

        public IList<GeneralLedgerFileData> AsGeneralLederFileData(IList<GeneralLedgerItemDto> dtoList, string fileName, string sagaReferenceId)
        {
            var returnVal = dtoList.Select(dto => new GeneralLedgerFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = sagaReferenceId,
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
            }).ToList();

            return returnVal;
        }

        public IList<MaterialMasterFileData> AsMaterialMasterFileData(IList<MaterialMasterSagaDto> dtoList, string fileName)
        {
            var returnVal = dtoList.Select(dto => new MaterialMasterFileData
            {
                DataId = Guid.NewGuid(),
                DateTimeFileUploaded = DateTime.Now,
                FileName = fileName,
                SagaReferenceId = dto.SagaReferenceId,

                Itm = dto.Itm,
                Litm = dto.Litm,
                Aitm = dto.Aitm,
                Dsc1 = dto.Dsc1,
                Dsc2 = dto.Dsc2,
                Dsc12 = dto.Dsc12,
                LongDsc = dto.LongDsc,
                Mcu = dto.Mcu,
                Stkt = dto.Stkt,
                Draw = dto.Draw,
                Srp3Desc = dto.Srp3Desc,
                Srp4Desc = dto.Srp4Desc,
                Srp4Desc2 = dto.Srp4Desc2,
                Srp5Desc = dto.Srp5Desc,
                Srp6Desc = dto.Srp6Desc,
                Srp7Desc = dto.Srp7Desc,
                Srp8Desc = dto.Srp8Desc,
                Prp0 = dto.Prp0,
                Prp0M = dto.Prp0M,
                Prp1Desc = dto.Prp1Desc,
                Prp1Desc2 = dto.Prp1Desc2,
                Prp2Desc = dto.Prp2Desc,
                Prp3Desc = dto.Prp3Desc,
                Sec1 = dto.Sec1,
                Sec2 = dto.Sec2,
                Prp4Desc = dto.Prp4Desc,
                Prp5Desc = dto.Prp5Desc,
                Prp6Desc = dto.Prp6Desc,
                Imprp3 = dto.Imprp3,
                Imprp3Desc = dto.Imprp3Desc,
                Imprp4 = dto.Imprp4,
                Imprp4Desc = dto.Imprp4Desc,
                Imuom1 = dto.Imuom1,
                Bq = dto.Bq,
                BwCk = dto.BwCk,
                Kgs = dto.Kgs,
                Mt = dto.Mt,
                GhMm = dto.GhMm,
                GwMm = dto.GwMm,
                Kg = dto.Kg
            }).ToList();

            return returnVal;
        }

        public MaterialMasterSagaDto AsMaterialMasterSagaDto(MaterialMasterDto dto)
        {
            var returnVal = new MaterialMasterSagaDto
            {
                SagaReferenceId = string.Empty,
                Itm = dto.Itm,
                Litm = dto.Litm,
                Aitm = dto.Aitm,
                Dsc1 = dto.Dsc1,
                Dsc2 = dto.Dsc2,
                Dsc12 = dto.Dsc12,
                LongDsc = dto.LongDsc,
                Mcu = dto.Mcu,
                Stkt = dto.Stkt,
                Draw = dto.Draw,
                Srp3Desc = dto.Srp3Desc,
                Srp4Desc = dto.Srp4Desc,
                Srp4Desc2 = dto.Srp4Desc2,
                Srp5Desc = dto.Srp5Desc,
                Srp6Desc = dto.Srp6Desc,
                Srp7Desc = dto.Srp7Desc,
                Srp8Desc = dto.Srp8Desc,
                Prp0 = dto.Prp0,
                Prp0M = dto.Prp0M,
                Prp1Desc = dto.Prp1Desc,
                Prp1Desc2 = dto.Prp1Desc2,
                Prp2Desc = dto.Prp2Desc,
                Prp3Desc = dto.Prp3Desc,
                Sec1 = dto.Sec1,
                Sec2 = dto.Sec2,
                Prp4Desc = dto.Prp4Desc,
                Prp5Desc = dto.Prp5Desc,
                Prp6Desc = dto.Prp6Desc,
                Imprp3 = dto.Imprp3,
                Imprp3Desc = dto.Imprp3Desc,
                Imprp4 = dto.Imprp4,
                Imprp4Desc = dto.Imprp4Desc,
                Imuom1 = dto.Imuom1,
                Bq = dto.Bq,
                BwCk = dto.BwCk,
                Kgs = dto.Kgs,
                Mt = dto.Mt,
                GhMm = dto.GhMm,
                GwMm = dto.GwMm,
                Kg = dto.Kg,
                Prp4 = dto.Prp4,
                Srp1 = dto.Srp1,
                Srp1Desc = dto.Srp1Desc,
                Prp3Desc2 = dto.Prp3Desc2,
                Prp2Desc2 = dto.Prp2Desc2,
                UnitCost = dto.UnitCost
            };

            return returnVal;
        }
    }
}