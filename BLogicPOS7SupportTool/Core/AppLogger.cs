using log4net;
using System;

namespace BLogicPOS7SupportTool.Core
{
    static class AppLogger
    {
        private static readonly ILog _log = LogManager.GetLogger("App");

        public static void Info(string message) => _log.Info(message);

        public static void Warn(string message) => _log.Warn(message);

        public static void Error(string message, Exception ex = null)
        {
            if (ex != null) _log.Error(message, ex);
            else _log.Error(message);
        }

        public static void Debug(string message) => _log.Debug(message);
    }
}
