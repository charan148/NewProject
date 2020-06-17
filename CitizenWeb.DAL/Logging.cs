using System;
using System.Collections.Generic;
using System.Text;

namespace CitizenWeb.DAL
{
    /// <summary></summary>
    public static class Logging
    {
        private static readonly log4net.ILog log = log4net.LogManager
           .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>Logs the error message.</summary>
        /// <param name="msg">The MSG.</param>
        public static void LogErrorMessage(string msg)
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            log.Error(msg);
        }

        /// <summary>Logs the information message.</summary>
        /// <param name="msg">The MSG.</param>
        public static void LogInfoMessage(string msg)
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            log.Info(msg);
        }

        /// <summary>Logs the debug message.</summary>
        /// <param name="msg">The MSG.</param>
        public static void LogDebugMessage(string msg)
        {
            var logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
            log4net.Config.XmlConfigurator.Configure(logRepository, new System.IO.FileInfo("log4net.config"));
            log.Debug(msg);
        }
    }
}
