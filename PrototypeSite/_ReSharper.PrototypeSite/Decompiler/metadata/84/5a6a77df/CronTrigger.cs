// Type: Quartz.CronTrigger
// Assembly: Quartz, Version=1.0.0.2, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\library\Quartz.dll

using System;

namespace Quartz
{
  [Serializable]
  public class CronTrigger : Trigger
  {
    public CronTrigger();
    public CronTrigger(string name, string group);
    public CronTrigger(string name, string group, string cronExpression);
    public CronTrigger(string name, string group, string jobName, string jobGroup);
    public CronTrigger(string name, string group, string jobName, string jobGroup, string cronExpression);
    public CronTrigger(string name, string group, string jobName, string jobGroup, string cronExpression, TimeZone timeZone);
    public CronTrigger(string name, string group, string jobName, string jobGroup, DateTime startTimeUtc, DateTime? endTime, string cronExpression);
    public CronTrigger(string name, string group, string jobName, string jobGroup, DateTime startTimeUtc, DateTime? endTime, string cronExpression, TimeZone timeZone);
    public override object Clone();
    public override DateTime? GetNextFireTimeUtc();
    public override DateTime? GetPreviousFireTimeUtc();
    public void SetNextFireTime(DateTime? fireTime);
    public void SetPreviousFireTime(DateTime? fireTime);
    public override DateTime? GetFireTimeAfter(DateTime? afterTimeUtc);
    public override bool GetMayFireAgain();
    protected override bool ValidateMisfireInstruction(int misfireInstruction);
    public override void UpdateAfterMisfire(ICalendar cal);
    public bool WillFireOn(DateTime test);
    public bool WillFireOn(DateTime test, bool dayOnly);
    public override void Triggered(ICalendar cal);
    public override void UpdateWithNewCalendar(ICalendar calendar, TimeSpan misfireThreshold);
    public override DateTime? ComputeFirstFireTimeUtc(ICalendar cal);
    public string GetExpressionSummary();
    protected DateTime? GetTimeAfter(DateTime afterTime);
    protected DateTime? GetTimeBefore(DateTime? date);
    public string CronExpressionString { get; set; }
    public CronExpression CronExpression { set; }
    public override DateTime StartTimeUtc { get; set; }
    public override DateTime? EndTimeUtc { get; set; }
    public TimeZone TimeZone { get; set; }
    public override DateTime? FinalFireTimeUtc { get; }
    public override bool HasMillisecondPrecision { get; }
  }
}
