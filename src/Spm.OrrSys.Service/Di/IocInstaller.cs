using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Spm.OrrSys.Service.Business;
using Spm.OrrSys.Service.Map;
using Spm.OrrSys.Service.Repositories;
using Spm.OrrSys.Service.Scheduler;
using Spm.OrrSys.Service.Soap.DataInterfacingService;
using Spm.OrrSys.Service.Soap.SqlReportService;
using Spm.OrrSys.Service.TestCertificates;
using Spm.Shared;

namespace Spm.OrrSys.Service.Di
{
    public class IocInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly().BasedOn<IMarkAsDomain>().WithService.FromInterface(),
                Classes.FromAssemblyContaining<DoBulkInsert>().BasedOn<IDoBulkInsert>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IOrrSysProcessVariableRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IScheduleProductionOrderStutus>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ICommunicateWithOrrSys>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ICommunicateWithSqlReportingServices>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapTestCertificateMessage>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IFormatTestCertificateData>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IOrrSysBaseRepository<IMarkAsDomain>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IFormatData<IMarkAsDomain>>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IFormatPlannedOrderData>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDeleteTestCertificates>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IWriteTestCertificates>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IPlannedOrderRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IProductAchievementRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IProductionOrderRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IOrrSysMappingRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IJdeImportRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ITestCertRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<ISapRepository>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDoPlannedOrderBusiness>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDoProductionOrderBusiness>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IUniqueNumbers>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IProvideSapInboundSpecificBusinessRules>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDoTestCertificateBusiness>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IDoProductAchievementBusiness>().WithService.FromInterface(),
                Classes.FromThisAssembly().BasedOn<IMapSoap>().WithService.FromInterface());
        }
    }
}