using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Quartz;
using Quartz.Impl;
using log4net;

namespace QuaintHouse.Scheduler.Schedule
{
    public class Scheduler
    {
        private static ILog logger = LogManager.GetLogger("Scheduler");
        
        private IScheduler scheduler;
        
        private JobLoader jobLoader;

        void Init()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
        }

        public void Start(int delaySeconds)
        {
            logger.Debug("Scheduler Start");

            Init();

            if (scheduler != null && !scheduler.IsStarted)
            {
                ScheduleAllJobs();
                scheduler.StartDelayed(TimeSpan.FromSeconds(delaySeconds));
            }
        }

        private void ScheduleAllJobs()
        {
            List<JobItem> jobs = jobLoader.LoadJobs();
            foreach (var jobItem in jobs)
            {
                logger.Debug("Scheduler Job: " + jobItem.Name);

                if (!jobItem.Enabled) continue;

                JobDetail jobDetail = BuildJobDetail(jobItem);
                Trigger trigger = BuildTrigger(jobItem);
                scheduler.ScheduleJob(jobDetail, trigger);
            }
        }

        private static Trigger BuildTrigger(JobItem jobItem)
        {
            Trigger trigger = TriggerBuilder.Build(jobItem.Trigger);
            trigger.Name = string.Format("{0}Trigger", jobItem.Name);
            return trigger;
        }

        public void Stop()
        {
            logger.Debug("Scheduler Stop");

            if (scheduler != null && scheduler.IsStarted)
            {
                scheduler.Shutdown();
            }
        }

        public void Schedule(string name)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null && !job.Enabled)
            {
                logger.Debug("Schedule Job: " + name);
                JobDetail jobDetail = BuildJobDetail(job);
                Trigger trigger = TriggerBuilder.Build(job.Trigger);
                scheduler.ScheduleJob(jobDetail, trigger);
                job.Enabled = true;
            }
        }

        public void Unschedule(string name)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null && job.Enabled)
            {
                logger.Debug("Unschedule Job: " + name);
                Trigger trigger = TriggerBuilder.Build(job.Trigger);
                scheduler.UnscheduleJob(trigger.Name, SchedulerConstants.DefaultGroup);
                job.Enabled = false;
            }
        }

        public void Reschedule(string name, Trigger trigger)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null)
            {
                logger.Debug("Reschedule Job: " + name);
                Unschedule(name);
                JobDetail jobDetail = BuildJobDetail(job);
                scheduler.ScheduleJob(jobDetail, trigger);
                job.Enabled = true;
            }
        }

        public void Schedule(JobDetail jobDetail, Trigger jobTrigger)
        {
            scheduler.ScheduleJob(jobDetail, jobTrigger);
        }

        public void Unschedule(Trigger jobTrigger)
        {
            scheduler.UnscheduleJob(jobTrigger.Name, SchedulerConstants.DefaultGroup);
        }

        public void RunNowJob(string name)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null)
            {
                logger.Debug("Schedule Run Now Job: " + name);
                
                Trigger trigger = TriggerBuilder.Build(job.Trigger);
                trigger.StartTimeUtc = DateTime.UtcNow;
                if (trigger is SimpleTrigger)
                {
                    ((SimpleTrigger) trigger).SetPreviousFireTime(DateTime.UtcNow);
                }

                Reschedule(name, trigger);
            }
        }

        public DateTime? GetNextFireTime(string name)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null)
            {
                Trigger trigger = TriggerBuilder.Build(job.Trigger);
                Trigger currTrigger = scheduler.GetTrigger(trigger.Name, SchedulerConstants.DefaultGroup);
                if (currTrigger != null)
                {
                    return trigger.GetNextFireTimeUtc();
                }
            }
            return null;
        }

        public DateTime? GetPreviousFireTime(string name)
        {
            JobItem job = jobLoader.LoadJob(name);
            if (job != null)
            {
                Trigger trigger = TriggerBuilder.Build(job.Trigger);
                Trigger currTrigger = scheduler.GetTrigger(trigger.Name, SchedulerConstants.DefaultGroup);
                if (currTrigger != null)
                {
                    return trigger.GetPreviousFireTimeUtc();
                }
            }
            return null;
        }

        public IList GetCurrentlyExecutingJobs()
        {
            return scheduler.GetCurrentlyExecutingJobs();
        }

        private JobDetail BuildJobDetail(JobItem job)
        {
            JobDetail jobDetail = new JobDetail(job.Name, SchedulerConstants.DefaultGroup, Type.GetType(job.Type));
            jobDetail.JobDataMap.PutAll(job.GetParams());
            jobDetail.JobDataMap.Put("JobName", job.Name);
            return jobDetail;
        }

        [Dependency]
        public JobLoader JobLoader
        {
            get { return jobLoader; }
            set { jobLoader = value; }
        }
    }
}
