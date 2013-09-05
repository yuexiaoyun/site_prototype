// Type: Quartz.Trigger
// Assembly: Quartz, Version=1.0.0.2, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\library\Quartz.dll

using Quartz.Util;
using System;

namespace Quartz
{
  [Serializable]
  public abstract class Trigger : ICloneable, IComparable
  {
    public const int DefaultPriority = 5;
    public Trigger();
    public Trigger(string name, string group);
    public Trigger(string name, string group, string jobName, string jobGroup);
    public virtual void ClearAllTriggerListeners();
    public virtual void AddTriggerListener(string listenerName);
    public virtual bool RemoveTriggerListener(string listenerName);
    public abstract void Triggered(ICalendar cal);
    public abstract DateTime? ComputeFirstFireTimeUtc(ICalendar cal);
    public virtual SchedulerInstruction ExecutionComplete(JobExecutionContext context, JobExecutionException result);
    public abstract bool GetMayFireAgain();
    public abstract DateTime? GetNextFireTimeUtc();
    public abstract DateTime? GetPreviousFireTimeUtc();
    public abstract DateTime? GetFireTimeAfter(DateTime? afterTime);
    protected abstract bool ValidateMisfireInstruction(int misfireInstruction);
    public abstract void UpdateAfterMisfire(ICalendar cal);
    public abstract void UpdateWithNewCalendar(ICalendar cal, TimeSpan misfireThreshold);
    public virtual void Validate();
    public override string ToString();
    public virtual int CompareTo(object obj);
    public override bool Equals(object obj);
    public override int GetHashCode();
    public virtual object Clone();
    public virtual string Name { get; set; }
    public virtual string Group { get; set; }
    public virtual string JobName { get; set; }
    public virtual string JobGroup { get; set; }
    public virtual string FullName { get; }
    public virtual Key Key { get; }
    public virtual string FullJobName { get; }
    public virtual string Description { get; set; }
    public virtual string CalendarName { get; set; }
    public virtual JobDataMap JobDataMap { get; set; }
    public virtual bool Volatile { get; set; }
    public virtual string[] TriggerListenerNames { get; set; }
    public abstract DateTime? FinalFireTimeUtc { get; }
    public virtual int MisfireInstruction { get; set; }
    public virtual string FireInstanceId { get; set; }
    public virtual DateTime? EndTimeUtc { get; set; }
    public virtual DateTime StartTimeUtc { get; set; }
    public abstract bool HasMillisecondPrecision { get; }
    public virtual int Priority { get; set; }
    public virtual bool HasAdditionalProperties { get; }
  }
}
