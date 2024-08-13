using System.Collections.Generic;
using System.Linq;
using Spm.File.Watcher.Service.Dto;
using Spm.Service.Messages;
using Spm.Shared;

namespace Spm.File.Watcher.Service.SapJdeMap
{
    public interface IMapPayloads
    {
        GoodsCommand MapGoodsPayload(GoodsDto input);
        PurchaseOrderPayload MapPurchaseOrderPayload(IList<PurchaseOrderDto> input);
    }

    public class MappingPayloads : IMapPayloads
    {
        public GoodsCommand MapGoodsPayload(GoodsDto input)
        {
            var goodsPayloadItem = new GoodsPayloadItem
            {
                //E2Bp2017GmHead//
                E2Bp2017GmHeadPostingDate = input.PstngDate,
                E2Bp2017GmHeadDocDate = input.DocDate,
                E2Bp2017GmHeadRefDocNo = input.RefDocNo,
                E2Bp2017GmHeadHeaderTxt = input.HeaderTxt,
                E2Bp2017GmCode = input.GmCode,

                //E2Bp2017GmItemCreate//
                E2Bp2017GmItemCreatePlant = input.Plant,
                E2Bp2017GmItemCreateStgeLoc = input.StgeLoc,
                E2Bp2017GmItemCreateMoveType = input.MoveType,
                E2Bp2017GmItemCreateEntryQnt = input.EntryQnt,
                E2Bp2017GmItemCreateEntryUom = input.EntryUom,
                E2Bp2017GmItemCreatePoNumber = input.PoNumber,
                E2Bp2017GmItemCreatePoItem = input.PoItem,
                E2Bp2017GmItemCreateMvtInd = input.MvtInd,
                E2Bp2017GmItemCreateNoMoreGr = input.NoMoreGr
            };

            var outputPayload = new GoodsPayload { GoodsPayloadItem = new List<GoodsPayloadItem> { goodsPayloadItem } };

            var output = new GoodsCommand
            {
                Payload = outputPayload
            };

            return output;
        }

        public PurchaseOrderPayload MapPurchaseOrderPayload(IList<PurchaseOrderDto> input)
        {
            var outputPayloadList = (
                    from inItem in input
                    let outItem = new PurchaseOrderPayloadItem
                    {
                        //E1BpmePoHeader//
                        E1BpmePoHeaderPoNumber = inItem.PoNumber,
                        E1BpmePoHeaderCompCode = inItem.CompCode,
                        E1BpmePoHeaderDocType = inItem.DocType,
                        E1BpmePoHeaderCreatDate = inItem.CreatDate,
                        E1BpmePoHeaderCreatedBy = inItem.CreatedBy,
                        E1BpmePoHeaderItemIntvl = inItem.ItemIntvl,
                        E1BpmePoHeaderVendor = inItem.Vendor,
                        E1BpmePoHeaderPurchOrg = inItem.PurchOrg,
                        E1BpmePoHeaderPurGroup = inItem.PurGroup,
                        E1BpmePoHeaderCurrency = inItem.Currency,

                        //E1BpmePoItem//
                        E1BpmePoItemPoItem = inItem.PoItem,
                        E1BpmePoItemDeleteInd = inItem.DeleteInd,
                        E1BpmePoItemShortText = inItem.ShortText,
                        E1BpmePoItemPlant = inItem.Plant,
                        E1BpmePoItemStgeLoc = inItem.StgeLoc,
                        E1BpmePoItemMatlGroup = inItem.MatlGroup,
                        E1BpmePoItemVendMat = inItem.VendMat,
                        E1BpmePoItemQuantity = inItem.Quantity,
                        E1BpmePoItemPoUnit = inItem.PoUnit,
                        E1BpmePoItemOrderPrUn = inItem.OrderprUn,
                        E1BpmePoItemNetPrice = inItem.NetPrice,
                        E1BpmePoItemPriceUnit = inItem.PriceUnit,
                        E1BpmePoItemOverDlvTol = inItem.OverDlvTol,
                        E1BpmePoItemNoMoreGr = inItem.NoMoreGr,
                        E1BpmePoItemAccTassCat = inItem.Acctasscat,
                        E1BpmePoItemPreqName = inItem.PreqName,

                        //E1BpmePoSchedule//
                        E1BpmePoSchedulePoItem = inItem.PoItem,
                        E1BpmePoScheduleSchedLine = inItem.SchedLine,
                        E1BpmePoScheduleQuantity = inItem.Quantity,
                        E1BpmePoScheduleDeliveryDate = inItem.DeliveryDate,

                        //E1BpmePoAccount//
                        E1BpmePoAccountPoItem = inItem.PoItem,
                        E1BpmePoAccountSerialNo = inItem.SerialNo,
                        E1BpmePoAccountGlAccount = inItem.GlAccount,
                        E1BpmePoAccountCostCenter = inItem.Costcenter
                    }
                    select outItem).ToList();

            var output = new PurchaseOrderPayload { PurchaseOrderPayloadItemList = outputPayloadList };

            return output;
        }
    }
}