namespace AppAny.Quartz.EntityFrameworkCore.Migrations
{
  public class QuartzLock
  {
    public static readonly string TableName = "LOCKS";

    public string SchedulerName { get; set; } = null!;
    public string LockName { get; set; } = null!;
  }
}
