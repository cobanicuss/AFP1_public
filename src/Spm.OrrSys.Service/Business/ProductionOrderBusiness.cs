using NServiceBus.Logging;
using Spm.OrrSys.Service.Repositories;

namespace Spm.OrrSys.Service.Business
{
    public class ProductionOrderBusinessRules : IDoProductionOrderBusiness
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ProductionOrderBusinessRules));

        private readonly IProductionOrderRepository _orrSys;
        private readonly ISapRepository _sap;
        private readonly IProvideSapInboundSpecificBusinessRules _sapInboundBusinessRules;

        public ProductionOrderBusinessRules(
            IProductionOrderRepository orrSys,
            ISapRepository sap,
            IProvideSapInboundSpecificBusinessRules sapInboundBusinessRules)
        {
            _orrSys = orrSys;
            _sap = sap;
            _sapInboundBusinessRules = sapInboundBusinessRules;
        }

        public void CreateProductionOrders()
        {
            var productionOrderDto = _sapInboundBusinessRules.CreateProductionOrderDto();

            var inboundProductionOrderList = _sap.GetInboundProductionOrderList();
            Logger.Info("---------------------------------------------------------");
            Logger.Info("Database=SAP. Table=DemandSapJdeWo");
            Logger.Info($"inboundProductionOrderList.Count={inboundProductionOrderList.Count}");

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Create total Production-Orders");
            var productionOrderList = _sapInboundBusinessRules.CreateJdeProductionOrders(inboundProductionOrderList, productionOrderDto);
            Logger.Info($"productionOrderList.Count={productionOrderList.Count}");

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Deleting old history.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF554801ZHistory}");
            _orrSys.DeleteOldHistory();

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Insert new history.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF554801ZHistory}");
            _orrSys.InsertNewHistory();

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Delete previously inserted Production-Orders.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF554801Z}");
            _orrSys.DeleteAllByTable();

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Bulk insert Production-Orders in place of the deleted ones.");
            Logger.Info($"Database=OrrSys. Table={Service.Constants.TableNameF554801Z}");
            _orrSys.BulkInsertByTypeList(productionOrderList);

            Logger.Info("---------------------------------------------------------");
            Logger.Info("Execute trigger for bulk insert Production-Orders into Oracle via SSIS.");
            Logger.Info($"Database=SAP. Sproc=SsisTransfereOracleProductionOrders");
            _sap.TriggerBulkInsertProductionOrdersIntoOracle();
        }
    }
}