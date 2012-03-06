using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace NHibernate.JetDriver.Tests
{
    public class TestingLoggingSetup
    {
        private static readonly object Lock = new object();
        private static bool _configured;

        public static void Configure()
        {
            lock (Lock)
            {
                if (_configured)
                {
                    return;
                }

                BasicConfigurator.Configure(new TraceAppender()
                {
                    Layout = new PatternLayout(PatternLayout.DetailConversionPattern),
                });

                SetLoggerLevel("NHibernate", Level.Info);
                SetLoggerLevel("NHibernate.SQL", Level.Debug);

                _configured = true;
            }
        }

        public static void SetLoggerLevel(System.Type type, Level level)
        {
            ILog log = LogManager.GetLogger(type);
            SetLoggerLevel(log, level);
        }
        public static void SetLoggerLevel(string loggerName, Level level)
        {
            ILog log = LogManager.GetLogger(loggerName);
            SetLoggerLevel(log, level);
        }
        public static void SetLoggerLevel(ILog logger, Level level)
        {
            Logger hierarchyLogger = (Logger)logger.Logger;
            SetLoggerLevel(hierarchyLogger, level);
        }
        public static void SetLoggerLevel(Logger hierarchyLogger, Level level)
        {
            hierarchyLogger.Level = level;
        }

    }
}