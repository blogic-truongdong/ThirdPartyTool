using BLogicSystems.XPos.Utilites.BLogicException;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ThirdPartyTool
{
    public class BLogicLogger
    {
        //#region private properties

        //private LogEntry m_LogEntry = null;
        //private string m_Message = null;
        //private string m_Title = null;
        //private static readonly ILog Logger = LogManager.GetLogger("BlogicLogger");

        private static readonly ILog Logger = LogManager.GetLogger("BlogicLogger");
        private static string SepStr = "==========";
        static BLogicLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Error(string message, string errCode)
        {
            Logger.Error(message + SepStr + errCode);
        }

        public static void Error(string message, Exception ex)
        {
            Logger.Error(message);
            Logger.Error(ex?.Message);
            Logger.Error(ex?.StackTrace);
            if (ex?.InnerException != null)
            {
                Logger.Error(ex.InnerException?.Message);
                Logger.Error(ex.InnerException?.StackTrace);
            }
        }

        public static void Error(string message, string errCode, Exception ex)
        {
            Logger.Error(message + SepStr + errCode);
            Logger.Error(ex?.Message);
            Logger.Error(ex?.StackTrace);
            if (ex.InnerException != null)
            {
                Logger.Error(ex.InnerException?.InnerException);
                Logger.Error(ex.InnerException?.InnerException);
            }
        }

        /// <summary>
        /// write to log file everything including inner exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Debug(string message, Exception ex)
        {
            Logger.Debug(message);
            Logger.Debug(ex.Message);
            Logger.Debug(ex.StackTrace);
            if (ex.InnerException != null)
            {
                Logger.Debug(ex.InnerException.Message);
                Logger.Debug(ex.InnerException.StackTrace);
            }
        }

        public static void Debug(string message, string errCode, Exception ex)
        {
            Logger.Debug(message + SepStr + errCode);
            Logger.Debug(ex.Message);
            Logger.Debug(ex.StackTrace);

            if (ex.InnerException != null)
            {

                Logger.Debug(ex.InnerException.Message);
                Logger.Debug(ex.InnerException.StackTrace);
            }

        }

        public static void Debug(string message, string errCode, BLogicApplicationException ex)
        {
            Logger.Debug(message + SepStr + errCode);
            Logger.Debug(ex.Message);
            Logger.Debug(ex.StackTrace);

            if (ex.InnerException != null)
            {
                Logger.Debug(ex.InnerException.Message);
                Logger.Debug(ex.InnerException.StackTrace);
            }

        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }


        /// <summary>
        /// write to log file message and errorCode
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errCode"></param>
        public static void Debug(string message, string errCode)
        {
            Logger.Debug(errCode + SepStr + message);
        }

        public static bool IsDebug()
        {
            return Logger.IsDebugEnabled;
        }
        public static void Info(string message)
        {
            Logger.Info(message);
        }
        public static void Fatal(string message)
        {
            Logger.Fatal(message);
        }
        //#endregion

        #region Constructor

        // //Default constructor with TraceTypes
        // private BLogicLogger(TraceTypes traceType)
        // {
        //     m_LogEntry = new LogEntry();
        //     m_LogEntry.Severity = (TraceEventType)(int)traceType;
        // }

        // //Constructor new BaseLogEntry with TraceTypes and Message
        // private  BLogicLogger(TraceTypes traceType, string message):this(traceType)
        // {
        //     m_LogEntry.Message = message;

        // }

        // //Constructor new BaseLogEntry with TraceTypes and Message and Title
        // private BLogicLogger(TraceTypes traceType, string message, string title) : this(traceType, message)
        // {
        //     m_LogEntry.Title = title;
        // }

        // //Construct new BaseLogEntry with TraceTypes and Message, Title and a IDictionary 
        // public BLogicLogger(TraceTypes traceType, string message, string title, IDictionary<string, object>properties): this(traceType, message, title)
        // {
        //     m_LogEntry.ExtendedProperties = properties;
        // }
        #endregion

        #region method implements here

        // //Write log with TraceTypes and Message
        // public static void Write(TraceTypes traceTypes, string message)
        // {
        //     BLogicLogger logger = new BLogicLogger(traceTypes, message);

        //     BLogicLogger.Write(logger);
        // }

        // //Write log with TraceTypes and Message and Title
        // public static void Write(TraceTypes traceTypes, string message, string title)
        // {
        //     BLogicLogger logger = new BLogicLogger(traceTypes, message, title);
        //     BLogicLogger.Write(logger);
        // }

        // //Write log with TraceTypes and Message and Title and extend properties
        // public  static void Write(TraceTypes traceTypes, string message, string title, IDictionary<string, object> properties)
        // {
        //     BLogicLogger logger = new BLogicLogger(traceTypes, message, title, properties);
        //     BLogicLogger.Write(logger);
        // }

        // //Write log with LogEntry object
        // private static void Write(BLogicLogger logdata)
        // {

        //     if (logdata == null)
        //     {
        //         throw new Exception("");
        //     }

        //     LoggingConfig.CreateLogWriterObject().Write(logdata.m_LogEntry);
        // }

        #endregion

        public static void Exception(string functionName, Exception ex, List<object> listData = null)
        {
            try
            {
                Logger.Error("***** Start Log Exception *****");
                Logger.Error($"FunctionName: {functionName}");

                if (ex != null)
                {
                    Logger.Error(ex?.Message);
                    Logger.Error(ex?.StackTrace);
                    if (ex?.InnerException != null)
                    {
                        Logger.Error(ex.InnerException?.Message);
                        Logger.Error(ex.InnerException?.StackTrace);
                    }
                }

                if (listData != null)
                {
                    JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    for (int i = 0; i < listData.Count; i++)
                    {
                        if (listData[i] != null)
                        {
                            try
                            {
                                Logger.Error($"Json Data {i + 1}: {JsonConvert.SerializeObject(listData[i], Formatting.None, jsonSerializerSettings)}");
                            }
                            catch
                            {
                                Logger.Error($"Object data {i + 1} can't be parse to string.");
                            }
                        }
                    }
                }

                Logger.Error("***** END *****");
            }
            catch (Exception blgEx)
            {
                Logger.Error(blgEx?.Message);
                Logger.Error(blgEx?.StackTrace);
            }
        }
    }
}
