using NServiceBus;
using NServiceBus.Hosting.Profiles;
using Spm.Service.ForSoap.Mapper;
using Spm.Shared;

namespace Spm.Service.ForSoap.Config
{
    public class DevelopmentProfileConfig : IHandleProfile<DevelopmentProfile>
    {
        void IHandleProfile.ProfileActivated(BusConfiguration config)
        {
            ProfileConfigVariables.SndPrn = DefaultSapVariables.OrrSysDevAndTest;
            ProfileConfigVariables.ComsUserName = Shared.Constants.SapSoapDevAndTestUserName;
            ProfileConfigVariables.ComsPasword = Shared.Constants.SapSoapDevAndTestPassword;
        }

        void IHandleProfile.ProfileActivated(Configure config) { }
    }

    public class TestProfileConfig : IHandleProfile<TestProfile>
    {
        void IHandleProfile.ProfileActivated(BusConfiguration config)
        {
            ProfileConfigVariables.SndPrn = DefaultSapVariables.OrrSysDevAndTest;
            ProfileConfigVariables.ComsUserName = Shared.Constants.SapSoapDevAndTestUserName;
            ProfileConfigVariables.ComsPasword = Shared.Constants.SapSoapDevAndTestPassword;
        }

        void IHandleProfile.ProfileActivated(Configure config) { }
    }

    public class ProductionProfileConfig : IHandleProfile<ProductionProfile>
    {
        void IHandleProfile.ProfileActivated(BusConfiguration config)
        {
            ProfileConfigVariables.SndPrn = DefaultSapVariables.OrrSysProd;
            ProfileConfigVariables.ComsUserName = Shared.Constants.SapSoapProdUserName;
            ProfileConfigVariables.ComsPasword = Shared.Constants.SapSoapProdPassword;
        }

        void IHandleProfile.ProfileActivated(Configure config) { }
    }
}