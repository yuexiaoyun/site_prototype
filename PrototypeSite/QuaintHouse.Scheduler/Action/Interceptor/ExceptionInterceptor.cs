using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Util;
using log4net;

namespace QuaintHouse.Scheduler.Action.Interceptor
{
    public class ExceptionInterceptor : IInterceptor
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        private string[] notifiedGroups;

        private string notifier;

        private MailManager mailManager;

        private string actionId;

        [Dependency("FailedNotifiedGroups")]
        public string NotifiedGroups
        {
            set { notifiedGroups = value.Split(','); }
        }

        [Dependency("FailedNotifer")]
        public string Notifier
        {
            set { notifier = value; }
        }

        [Dependency]
        public MailManager MailManager
        {
            set { mailManager = value; }
        }

        [Dependency("ActionId")]
        public string ActionId
        {
            set { actionId = value; }
        }

        public void Intercepte(ActionProxy actionProxy)
        {
            logger.Debug("exception interceptor, order:" + Order);
            try
            {
                actionProxy.Execute();
            }
            catch (Exception ex)
            {
                logger.Error("ExceptionInterceptor:", ex);

                string subject= "Action Error, ActionId = " + actionId + ", Exception = " + ex.GetType().Name;
                StringBuilder mesasge = new StringBuilder();
                mesasge.Append("ActionId=").AppendLine(actionId)
                    .Append("Environment=").AppendLine(Environment.MachineName)
                    .Append("Exception Message=").AppendLine(ex.Message)
                    .Append("Exception Trace=").AppendLine(ex.StackTrace)
                    .Append("Exception Source=").AppendLine(ex.Source);
                string from = notifier;
                List<string> to = notifiedGroups.ToList();

                mailManager.BeginSend(subject, mesasge.ToString(), from, null, to, null, null);
                throw;
            }
            
        }

        public int Order
        {
            get { return -10; }
        }
    }
}
