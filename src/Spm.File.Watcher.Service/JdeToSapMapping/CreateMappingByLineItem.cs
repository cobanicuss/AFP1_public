using System.Collections.Generic;
using Spm.File.Watcher.Service.Dto;
using Spm.File.Watcher.Service.Validation;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class CreateMappingByLineItem : ICreateMappingByLineItem
    {
        private readonly IImplementMapping _implementMapping;
        private readonly ICastDto _castDto;

        public CreateMappingByLineItem(
            IImplementMapping implementMapping,
            ICastDto castDto)
        {
            _implementMapping = implementMapping;
            _castDto = castDto;
        }

        public GeneralLedgerSapDto ForGeneralLedger(GeneralLedgerItemDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber)
        {
            var sap = new GeneralLedgerSapDto
            {
                UserName = Constants.DefaultUserName,
                HeaderTxt = jde.HeaderTxt,
                CompCode = Constants.Default2Zeros70,
                DocDate = _implementMapping.ForGlDocDate(jde.DocDate, mappingProblemList, rowNumber),
                PstingDate = _implementMapping.ForPostingDate(jde.PstingDate, mappingProblemList, rowNumber),
                DocType = _implementMapping.ForDocType(Constants.GenralLedger, mappingProblemList, rowNumber),
                RefDocNo = jde.RefDocNo,
                ItemNoAcc = jde.ItemNoAcc,
                AllocNmbr = jde.AllocNmbr,
                ItemText = string.Empty,
                CostCentre = _implementMapping.ForGlCostCenter(jde.GlAccount, jde.RefDocNo, jde.CostCentre, mappingProblemList, rowNumber),
                GlAccount = _implementMapping.ForGlAccount(jde.GlAccount, jde.RefDocNo, mappingProblemList, rowNumber),
                GlProfitCenter = _implementMapping.ForGlProfitCentre(jde.GlAccount, jde.CostCentre, mappingProblemList, rowNumber),
                AmtDoccur = jde.AmtDoccur,
                Currency = Constants.DefaultCurrency
            };

            return sap;
        }

        public MappingResultGoodsDto ForGoods(GoodsDto jde, int rowNumber, string type, string sagaReferenceId)
        {
            var mappingProblemList = new List<ProblemDto>();
            var sap = new GoodsSagaDto
            {
                SagaReferenceId = sagaReferenceId,
                PstngDate = _implementMapping.ForPostingDate(jde.PstngDate, mappingProblemList, rowNumber),
                DocDate = _implementMapping.ForDocDate(jde.DocDate),
                RefDocNo = jde.RefDocNo,
                HeaderTxt = _implementMapping.ForHeaderText(jde.Id, mappingProblemList, rowNumber),
                GmCode = _implementMapping.ForGmCode(jde.GmCode),
                Plant = _implementMapping.ForPlant(jde.Plant, mappingProblemList, rowNumber),
                StgeLoc = _implementMapping.ForLocation(jde.StgeLoc, mappingProblemList, rowNumber),
                MoveType = type,
                EntryQnt = jde.EntryQnt,
                EntryUom = _implementMapping.ForPoUnit(jde.EntryUom, mappingProblemList, rowNumber),
                PoNumber = _implementMapping.ForPoNumber(jde.PoNumber, mappingProblemList, rowNumber),
                PoItem = _implementMapping.ForPoItem(jde.PoItem, mappingProblemList, rowNumber),
                MvtInd = jde.MvtInd,
                NoMoreGr = string.Empty,
                TranTypeInd = jde.TranTypeInd,
                Id = jde.Id,
                OrderType = jde.OrderType,
                DocType = jde.DocType,
                ReceiptDoc = jde.ReceiptDoc
            };

            var returnVal = new MappingResultGoodsDto
            {
                Mapped = sap,
                MappingProblemList = mappingProblemList
            };

            return returnVal;
        }

        public MappingResultMaterialMasterDto ForMaterialMaster(MaterialMasterDto dto, int rowNumber, string sagaReferenceId)
        {
            var jde = _castDto.AsMaterialMasterSagaDto(dto);

            jde.SagaReferenceId = sagaReferenceId;

            var sap = new MaterialMasterSapDto();
            var mappingProblemList = new List<ProblemDto>();

            var packWeight = _implementMapping.ForPackWeight(jde.BwCk, mappingProblemList, rowNumber);
            var kgDivMt = _implementMapping.ForKgDivMt(jde.Kg, jde.Mt, mappingProblemList, rowNumber);
            var plant = _implementMapping.ForPlantBranch(jde.Mcu, mappingProblemList, rowNumber);
            var profitCenre = _implementMapping.ForProfitCenre(jde.Mcu, mappingProblemList, rowNumber);
            var storageType = _implementMapping.ForStorageType(jde.Mcu, mappingProblemList, rowNumber);
            var storageSection = _implementMapping.ForStorageSection(jde.Mcu, jde.Prp4, mappingProblemList, rowNumber);
            var materialGroup = _implementMapping.ForMaterialGroupByPlant(jde.Mcu, mappingProblemList, rowNumber);
            var productHierachy = _implementMapping.ForProductHierachy(jde.Mcu, mappingProblemList, rowNumber);
            var actualHeigh = _implementMapping.ForActualHeight(jde.Sec1, mappingProblemList, rowNumber);
            var actualWidth = _implementMapping.ForActualWidth(jde.Mcu, jde.Sec1, jde.Sec2, mappingProblemList, rowNumber);
            var length3Deci = _implementMapping.ForThreeDecimalPlacesOnly(jde.Mt);
            var redBlueBlack = _implementMapping.ForRedBlueBlack(jde.Srp4Desc);
            var physicalPackSize = _implementMapping.ForPhysicalPackSize(jde.Mcu, jde.GhMm, jde.GwMm, mappingProblemList, rowNumber);
            var prpm = _implementMapping.ForPrp(jde.Mcu, jde.Prp0, mappingProblemList, rowNumber);
            var productAttribute = _implementMapping.ForProductAttribute(jde.Mcu);
            var sizeOne = _implementMapping.ForSizeOne(jde.Mcu, jde.Sec1, jde.Dsc1, mappingProblemList, rowNumber);
            var tdLine = _implementMapping.ForTdLine(jde.Prp2Desc2, jde.Dsc1, jde.Dsc2);
            var maktx = _implementMapping.ForMaktx(jde.Dsc12);
            var dzeit = _implementMapping.ForDzeit(jde.Srp1);
            var zzdm2N = _implementMapping.ForZzdm2N(jde.Sec1, jde.Sec2);
            var verpr = _implementMapping.ForUnitCost(jde.Kg, jde.UnitCost, mappingProblemList, rowNumber);

            var denominator0 = _implementMapping.ForDenominator(jde.BwCk, mappingProblemList, rowNumber);
            var denominator1 = _implementMapping.ForDenominator(kgDivMt, mappingProblemList, rowNumber);
            var denominator2 = _implementMapping.ForDenominator(jde.Kg, mappingProblemList, rowNumber);

            var numerator0 = _implementMapping.ForNumerator(jde.BwCk, mappingProblemList, rowNumber);
            var numerator1 = _implementMapping.ForNumerator(kgDivMt, mappingProblemList, rowNumber);
            var numerator2 = _implementMapping.ForNumerator(jde.Kg, mappingProblemList, rowNumber);

            sap.SagaReferenceId = sagaReferenceId;
            sap.E1MaramBismt = jde.Aitm;
            sap.E1MarcmWerks = plant;
            sap.E1MarcmMsgfn = Constants.Default2Zeros9;
            sap.E1MaktmMaktx = maktx;
            sap.E1MaramMatkl = materialGroup;
            sap.E1MaramPrdha = productHierachy;
            sap.E1MaramGroes = jde.Kgs;
            sap.E1MarcmDzeit = dzeit;

            #region E1Cucfg[0]
            //sap.E1CucfgPosex0 is not used. Leave comment in.//
            sap.E1CucfgConfigId0 = Constants.Default5Zeros1;
            sap.E1CucfgRootId0 = Constants.Default7Zeros1;

            #region E1Cuins[0]
            sap.E1CuinsInstId0 = Constants.Default7Zeros1;
            sap.E1CuinsObjType0 = Constants.DefaultMara;
            sap.E1CuinsClassType0 = Constants.DefaultThreeHundred;
            sap.E1CuinsObjKey0 = Constants.DefaultLength;
            #endregion

            #region E1Cuval[0]
            sap.E1CuvalInstId0 = Constants.Default7Zeros1;
            sap.E1CuvalCharc0 = Constants.DefaultLengthPerKgM;
            sap.E1CuvalValue0 = kgDivMt;
            #endregion

            #region E1Cuval[1]
            sap.E1CuvalInstId1 = Constants.Default7Zeros1;
            sap.E1CuvalCharc1 = Constants.DefaultLengthActualHeight;
            sap.E1CuvalValue1 = actualHeigh;
            #endregion

            #region E1Cuval[2]
            sap.E1CuvalInstId2 = Constants.Default7Zeros1;
            sap.E1CuvalCharc2 = Constants.DefaultLengthActualWidth;
            sap.E1CuvalValue2 = actualWidth;
            #endregion

            #region E1Cuval[3]
            sap.E1CuvalInstId3 = Constants.Default7Zeros1;
            sap.E1CuvalCharc3 = Constants.DefaultLengthNrOfProcesses;
            sap.E1CuvalValue3 = Constants.Default1;
            #endregion
            #endregion

            #region E1Cucfg[1]
            sap.E1CucfgPosex1 = plant;
            sap.E1CucfgConfigId1 = Constants.Default5Zeros2;
            sap.E1CucfgRootId1 = Constants.Default7Zeros1;

            #region E1Cuins[1]
            sap.E1CuinsInstId1 = Constants.Default7Zeros1;
            sap.E1CuinsObjType1 = Constants.DefaultMara;
            sap.E1CuinsClassType1 = Constants.DefaultThreeHundred;
            sap.E1CuinsObjKey1 = Constants.DefaultLength;
            #endregion

            #region E1Cuval[4]
            sap.E1CuvalInstId4 = Constants.Default7Zeros1;
            sap.E1CuvalCharc4 = Constants.DefaultLengthPerKgM;
            sap.E1CuvalValue4 = kgDivMt;
            #endregion

            #region E1Cuval[5]
            sap.E1CuvalInstId5 = Constants.Default7Zeros1;
            sap.E1CuvalCharc5 = Constants.DefaultLengthActualHeight;
            sap.E1CuvalValue5 = actualHeigh;
            #endregion

            #region E1Cuval[6]
            sap.E1CuvalInstId6 = Constants.Default7Zeros1;
            sap.E1CuvalCharc6 = Constants.DefaultLengthActualWidth;
            sap.E1CuvalValue6 = actualWidth;
            #endregion

            #region E1Cuval[7]
            sap.E1CuvalInstId7 = Constants.Default7Zeros1;
            sap.E1CuvalCharc7 = Constants.DefaultLengthNrOfProcesses;
            sap.E1CuvalValue7 = Constants.Default1;
            #endregion
            #endregion

            #region E1Mvkem[0]
            sap.E1MvkemMsgfn0 = Constants.Default2Zeros9;
            sap.E1MvkemVkorg0 = Constants.Default2Zeros20;
            sap.E1MvkemProdh0 = productHierachy;
            sap.E1MvkemPrat10 = productAttribute;
            sap.E1MvkemPrat40 = productAttribute;
            #endregion

            #region E1Mvkem[1]
            sap.E1MvkemMsgfn1 = Constants.Default2Zeros9;
            sap.E1MvkemVkorg1 = Constants.Default2Zeros70;
            sap.E1MvkemProdh1 = productHierachy;
            sap.E1MvkemPrat11 = productAttribute;
            sap.E1MvkemPrat41 = productAttribute;
            #endregion

            sap.E1MarcmPrctr = profitCenre;

            #region E1Mtxhm[E1Mtxlm[0]]
            sap.E1MtxhmMsgfn0 = Constants.Default2Zeros9;
            sap.E1MtxhmTdobject0 = Constants.DefaultMvke;
            sap.E1MtxhmTdid0 = Constants.Default3Zeros1;
            sap.E1MtxhmTdspras0 = Constants.DefaultEn;
            sap.E1MtxhmTdtexttype0 = Constants.DefaultTx;
            sap.E1MtxhmTdname0 = Constants.SalesTxtName1;
            sap.E1MtxlmMsgfn0 = Constants.Default2Zeros9;
            sap.E1MtxlmTdformat0 = Constants.DefaultStar;
            sap.E1MtxlmTdline0 = tdLine;
            #endregion

            #region E1Mtxhm[E1Mtxlm[1]]
            sap.E1MtxhmMsgfn1 = Constants.Default2Zeros9;
            sap.E1MtxhmTdobject1 = Constants.DefaultMaterial;
            sap.E1MtxhmTdid1 = Constants.DefaultBest;
            sap.E1MtxhmTdspras1 = Constants.DefaultEn;
            sap.E1MtxhmTdtexttype1 = Constants.DefaultTx;
            //sap.E1MtxhmTdname1 is not used. Leave comment in.//
            sap.E1MtxlmMsgfn1 = Constants.Default2Zeros9;
            sap.E1MtxlmTdformat1 = Constants.DefaultStar;
            sap.E1MtxlmTdline1 = tdLine;
            #endregion

            #region E1Mtxhm[E1Mtxlm[2]]
            sap.E1MtxhmMsgfn2 = Constants.Default2Zeros9;
            sap.E1MtxhmTdobject2 = Constants.DefaultMvke;
            sap.E1MtxhmTdid2 = Constants.Default3Zeros1;
            sap.E1MtxhmTdspras2 = Constants.DefaultEn;
            sap.E1MtxhmTdtexttype2 = Constants.DefaultTx;
            sap.E1MtxhmTdname2 = Constants.SalesTxtName2;
            sap.E1MtxlmMsgfn2 = Constants.Default2Zeros9;
            sap.E1MtxlmTdformat2 = Constants.DefaultStar;
            sap.E1MtxlmTdline2 = tdLine;
            #endregion

            sap.E1MarcmEkgrp = prpm;
            sap.E1MarcmMaabc = Constants.DefaultA;
            sap.E1MarcmDispr = jde.Srp6Desc;
            sap.E1MarcmBstrf = packWeight;
            sap.E1MlgnmMsgfn = Constants.Default2Zeros9;
            sap.E1MlgnmLgnum = storageType;
            sap.E1MlgnmLgbkz = storageSection;
            sap.E1MlgnmLhmg1 = jde.Bq;
            sap.E1MlgtmKober = storageSection;
            sap.E1MlgtmNsmng = packWeight;
            sap.E1MbewmMsgfn = Constants.Default2Zeros9;
            sap.E1MbewmBwkey = plant;
            sap.E1MbewmVerpr = verpr;
            sap.Z1MaramZzsan = jde.Prp5Desc;
            sap.Z1MaramZzquln = jde.Prp2Desc2;
            sap.Z1MaramZzpcsn = jde.Prp1Desc2;

            sap.Z1MaramZzdm1N = sizeOne;

            sap.Z1MaramZzdm2N = zzdm2N;
            sap.Z1MaramZzdm5N = length3Deci;
            sap.Z1MaramZzcol1 = redBlueBlack;
            sap.Z1MaramZzprfn = jde.Srp7Desc;
            sap.Z1MaramZzstol = jde.Draw;
            sap.Z1MaramZzroll = jde.Prp0;
            sap.Z1MaramZzmtwc = jde.Prp4;
            sap.Z1MaramZzpkdm = physicalPackSize;

            #region E1Marmm[0]
            sap.E1MarmmMsgfn0 = Constants.Default2Zeros9;
            sap.E1MarmmMeinh0 = Constants.DefaultPk;
            sap.E1MarmmUmren0 = denominator0;
            sap.E1MarmmUmrez0 = numerator0;
            #endregion

            #region E1Marmm[1]
            sap.E1MarmmMsgfn1 = Constants.Default2Zeros9;
            sap.E1MarmmMeinh1 = Constants.DefaultM;
            sap.E1MarmmUmren1 = denominator1;
            sap.E1MarmmUmrez1 = numerator1;
            #endregion

            #region E1Marmm[2]
            sap.E1MarmmMsgfn2 = Constants.Default2Zeros9;
            sap.E1MarmmMeinh2 = Constants.DefaultEa;
            sap.E1MarmmUmren2 = denominator2;
            sap.E1MarmmUmrez2 = numerator2;
            #endregion

            #region E1Marmm[3]
            sap.E1MarmmMsgfn3 = Constants.Default2Zeros9;
            sap.E1MarmmMeinh3 = Constants.DefaultTn;
            sap.E1MarmmUmren3 = Constants.Default1;
            sap.E1MarmmUmrez3 = Constants.DefaultOneThousand;
            #endregion

            #region E1Marmm[4]
            sap.E1MarmmMsgfn4 = Constants.Default2Zeros5;
            sap.E1MarmmMeinh4 = Constants.DefaultKilograms;
            sap.E1MarmmUmren4 = Constants.Default1;
            sap.E1MarmmUmrez4 = Constants.Default1;
            sap.E1MarmmNumtp4 = Constants.DefaultZg;
            sap.E1MarmmVolume4 = Constants.Default1;
            sap.E1MarmmVoleh4 = Constants.DefaultKgp;
            sap.E1MarmmBrgew4 = Constants.Default1;
            sap.E1MarmmGewei = Constants.DefaultKilograms;
            #endregion

            var returnVal = new MappingResultMaterialMasterDto
            {
                Mapped = new KeyValuePair<MaterialMasterSagaDto, MaterialMasterSapDto>(jde, sap),
                MappingProblemList = mappingProblemList
            };

            return returnVal;
        }

        public PurchaseOrderSapDto ForPurchaseOrderCreate(PurchaseOrderDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber)
        {
            var sap = new PurchaseOrderSapDto
            {
                PoNumber = jde.PoNumber,
                CompCode = _implementMapping.ForCompCode(jde.CompCode, mappingProblemList, rowNumber),
                DocType = _implementMapping.ForDocType(Constants.PurchaseOrderCreate, mappingProblemList, rowNumber),
                CreateDate = _implementMapping.ForCreateDate(jde.CreateDate, mappingProblemList, rowNumber),
                CreatedBy = Constants.DefaultUserName,
                ItemIntvl = Constants.DefaultPentaZero,
                Vendor = _implementMapping.ForVendor(jde.Vendor, mappingProblemList, rowNumber),
                PurchOrg = _implementMapping.ForPurchaseOrg(jde.PurchOrg),
                PurGroup = _implementMapping.ForPurGroup(jde.PurGroup, mappingProblemList, rowNumber),
                Currency = jde.Currency,
                PoItem = _implementMapping.ForPoItem(jde.PoItem, mappingProblemList, rowNumber),
                DeleteInd = jde.DeleteInd,
                ShortText = jde.ShortText,
                Plant = _implementMapping.ForPlant(jde.Plant, mappingProblemList, rowNumber),
                StgeLoc = Constants.Default3Zeros1,
                MatlGroup = _implementMapping.ForMaterialGroup(jde.Plant, jde.LnType, jde.Taxable, mappingProblemList, rowNumber),
                VendMat = jde.VendMat,
                Quantity = jde.Quantity,
                PoUnit = _implementMapping.ForPoUnit(jde.PoUnit, mappingProblemList, rowNumber),
                OrderPrUn = _implementMapping.ForOrderPrUn(jde.OrderPrUn, mappingProblemList, rowNumber),
                NetPrice = _implementMapping.ForNetPrice(jde.NetPrice, mappingProblemList, rowNumber),
                PriceUnit = jde.PriceUnit,
                OverDlvTol = jde.OverDlvTol,
                Acctasscat = jde.Acctasscat,
                PreqName = jde.PreqName,
                SchedLine = Constants.Default3Zeros1,
                SerialNo = jde.SerialNo,
                GlAccount = _implementMapping.ForGlAccount(jde.Plant, jde.LnType, jde.GlAccount, jde.Taxable, mappingProblemList, rowNumber),
                CostCenter = _implementMapping.ForCostCenter(jde.Plant, jde.LnType, jde.CostCenter, jde.Taxable, mappingProblemList, rowNumber),
                DeliveryDate = _implementMapping.ForDeliveryDate(jde.DeliveryDate, mappingProblemList, rowNumber)
            };

            return sap;
        }

        public PurchaseOrderSapDto ForPurchaseOrderChange(PurchaseOrderDto jde, ICollection<ProblemDto> mappingProblemList, int rowNumber)
        {
            var sap = new PurchaseOrderSapDto
            {
                PoNumber = _implementMapping.ForPoNumber(jde.PoNumber, mappingProblemList, rowNumber),
                CompCode = _implementMapping.ForCompCode(jde.CompCode, mappingProblemList, rowNumber),
                DocType = _implementMapping.ForDocType(Constants.PurchaseOrderChange, mappingProblemList, rowNumber),
                CreateDate = _implementMapping.ForCreateDate(jde.CreateDate, mappingProblemList, rowNumber),
                CreatedBy = Constants.DefaultUserName,
                ItemIntvl = Constants.DefaultItemIntvlChange,
                Vendor = _implementMapping.ForVendor(jde.Vendor, mappingProblemList, rowNumber),
                PurchOrg = _implementMapping.ForPurchaseOrg(jde.PurchOrg),
                PurGroup = _implementMapping.ForPurGroup(jde.PurGroup, mappingProblemList, rowNumber),
                Currency = jde.Currency,
                PoItem = _implementMapping.ForPoItem(jde.PoItem, mappingProblemList, rowNumber),
                DeleteInd = jde.DeleteInd,
                ShortText = jde.ShortText,
                Plant = _implementMapping.ForPlant(jde.Plant, mappingProblemList, rowNumber),
                StgeLoc = Constants.Default3Zeros1,
                MatlGroup = _implementMapping.ForMaterialGroup(jde.Plant, jde.LnType, jde.Taxable, mappingProblemList, rowNumber),
                VendMat = jde.VendMat,
                Quantity = jde.Quantity,
                PoUnit = _implementMapping.ForPoUnit(jde.PoUnit, mappingProblemList, rowNumber),
                OrderPrUn = _implementMapping.ForOrderPrUn(jde.OrderPrUn, mappingProblemList, rowNumber),
                NetPrice = _implementMapping.ForNetPrice(jde.NetPrice, mappingProblemList, rowNumber),
                PriceUnit = jde.PriceUnit,
                OverDlvTol = jde.OverDlvTol,
                NoMoreGr = jde.NoMoreGr,
                Acctasscat = jde.Acctasscat,
                PreqName = jde.PreqName,
                SchedLine = Constants.Default3Zeros1,
                SerialNo = Constants.PurchaseOrderChangeSerialNo,
                GlAccount = _implementMapping.ForGlAccount(jde.Plant, jde.LnType, jde.GlAccount, jde.Taxable, mappingProblemList, rowNumber),
                CostCenter = _implementMapping.ForCostCenter(jde.Plant, jde.LnType, jde.CostCenter, jde.Taxable, mappingProblemList, rowNumber),
                DeliveryDate = _implementMapping.ForDeliveryDate(jde.DeliveryDate, mappingProblemList, rowNumber)
            };

            return sap;
        }
    }
}
