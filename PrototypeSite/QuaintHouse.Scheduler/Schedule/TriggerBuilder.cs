using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;

namespace QuaintHouse.Scheduler.Schedule
{
    public class TriggerBuilder
    {
        public static Trigger Build(JobTrigger jobTrigger)
        {
            switch (jobTrigger.Type)
            {
                case TriggerType.Cron:
                    CronTrigger trigger = new CronTrigger();
                    trigger.CronExpressionString = TrimDelimiter(jobTrigger.Value);
                    return trigger;
                    
                case TriggerType.Monthly:
                    var monthTime = BuildTime(jobTrigger.Value);
                    return TriggerUtils.MakeMonthlyTrigger(monthTime.DayOfMonth, monthTime.Hour, monthTime.Minute);

                case TriggerType.Daily:
                    var dailyTime = BuildTime(jobTrigger.Value);
                    return TriggerUtils.MakeDailyTrigger(dailyTime.Hour, dailyTime.Minute);

                case TriggerType.Hourly:
                    var hourTime = BuildTime(jobTrigger.Value);
                    return TriggerUtils.MakeHourlyTrigger(hourTime.Interval);

                case TriggerType.Minutely:
                    var minuteTime = BuildTime(jobTrigger.Value);
                    return TriggerUtils.MakeMinutelyTrigger(minuteTime.Interval);

                case TriggerType.Secondly:
                    var secondTime = BuildTime(jobTrigger.Value);
                    return TriggerUtils.MakeSecondlyTrigger(secondTime.Interval);

                default:
                    return null;

            }
        }

        private static string TrimDelimiter(string value)
        {
            if(value.StartsWith("["))
            {
                value = value.TrimStart('[');
            }

            if(value.EndsWith("]"))
            {
                value = value.TrimEnd(']');
            }

            return value;
        }

        private static TimeValue BuildTime(string value)
        {
            TimeValue timeValue = new TimeValue();

            value = TrimDelimiter(value);

            string[] timeSegments = value.Split(',');
            if(timeSegments.Length <= 1)
            {
                timeValue.Interval = int.Parse(timeSegments[0]);
            }
            else if(timeSegments.Length == 2)
            {
                timeValue.Hour = int.Parse(timeSegments[0]);
                timeValue.Minute = int.Parse(timeSegments[1]);
            }
            else
            {
                timeValue.DayOfMonth = int.Parse(timeSegments[0]);
                timeValue.Hour = int.Parse(timeSegments[1]);
                timeValue.Minute = int.Parse(timeSegments[2]);
            }

            return timeValue;
        }
    }

    public class TimeValue
    {
        public int DayOfMonth { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Interval { get; set; }
    }
}
