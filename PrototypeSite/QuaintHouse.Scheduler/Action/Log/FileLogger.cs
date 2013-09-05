using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Action.Log
{
    public class FileLogger : ILog
    {
        StringBuilder builder = new StringBuilder();
        private DateTime lastFlushTime = DateTime.Now;
        private const int MaxFlushSeconds = 30;
        private const int MaxDirectWriteLength = 102400;
        private const int MaxFlushLength = 8192;
        string logFile;

        public void SetLogFile(string logFile)
        {
            this.logFile = logFile;
            if (!File.Exists(this.logFile))
            {
                FileInfo fileInfo = new FileInfo(this.logFile);
                if (fileInfo.Directory != null)
                {
                    fileInfo.Directory.Create();
                }
            }
            builder = new StringBuilder();
        }

        public virtual void Log(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "";
            }

            if (message.Length > MaxDirectWriteLength)
            {
                DirectWriteMessage(message, null);
                return;
            }

            builder.AppendLine(ErrorMessage(message));
            if (builder.Length > MaxFlushLength || (DateTime.Now - lastFlushTime).TotalSeconds > MaxFlushSeconds)
            {
                Flush();
                lastFlushTime = DateTime.Now;
            }
        }

        public virtual void Log(string message, Exception exception)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "";
            }

            if (message.Length > MaxDirectWriteLength)
            {
                DirectWriteMessage(message, exception);
                return;
            }

            builder.AppendLine(ErrorMessage(message, exception));
            if (builder.Length > MaxFlushLength || (DateTime.Now - lastFlushTime).TotalSeconds > MaxFlushSeconds)
            {
                Flush();
                lastFlushTime = DateTime.Now;
            }
        }

        public virtual void Flush()
        {
            if (builder.Length > 0)
            {
                File.AppendAllText(logFile, builder.ToString());
                builder.Remove(0, builder.Length);
            }
        }

        public void DirectWriteMessage(string message, Exception exception)
        {
            Flush();

            File.AppendAllText(logFile, string.Format("{0} [{1}] "
                , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), System.Threading.Thread.CurrentThread.Name));

            File.AppendAllText(logFile, message);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("");
            if (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.Source);
                stringBuilder.AppendLine(exception.StackTrace);
            }
            stringBuilder.AppendLine("");
            File.AppendAllText(logFile, stringBuilder.ToString());

            lastFlushTime = DateTime.Now;
        }

        public string GetLogs()
        {
            return builder.Length > MaxFlushLength ? builder.ToString(0, MaxFlushLength) : builder.ToString();
        }

        public string LogFile
        {
            get { return logFile; }
        }

        protected string ErrorMessage(string message)
        {
            return ErrorMessage(message, null);
        }

        protected string ErrorMessage(string message, Exception exception)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            stringBuilder.Append(" [").Append(System.Threading.Thread.CurrentThread.Name).Append("] ");
            stringBuilder.AppendLine(message);
            if (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.Source);
                stringBuilder.AppendLine(exception.StackTrace);
            }
            return stringBuilder.ToString();
        }
    }
}
