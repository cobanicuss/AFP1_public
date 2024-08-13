using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Appender;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;

namespace Spm.Shared
{
    public interface ILoggingConfig
    {
        LogLevelType LoggingLevel { get; set; }

        string LogFileName { get; set; }
        long MaxFileSizeBytes { get; set; }
        int MaxFileBakupSize { get; set; }
        string ConsoleMessageLayout { get; set; }
        string ConsoleLayout { get; set; }
        string FileLayout { get; set; }

        IEnumerable<string> TypeNameForFile { get; set; }
        IEnumerable<string> TypeNameForConsole { get; set; }

        RollingFileAppender GetRollingFileAppender(LogLevelType? customLogLevel = null);
        ColoredConsoleAppender GetColoredConsoleAppender();
        ColoredConsoleAppender GetHandlerAppender();
    }

    public class LoggingConfig : ILoggingConfig
    {
        public LogLevelType LoggingLevel { get; set; }

        private const string DefaultFileName = "SpmLogFile.log";
        private string _fileName;

        private const long DefaultMaximumFileSize = 5242880;
        private long _maximumFileSize;

        private const int DefaultFileBackupSize = 5;
        private int _fileBackupSize;

        private const string DefaultConsoleMessageLayout = "%date{ISO8601}:%m%n";
        private string _consoleMessageLayout;

        private const string DefaultConsoleLayout = "%date{ISO8601} %-5p %m%n";
        private string _consoleLayout;

        private const string DefaultFileLayout = "%date{ISO8601} [%t] %-5p [%c] %m%n";
        private string _fileLayout;

        public IEnumerable<string> TypeNameForConsole { get; set; }
        public IEnumerable<string> TypeNameForFile { get; set; }

        public string LogFileName
        {
            get { return string.IsNullOrEmpty(_fileName) ? DefaultFileName : _fileName; }
            set { _fileName = value; }
        }

        /// <summary>
        /// File size in bytes ([size in MB] x 1024 x 1024) Bytes.
        /// Default is (5 x 1024 x 1024) = 5242880 Bytes.
        /// </summary>
        public long MaxFileSizeBytes
        {
            get { return _maximumFileSize == 0 ? DefaultMaximumFileSize : _maximumFileSize; }
            set { _maximumFileSize = value; }
        }

        public int MaxFileBakupSize
        {
            get { return _fileBackupSize == 0 ? DefaultFileBackupSize : _fileBackupSize; }
            set { _fileBackupSize = value; }
        }

        public string ConsoleMessageLayout
        {
            get { return string.IsNullOrEmpty(_consoleMessageLayout) ? DefaultConsoleMessageLayout : _consoleMessageLayout; }
            set { _consoleMessageLayout = value; }
        }

        public string ConsoleLayout
        {
            get { return string.IsNullOrEmpty(_consoleLayout) ? DefaultConsoleLayout : _consoleLayout; }
            set { _consoleLayout = value; }
        }

        public string FileLayout
        {
            get { return string.IsNullOrEmpty(_fileLayout) ? DefaultFileLayout : _fileLayout; }
            set { _fileLayout = value; }
        }

        protected PatternLayout GetPatternLayout(string layoutPattern)
        {
            var layout = new PatternLayout
            {
                ConversionPattern = layoutPattern
            };
            layout.ActivateOptions();

            return layout;
        }

        protected LoggerMatchFilter GetDenyFilterForHandler(string typeName)
        {
            return FilterForHandler(typeName, false);
        }

        protected LoggerMatchFilter GetAcceptFilterForHandler(string typeName)
        {
            return FilterForHandler(typeName, true);
        }

        private static LoggerMatchFilter FilterForHandler(string typeName, bool acceptOnMatch)
        {
            var loggerMatchFilter = new LoggerMatchFilter
            {
                LoggerToMatch = typeName,
                AcceptOnMatch = acceptOnMatch
            };

            return loggerMatchFilter;
        }

        public RollingFileAppender GetRollingFileAppender(LogLevelType? customLogLevel = null)
        {
            var rollingFileAppender = new RollingFileAppender
            {
                File = LogFileName,
                AppendToFile = true,
                Threshold = GetLoggingLevel(customLogLevel ?? LoggingLevel),
                Layout = GetPatternLayout(FileLayout),
                MaxSizeRollBackups = MaxFileBakupSize,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxFileSize = MaxFileSizeBytes
            };

            if (TypeNameForFile == null || !TypeNameForFile.Any()) return rollingFileAppender;

            foreach (var type in TypeNameForFile) { rollingFileAppender.AddFilter(GetAcceptFilterForHandler(type)); }

            rollingFileAppender.AddFilter(new DenyAllFilter());

            return rollingFileAppender;
        }

        public ColoredConsoleAppender GetColoredConsoleAppender()
        {
            var coloredConsoleAppender = new ColoredConsoleAppender
            {
                Threshold = GetLoggingLevel(LoggingLevel),
                Layout = GetPatternLayout(ConsoleLayout)
            };

            coloredConsoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Debug,
                ForeColor = ColoredConsoleAppender.Colors.Green
            });
            coloredConsoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.Green
            });
            coloredConsoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Warn,
                ForeColor = ColoredConsoleAppender.Colors.Yellow | ColoredConsoleAppender.Colors.HighIntensity
            });
            coloredConsoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Error,
                ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity
            });
            coloredConsoleAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Fatal,
                ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity
            });

            coloredConsoleAppender.ClearFilters();

            foreach (var type in TypeNameForConsole) { coloredConsoleAppender.AddFilter(GetDenyFilterForHandler(type)); }

            return coloredConsoleAppender;
        }

        public ColoredConsoleAppender GetHandlerAppender()
        {
            var handlerAppender = new ColoredConsoleAppender
            {
                Threshold = GetLoggingLevel(LoggingLevel),
                Layout = GetPatternLayout(ConsoleMessageLayout),
            };

            handlerAppender.AddMapping(new ColoredConsoleAppender.LevelColors
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.White
            });

            foreach (var type in TypeNameForConsole) { handlerAppender.AddFilter(GetAcceptFilterForHandler(type)); }

            handlerAppender.AddFilter(new DenyAllFilter());

            return handlerAppender;
        }

        private Level GetLoggingLevel(LogLevelType? level)
        {
            if (level == null) throw new ArgumentOutOfRangeException(nameof(level));

            switch (level)
            {
                case LogLevelType.Debug:
                    return Level.Debug;
                case LogLevelType.Info:
                    return Level.Info;
                case LogLevelType.Warn:
                    return Level.Warn;
            }

            throw new ArgumentOutOfRangeException(nameof(level));
        }
    }
}