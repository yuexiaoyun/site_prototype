using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Exortech.NetReflector;
using QuaintHouse.Scheduler.Exceptions;

namespace QuaintHouse.Scheduler.Schedule
{
    [ReflectorType("jobStore")]
    public class JobStore
    {
        [ReflectorCollection("jobs", InstanceType = typeof(JobItem[]), Required = true)]
        public JobItem[] Jobs { get; set; }

        public static T LoadConfig<T>(string xmlPath) where T : class
        {
            try
            {
                using (var xml = new XmlTextReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlPath)))
                {
                    var jobs = NetReflector.Read(xml) as T;
                    xml.Close();
                    return jobs;
                }
            }
            catch (Exception e)
            {
                throw new JobConfigErrorException("Parse xml config to JobStore failed", e);
            }
        }
    }
}
