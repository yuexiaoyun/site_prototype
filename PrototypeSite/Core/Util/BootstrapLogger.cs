using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace Core.Util
{
    public class BootstrapLogger
    {
        private static ILog logger = LogManager.GetLogger("Global");

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void DebugFormat(string foramt, params object[] args)
        {
            logger.DebugFormat(foramt, args);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void InfoFormat(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Warn(string message, Exception exception)
        {
            logger.Warn(message, exception);
        }

        public static void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }
    }
}
