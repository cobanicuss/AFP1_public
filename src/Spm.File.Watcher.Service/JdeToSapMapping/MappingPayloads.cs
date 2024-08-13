using System.Collections.Generic;
using System.Linq;
using Spm.File.Watcher.Service.Dto;
using Spm.Shared.Payloads;

namespace Spm.File.Watcher.Service.JdeToSapMapping
{
    public class MappingPayloads : IMapPayloads
    {
        public GoodsPayload MapGoodsPayload(GoodsDto input)
        {
            var goodsPayloadItem = new GoodsPayloadItem
            {
                //E2Bp2017GmHead\\
                HeadPostingDate = input.PstngDate,
                HeadDocDate = input.DocDate,
                HeadRefDocNo = input.RefDocNo,
                HeadHeaderTxt = input.HeaderTxt,
                Code = input.GmCode,

                //E2Bp2017GmItemCreate\\
                ItemCreatePlant = input.Plant,
                ItemCreateStgeLoc = input.StgeLoc,
                ItemCreateMoveType = input.MoveType,
                ItemCreateEntryQnt = input.EntryQnt,
                ItemCreateEntryUom = input.EntryUom,
                ItemCreatePoNumber = input.PoNumber,
                ItemCreatePoItem = input.PoItem,
                ItemCreateMvtInd = input.MvtInd,
                ItemCreateNoMoreGr = input.NoMoreGr
            };

            var returnVal = new GoodsPayload
            {
                GoodsPayloadItem = new List<GoodsPayloadItem> { goodsPayloadItem }
            };

            return returnVal;
        }

        public PurchaseOrderPayload MapPurchaseOrderPayload(IList<PurchaseOrderSapDto> input)
        {
            var outputPayloadList = (
                    from inItem in input
                    let outItem = new PurchaseOrderPayloadItem
                    {
                        //E1BpmePoHeader\\
                        E1BpmePoHeaderPoNumber = inItem.PoNumber,
                        E1BpmePoHeaderCompCode = inItem.CompCode,
                        E1BpmePoHeaderDocType = inItem.DocType,
                        E1BpmePoHeaderCreatDate = inItem.CreateDate,
                        E1BpmePoHeaderCreatedBy = inItem.CreatedBy,
                        E1BpmePoHeaderItemIntvl = inItem.ItemIntvl,
                        E1BpmePoHeaderVendor = inItem.Vendor,
                        E1BpmePoHeaderPurchOrg = inItem.PurchOrg,
                        E1BpmePoHeaderPurGroup = inItem.PurGroup,
                        E1BpmePoHeaderCurrency = inItem.Currency,

                        //E1BpmePoItem\\
                        E1BpmePoItemPoItem = inItem.PoItem,
                        E1BpmePoItemDeleteInd = inItem.DeleteInd,
                        E1BpmePoItemShortText = inItem.ShortText,
                        E1BpmePoItemPlant = inItem.Plant,
                        E1BpmePoItemStgeLoc = inItem.StgeLoc,
                        E1BpmePoItemMatlGroup = inItem.MatlGroup,
                        E1BpmePoItemVendMat = inItem.VendMat,
                        E1BpmePoItemQuantity = inItem.Quantity,
                        E1BpmePoItemPoUnit = inItem.PoUnit,
                        E1BpmePoItemOrderPrUn = inItem.OrderPrUn,
                        E1BpmePoItemNetPrice = inItem.NetPrice,
                        E1BpmePoItemPriceUnit = inItem.PriceUnit,
                        E1BpmePoItemOverDlvTol = inItem.OverDlvTol,
                        E1BpmePoItemNoMoreGr = inItem.NoMoreGr,
                        E1BpmePoItemAccTassCat = inItem.Acctasscat,
                        E1BpmePoItemPreqName = inItem.PreqName,

                        //E1BpmePoSchedule\\
                        E1BpmePoSchedulePoItem = inItem.PoItem,
                        E1BpmePoScheduleSchedLine = inItem.SchedLine,
                        E1BpmePoScheduleQuantity = inItem.Quantity,
                        E1BpmePoScheduleDeliveryDate = inItem.DeliveryDate,

                        //E1BpmePoAccount\\
                        E1BpmePoAccountPoItem = inItem.PoItem,
                        E1BpmePoAccountSerialNo = inItem.SerialNo,
                        E1BpmePoAccountGlAccount = inItem.GlAccount,
                        E1BpmePoAccountCostCenter = inItem.CostCenter
                    }
                    select outItem).ToList();

            var output = new PurchaseOrderPayload { PurchaseOrderPayloadItemList = outputPayloadList };

            return output;
        }

