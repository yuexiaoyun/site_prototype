using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace QuaintHouse.Scheduler.Action
{
    public class ActionContext
    {
        readonly Dictionary<string, object> context = new Dictionary<string, object>(); 

        public ActionContext(JobDataMap mergedJobDataMap)
        {
            foreach (DictionaryEntry jobData in mergedJobDataMap)
            {
                context.Add(jobData.Key.ToString(), jobData.Value);
            }
        }

        public void Put(string name, object value)
        {
            context.Add(name, value);
        }

        public object Get(string name)
        {
            object result;
            if (context.TryGetValue(name, out result))
            {
                return result;
            }
            return null;
        }
    }
}
