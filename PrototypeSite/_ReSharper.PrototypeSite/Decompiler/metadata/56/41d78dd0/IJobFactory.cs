// Type: Quartz.Spi.IJobFactory
// Assembly: Quartz, Version=2.1.2.400, Culture=neutral, PublicKeyToken=f6b8c98a402cc8a4
// Assembly location: E:\Saturni\prototype\library\Quartz.dll

using Quartz;

namespace Quartz.Spi
{
  public interface IJobFactory
  {
    IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler);
    void ReturnJob(IJob job);
  }
}
