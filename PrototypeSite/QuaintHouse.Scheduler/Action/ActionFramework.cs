using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Ioc;
using QuaintHouse.Scheduler.Exceptions;
using log4net;

namespace QuaintHouse.Scheduler.Action
{
    public class ActionFramework 
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        private Dictionary<string, ActionBuilder> actionBuilders = new Dictionary<string, ActionBuilder>();

        public virtual void LoadConfiguration(params IConfiguration[] actionConfigs)
        {
            foreach (IConfiguration configuration in actionConfigs)
            {
                Container actionContainer = ContainerFactory.GetChildContainer(configuration.ActionId());
                configuration.Configure(actionContainer);
                actionBuilders.Add(configuration.ActionId(), new ActionBuilder(configuration.ActionId(), actionContainer));
            }
        }
 
        public virtual void Execute(string actionId, ActionContext actionContext)
        {
            logger.Debug("Action Framwork, execute action: " + actionId);
            if(actionBuilders.ContainsKey(actionId))
            {
                ActionBuilder actionBuilder = actionBuilders[actionId];
                ActionProxy actionProxy = actionBuilder.BuildAction();
                actionProxy.ActionContext = actionContext;
                actionProxy.Execute();
                return;
            }
            throw new ActionNotFoundException("Action not found, actionId = " + actionId);
        }

        public Dictionary<string, ActionBuilder> ActionBuilders
        {
            get { return actionBuilders; }
            set { actionBuilders = value; }
        }
    }
}
