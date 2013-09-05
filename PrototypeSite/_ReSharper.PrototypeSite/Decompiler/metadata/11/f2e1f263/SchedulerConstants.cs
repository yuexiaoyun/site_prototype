// Type: Quartz.SchedulerConstants
// Assembly: Quartz, Version=1.0.0.2, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\PrototypeSite\site\bin\Quartz.dll

using System.Runtime.InteropServices;

namespace Quartz
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct SchedulerConstants
  {
    public const string DefaultGroup = "DEFAULT";
    public const string DefaultManualTriggers = "MANUAL_TRIGGER";
    public const string DefaultRecoveryGroup = "RECOVERING_JOBS";
    public const string DefaultFailOverGroup = "FAILED_OVER_JOBS";
    public const string FailedJobOriginalTriggerName = "QRTZ_FAILED_JOB_ORIG_TRIGGER_NAME";
    public const string FailedJobOriginalTriggerGroup = "QRTZ_FAILED_JOB_ORIG_TRIGGER_GROUP";
    public const string FailedJobOriginalTriggerFiretimeInMillisecoonds = "QRTZ_FAILED_JOB_ORIG_TRIGGER_FIRETIME_IN_MILLISECONDS_AS_STRING";
  }
}
