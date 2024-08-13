using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Castle.Windsor;
using Castle.Windsor.Installer;
using log4net.Config;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using Spm.Shared;

namespace Spm.Service.ReceiveFromSap
{
    public class Global : HttpApplication
    {
        public static IBus Bus { get; private set; }
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(Global));
        public static IWindsorContainer IocContainer;

        protected void Application_Start(object sender, EventArgs e)
        {
            ConfigureLogging();

            IocContainer = new WindsorContainer();
            IocContainer.Install(FromAssembly.This());

            var busConfiguration = new BusConfiguration();
            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.DisableFeature<TimeoutManager>();
            busConfiguration.DisableFeature<SecondLevelRetries>();
            busConfiguration.EndpointName(Constants.EndPoitName);
            busConfiguration.UseTransport<MsmqTransport>();
            busConfiguration.UsePersistence<InMemoryPersistence>();

            Bus = NServiceBus.Bus.Create(busConfiguration).Start();
        }

        protected void Session_Start(object sender, EventArgs e) { }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var soapActionValue = HttpContext.Current.Request.Headers["SoapAction"];
            var inputStream = new byte[HttpContext.Current.Request.ContentLength];

            HttpContext.Current.Request.InputStream.Read(inputStream, 0, inputStream.Length);
            HttpContext.Current.Request.InputStream.Position = 0;

            // ReSharper disable once AccessToStaticMemberViaDerivedType
            var soapMessage = ASCIIEncoding.ASCII.GetString(inputStream); //LEAVE ASCIIEncoding as is!!!//

            if (string.IsNullOrEmpty(soapMessage) || string.IsNullOrEmpty(soapActionValue)) return;

            Logger.Info($"SoapAction={soapActionValue}");
            Logger.Info($"SOAP in Request:{soapMessage}");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) { }

        protected void Application_Error(object sender, EventArgs e) { }

        protected void Session_End(object sender, EventArgs e) { }

        protected void Application_End(object sender, EventArgs e)
        {
            IocContainer.Dispose();
        }

        protected void ConfigureLogging()
        {
            var typeList1 = new List<string> { typeof(LogIncommingMessages).ToString() };

            ILoggingConfig log4NetSoapMessage = new LoggingConfig() //DONT use injection here: Config!//
            {
                LogFileName = Constants.InboundMessageLogPath,
                FileLayout = Constants.FileLayout,
                TypeNameForFile = typeList1
            };

            var typeList2 = new List<string> { typeof(SpmWebService).ToString(), typeof(Global).ToString() };

            ILoggingConfig log4NetService = new LoggingConfig() //DONT use injection here: Config!//
            {
                LogFileName = Constants.MethodLogPath,
                TypeNameForFile = typeList2
            };

            var rollingFileAppender1 = log4NetSoapMessage.GetRollingFileAppender();
            var rollingFileAppender2 = log4NetService.GetRollingFileAppender();

            rollingFileAppender1.ActivateOptions();
            rollingFileAppender2.ActivateOptions();

            BasicConfigurator.Configure(rollingFileAppender1, rollingFileAppender2);

            NServiceBus.Logging.LogManager.Use<Log4NetFactory>();
        }
    }
}