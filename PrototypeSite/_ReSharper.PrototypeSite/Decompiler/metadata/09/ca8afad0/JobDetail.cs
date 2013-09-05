// Type: Quartz.JobDetail
// Assembly: Quartz, Version=1.0.0.2, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\library\Quartz.dll

using Quartz.Util;
using System;

namespace Quartz
{
  [Serializable]
  public class JobDetail : ICloneable
  {
    public JobDetail();
    public JobDetail(string name, string group, Type jobType);
    public JobDetail(string name, string group, Type jobType, bool isVolatile, bool isDurable, bool requestsRecovery);
    public virtual void Validate();
    public virtual void AddJobListener(string listenerName);
    public virtual bool RemoveJobListener(string listenerName);
    public override string ToString();
    public virtual object Clone();
    protected virtual bool IsEqual(JobDetail detail);
    public override bool Equals(object obj);
    public bool Equals(JobDetail detail);
    public override int GetHashCode();
    public virtual string Name { get; set; }
    public virtual string Group { get; set; }
    public virtual string FullName { get; }
    public virtual Key Key { get; }
    public virtual string Description { get; set; }
    public virtual Type JobType { get; set; }
    public virtual JobDataMap JobDataMap { get; set; }
    public virtual bool RequestsRecovery { get; set; }
    public virtual bool Volatile { get; set; }
    public virtual bool Durable { get; set; }
    public virtual bool Stateful { get; }
    public virtual string[] JobListenerNames { get; set; }
  }
}