        public MaterialMasterPayload MapMaterialMasterPayload(MaterialMasterSapDto input)
        {
            var materialMasterPayloadItem = new MaterialMasterPayloadItem
            {
                E1MaramBismt = input.E1MaramBismt,
                E1MarcmWerks = input.E1MarcmWerks,
                E1MarcmMsgfn = input.E1MarcmMsgfn,
                E1MaktmMaktx = input.E1MaktmMaktx,
                E1MaramMatkl = input.E1MaramMatkl,
                E1MaramPrdha = input.E1MaramPrdha,
                E1MaramGroes = input.E1MaramGroes,
                E1MarcmDzeit = input.E1MarcmDzeit,

                //E1Cucfg[E1Cuins[0]]\\
                E1CucfgPosex0 = input.E1CucfgPosex0,
                E1CucfgConfigId0 = input.E1CucfgConfigId0,
                E1CucfgRootId0 = input.E1CucfgRootId0,
                E1CuinsInstId0 = input.E1CuinsInstId0,
                E1CuinsObjType0 = input.E1CuinsObjType0,
                E1CuinsClassType0 = input.E1CuinsClassType0,
                E1CuinsObjKey0 = input.E1CuinsObjKey0,

                //E1Cucfg[E1Cuins[1]]\\
                E1CucfgPosex1 = input.E1CucfgPosex1,
                E1CucfgConfigId1 = input.E1CucfgConfigId1,
                E1CucfgRootId1 = input.E1CucfgRootId1,
                E1CuinsInstId1 = input.E1CuinsInstId1,
                E1CuinsObjType1 = input.E1CuinsObjType1,
                E1CuinsClassType1 = input.E1CuinsClassType1,
                E1CuinsObjKey1 = input.E1CuinsObjKey1,

                //E1Cuval[0]\\
                E1CuvalInstId0 = input.E1CuvalInstId0,
                E1CuvalCharc0 = input.E1CuvalCharc0,
                E1CuvalValue0 = input.E1CuvalValue0,

                //E1Cuval[1]\\
                E1CuvalInstId1 = input.E1CuvalInstId1,
                E1CuvalCharc1 = input.E1CuvalCharc1,
                E1CuvalValue1 = input.E1CuvalValue1,

                //E1Cuval[2]\\
                E1CuvalInstId2 = input.E1CuvalInstId2,
                E1CuvalCharc2 = input.E1CuvalCharc2,
                E1CuvalValue2 = input.E1CuvalValue2,

                //E1Cuval[3]\\
                E1CuvalInstId3 = input.E1CuvalInstId3,
                E1CuvalCharc3 = input.E1CuvalCharc3,
                E1CuvalValue3 = input.E1CuvalValue3,

                //E1Cuval[4]\\
                E1CuvalInstId4 = input.E1CuvalInstId4,
                E1CuvalCharc4 = input.E1CuvalCharc4,
                E1CuvalValue4 = input.E1CuvalValue4,

                //E1Cuval[5]\\
                E1CuvalInstId5 = input.E1CuvalInstId5,
                E1CuvalCharc5 = input.E1CuvalCharc5,
                E1CuvalValue5 = input.E1CuvalValue5,

                //E1Cuval[6]\\
                E1CuvalInstId6 = input.E1CuvalInstId6,
                E1CuvalCharc6 = input.E1CuvalCharc6,
                E1CuvalValue6 = input.E1CuvalValue6,

                //E1Cuval[7]\\
                E1CuvalInstId7 = input.E1CuvalInstId7,
                E1CuvalCharc7 = input.E1CuvalCharc7,
                E1CuvalValue7 = input.E1CuvalValue7,

                //E1Mvkem[0]\\
                E1MvkemMsgfn0 = input.E1MvkemMsgfn0,
                E1MvkemVkorg0 = input.E1MvkemVkorg0,
                E1MvkemProdh0 = input.E1MvkemProdh0,
                E1MvkemPrat10 = input.E1MvkemPrat10,
                E1MvkemPrat40 = input.E1MvkemPrat40,

                //E1Mvkem[1]\\
                E1MvkemMsgfn1 = input.E1MvkemMsgfn1,
                E1MvkemVkorg1 = input.E1MvkemVkorg1,
                E1MvkemProdh1 = input.E1MvkemProdh1,
                E1MvkemPrat11 = input.E1MvkemPrat11,
                E1MvkemPrat41 = input.E1MvkemPrat41,

                E1MarcmPrctr = input.E1MarcmPrctr,

                //E1Mtxhm[E1Mtxlm[0]]\\
                E1MtxhmMsgfn0 = input.E1MtxhmMsgfn0,
                E1MtxhmTdobject0 = input.E1MtxhmTdobject0,
                E1MtxhmTdid0 = input.E1MtxhmTdid0,
                E1MtxhmTdspras0 = input.E1MtxhmTdspras0,
                E1MtxhmTdtexttype0 = input.E1MtxhmTdtexttype0,
                E1MtxhmTdname0 = input.E1MtxhmTdname0,
                E1MtxlmMsgfn0 = input.E1MtxlmMsgfn0,
                E1MtxlmTdformat0 = input.E1MtxlmTdformat0,
                E1MtxlmTdline0 = input.E1MtxlmTdline0,

                //E1Mtxhm[E1Mtxlm[1]]\\
                E1MtxhmMsgfn1 = input.E1MtxhmMsgfn1,
                E1MtxhmTdobject1 = input.E1MtxhmTdobject1,
                E1MtxhmTdid1 = input.E1MtxhmTdid1,
                E1MtxhmTdspras1 = input.E1MtxhmTdspras1,
                E1MtxhmTdtexttype1 = input.E1MtxhmTdtexttype1,
                E1MtxhmTdname1 = input.E1MtxhmTdname1,
                E1MtxlmMsgfn1 = input.E1MtxlmMsgfn1,
                E1MtxlmTdformat1 = input.E1MtxlmTdformat1,
                E1MtxlmTdline1 = input.E1MtxlmTdline1,

                //E1Mtxhm[E1Mtxlm[2]]\\
                E1MtxhmMsgfn2 = input.E1MtxhmMsgfn2,
                E1MtxhmTdobject2 = input.E1MtxhmTdobject2,
                E1MtxhmTdid2 = input.E1MtxhmTdid2,
                E1MtxhmTdspras2 = input.E1MtxhmTdspras2,
                E1MtxhmTdtexttype2 = input.E1MtxhmTdtexttype2,
                E1MtxhmTdname2 = input.E1MtxhmTdname2,
                E1MtxlmMsgfn2 = input.E1MtxlmMsgfn2,
                E1MtxlmTdformat2 = input.E1MtxlmTdformat2,
                E1MtxlmTdline2 = input.E1MtxlmTdline2,

                E1MarcmEkgrp = input.E1MarcmEkgrp,
                E1MarcmMaabc = input.E1MarcmMaabc,
                E1MarcmDispr = input.E1MarcmDispr,
                E1MarcmBstrf = input.E1MarcmBstrf,
                E1MlgnmMsgfn = input.E1MlgnmMsgfn,
                E1MlgnmLgnum = input.E1MlgnmLgnum,
                E1MlgnmLgbkz = input.E1MlgnmLgbkz,
                E1MlgnmLhmg1 = input.E1MlgnmLhmg1,
                E1MlgtmKober = input.E1MlgtmKober,
                E1MlgtmNsmng = input.E1MlgtmNsmng,
                E1MbewmMsgfn = input.E1MbewmMsgfn,
                E1MbewmBwkey = input.E1MbewmBwkey,
                E1MbewmVerpr = input.E1MbewmVerpr,
                Z1MaramZzsan = input.Z1MaramZzsan,
                Z1MaramZzquln = input.Z1MaramZzquln,
                Z1MaramZzpcsn = input.Z1MaramZzpcsn,
                Z1MaramZzdm1N = input.Z1MaramZzdm1N,
                Z1MaramZzdm2N = input.Z1MaramZzdm2N,
                Z1MaramZzdm5N = input.Z1MaramZzdm5N,
                Z1MaramZzcol1 = input.Z1MaramZzcol1,
                Z1MaramZzprfn = input.Z1MaramZzprfn,
                Z1MaramZzstol = input.Z1MaramZzstol,
                Z1MaramZzroll = input.Z1MaramZzroll,
                Z1MaramZzmtwc = input.Z1MaramZzmtwc,
                Z1MaramZzpkdm = input.Z1MaramZzpkdm,

                //E1Marmm[0]\\
                E1MarmmMsgfn0 = input.E1MarmmMsgfn0,
                E1MarmmMeinh0 = input.E1MarmmMeinh0,
                E1MarmmUmrez0 = input.E1MarmmUmrez0,
                E1MarmmUmren0 = input.E1MarmmUmren0,

                //E1Marmm[1]\\
                E1MarmmMsgfn1 = input.E1MarmmMsgfn1,
                E1MarmmMeinh1 = input.E1MarmmMeinh1,
                E1MarmmUmrez1 = input.E1MarmmUmrez1,
                E1MarmmUmren1 = input.E1MarmmUmren1,

                //E1Marmm[2]\\
                E1MarmmMsgfn2 = input.E1MarmmMsgfn2,
                E1MarmmMeinh2 = input.E1MarmmMeinh2,
                E1MarmmUmrez2 = input.E1MarmmUmrez2,
                E1MarmmUmren2 = input.E1MarmmUmren2,

                //E1Marmm[3]\\
                E1MarmmMsgfn3 = input.E1MarmmMsgfn3,
                E1MarmmMeinh3 = input.E1MarmmMeinh3,
                E1MarmmUmrez3 = input.E1MarmmUmrez3,
                E1MarmmUmren3 = input.E1MarmmUmren3,

                //E1Marmm[4]\\
                E1MarmmMsgfn4 = input.E1MarmmMsgfn4,
                E1MarmmMeinh4 = input.E1MarmmMeinh4,
                E1MarmmUmren4 = input.E1MarmmUmren4,
                E1MarmmUmrez4 = input.E1MarmmUmrez4,
                E1MarmmNumtp4 = input.E1MarmmNumtp4,
                E1MarmmVolume4 = input.E1MarmmVolume4,
                E1MarmmVoleh4 = input.E1MarmmVoleh4,
                E1MarmmBrgew4 = input.E1MarmmBrgew4,
                E1MarmmGewei = input.E1MarmmGewei

            };

            var returnVal = new MaterialMasterPayload
            {
                MaterialMasterPayloadItem = new List<MaterialMasterPayloadItem> { materialMasterPayloadItem }
            };

            return returnVal;
        }

        public GeneralLedgerPayload MapGeneralLedgerPayload(IList<GeneralLedgerSapDto> input)
        {
            var outputPayloadList = (
               from inItem in input
               let outItem = new GeneralLedgerPayloadItem
               {
                   UserName = inItem.UserName,
                   HeaderTxt = inItem.HeaderTxt,
                   CompanyCode = inItem.CompCode,
                   DocDate = inItem.DocDate,
                   PstngDate = inItem.PstingDate,
                   DocType = inItem.DocType,
                   RefDocNo = inItem.RefDocNo,
                   AcItemNoAcc = inItem.ItemNoAcc.ToString(),
                   ItemText = inItem.ItemText,
                   AllocNmbr = inItem.AllocNmbr,
                   CostCentre = inItem.CostCentre,
                   Account = inItem.GlAccount,
                   ProfitCentre = inItem.GlProfitCenter,
                   Currency = inItem.Currency,
                   Doccur = inItem.AmtDoccur
               }
               select outItem).ToList();

            var returnVal = new GeneralLedgerPayload { GeneralLedgerPayloadItem = outputPayloadList };

            return returnVal;
        }
    }
}