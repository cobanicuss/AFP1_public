using NServiceBus;

namespace Spm.Shared
{
    public interface IMarkAsMapper { }
    public interface IMarkAsDomain { }
    public interface IMarkAsTransition { }
    public interface IMarkAsMessageCreator { }
    public interface IMarkAsHistoryChecker { }
    public interface IMarkAsValidator { }
    public interface IMarkAsBusinessRule { }
    public interface IMarkAsDto { }

    public class DevelopmentProfile : IProfile{}
    public class TestProfile : IProfile { }
    public class ProductionProfile : IProfile { }
}