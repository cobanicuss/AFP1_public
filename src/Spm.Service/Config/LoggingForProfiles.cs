using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net.Config;
using NServiceBus;
using NServiceBus.Log4Net;
using NServiceBus.Logging;
using Spm.Shared;

namespace Spm.Service.Config
{
    public class LoggingForDevelopment : NServiceBus.Hosting.Profiles.IConfigureLoggingForProfile<DevelopmentProfile>
    {
        public void Configure(IConfigureThisEndpoint specifier)
        {
            ILoggingConfig log4NetConfig = new LoggingConfig
            {
                LogFileName = Shared.Constants.SpmServiceLogFilePath,
                TypeNameForConsole = new EndpointConfigQuery().GetHandlerList(),
                LoggingLevel = LogLevelType.Info
            };

            var coloredConsoleAppender = log4NetConfig.GetColoredConsoleAppender();
            var handlerAppender = log4NetConfig.GetHandlerAppender();
            var rollingFileAppender = log4NetConfig.GetRollingFileAppender();

            coloredConsoleAppender.ActivateOptions();
            rollingFileAppender.ActivateOptions();
            handlerAppender.ActivateOptions();

            BasicConfigurator.Configure(coloredConsoleAppender, rollingFileAppender, handlerAppender);

            LogManager.Use<Log4NetFactory>();
        }
    }

    public class LoggingForTest : NServiceBus.Hosting.Profiles.IConfigureLoggingForProfile<TestProfile>
    {
        public void Configure(IConfigureThisEndpoint specifier)
        {
            ILoggingConfig log4NetConfig = new LoggingConfig
            {
                LogFileName = Shared.Constants.SpmServiceLogFilePath,
                TypeNameForConsole = new EndpointConfigQuery().GetHandlerList(),
                LoggingLevel = LogLevelType.Info,
                MaxFileBakupSize = 10
            };

            var coloredConsoleAppender = log4NetConfig.GetColoredConsoleAppender();
            var handlerAppender = log4NetConfig.GetHandlerAppender();
            var rollingFileAppender = log4NetConfig.GetRollingFileAppender(LogLevelType.Debug);

            coloredConsoleAppender.ActivateOptions();
            rollingFileAppender.ActivateOptions();
            handlerAppender.ActivateOptions();

            BasicConfigurator.Configure(coloredConsoleAppender, rollingFileAppender, handlerAppender);

            LogManager.Use<Log4NetFactory>();
        }
    }

    public class LoggingForProduction : NServiceBus.Hosting.Profiles.IConfigureLoggingForProfile<ProductionProfile>
    {
        public void Configure(IConfigureThisEndpoint specifier)
        {
            ILoggingConfig log4NetConfig = new LoggingConfig
            {
                LogFileName = Shared.Constants.SpmServiceLogFilePath,
                TypeNameForConsole = new EndpointConfigQuery().GetHandlerList(),
                LoggingLevel = LogLevelType.Info,
                MaxFileBakupSize = 20
            };

            var coloredConsoleAppender = log4NetConfig.GetColoredConsoleAppender();
            var handlerAppender = log4NetConfig.GetHandlerAppender();
            var rollingFileAppender = log4NetConfig.GetRollingFileAppender(LogLevelType.Debug);

            coloredConsoleAppender.ActivateOptions();
            rollingFileAppender.ActivateOptions();
            handlerAppender.ActivateOptions();

            BasicConfigurator.Configure(coloredConsoleAppender, rollingFileAppender, handlerAppender);

            LogManager.Use<Log4NetFactory>();
        }
    }

    public class EndpointConfigQuery
    {
        public IEnumerable<string> GetHandlerList()
        {
            var handlerList = from type in Assembly.GetAssembly(GetType()).GetTypes()
                              where type.ToString().ToUpper().Contains(Constants.SpmServiceSagas)
                              select type.ToString();
            return handlerList;
        }
    }
}