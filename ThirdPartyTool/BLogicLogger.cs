using log4net;
using System;

namespace ThirdPartyTool
{
    public class BLogicLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger("BLogicLogger");
        static BLogicLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Error(string message, Exception ex)
        {
            Logger.Error(message);
            Logger.Error(ex.Message);
            Logger.Error(ex.StackTrace);
            if (ex.InnerException != null)
            {
                Logger.Error(ex.InnerException.Message);
                Logger.Error(ex.InnerException.StackTrace);
            }
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }
    }
}
