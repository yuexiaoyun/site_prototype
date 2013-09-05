using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using QuaintHouse.Scheduler.Action.Log;
using log4net;
using ActionLogger = QuaintHouse.Scheduler.Action.Log.ILog;
using ILog = log4net.ILog;

namespace QuaintHouse.Scheduler.Action.Interceptor
{
    public class LoggingInterceptor : IInterceptor
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        private ActionLogger actionLogger;

        private ActionLogData actionLogData;

        private string actionId;

        private string actionLogFolder;

        [Dependency]
        public ActionLogger ActionLogger
        {
            set { actionLogger = value; }
        }

        [Dependency]
        public ActionLogData ActionLogData
        {
            set { actionLogData = value; }
        }

        [Dependency("ActionId")]
        public string ActionId
        {
            set { actionId = value; }
        }

        [Dependency("ActionLogFolder")]
        public string ActionLogFolder
        {
            set { actionLogFolder = value; }
        }

        public void Intercepte(ActionProxy actionProxy)
        {
            logger.Debug("logging interceptor, order:" + Order);

            ActionLog actionLog = new ActionLog();
            actionLog.ActionId = actionId;
            actionLog.StartDate = DateTime.Now;
            actionLog.Status = "Start";

            if (actionLogger is FileLogger)
            {
                string logFile = CreateLogFilePath(actionLog);
                ((FileLogger)actionLogger).SetLogFile(logFile);
                actionLog.LogPath = logFile;
            }

            actionLogger.Log("Start Executing Action, actionId=" + actionId +
                             ", Environment=" + Environment.MachineName + ", PID=" + Process.GetCurrentProcess().Id);

            actionLogData.CreateActionLog(actionLog);

            try
            {
                actionProxy.Execute();
                actionLog.Status = "Success";
                actionLogger.Log("Finish Executing Action");
            }
            catch (Exception ex)
            {
                actionLog.Status = "Fail";
                actionLogger.Log("Failed Executing Action", ex);
                throw;
            }
            finally
            {
                actionLog.EndDate = DateTime.Now;
                actionLogData.CompleteActionLog(actionLog);

                actionLogger.Flush();
            }
        }

        public int Order
        {
            get { return 10; }
        }

        private string CreateLogFilePath(ActionLog actionLog)
        {
            return actionLogFolder + Path.DirectorySeparatorChar + actionLog.ActionId
                   + Path.DirectorySeparatorChar + actionLog.StartDate.Year
                   + Path.DirectorySeparatorChar + actionLog.StartDate.Month
                   + Path.DirectorySeparatorChar + actionLog.StartDate.Day
                   + Path.DirectorySeparatorChar + Guid.NewGuid();
        }
    }
}
