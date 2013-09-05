using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Ioc;

namespace QuaintHouse.Scheduler.Action
{
    public class ActionBuilder
    {
        private string actionId;
        private Container actionContainer;

        public ActionBuilder(string actionId, Container actionContainer)
        {
            this.actionId = actionId;
            this.actionContainer = actionContainer;
        }

        public string ActionId
        {
            get { return actionId; }
            set { actionId = value; }
        }

        public Container ActionContainer
        {
            get { return actionContainer; }
            set { actionContainer = value; }
        }

        public ActionProxy BuildAction()
        {
            return actionContainer.GetInstance<ActionProxy>();
        }
    }
}
