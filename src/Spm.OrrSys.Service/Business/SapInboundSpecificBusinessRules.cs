using System;
using System.Collections.Generic;
using System.Linq;
using Spm.OrrSys.Service.Domain;
using Spm.OrrSys.Service.Dto;
using Spm.Shared;

namespace Spm.OrrSys.Service.Business
{
    public class SapInboundSpecificBusinessRules : IProvideSapInboundSpecificBusinessRules
    {
        private readonly IUniqueNumbers _uniqueNumbers;

        public SapInboundSpecificBusinessRules(IUniqueNumbers uniqueNumbers)
        {
            _uniqueNumbers = uniqueNumbers;
        }

        public PlannedOrderDto CreatePlannedOrderDto()
        {
            var jdeRequires15Digits = 15;
            var unique15BitNumberAsStr = _uniqueNumbers.GetUniqueNDigitNumberAsString(jdeRequires15Digits);

            var returnVal = new PlannedOrderDto
            {
                Fteddt = ConvertDate.DateToJdeDate(DateTime.Today.Date),
                Ftedbt = unique15BitNumberAsStr,
                Ftedtn = $"{Constants.OrrSysPrefix1}{unique15BitNumberAsStr}"
            };

            return returnVal;
        }

        public ProductionOrderDto CreateProductionOrderDto()
        {
            var jdeRequires15Digits = 15;
            var unique15BitNumberAsStr = _uniqueNumbers.GetUniqueNDigitNumberAsString(jdeRequires15Digits);

            var returnVal = new ProductionOrderDto
            {
                Syedbt = unique15BitNumberAsStr,
                Syedln = $"{Constants.OrrSysPrefix1}{unique15BitNumberAsStr}"
            };

            return returnVal;
        }

        public List<OrrSysF3460Z1> CreateJdePlannedOrders(
                                                    IList<DemandSapJde> inboundPlannedOrderList,
                                                    IList<JdeItemMasterDetailDto> jdeItemMasterDetailList,
                                                    int startCount,
                                                    PlannedOrderDto plannedOrderDto,
                                                    IList<OrrSysF3460Z1> openPlannedOrders)
        {
            var returnedOpenPlannedOrderList = new List<OrrSysF3460Z1>();
            returnedOpenPlannedOrderList.AddRange(openPlannedOrders);

            var count = startCount;

            foreach (var item in inboundPlannedOrderList)
            {
                var supplyingPlant = item.Ibmcu.PadLeft(12);
                var tempItem = item;
                var ftitmTemp = jdeItemMasterDetailList.FirstOrDefault(x => x.Imitm.Equals(tempItem.Ibitm));

                if (ftitmTemp == null)
                    throw new ArgumentException($"No JDE short item number could be found for SapDemandItemNumber={tempItem.Ibitm}. Cannot proceed.");

                var f3460Z1 = new OrrSysF3460Z1
                {
                    FTEDUS = Constants.Ftedus,
                    FTEDBT = plannedOrderDto.Ftedbt,
                    FTEDTN = plannedOrderDto.Ftedtn,
                    FTEDLN = count,
                    FTTYTN = Constants.Fttytn,
                    FTEDDT = plannedOrderDto.Fteddt,
                    FTDRIN = Constants.Ftdrin,
                    FTEDSP = Constants.Ftedsp,
                    FTTNAC = Constants.Fttnac,
                    FTITM = ftitmTemp.Imitm,
                    FTLITM = ftitmTemp.Imlitm,
                    FTAITM = ftitmTemp.Imaitm,
                    FTMCU = supplyingPlant,
                    FTDRQJ = ConvertDate.DateToJdeDate(item.CyclePlannedBefore),
                    FTUORG = item.Qyy * 10000,
                    FTFQT = item.Qyy * 10000,
                    FTTYPF = item.CyclePlannedBefore >= DateTime.Today.Date ? Constants.ForwardDemand : Constants.OverdueDemand,
                    FTDCTO = Constants.Forcast
                };

                returnedOpenPlannedOrderList.Add(f3460Z1);
                count = count + 1000;
            }

            return returnedOpenPlannedOrderList;
        }

        public List<OrrSysF554801Z> CreateJdeProductionOrders(IEnumerable<DemandSapJdeWo> inboundProductionOrderList, ProductionOrderDto productionOrderDto)
        {
            var productionOrderList = new List<OrrSysF554801Z>();
            var startCount = 1000;

            foreach (var item in inboundProductionOrderList)
            {
                var f3460Z1 = new OrrSysF554801Z
                {
                    SYEDUS = Constants.Syedus,
                    SYEDBT = productionOrderDto.Syedbt,
                    SYEDTN = productionOrderDto.Syedln,
                    SYEDLN = startCount,
                    SYEDCT = Constants.Syedct,
                    SYTYTN = Constants.Sytytn,
                    SYEDDT = ConvertDate.DateToJdeDate(DateTime.Now),
                    SYDRIN = Constants.Sydrin,
                    SYEDDL = startCount,
                    SYEDSP = Constants.Syedsp,
                    SYTNAC = Constants.Sytnac,
                    SYDCTO = Constants.Syedct,
                    SYITM = item.Ibitm,
                    SYLITM = item.Iblitm,
                    SYDRQJ = ConvertDate.DateToJdeDate(item.CyclePlannedBefore),
                    SYUORG = item.Qty * 10000,
                    SYSOQS = item.Qty * 10000,
                    SYUM = item.BaseUom,
                    SYMCU = item.Ibmcu,
                    SYVR01 = item.ProOrder
                };

                productionOrderList.Add(f3460Z1);
                startCount = startCount + 1000;
            }

            return productionOrderList;
        }

        public List<int> GetDestinctJdeShortItemNumber(IEnumerable<DemandSapJde> inboundPlannedOrderList)
        {
            var returnVal = inboundPlannedOrderList.Select(x => x.Ibitm).Distinct().ToList();
            return returnVal;
        }
    }
}