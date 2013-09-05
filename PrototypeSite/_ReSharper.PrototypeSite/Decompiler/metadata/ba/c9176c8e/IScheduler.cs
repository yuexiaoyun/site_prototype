// Type: Quartz.IScheduler
// Assembly: Quartz, Version=1.0.0.2, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\library\Quartz.dll

using Quartz.Collection;
using Quartz.Spi;
using System;
using System.Collections;

namespace Quartz
{
  public interface IScheduler
  {
    bool IsJobGroupPaused(string groupName);
    bool IsTriggerGroupPaused(string groupName);
    SchedulerMetaData GetMetaData();
    IList GetCurrentlyExecutingJobs();
    ISet GetPausedTriggerGroups();
    IJobListener GetGlobalJobListener(string name);
    ITriggerListener GetGlobalTriggerListener(string name);
    void Start();
    void StartDelayed(TimeSpan delay);
    void Standby();
    void Shutdown();
    void Shutdown(bool waitForJobsToComplete);
    DateTime ScheduleJob(JobDetail jobDetail, Trigger trigger);
    DateTime ScheduleJob(Trigger trigger);
    bool UnscheduleJob(string triggerName, string groupName);
    DateTime? RescheduleJob(string triggerName, string groupName, Trigger newTrigger);
    void AddJob(JobDetail jobDetail, bool replace);
    bool DeleteJob(string jobName, string groupName);
    void TriggerJob(string jobName, string groupName);
    void TriggerJobWithVolatileTrigger(string jobName, string groupName);
    void TriggerJob(string jobName, string groupName, JobDataMap data);
    void TriggerJobWithVolatileTrigger(string jobName, string groupName, JobDataMap data);
    void PauseJob(string jobName, string groupName);
    void PauseJobGroup(string groupName);
    void PauseTrigger(string triggerName, string groupName);
    void PauseTriggerGroup(string groupName);
    void ResumeJob(string jobName, string groupName);
    void ResumeJobGroup(string groupName);
    void ResumeTrigger(string triggerName, string groupName);
    void ResumeTriggerGroup(string groupName);
    void PauseAll();
    void ResumeAll();
    string[] GetJobNames(string groupName);
    Trigger[] GetTriggersOfJob(string jobName, string groupName);
    string[] GetTriggerNames(string groupName);
    JobDetail GetJobDetail(string jobName, string jobGroup);
    Trigger GetTrigger(string triggerName, string triggerGroup);
    TriggerState GetTriggerState(string triggerName, string triggerGroup);
    void AddCalendar(string calName, ICalendar calendar, bool replace, bool updateTriggers);
    bool DeleteCalendar(string calName);
    ICalendar GetCalendar(string calName);
    string[] GetCalendarNames();
    bool Interrupt(string jobName, string groupName);
    void AddGlobalJobListener(IJobListener jobListener);
    void AddJobListener(IJobListener jobListener);
    bool RemoveGlobalJobListener(IJobListener jobListener);
    bool RemoveGlobalJobListener(string name);
    bool RemoveJobListener(string name);
    IJobListener GetJobListener(string name);
    void AddGlobalTriggerListener(ITriggerListener triggerListener);
    void AddTriggerListener(ITriggerListener triggerListener);
    bool RemoveGlobalTriggerListener(ITriggerListener triggerListener);
    bool RemoveGlobalTriggerListener(string name);
    bool RemoveTriggerListener(string name);
    ITriggerListener GetTriggerListener(string name);
    void AddSchedulerListener(ISchedulerListener schedulerListener);
    bool RemoveSchedulerListener(ISchedulerListener schedulerListener);
    string SchedulerName { get; }
    string SchedulerInstanceId { get; }
    SchedulerContext Context { get; }
    bool InStandbyMode { get; }
    bool IsShutdown { get; }
    IJobFactory JobFactory { set; }
    string[] JobGroupNames { get; }
    string[] TriggerGroupNames { get; }
    string[] CalendarNames { get; }
    IList GlobalJobListeners { get; }
    ISet JobListenerNames { get; }
    IList GlobalTriggerListeners { get; }
    ISet TriggerListenerNames { get; }
    IList SchedulerListeners { get; }
    bool IsStarted { get; }
  }
}
