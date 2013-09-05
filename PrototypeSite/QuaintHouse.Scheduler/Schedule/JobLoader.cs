using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace QuaintHouse.Scheduler.Schedule
{
    public class JobLoader
    {
        private JobStore jobStore;

        private string jobsPath;
        [Dependency("JobStore")]
        public string JobsPath
        {
            get { return jobsPath; }
            set { jobsPath = value; }
        }

        public JobLoader()
        {
            string path = string.IsNullOrEmpty(jobsPath) ? "JobStore.config" : jobsPath;

            jobStore = JobStore.LoadConfig<JobStore>(path);
        }

        public List<JobItem> LoadJobs()
        {
            return new List<JobItem>(jobStore.Jobs);
        }

        public JobItem LoadJob(string jobName)
        {
            return jobStore.Jobs.First(j => j.Name.Equals(jobName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
