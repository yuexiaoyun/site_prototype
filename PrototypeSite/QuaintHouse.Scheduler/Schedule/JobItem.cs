using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exortech.NetReflector;

namespace QuaintHouse.Scheduler.Schedule
{
    [ReflectorType("job")]
    public class JobItem
    {
        private bool enabled = true;

        [ReflectorProperty("name", InstanceType = typeof(string), Required = true)]
        public string Name { get; set; }
        [ReflectorProperty("enabled", InstanceType = typeof(bool), Required = false)]
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        [ReflectorProperty("action", InstanceType = typeof(string), Required = true)]
        public string Action { get; set; }
        [ReflectorProperty("type", InstanceType = typeof(string), Required = true)]
        public string Type { get; set; }
        [ReflectorProperty("trigger", InstanceType = typeof(JobTrigger), Required = true)]
        public JobTrigger Trigger { get; set; }
        [ReflectorProperty("params", InstanceType = typeof(JobParam[]), Required = false)]
        public JobParam[] Params { get; set; }

        public Dictionary<string, string> GetParams()
        {
            Dictionary<string, string> paramsDict = new Dictionary<string, string>();
            
            if (Params == null || Params.Length <= 0)
                return paramsDict;

            foreach (var jobParam in Params)
            {
                paramsDict.Add(jobParam.Name, jobParam.Value);
            }
            return paramsDict;
        }
    }

    [ReflectorType("trigger")]
    public class JobTrigger
    {
        [ReflectorProperty("type", InstanceType = typeof(TriggerType), Required = true)]
        public TriggerType Type { get; set; }
        [ReflectorProperty("value", InstanceType = typeof(string), Required = true)]
        public string Value { get; set; }
    }

    [ReflectorType("param")]
    public class JobParam
    {
        [ReflectorProperty("name", InstanceType = typeof(string), Required = true)]
        public string Name { get; set; }
        [ReflectorProperty("value", InstanceType = typeof(string), Required = true)]
        public string Value { get; set; }
    }

    public enum TriggerType
    {
        Cron,
        Monthly,
        Daily,
        Hourly,
        Minutely,
        Secondly,
    }
}
