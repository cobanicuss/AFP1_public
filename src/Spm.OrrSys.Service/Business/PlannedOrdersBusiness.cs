using System;
using System.Linq;
using NServiceBus.Logging;
using Spm.OrrSys.Service.Repositories;

namespace Spm.OrrSys.Service.Business
{
    public class PlannedOrderBusinessRules : IDoPlannedOrderBusiness
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PlannedOrderBusinessRules));

        private readonly IPlannedOrderRepository _plannedOrder;
        private readonly IJdeImportRepository _jdeImport;
        private readonly ISapRepository _sap;
        private readonly IProvideSapInboundSpecificBusinessRules _sapInboundBusinessRules;

        public PlannedOrderBusinessRules(IPlannedOrderRepository plannedOrder,
                IJdeImportRepository jdeImport,
                ISapRepository sap,
                IProvideSapInboundSpecificBusinessRules sapInboundBusinessRules)
        {
            _plannedOrder = plannedOrder;
            _jdeImport = jdeImport;
            _sap = sap;
            _sapInboundBusinessRules = sapInboundBusinessRules;
        }

        public void CreatePlannedOrders()
        {
            var plannedOrderDto = _sapInboundBusinessRules.CreatePlannedOrderDto();

            var openPlannedOrderList = _jdeImport.GetOpenPlannedOrderList(plannedOrderDto.Ftedbt, plannedOrderDto.Ftedtn, plannedOrderDto.Fteddt);
            Logger.Info("---------------------------------------------------------");
            Logger.Info("Database=JDE_Import. Join on F4801,F4102,F4111");
            Logger.Info($"openPlannedOrderList.Count={openPlannedOrderList.Count}");

            var inboundPlannedOrderList = _sap.GetInboundPlannedOrderList();
            Logger.Info("---------------------------------------------------------");
            Logger.Info("Database=SAP. Table=DemandSapJde");
            Logger.Info($"inboundPlannedOrderList.Count={inboundPlannedOrderList.Count}");

            var destinctJdeShortItemList = _sapInboundBusinessRules.GetDestinctJdeShortItemNumber(inboundPlannedOrderList);
            Logger.Info("---------------------------------------------------------");
            Logger.Info("Get distinct from inboundPlannedOrderList.");
            Logger.Info($"distinctJdeShortItemList.Count={destinctJdeShortItemList.Count}");

            var jdeShortItemList = _jdeImport.GetJdeItemDetails(destinctJdeShortItemList);

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Database=JDE_Import. Table=F4101");
            if (jdeShortItemList == null || !jdeShortItemList.Any()) throw new Exception("JDE short item numbers are null. Cannot proceed!");
            Logger.Info($"jdeShortItemList.Count={jdeShortItemList.Count}");

            var startCount = (openPlannedOrderList.Count + 1) * 1000;

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Create total PlannedOrders.");
            var totalPlannedOrders = _sapInboundBusinessRules.CreateJdePlannedOrders(inboundPlannedOrderList, jdeShortItemList, startCount, plannedOrderDto, openPlannedOrderList);
            Logger.Info($"totalPlannedOrders.Count={totalPlannedOrders.Count}");

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Create history of todays Planned-Orders.");
            Logger.Info($"{Service.Constants.PlannedOrderHistoryKeptDays}-Days back.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF3460Z1History},{Service.Constants.TableNameF3460Z1}");
            _plannedOrder.CreateHistoryTraking();

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Delete previously inserted Planned-Orders.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF3460Z1}");
            _plannedOrder.DeleteAllByTable();

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Bulk insert Planned-Orders in place of deleted ones.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF3460Z1}");
            _plannedOrder.BulkInsertByTypeList(totalPlannedOrders);

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Execute trigger: bulk insert Planned-Orders into Oracle.");
            Logger.Info("Database=SAP. Sproc=SsisTransfereOraclePlannedOrders");
            _sap.TriggerBulkInsertPlannedOrdersIntoOracle();
        }
    }
}
